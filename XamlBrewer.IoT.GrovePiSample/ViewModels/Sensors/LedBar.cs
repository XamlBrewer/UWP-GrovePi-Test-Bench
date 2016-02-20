using GrovePi;
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
            TestDescription = "LED Bar will run through different states for about a minute.";
        }

        /// <summary>
        /// Tries some calls to get LEDs blinking.
        /// </summary>
        /// <remarks>Requires recent firmware on the GrovePi, which I don't have...</remarks>
        public override async Task Test()
        {
            var blinky = DeviceFactory.Build.BuildLedBar(Pin.DigitalPin4);
            if (blinky == null)
            {
                State = "Failed to intialize.";
                return;
            }

            blinky.Initialize(GrovePi.Sensors.Orientation.RedToGreen);
            var board = DeviceFactory.Build.GrovePi();
            board.PinMode(Pin.DigitalPin4, PinMode.Output);

            for (var i = 1; i < 10; i++)
            {
                try
                {
                    blinky.SetLed(0, (byte)i, GrovePi.Sensors.SensorStatus.On);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    blinky.ToggleLed((byte)i);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    blinky.SetLevel((byte)i);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    board.DigitalWrite(Pin.DigitalPin4, (byte)i);
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    State = "Level " + i.ToString();
                }
                catch (Exception ex)
                {
                    State = ex.Message;
                }
            }

            State = String.Empty;
        }
    }
}
