using GrovePi;
using Mvvm.Services;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class RotaryAngleSensor : SensorBase
    {
        public RotaryAngleSensor()
        {
            ImagePath = "ms-appx:///Assets/Sensors/RotaryAngleSensor.jpg";
            TestDescription = "During 1 minute the knob will register your manipulation.";
        }

        public override async Task Test()
        {
            var btn = DeviceFactory.Build.RotaryAngleSensor(Pin.AnalogPin0);
            if (btn == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 300; i++)
            {
                try
                {
                    State = btn.SensorValue().ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(this.Name + " - " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(.2));
            }

            State = String.Empty;
        }
    }
}
