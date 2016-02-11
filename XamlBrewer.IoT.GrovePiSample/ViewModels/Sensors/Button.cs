using GrovePi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class Button : SensorBase
    {
        public Button()
        {
            ImagePath = "ms-appx:///Assets/Sensors/Button.jpg";
        }

        public override async Task Test()
        {
            var btn = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin3);
            if (btn == null)
            {
                State = "Failed to intialize.";
                return;
            }

            State = btn.CurrentState.ToString();
            return;
        }
    }
}
