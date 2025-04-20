using System.Diagnostics;
using System.Windows;

namespace TestWPFAsync
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            Debug.WriteLine($"Ctor CurrentThread={Thread.CurrentThread.ManagedThreadId}");
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Loaded CurrentThread={Thread.CurrentThread.ManagedThreadId}");
        }
    }
}