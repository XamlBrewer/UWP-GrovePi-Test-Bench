using GrovePi;
using Mvvm.Services;
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
            TestDescription = "The sensor will measure the light intensity during 1 minute.";
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
                try
                {
                    State = sensor.SensorValue().ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(this.Name + " - " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(.5));
            }

            State = String.Empty;
        }
    }
}
