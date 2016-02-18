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
            // Check the board.
            var board = DeviceFactory.Build.GrovePi();
            if (board == null)
            {
                Message = "Sorry, your GrovePi board could not be detected.";
            }
            else
            {
                try
                {
                    Message = string.Format("Your GrovePi board is ready. (Firmware version {0})", board.GetFirmwareVersion());
                }
                catch (Exception)
                {
                    Message = "Your Grove board is ready.";
                }

                AddSensors();
            }
        }

        public List<SensorBase> Sensors { get; } = new List<SensorBase>();

        private void AddSensors()
        {
            Sensors.Add(new Led() { Name = "Blinky", Port = "D5" });
            Sensors.Add(new TemperatureSensor() { Name = "Temperature Sensor", Port = "A2" });
            Sensors.Add(new LightSensor() { Name = "Light Sensor", Port = "A1" });
            Sensors.Add(new RotaryAngleSensor { Name = "Rotary Encoder", Port = "A0" });
            Sensors.Add(new Button() { Name = "Push Button", Port = "D3" });
            Sensors.Add(new LedBar() { Name = "LED Bar", Port = "D4" });
            Sensors.Add(new PassiveInfraRedSensor() { Name = "Motion Sensor", Port = "D2" });
            Sensors.Add(new VibrationMotor() { Name = "Vibration Motor", Port = "D6" });
            Sensors.Add(new InfraredEmitter() { Name = "IR Emitter", Port = "D7" });
            Sensors.Add(new InfraredReceiver() { Name = "IR Receiver", Port = "D8" });
        }
    }
}
