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

namespace GUI2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class EventSimulationGUI : Window
    {
        public EventSimulationGUI()
        {
            WindowState = WindowState.Maximized;
            InitializeComponent();
        }

        private void ReplicationsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ReplicationsComboBox.SelectedItem != null)
            {
                if (ReplicationsComboBox.SelectedItem is ComboBoxItem selectedItem)
                {
                    switch (selectedItem.Content.ToString())
                    {
                        case "1 Milion":
                            ReplicationsCount.Text = "1000000";
                            break;
                        case "10 Milion":
                            ReplicationsCount.Text = "10000000";
                            break;
                        case "100 Thousand":
                            ReplicationsCount.Text = "100000";
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}