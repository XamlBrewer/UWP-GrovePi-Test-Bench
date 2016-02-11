using GrovePi;
using Mvvm;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using XamlBrewer.IoT.Sensors;

namespace XamlBrewer.IoT.GrovePiSample.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private string message = "Welcome";

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public MainPageViewModel()
        {
            AddSensors();
            startTestCommand = new DelegateCommand(StartTest_Executed);
        }

        private ICommand startTestCommand;

        public ICommand StartTestCommand
        {
            get { return startTestCommand; }
        }

        public List<SensorBase> Sensors { get; } = new List<SensorBase>();

        private void AddSensors()
        {
            Sensors.Add(new Led() { Name = "Blinky", Port = "D4" });
            Sensors.Add(new TemperatureSensor() { Name = "Celsius", Port = "A2" });
            Sensors.Add(new LightSensor() { Name = "Light", Port = "A1" });
            Sensors.Add(new Button() { Name = "PushButton", Port = "D3" });
            Sensors.Add(new LedBar() { Name = "LED Bar", Port = "D2" });
        }

        private async void StartTest_Executed()
        {
            Message = "Full test started.";

            try
            {
                // Detect GrovePi.
                var board = DeviceFactory.Build.GrovePi();
                if (board == null)
                {
                    Message = "GrovePi board not detected.";
                    return;
                }

                // board.PinMode(Pin.DigitalPin4, PinMode.Output);
                // board.DigitalWrite(Pin.DigitalPin4, 255);

                foreach (var sensor in Sensors)
                {
                    Message = "Testing " + sensor.Name + " on " + sensor.Port + ".";
                    sensor.IsUnderTest = true;

                    try
                    {
                        await sensor.Test();
                    }
                    catch (Exception ex)
                    {
                        sensor.State = ex.Message;
                    }
                    finally {
                        sensor.IsUnderTest = false;
                    }
                }

                Message = "Full test finished.";
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
