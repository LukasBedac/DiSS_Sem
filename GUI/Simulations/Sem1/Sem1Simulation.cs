using Core.NumberGen;
using Core.SimulationCore;
using DiscordRPC;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace GUI.Simulations.Sem1
{
    public class Sem1Simulation : Simulation
    {
        private int Strategy { get; set; }
        private OxyPlot.PlotModel PlotModel { get; set; }
        private LineSeries LineSeries { get; set; } = new LineSeries();
        private const double CHECK = 0.000000000001;
        private const int CHARTPOINTS = 100;
        private int WritePointNumber = 1;
        #region Randoms
        private OwnRandom? ShockAbsorbers { get; set; } = null;
        private OwnRandom? BrakeDiscs { get; set; } = null;
        private OwnRandom? Headlights { get; set; } = null;
        private OwnRandom? Supplier1_1 { get; set; } = null;
        private OwnRandom? Supplier1_2 { get; set; } = null;
        private OwnRandom? Supplier2_1 { get; set; } = null;
        private OwnRandom? Supplier2_2 { get; set; } = null;
        private Random? ArrivalChance {  get; set; } = null;
        #endregion Randoms
        #region WarehouseStoringAndFines
        private const double SAStoring = 0.2;
        private const double BDStoring = 0.3;
        private const double HLStoring = 0.25;
        private const double Fine = 0.3;
        private const int NeededSA = 100;
        private const int NeededBD = 200;
        private const int NeededHL = 150;
        #endregion WarehouseStoringAndFines
        #region SimulationVariables
        private const int Weeks = 30;
        

        private int WarehouseStorageSA { get; set; } = 0;
        private int WarehouseStorageBD { get; set; } = 0;
        private int WarehouseStorageHL { get; set; } = 0;
        private double Cost { get; set; } = 0.0;
        private double TotalCost { get; set; } = 0.0;
        private int DoneReplications { get; set; } = 0;
        private CustomDemand? CustomDemand { get; set; } = null;
        #endregion SimulationVariables
        public Sem1Simulation(int replications, int strategy, OxyPlot.PlotModel plotModel)
        {
            
            Replications = replications;
            CalculateReplicationsToChart();
            Strategy = strategy;
            PlotModel = plotModel;
            Application.Current.Dispatcher.Invoke(() => //Updates GUI continouslly
            {
                if (replications == 1)
                {
                    PlotModel.DefaultXAxis.Title = "Days";
                }
                if (Strategy < 1 || Strategy > 4)
                {
                    CustomDemand = new CustomDemand();
                }
                PlotModel.Series.Clear();
            }); 
            PlotModel.Series.Add(LineSeries);
            
            Task.Run(() => {
                Simulate();
                });
        }

       

        public override void Simulate()
        {
            base.Simulate();
        }
        public override void OneReplication()
        {
            if (Strategy < 1 || Strategy > 4)
            {
                CustomSimulation();
            }
            if (Strategy == 1)
            {
                for (int week = 1; week <= Weeks; week++)
                {
                    if (week >= 11 && Supplier1_2 != null)
                    {
                        double chance = Supplier1_2.NextDouble();
                        if ((ArrivalChance?.NextDouble() * 100) <= chance)
                        {
                            WarehouseStorageHL += NeededHL;
                            WarehouseStorageSA += NeededSA;
                            WarehouseStorageBD += NeededBD;
                        }
                    }
                    else
                    {
                        if (Supplier1_1 != null)
                        {
                            double chance = Supplier1_1.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageSA += NeededSA;
                                WarehouseStorageBD += NeededBD;
                                WarehouseStorageHL += NeededHL;
                            }
                        }
                    }
                    if (Replications == 1)
                    {
                        OneRepSetting(week);
                    } else
                    {
                        AddCost(4);
                        DeliverProducts();
                        AddCost(3);
                    }
                }
                
            }
            if (Strategy == 2)
            {
                for (int week = 1; week <= Weeks; week++)
                {
                    if (week >= 16 && Supplier2_2 != null)
                    {
                        double chance = Supplier2_2.NextDouble();
                        if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                        {
                            WarehouseStorageHL += NeededHL;
                            WarehouseStorageSA += NeededSA;
                            WarehouseStorageBD += NeededBD;
                        }
                    }
                    else
                    {
                        if (Supplier2_1 != null)
                        {
                            double chance = Supplier2_1.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageSA += NeededSA;
                                WarehouseStorageBD += NeededBD;
                                WarehouseStorageHL += NeededHL;
                            }
                        }
                    }
                    if (Replications == 1)
                    {
                        OneRepSetting(week);
                    } else { 
                        AddCost(4);
                        DeliverProducts();
                        AddCost(3);
                    }
                }
            }
            if (Strategy == 3)
            {
                for (int week = 1; week <= Weeks; week++)
                {

                    if (week % 2 == 1)
                    {
                        if (week >= 11 && Supplier1_2 != null)
                        {
                            double chance = Supplier1_2.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageHL += NeededHL;
                                WarehouseStorageSA += NeededSA;
                                WarehouseStorageBD += NeededBD;
                            }
                        }
                        else
                        {
                            if (Supplier1_1 != null)
                            {
                                double chance = Supplier1_1.NextDouble();
                                if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                                {
                                    WarehouseStorageSA += NeededSA;
                                    WarehouseStorageBD += NeededBD;
                                    WarehouseStorageHL += NeededHL;
                                }
                            }
                        }
                    } else
                    {
                        if (week >= 16 && Supplier2_2 != null)
                        {
                            double chance = Supplier2_2.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageHL += NeededHL;
                                WarehouseStorageSA += NeededSA;
                                WarehouseStorageBD += NeededBD;
                            }
                        }
                        else
                        {
                            if (Supplier2_1 != null)
                            {
                                double chance = Supplier2_1.NextDouble();
                                if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                                {
                                    WarehouseStorageSA += NeededSA;
                                    WarehouseStorageBD += NeededBD;
                                    WarehouseStorageHL += NeededHL;
                                }
                            }
                        }
                    }
                    if (Replications == 1)
                    {
                        OneRepSetting(week);
                    }
                    else
                    {
                        AddCost(4);
                        DeliverProducts();
                        AddCost(3);
                    }
                }
            }
            if (Strategy == 4)
            {
                for (int week = 1; week <= Weeks; week++)
                {

                    if (week % 2 == 0)
                    {
                        if (week >= 11 && Supplier1_2 != null)
                        {
                            double chance = Supplier1_2.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageHL += NeededHL;
                                WarehouseStorageSA += NeededSA;
                                WarehouseStorageBD += NeededBD;
                            }
                        }
                        else
                        {
                            if (Supplier1_1 != null)
                            {
                                double chance = Supplier1_1.NextDouble();
                                if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                                {
                                    WarehouseStorageSA += NeededSA;
                                    WarehouseStorageBD += NeededBD;
                                    WarehouseStorageHL += NeededHL;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (week >= 16 && Supplier2_2 != null)
                        {
                            double chance = Supplier2_2.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageHL += NeededHL;
                                WarehouseStorageSA += NeededSA;
                                WarehouseStorageBD += NeededBD;
                            }
                        }
                        else
                        {
                            if (Supplier2_1 != null)
                            {
                                double chance = Supplier2_1.NextDouble();
                                if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                                {
                                    WarehouseStorageSA += NeededSA;
                                    WarehouseStorageBD += NeededBD;
                                    WarehouseStorageHL += NeededHL;
                                }
                            }
                        }
                    }
                    if (Replications == 1)
                    {
                        OneRepSetting(week);
                    }
                    else
                    {
                        AddCost(4);
                        DeliverProducts();
                        AddCost(3);
                    }
                }
            }
        }

        

        private void CustomSimulation()
        {
            for (int week = 0; week < Weeks; week++)
            {
                if (CustomDemand.Supplier[week] == 1)
                {
                    if (week < 11 && Supplier1_1 != null)
                    {
                        double chance = Supplier1_1.NextDouble();
                        if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                        {
                            WarehouseStorageSA += CustomDemand.SAToExport[week];
                            WarehouseStorageBD += CustomDemand.BDToExport[week];
                            WarehouseStorageHL += CustomDemand.HLToExport[week];
                        }
                    } else
                    {
                        if (Supplier1_2 != null)
                        {
                            double chance = Supplier1_2.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageSA += CustomDemand.SAToExport[week];
                                WarehouseStorageBD += CustomDemand.BDToExport[week];
                                WarehouseStorageHL += CustomDemand.HLToExport[week];
                            }
                        }
                    } 
                } else if (CustomDemand.Supplier[week] == 2)
                {
                    if (week < 16 && Supplier2_1 != null)
                    {
                        double chance = Supplier2_1.NextDouble();
                        if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                        {
                            WarehouseStorageSA += CustomDemand.SAToExport[week];
                            WarehouseStorageBD += CustomDemand.BDToExport[week];
                            WarehouseStorageHL += CustomDemand.HLToExport[week];
                        }
                    }
                    else
                    {
                        if (Supplier2_2 != null)
                        {
                            double chance = Supplier2_2.NextDouble();
                            if ((ArrivalChance?.NextDouble() * 100) <= chance + CHECK)
                            {
                                WarehouseStorageSA += CustomDemand.SAToExport[week];
                                WarehouseStorageBD += CustomDemand.BDToExport[week];
                                WarehouseStorageHL += CustomDemand.HLToExport[week];
                            }
                        }
                    }
                }
                if (Replications == 1)
                {
                    OneRepSetting(week);
                } else
                {
                    AddCost(4);
                    DeliverProducts();
                    AddCost(3);
                }
            }
        }
          
        private void DeliverProducts()
        {
            int NumberOfSAToExport = 0;
            int NumberOfBDToExport = 0;
            int NumberOfHLToExport = 0;

            NumberOfSAToExport = ShockAbsorbers != null ? ShockAbsorbers.NextInt() : throw new Exception("Random not inicialized");
            NumberOfBDToExport = BrakeDiscs != null ? BrakeDiscs.NextInt() : throw new Exception("Random not inicialized");
            NumberOfHLToExport = Headlights != null ? Headlights.NextInt() : throw new Exception("Random not inicialized");

            int DeliveredSA = Math.Min(WarehouseStorageSA, NumberOfSAToExport);
            int DeliveredBD = Math.Min(WarehouseStorageBD, NumberOfBDToExport);
            int DeliveredHL = Math.Min(WarehouseStorageHL, NumberOfHLToExport);
            
            int UnDeliveredSA = NumberOfSAToExport - DeliveredSA;
            int UnDeliveredBD = NumberOfBDToExport - DeliveredBD;
            int UnDeliveredHL = NumberOfHLToExport - DeliveredHL;

            WarehouseStorageSA -= DeliveredSA;
            WarehouseStorageBD -= DeliveredBD;
            WarehouseStorageHL -= DeliveredHL;

            //Pay fine for every undelivered product
            Cost += ((UnDeliveredSA + UnDeliveredBD + UnDeliveredHL) * Fine);
        }
        private void AddCost(int days)
        {
            Cost += ((WarehouseStorageSA * SAStoring) * days);
            Cost += ((WarehouseStorageBD * BDStoring) * days);
            Cost += ((WarehouseStorageHL * HLStoring) * days);
        }

        public override void BeforeReplication()
        {

            if (Strategy < 1 && Strategy > 4)
            {
                throw new Exception("Bad selected simulation");
            }
            
            WarehouseStorageSA = 0;
            WarehouseStorageBD = 0;
            WarehouseStorageHL = 0;
                
            Cost = 0.0;
        }
        public override void AfterReplication()
        {
            if (Strategy < 1 || Strategy > 5 || Replications == 1)
            {
                return;
            }
            DoneReplications++;
            TotalCost += Cost;
            if (DoneReplications > ((Replications / 100) * 30) && DoneReplications % WritePointNumber == 0)
            {
                Application.Current.Dispatcher.Invoke(() => //Updates GUI continouslly
                {
                    DataPoint point = new DataPoint(DoneReplications, TotalCost / DoneReplications);
                    LineSeries.Points.Add(point);
                    PlotModel.InvalidatePlot(true);
                });
            }
        }
        public override void BeforeSimulation()
        {
            InitParts();
            InitSuppliers();
            ArrivalChance = new Random(SeedGenerator.GetSeed());
        }

        private void InitSuppliers()
        {
            Supplier1_1 = new OwnRandom(false, new List<Scope> { new Scope(10, 70, -1) });
            Supplier1_2 = new OwnRandom(false, new List<Scope> { new Scope(30, 95, -1) });
            
            Supplier2_1 = new OwnRandom(false, new List<Scope> 
            { 
                new Scope(5, 10, 0.4),
                new Scope(10, 50, 0.3),
                new Scope(50, 70, 0.2),
                new Scope(70, 80, 0.06),
                new Scope(80, 95, 0.04),
            });
            Supplier2_2 = new OwnRandom(false, new List<Scope>
            {
                new Scope(5, 10, 0.2),
                new Scope(10, 50, 0.4),
                new Scope(50, 70, 0.3),
                new Scope(70, 80, 0.06),
                new Scope(80, 95, 0.04),
            });
        }

        private void InitParts()
        {
            ShockAbsorbers = new OwnRandom(true, new List<Scope> { new Scope(50, 100, -1) });
            BrakeDiscs = new OwnRandom(true, new List<Scope> { new Scope(60, 250, -1) });
            Headlights = new OwnRandom(true, new List<Scope>
            {
                new Scope(30, 60, 0.2),
                new Scope(60, 100, 0.4),
                new Scope(100, 140, 0.3),
                new Scope(140, 160, 0.1),
            });
        }

        private void OneRepSetting(int week)
        {
            double MondayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(MondayCost, 1, week);
            double TuesdayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(TuesdayCost, 2, week);
            double WednesdayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(WednesdayCost, 3, week);
            double ThursdayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(ThursdayCost, 4, week);
            DeliverProducts();
            double FridayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(FridayCost, 5, week);
            double SaturdayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(SaturdayCost, 6, week);
            double SundayCost = Cost + AddOneDayCost();
            UpdateChartOneRep(SundayCost, 7, week);
        }

        private double AddOneDayCost()
        {
            AddCost(1);
            return (WarehouseStorageSA * SAStoring) + (WarehouseStorageBD * BDStoring)
                + (WarehouseStorageHL * HLStoring);
        }
        private void UpdateChartOneRep(double dayCost,int day, int week)
        {
            Thread.Sleep(150);
            TotalCost += Cost;
            Application.Current.Dispatcher.Invoke(() => //Updates GUI continouslly
            {
                DataPoint point;
                if (week == 1)
                {
                    point = new DataPoint(day, dayCost);
                }
                point = new DataPoint(day + (week * 7), dayCost);
                LineSeries.Points.Add(point);
                PlotModel.InvalidatePlot(true);
            });
        }
        private void CalculateReplicationsToChart()
        {
            int ReptToShow = (int)(Replications * 0.7);
            WritePointNumber = ReptToShow / CHARTPOINTS < 1 ? 1 : ReptToShow / CHARTPOINTS;
        }
    }
    internal class CustomDemand
    {
        public List<int> Supplier = [];
        public List<int> SAToExport = [];
        public List<int> BDToExport = [];
        public List<int> HLToExport = [];
        public CustomDemand() 
        {
            //"C:\Users\Lukas\Desktop\Letny semester\DISS\Cvicenia\Sem1\Core\GUI\Simulations\Sem1\CustomPartsDemand.csv"
            using (StreamReader reader = new StreamReader("../../../../GUI/Simulations/Sem1/CustomPartsDemand.csv"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine() ?? throw new Exception("Reader was null");
                    string[] fields = line?.Split(';') ?? throw new Exception("Value was null");
                    Supplier.Add(int.Parse(fields[0]));
                    SAToExport.Add(int.Parse(fields[1]));
                    BDToExport.Add(int.Parse(fields[2]));
                    HLToExport.Add(int.Parse(fields[3]));
                }
            }
        }
    }
}
