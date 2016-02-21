using Windows.UI.Xaml.Controls;
using XamlBrewer.IoT.GrovePiSample.ViewModels;

namespace XamlBrewer.IoT.GrovePiSample
{
    public sealed partial class BurglarAlertPage : Page
    {
        public BurglarAlertPage()
        {
            this.InitializeComponent();
        }

        internal BurglarAlertViewModel ViewModel { get; } = new BurglarAlertViewModel();
    }
}
