using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using TestCaseWPF.Services;
using TestCaseWPF.ViewModels;
using TestCaseWPF.Views.Windows;

namespace TestCaseWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void OnClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
            System.Windows.Application.Current.Shutdown();
        }
    }
}