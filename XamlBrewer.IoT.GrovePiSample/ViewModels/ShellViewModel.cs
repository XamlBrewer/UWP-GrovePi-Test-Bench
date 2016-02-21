using Windows.UI.Xaml.Controls;
using XamlBrewer.IoT.GrovePiSample;

namespace Mvvm
{
    class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            // Build the menu
            // Symbol enumeration is here: https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.symbol.aspx
            Menu.Add(new MenuItem() { Glyph = Symbol.Home, Text = "Home", NavigationDestination = typeof(MainPage) });
            Menu.Add(new MenuItem() { Glyph = Symbol.View, Text = "Burglar Alert", NavigationDestination = typeof(BurglarAlertPage) });
        }
    }
}
