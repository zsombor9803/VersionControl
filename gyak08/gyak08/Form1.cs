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
        private List<Entities.Ball> _balls = new List<Entities.Ball>();
        
        private Entities.BallFactory _factory;       

        public Entities.BallFactory Factory 
        {
            get { return _factory; } 
            set { _factory = value; } 
        }
        public Form1()
        {
            InitializeComponent();
            Factory = new Entities.BallFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {
            var ball = Factory.CreateNew();
            _balls.Add(ball);
            ball.Left = -ball.Width;
            mainPanel.Controls.Add(ball);
        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {
            var jobbos = 0;
            foreach (var item in _balls)
            {
                item.MoveBall();
                if (item.Left> jobbos)
                {
                    jobbos = item.Left;
                }
            }

            if (jobbos > 1000)
            {
                var oldestBall = _balls[0];
                mainPanel.Controls.Remove(oldestBall);
                _balls.Remove(oldestBall);
            }
        }
    }
}
