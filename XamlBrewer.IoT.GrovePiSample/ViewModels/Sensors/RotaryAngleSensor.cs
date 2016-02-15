using GrovePi;
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
            TestDescription = "During 1 minute the knob will register you manipulation.";
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
                State = btn.SensorValue().ToString();
                await Task.Delay(TimeSpan.FromSeconds(.2));
            }

            return;
        }
    }
}
