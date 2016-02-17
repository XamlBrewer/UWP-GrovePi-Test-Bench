using GrovePi;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    // NOT OPERATIONAL
    internal class InfraredReceiver : SensorBase
    {
        public InfraredReceiver()
        {
            ImagePath = "ms-appx:///Assets/Sensors/InfraredReceiver.jpg";
            TestDescription = "During 1 minute the sensor will detect input.";
        }

        public override async Task Test()
        {
            // Is InfraredReceiver a Digital On/Off sensor, just like the button?
            // No, it is not ...
            var btn = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin8);
            if (btn == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 240; i++)
            {
                State = btn.CurrentState.ToString();
                await Task.Delay(TimeSpan.FromSeconds(.25));
            }

            return;
        }
    }
}
