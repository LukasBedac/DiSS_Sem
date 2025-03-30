using OxyPlot.Series;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// Interaction logic for Sem1_Page.xaml
    /// </summary>
    public partial class MonteCarlo : UserControl
    {
        public PlotModel? PlotModel { get; set; }
        public LineSeries? LineSeries { get; set; }
        public List<DataPoint>? DataPoints { get; set; } = [];
        public MonteCarlo()
        {
            InitializeComponent();
            InitGraph();
        }

        private void InitGraph()
        {
            PlotModel = new PlotModel();
            PlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                Title = "Replications",
                MajorGridlineStyle = OxyPlot.LineStyle.Solid,
                MinorGridlineStyle = OxyPlot.LineStyle.Dot,
                StringFormat = "N0"
            });
            PlotModel.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                Title = "Price",
                MajorGridlineStyle = OxyPlot.LineStyle.Solid,
                MinorGridlineStyle = OxyPlot.LineStyle.Dot,
                StringFormat = "N0"
            });

            LineSeries = new LineSeries();
            PlotModel.Series.Add(LineSeries);
            Chart.Model = PlotModel;
        }
    }
}
