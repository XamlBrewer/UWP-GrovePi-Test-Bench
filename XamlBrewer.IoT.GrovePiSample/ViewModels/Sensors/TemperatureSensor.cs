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
            TestDescription = "The sensor will measure the temperature in °C during 1 minute.";
        }

        public override async Task Test()
        {
            var sensor = DeviceFactory.Build.TemperatureSensor(Pin.AnalogPin2, GrovePi.Sensors.TemperatureSensorModel.OnePointTwo);
            if (sensor == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 120; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(.5));
                State = sensor.TemperatureInCelcius().ToString();
            }
        }
    }
}
