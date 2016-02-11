using Mvvm;
using System.Threading.Tasks;
using System.Windows.Input;

namespace XamlBrewer.IoT.GrovePiSample.ViewModels
{
    internal class SensorBase : ViewModelBase
    {
        private string name;
        private string port;
        private string state;
        private bool isUnterTest;

        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        public string Port
        {
            get { return port; }
            set { SetProperty(ref port, value); }
        }

        public string State
        {
            get { return state; }
            set { SetProperty(ref state, value); }
        }

        public bool IsUnderTest
        {
            get { return isUnterTest; }
            set { SetProperty(ref isUnterTest, value); }
        }

        public string ImagePath { get; set; }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        public virtual async Task Test()
        { }
#pragma warning restore CS1998

        public ICommand TestCommand
        {
            get { return new DelegateCommand(Test_Executed); }
        }

        private async void Test_Executed()
        {
            await Test();
        }
    }
}
