using GrovePi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.GrovePiSample
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        internal MainPageViewModel ViewModel { get; } = new MainPageViewModel();

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            var blinky = DeviceFactory.Build.Led(Pin.DigitalPin4);
            if (blinky == null)
            {
               // Message = "Failed to intialize.";
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                blinky.ChangeState(GrovePi.Sensors.SensorStatus.On);
               // Message = "on";
                await Task.Delay(TimeSpan.FromSeconds(1));
                blinky.ChangeState(GrovePi.Sensors.SensorStatus.Off);
               // Message = "off";
                await Task.Delay(TimeSpan.FromSeconds(1));
            }
        }
    }
}
