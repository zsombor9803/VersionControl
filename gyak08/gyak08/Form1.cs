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

        private Abstractions.Toy _nextToy;

        private Abstractions.IToyFactory _factory;       

        public Abstractions.IToyFactory Factory 
        {
            get { return _factory; } 
            set { _factory = value; DisplayNext(); } 
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void CAR_Click(object sender, EventArgs e)
        {
            Factory = new Entities.CarFactory();
        }

        private void BALL_Click(object sender, EventArgs e)
        {
            Factory = new Entities.BallFactory 
            { 
                BallColor = button1.BackColor
            };
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
                Controls.Remove(_nextToy);
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            Controls.Add(_nextToy);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var colorPicker = new ColorDialog();

            colorPicker.Color = button.BackColor;
            if (colorPicker.ShowDialog() != DialogResult.OK)
            {
                return;               
            }
            button.BackColor = colorPicker.Color;
        }
    }
}
