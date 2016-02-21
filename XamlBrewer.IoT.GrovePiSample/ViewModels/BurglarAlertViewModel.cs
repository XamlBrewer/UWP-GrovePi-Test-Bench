using GrovePi;
using Mvvm;
using Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using XamlBrewer.IoT.Sensors;

namespace XamlBrewer.IoT.GrovePiSample.ViewModels
{
    class BurglarAlertViewModel : ViewModelBase
    {
        private string message = "Welcome";
        private PassiveInfraRedSensor irSensor;
        private Led blinky;
        private VibrationMotor buzzer;
        private BurglarAlertState state = BurglarAlertState.Inactive;
        private DispatcherTimer timer = new DispatcherTimer();
        private DelegateCommand startCommand;
        private DelegateCommand stopCommand;
        private DateTime motionDetection;
        private bool ledState = false; // LED.CurrentState does not work on my hardware...

        public BurglarAlertViewModel()
        {
            // Check the board.
            var board = DeviceFactory.Build.GrovePi();
            if (board == null)
            {
                Message = "Sorry, your GrovePi board could not be detected.";
            }
            else
            {
                Message = "Your Grove board is ready.";
            }

            AddSensors();

            startCommand = new DelegateCommand(Start_Executed);
            stopCommand = new DelegateCommand(Stop_Executed);

            timer.Interval = TimeSpan.FromSeconds(.5);
            timer.Tick += Timer_Tick;
        }

        public string Name { get; } = "Burglar Alert";

        public string Description { get; } = "TODO ;-)";

        public BurglarAlertState State
        {
            get { return state; }
            set
            {
                SetProperty(ref state, value);
                Message = "Switching to " + state.ToString();
            }
        }

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public ICommand StartCommand
        {
            get { return startCommand; }
        }

        public ICommand StopCommand
        {
            get { return stopCommand; }
        }

        private void AddSensors()
        {
            blinky = new Led() { Name = "Blinky", Port = "D5" };
            irSensor = new PassiveInfraRedSensor() { Name = "Motion Sensor", Port = "D2" };
            buzzer = new VibrationMotor() { Name = "Vibration Motor", Port = "D6" };
        }

        private async Task SetState(BurglarAlertState state)
        {
            blinky.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.Off);
            buzzer.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.Off);

            State = state;

            switch (state)
            {
                case BurglarAlertState.Inactive:
                    timer.Stop();
                    break;
                case BurglarAlertState.Active:
                    timer.Start();
                    break;
                case BurglarAlertState.MotionDetected:
                    motionDetection = DateTime.Now;
                    break;
                case BurglarAlertState.Alerting:
                    buzzer.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.On);
                    break;
                default:
                    break;
            }
        }

        private async void Start_Executed()
        {
            await SetState(BurglarAlertState.Active);
        }

        private async void Stop_Executed()
        {
            await SetState(BurglarAlertState.Inactive);
        }

        /// <summary>
        /// Main process loop.
        /// </summary>
        private async void Timer_Tick(object sender, object e)
        {
            try
            {
                switch (state)
                {
                    case BurglarAlertState.Inactive:
                        break;
                    case BurglarAlertState.Active:
                        if (irSensor.Sensor.CurrentState == GrovePi.Sensors.SensorStatus.On)
                        {
                            await SetState(BurglarAlertState.MotionDetected);
                        }
                        break;
                    case BurglarAlertState.MotionDetected:
                        // Toggle LED
                        ToggleLed();

                        Log.Info("Motion detected " + (DateTime.Now - motionDetection).Seconds.ToString());
                        if ((DateTime.Now - motionDetection).Seconds > 10)
                        {
                            if (irSensor.Sensor.CurrentState == GrovePi.Sensors.SensorStatus.On)
                            {
                                await SetState(BurglarAlertState.Alerting);
                            }
                            else
                            {
                                await SetState(BurglarAlertState.Active);
                            }
                        }
                        break;
                    case BurglarAlertState.Alerting:
                        // Toggle LED
                        ToggleLed();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }

        private void ToggleLed()
        {
            ledState = !ledState;
            if (ledState)
            {
                blinky.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.On);
            }
            else
            {
                blinky.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.Off);
            }
        }
    }

    enum BurglarAlertState
    {
        Inactive,
        Active,
        MotionDetected,
        Alerting
    }
}
