using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak08
{
    public partial class Form1 : Form
    {
        private List<Abstractions.Toy> _toys = new List<Abstractions.Toy>();
        
        private Abstractions.IToyFactory _factory;       

        public Abstractions.IToyFactory Factory 
        {
            get { return _factory; } 
            set { _factory = value; } 
        }
        public Form1()
        {
            InitializeComponent();
            //Factory = new Entities.BallFactory();
            Factory = new Entities.CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var toy = Factory.CreateNew();
            _toys.Add(toy);
            toy.Left = -toy.Width;
            mainPanel.Controls.Add(toy);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var jobbos = 0;
            foreach (var item in _toys)
            {
                item.MoveToy();
                if (item.Left> jobbos)
                {
                    jobbos = item.Left;
                }
            }

            if (jobbos > 1000)
            {
                var oldestBall = _toys[0];
                mainPanel.Controls.Remove(oldestBall);
                _toys.Remove(oldestBall);
            }
        }
    }
}
