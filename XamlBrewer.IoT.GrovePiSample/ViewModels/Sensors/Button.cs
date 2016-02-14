using GrovePi;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class Button : SensorBase
    {
        public Button()
        {
            ImagePath = "ms-appx:///Assets/Sensors/Button.jpg";
            TestDescription = "During 1 minute the button will listen for press events.";
        }

        public override async Task Test()
        {
            var btn = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin3);
            if (btn == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 300; i++)
            {
                State = btn.CurrentState.ToString();
                await Task.Delay(TimeSpan.FromSeconds(.2));
            }

            return;
        }
    }
}
