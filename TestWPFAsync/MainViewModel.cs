using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Diagnostics;
using System.Windows.Input;

namespace TestWPFAsync
{
    public class MainViewModel : ViewModelBase
    {
        private TestAsyncClass testAsyncClass;
        public MainViewModel()
        {
            testAsyncClass = new TestAsyncClass();
        }


        public string Message { get; set; }

        private ICommand _TestAsycnCommand;
        public ICommand TestAsycnCommand
        {
            get
            {
                if (_TestAsycnCommand == null)
                {
                    _TestAsycnCommand = new RelayCommand(async () =>
                    {
                        Debug.WriteLine($"TestAsycnCommand1 CurrentThread={Thread.CurrentThread.ManagedThreadId}");
                        var str = await testAsyncClass.TestMethodAsync().ConfigureAwait(false);
                        Debug.WriteLine($"TestAsycnCommand2 CurrentThread={Thread.CurrentThread.ManagedThreadId}");
                        Message = str;
                        RaisePropertyChanged(nameof(Message));
                    });
                }
                return _TestAsycnCommand;
            }
        }

        private ICommand _ChangeMessageCommand;
        public ICommand ChangeMessageCommand
        {
            get
            {
                if (_ChangeMessageCommand == null)
                {
                    _ChangeMessageCommand = new RelayCommand(() =>
                    {
                        Debug.WriteLine($"ChangeMessageCommand CurrentThread={Thread.CurrentThread.ManagedThreadId}");
                        Message = "Test Async Message";
                        RaisePropertyChanged(nameof(Message));
                    });
                }
                return _ChangeMessageCommand;
            }
        }
    }

    public class TestAsyncClass
    {
        public async Task<string> TestMethodAsync()
        {
            Debug.WriteLine($"TestMethodAsync CurrentThread={Thread.CurrentThread.ManagedThreadId}");
            string a = DateTime.Now.ToString("yyyyMMdd HH:mm:ss.ffffff");
            await Task.Delay(5000);
            return a;
        }
    }
}
