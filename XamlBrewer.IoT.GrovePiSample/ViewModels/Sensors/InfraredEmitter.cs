using GrovePi;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    // NOT OPERATIONAL
    internal class InfraredEmitter : SensorBase
    {
        public InfraredEmitter()
        {
            ImagePath = "ms-appx:///Assets/Sensors/InfraredEmitter.jpg";
            TestDescription = "During 1 minute the emitter will send every other second.";
        }

        public override async Task Test()
        {
            // Is Infrared emittor a digital actuator. Just like the LED ?
            // No, it's not...
            var ir = DeviceFactory.Build.Led(Pin.DigitalPin7);
            if (ir == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 30; i++)
            {
                ir.ChangeState(GrovePi.Sensors.SensorStatus.On);
                State = "on";
                await Task.Delay(TimeSpan.FromSeconds(1));
                ir.ChangeState(GrovePi.Sensors.SensorStatus.Off);
                State = "off";
                await Task.Delay(TimeSpan.FromSeconds(1));
            }

            State = "OK";
        }
    }
}
