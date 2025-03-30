using Core.EventSimulation;
using Core.NumberGen;
using Core.NumberGen.Distributions;
using Core.SimulationCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUI.Simulations.Sem2
{
    public class Sem2Simulation : Simulation
    {
        private const int DAYS = 249;
        
        private PriorityQueue<Event, double> UnstartedOrdersQueue = new();
        private PriorityQueue<Event, double> StainingAndVarnishingQueue = new();
        private PriorityQueue<Event, double> AssemblyQueue = new();
        private PriorityQueue<Event, double> ForgeAssemblyQueue = new();

        private int ActualDay = 0;
        private string DateToGUI = "";

        public bool Pause { get; set; } = false;


        #region Distributions
        private EvenDist? TableCutting;
        private EvenDist? TableStainingAndVarnishing;
        private EvenDist? TableAssembly;
        private EvenDist? ChairCutting;
        private EvenDist? ChairStainingAndVarnishing;
        private EvenDist? ChairAssembly;
        private EvenDist? WardrobeCutting;
        private EvenDist? WardrobeStainingAndVarnishing;
        private EvenDist? WardrobeAssembly;
        private EvenDist? WardrobeForgeAssembly;

        private ExponentialDist? NewOrder;
        private EvenDist? TypeOfFurnishing;
        private TriangularDist? WarehouseAssemblyPointMove;
        private TriangularDist? MaterialPreparation;
        private TriangularDist? AssemblyPointsMove;

        #endregion Distributions

        public Sem2Simulation() 
        {
            InitDistributions();
        }


        public override void OneReplication()
        {
            while (ActualDay != DAYS) 
            {
                if (Stopped)
                {
                    Thread.Sleep(250);
                }
            }
        }

        public override void BeforeReplication()
        {
            ActualDay = 0;
            UnstartedOrdersQueue.Clear();
            StainingAndVarnishingQueue.Clear();
            AssemblyQueue.Clear();
            ForgeAssemblyQueue.Clear();
        }

        public override void AfterReplication()
        {
            
        }
        private void InitDistributions()
        {
            NewOrder = new ExponentialDist(30);
            TypeOfFurnishing = new EvenDist(true, new List<Scope>
            {
                new Scope(0,1, 0.5),
                new Scope(1,2, 0.15),
                new Scope(2,3, 0.35)
            });
            WarehouseAssemblyPointMove = new TriangularDist(false, new Scope(60, 480, 120));
            MaterialPreparation = new TriangularDist(false, new Scope(300, 900, 500));
            AssemblyPointsMove = new TriangularDist(false, new Scope(120, 500, 150));

            TableCutting = new EvenDist(false, new List<Scope>
            {
                new Scope(10,25, 0.6),
                new Scope(25,50, 0.4)
            });
            TableStainingAndVarnishing = new EvenDist(false, new List<Scope> { new Scope(200, 610, -1d) });
            TableAssembly = new EvenDist(false, new List<Scope> { new Scope(30, 60, -1d) });

            ChairCutting = new EvenDist(false, new List<Scope> { new Scope(12, 16, -1d) });
            ChairStainingAndVarnishing = new EvenDist(false, new List<Scope> { new Scope(210, 540, -1d) });
            ChairAssembly = new EvenDist(false, new List<Scope> { new Scope(14, 24, -1d) });

            WardrobeCutting = new EvenDist(false, new List<Scope> { new Scope(15, 80, -1d) });
            WardrobeStainingAndVarnishing = new EvenDist(false, new List<Scope> { new Scope(600, 700, -1d) });
            WardrobeAssembly = new EvenDist(false, new List<Scope> { new Scope(35, 75, -1d) });
            WardrobeForgeAssembly = new EvenDist(false, new List<Scope> { new Scope(15, 25, -1d) });
        }
    }
}
