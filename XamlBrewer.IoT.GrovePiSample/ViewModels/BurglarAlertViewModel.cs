using GrovePi;
using Mvvm;
using Mvvm.Services;
using System;
using System.Collections.Generic;
using System.Windows.Input;
using Windows.UI.Xaml;
using XamlBrewer.IoT.Sensors;

namespace XamlBrewer.IoT.GrovePiSample.ViewModels
{
    class BurglarAlertViewModel : ViewModelBase
    {
        private string info = "Sleeping";
        private PassiveInfraRedSensor irSensor;
        private Led blinky;
        private VibrationMotor buzzer;
        private BurglarAlertState state = BurglarAlertState.Sleeping;
        private DispatcherTimer timer = new DispatcherTimer();
        private DelegateCommand startCommand;
        private DelegateCommand stopCommand;
        private DateTime motionDetection;
        private bool ledState = false; // LED.CurrentState does not work on my hardware...

        public BurglarAlertViewModel()
        {
            AddSensors();

            startCommand = new DelegateCommand(Start_Executed);
            stopCommand = new DelegateCommand(Stop_Executed);

            timer.Interval = TimeSpan.FromSeconds(.5);
            timer.Tick += Timer_Tick;
        }

        public string Name { get; } = "Burglar Alert";

        public BurglarAlertState State
        {
            get { return state; }
            set
            {
                blinky.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.Off);
                buzzer.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.Off);

                SetProperty(ref state, value);

                Info = State.ToString();

                switch (state)
                {
                    case BurglarAlertState.Sleeping:
                        timer.Stop();
                        break;
                    case BurglarAlertState.Active:
                        timer.Start();
                        break;
                    case BurglarAlertState.MotionDetected:
                        motionDetection = DateTime.Now;
                        break;
                    case BurglarAlertState.Alerting:
                        try
                        {
                            buzzer.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.On);
                        }
                        catch (Exception ex)
                        {
                            Log.Error(ex.Message);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public string Info
        {
            get { return info; }
            set { SetProperty(ref info, value); }
        }

        public List<SensorBase> Sensors { get; } = new List<SensorBase>();

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
            irSensor = new PassiveInfraRedSensor() { Name = "Motion Sensor", Port = "D2" };
            Sensors.Add(irSensor);

            blinky = new Led() { Name = "Blinky", Port = "D5" };
            Sensors.Add(blinky);

            buzzer = new VibrationMotor() { Name = "Vibration Motor", Port = "D6" };
            Sensors.Add(buzzer);
        }

        private void Start_Executed()
        {
            State = BurglarAlertState.Active;
        }

        private void Stop_Executed()
        {
            State = BurglarAlertState.Sleeping;
        }

        /// <summary>
        /// Main process loop.
        /// </summary>
        private void Timer_Tick(object sender, object e)
        {
            try
            {
                switch (state)
                {
                    case BurglarAlertState.Sleeping:
                        break;
                    case BurglarAlertState.Active:
                        if (irSensor.Sensor.CurrentState == GrovePi.Sensors.SensorStatus.On)
                        {
                            State = BurglarAlertState.MotionDetected;
                        }
                        break;
                    case BurglarAlertState.MotionDetected:
                        // Toggle LED
                        ToggleLed();

                        Log.Info("Motion detected " + (DateTime.Now - motionDetection).Seconds.ToString());
                        var delay = (DateTime.Now - motionDetection).Seconds;
                        Info = string.Format("Motion detected ({0})", 10 - delay);

                        if (delay > 10)
                        {
                            if (irSensor.Sensor.CurrentState == GrovePi.Sensors.SensorStatus.On)
                            {
                                State = BurglarAlertState.Alerting;
                            }
                            else
                            {
                                State = BurglarAlertState.Active;
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
            try
            {
                if (ledState)
                {
                    blinky.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.On);
                }
                else
                {
                    blinky.Sensor.ChangeState(GrovePi.Sensors.SensorStatus.Off);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
        }
    }

    enum BurglarAlertState
    {
        Sleeping,
        Active,
        MotionDetected,
        Alerting
    }
}
