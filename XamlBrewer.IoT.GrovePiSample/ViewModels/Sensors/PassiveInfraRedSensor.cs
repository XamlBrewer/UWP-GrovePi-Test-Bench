using GrovePi;
using Mvvm.Services;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class PassiveInfraRedSensor : SensorBase
    {
        public PassiveInfraRedSensor()
        {
            ImagePath = "ms-appx:///Assets/Sensors/PassiveInfraRedSensor.jpg";
            TestDescription = "During 1 minute the sensor will detect motion.";
        }

        public override async Task Test()
        {
            // PIR is a Digital On/Off sensor, just like the button.
            var btn = DeviceFactory.Build.ButtonSensor(Pin.DigitalPin2);
            if (btn == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 240; i++)
            {
                try
                {
                    State = btn.CurrentState.AsMotionState().ToString();
                }
                catch (Exception ex)
                {
                    Log.Error(this.Name + " - " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(.25));
            }

            return;
        }
    }
}
