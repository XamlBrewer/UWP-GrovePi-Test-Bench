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
        }

        public override async Task Test()
        {
            var sensor = DeviceFactory.Build.LightSensor(Pin.AnalogPin1);
            if (sensor == null)
            {
                State = "Failed to intialize.";
                return;
            }

            var light = sensor.SensorValue();

            State = light.ToString();
        }
    }
}
