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

        /// <summary>
        /// Blinky.
        /// </summary>
        /// <remarks>Extra code added because current state detection does not work on my board.</remarks>
        public override async Task Test()
        {
            // Test: read current state
            var board = DeviceFactory.Build.GrovePi();

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
                    Log.Info(blinky.CurrentState.ToString());
                    // State = blinky.CurrentState.ToString(); // Always 'Off'
                    var state = board.AnalogRead(Pin.DigitalPin5);
                    Log.Info(state.ToString());
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
                    Log.Info(blinky.CurrentState.ToString());
                    var state = board.AnalogRead(Pin.DigitalPin5);
                    Log.Info(state.ToString());
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
