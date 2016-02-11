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
        }

        public override async Task Test()
        {
            var blinky = DeviceFactory.Build.BuildLedBar(Pin.DigitalPin4);
            if (blinky == null)
            {
                State = "Failed to intialize.";
                return;
            }

            blinky.Initialize(GrovePi.Sensors.Orientation.RedToGreen);
            await Task.Delay(TimeSpan.FromSeconds(.5));

            // var board = DeviceFactory.Build.GrovePi();

            // board.PinMode(Pin.DigitalPin4, PinMode.Output);
            // board.DigitalWrite(Pin.DigitalPin4, 255);

            for (var i = 1; i < 10; i++)
            {
                try
                {
                    // blinky.SetLed(0, i, GrovePi.Sensors.SensorStatus.On);
                    blinky.ToggleLed((byte)i);
                    // blinky.SetLevel((byte)i);
                    // board.DigitalWrite(Pin.DigitalPin4, (byte)i);


                    await Task.Delay(TimeSpan.FromSeconds(.2));
                    State = i.ToString();
                }
                catch (Exception ex)
                {
                    State = ex.Message;
                }
            }

            State = "OK";
        }
    }
}
