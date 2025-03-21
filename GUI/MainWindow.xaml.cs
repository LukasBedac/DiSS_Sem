using Core.SimulationCore;
using Core.Test;
using GUI.Simulations.Sem1;
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
        public Sem1Simulation? WareHouseSimulation { get; set; } = null;
        public Sem1_Page? Sem1Chart { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();
            HideSem1Buttons();
            //Generating tests for probability correction
            //TestGen gen = new TestGen();
        }

        private void Sem1_Click(object sender, RoutedEventArgs e)
        {
            Sem1Chart = new Sem1_Page();
            MainContent.Content = Sem1Chart;
            ShowSem1Buttons();
        }
        private void Sem2_Click(object sender, RoutedEventArgs e)
        {

            HideSem1Buttons();
        }
        private void Sem3_Click(object sender, RoutedEventArgs e)
        {

            HideSem1Buttons();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void StopBtn_Click(object sender, RoutedEventArgs e)
        {
            if (WareHouseSimulation != null)
            {
                WareHouseSimulation.Stopped = true;
            } else
            {
                throw new Exception("Simulation was not inicialized");
            }
        }

        private void StrategyABtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChartSwitchButton.IsChecked == true)
            {
                WareHouseSimulation = new Sem1Simulation(1, 1, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            } else
            {
                int replications = int.Parse(ReplicationCount.Text);
                WareHouseSimulation = new Sem1Simulation(replications, 1, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
        }

        private void StrategyBBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChartSwitchButton.IsChecked == true)
            {
                WareHouseSimulation = new Sem1Simulation(1, 2, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            } else
            {
                int replications = int.Parse(ReplicationCount.Text);
                WareHouseSimulation = new Sem1Simulation(replications, 2, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
        }

        private void StrategyCBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChartSwitchButton.IsChecked == true)
            {
                WareHouseSimulation = new Sem1Simulation(1, 3, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            } else
            {
                int replications = int.Parse(ReplicationCount.Text);
                WareHouseSimulation = new Sem1Simulation(replications, 3, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
        }

        private void StrategyDBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChartSwitchButton.IsChecked == true)
            {
                WareHouseSimulation = new Sem1Simulation(1, 4, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
            else
            {
                int replications = int.Parse(ReplicationCount.Text);
                WareHouseSimulation = new Sem1Simulation(replications, 4, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
        }
        private void CustomStrategyBtn_Click(object sender, RoutedEventArgs e)
        {
            if (ChartSwitchButton.IsChecked == true)
            {
                WareHouseSimulation = new Sem1Simulation(1, 4, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
            else
            {
                int replications = int.Parse(ReplicationCount.Text);
                WareHouseSimulation = new Sem1Simulation(replications, 5, Sem1Chart?.PlotModel ?? throw new Exception("Chart not inicialized"));
            }
        }
        private void HideSem1Buttons()
        {
            StartBtn.Visibility = Visibility.Visible;
            ChckBoxName.Visibility = Visibility.Hidden;
            ChartSwitchButton.Visibility = Visibility.Hidden;
            StrategyABtn.Visibility = Visibility.Hidden;
            StrategyBBtn.Visibility = Visibility.Hidden;
            StrategyCBtn.Visibility = Visibility.Hidden;
            StrategyDBtn.Visibility = Visibility.Hidden;
            CustomStrategyBtn.Visibility = Visibility.Hidden;
        }
        private void ShowSem1Buttons()
        {
            StartBtn.Visibility = Visibility.Hidden;
            ChckBoxName.Visibility = Visibility.Visible;
            ChartSwitchButton.Visibility = Visibility.Visible;
            StrategyABtn.Visibility = Visibility.Visible;
            StrategyBBtn.Visibility = Visibility.Visible;
            StrategyCBtn.Visibility = Visibility.Visible;
            StrategyDBtn.Visibility = Visibility.Visible;
            CustomStrategyBtn.Visibility = Visibility.Visible;
        }

        
    }
}