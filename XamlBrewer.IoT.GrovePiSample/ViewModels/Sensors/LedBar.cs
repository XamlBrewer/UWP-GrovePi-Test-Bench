using GrovePi;
using Mvvm.Services;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class LedBar : SensorBase
    {
        public LedBar()
        {
            ImagePath = "ms-appx:///Assets/Sensors/LedBar.jpg";
            TestDescription = "LED Bar will move from 0 to 10 in 10 seconds.";
        }

        /// <summary>
        /// Walks through the levels.
        /// </summary>
        /// <remarks>Requires firmware version 1.2.2 on the GrovePi.</remarks>
        public override async Task Test()
        {
            var blinky = DeviceFactory.Build.BuildLedBar(Pin.DigitalPin4);
            if (blinky == null)
            {
                State = "Failed to intialize.";
                return;
            }

            blinky.Initialize(GrovePi.Sensors.Orientation.RedToGreen);

            for (var i = 1; i < 10; i++)
            {
                try
                {
                    blinky.SetLevel((byte)i);
                    State = "Level " + i.ToString();
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }

            try
            {
                blinky.SetLevel((byte)0);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }

            State = String.Empty;
        }
    }
}
