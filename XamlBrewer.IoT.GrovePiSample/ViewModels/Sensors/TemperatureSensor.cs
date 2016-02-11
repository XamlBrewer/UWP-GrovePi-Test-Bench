using GrovePi;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class TemperatureSensor : SensorBase
    {
        public TemperatureSensor()
        {
            ImagePath = "ms-appx:///Assets/Sensors/TemperatureSensor.jpg";
        }

        public override async Task Test()
        {
            var sensor = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin2, GrovePi.Sensors.TemperatureSensorModel.OnePointTwo);
            if (sensor == null)
            {
                State = "Failed to intialize.";
                return;
            }

            var temp = sensor.TemperatureInCelcius();

            State = temp.ToString();
        }
    }
}
