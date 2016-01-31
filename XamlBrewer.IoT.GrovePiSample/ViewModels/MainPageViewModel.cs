using GrovePi;
using Mvvm;
using Mvvm.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamlBrewer.IoT.GrovePiSample.ViewModels
{
    class MainPageViewModel : ViewModelBase
    {
        private string message = "Welcome";

        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        public MainPageViewModel()
        {
            startTestCommand = new DelegateCommand(StartTest_Executed);
        }

        private ICommand startTestCommand;

        public ICommand StartTestCommand
        {
            get { return startTestCommand; }
        }

        private async void StartTest_Executed()
        {
            // Toast.ShowInfo("Starting test."); // The notification platform is unavailable.

            try
            {
                var blinky = DeviceFactory.Build.Led(Pin.DigitalPin4);
                if (blinky == null)
                {
                    Message = "Failed to intialize led.";
                    return;
                }

                for (int i = 0; i < 10; i++)
                {
                    blinky.ChangeState(GrovePi.Sensors.SensorStatus.On);
                    Message = "Blinky on";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                    blinky.ChangeState(GrovePi.Sensors.SensorStatus.Off);
                    Message = "Blinky off";
                    await Task.Delay(TimeSpan.FromSeconds(1));
                }
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
        }
    }
}
