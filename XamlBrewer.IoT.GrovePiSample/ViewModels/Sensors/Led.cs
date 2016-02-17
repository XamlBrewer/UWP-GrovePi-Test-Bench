using GrovePi;
using GrovePi.Sensors;
using Mvvm.Services;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class Led : SensorBase
    {
        public Led()
        {
            ImagePath = "ms-appx:///Assets/Sensors/Led.jpg";
            TestDescription = "During 1 minute the LED will blink every other second.";
        }

        public override async Task Test()
        {
            var blinky = DeviceFactory.Build.Led(Pin.DigitalPin5);
            if (blinky == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 30; i++)
            {
                try
                {
                    blinky.ChangeState(SensorStatus.On);
                    // State = blinky.CurrentState.ToString(); // Always 'Off'
                    State = "On";
                }
                catch (Exception ex)
                {
                    Log.Error(this.Name + " - " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(1));

                try
                {
                    var led = blinky.ChangeState(SensorStatus.Off);
                    State = "Off";
                }
                catch (Exception ex)
                {
                    Log.Error(this.Name + " - " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            State = String.Empty;
        }
    }
}
