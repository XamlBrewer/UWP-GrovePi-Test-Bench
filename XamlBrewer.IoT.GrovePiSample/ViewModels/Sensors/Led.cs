﻿using GrovePi;
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
                blinky.ChangeState(GrovePi.Sensors.SensorStatus.On);
                State = "on";
                await Task.Delay(TimeSpan.FromSeconds(1));
                blinky.ChangeState(GrovePi.Sensors.SensorStatus.Off);
                State = "off";
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            State = "OK";
        }
    }
}
