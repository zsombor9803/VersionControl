﻿using System;
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

            List<decimal> Nyereségek = new List<decimal>();
            int intervallum = 30;
            DateTime kezdőDátum = (from x in Ticks select x.TradingDay).Min();
            DateTime záróDátum = new DateTime(2016, 12, 30);
            TimeSpan z = záróDátum - kezdőDátum;

            for (int i = 0; i < z.Days - intervallum; i++)
            {
                decimal ny = GetPortfolioValue(kezdőDátum.AddDays(i + intervallum))
                             - GetPortfolioValue(kezdőDátum.AddDays(i));
                Nyereségek.Add(ny);
                Console.WriteLine(i + "" + ny);
            }

            var nyereségrendezés = (from x in Nyereségek orderby x select x).ToList();
            MessageBox.Show(nyereségrendezés[nyereségrendezés.Count() / 5].ToString());

        }

        private void CreatePortfolio()
        {
            //PortfolioItem p = new PortfolioItem() és p.Index = "OTP" stb... egyszerűbb formában való megadása
            Portfolios.Add(new Entities.PortfolioItem() { Index = "OTP", Volume = 10 }); 
            Portfolios.Add(new Entities.PortfolioItem() { Index = "ZWACK", Volume = 10 });
            Portfolios.Add(new Entities.PortfolioItem() { Index = "ELMU", Volume = 10 });

            dataGridView2.DataSource = Portfolios;
        }

        private decimal GetPortfolioValue(DateTime date)
        {
            decimal value = 0;
            foreach (var item in Portfolios)
            {
                var last = (from x in Ticks
                            where item.Index == x.Index.Trim()
                            && date <= x.TradingDay select x).First();
                value += (decimal)last.Price * item.Volume;
            }
            return value;
        }
    }
}
