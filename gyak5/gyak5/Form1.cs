using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak5
{
    public partial class Form1 : Form
    {
        List<Tick> Ticks;
        PortfolioEntities context = new PortfolioEntities();
        List<Entities.PortfolioItem> Portfolios = new List<Entities.PortfolioItem>(); //csak Entities.-al működik
        
        public Form1()
        {
            InitializeComponent();
            
            Ticks = context.Ticks.ToList();
            dataGridView1.DataSource = Ticks;

            CreatePortfolio();
        }

        private void CreatePortfolio()
        {
            //PortfolioItem p = new PortfolioItem() és p.Index = "OTP" stb... egyszerűbb formában való megadása
            Portfolios.Add(new Entities.PortfolioItem() { Index = "OTP", Volume = 10 }); 
            Portfolios.Add(new Entities.PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolios.Add(new Entities.PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolios;
        }
    }
}
