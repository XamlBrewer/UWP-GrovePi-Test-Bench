using GrovePi;
using GrovePi.Sensors;
using Mvvm.Services;
using System;
using System.Threading.Tasks;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.Sensors
{
    internal class VibrationMotor : SensorBase
    {
        public VibrationMotor()
        {
            ImagePath = "ms-appx:///Assets/Sensors/VibrationMotor.jpg";
            TestDescription = "During 1 minute the motor will vibrate every other second.";
        }

        public ILed Sensor { get; } = DeviceFactory.Build.Led(Pin.DigitalPin6);

        public override async Task Test()
        {
            // Vibration motor is a digital actuator. Just like the LED.
            var motor = Sensor;
            if (motor == null)
            {
                State = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 30; i++)
            {
                try
                {
                    motor.ChangeState(SensorStatus.On);
                    // State = motor.CurrentState.ToString(); // Always 'Off'
                    State = "On";
                }
                catch (Exception ex)
                {
                    Log.Error(this.Name + " - " + ex.Message);
                }

                await Task.Delay(TimeSpan.FromSeconds(1));

                try
                {
                    var led = motor.ChangeState(SensorStatus.Off);
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
