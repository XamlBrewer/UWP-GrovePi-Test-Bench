using GrovePi;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class LightSensor : SensorBase
    {
        public LightSensor()
        {
            ImagePath = "ms-appx:///Assets/Sensors/LightSensor.jpg";
            TestDescription = "The sensor will measure the light intensity during 30 seconds.";
        }

        public override async Task Test()
        {
            var sensor = DeviceFactory.Build.LightSensor(Pin.AnalogPin1);
            if (sensor == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 120; i++)
            {
                await Task.Delay(TimeSpan.FromSeconds(.5));
                State = sensor.SensorValue().ToString();
            }
        }
    }
}
