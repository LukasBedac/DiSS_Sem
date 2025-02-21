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

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindow1.WindowState = WindowState.Maximized;
        }

        private void Sem1_Click(object sender, RoutedEventArgs e)
        {
            MainContent.Content = new Sem1_Page();
        }
        private void Sem2_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Sem3_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}