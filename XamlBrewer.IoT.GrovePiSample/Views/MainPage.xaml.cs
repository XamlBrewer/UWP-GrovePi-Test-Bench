using Windows.UI.Xaml.Controls;
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
    }
}
