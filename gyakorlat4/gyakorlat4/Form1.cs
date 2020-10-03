using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace gyakorlat4
{
    public partial class Form1 : Form
    {
        List<Flat> Flats;
        RealEstateEntities context = new RealEstateEntities();

        Excel.Application xlApp; //Microsoft Excel alkalmazás
        Excel.Workbook xlWB; //létrehozott munkafüzet
        Excel.Worksheet xlSheet; //Munkalap a munkafüzeten belül
        public Form1()
        {
            InitializeComponent();
            LoadData();
            CreateExcel();
        }

        private void LoadData()
        {
            Flats = context.Flats.ToList();
        }

        private string GetCell(int x, int y) //hogy ez mit csinál...
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }

        private void CreateTable()
        {
            string[] headers = new string[]
            {
                "Kód","Eladó","Oldal","Kerület","Lift","Szobák száma",
                "Alapterület (m2)","Ár (mFt)","Négyzetméter ár (Ft/m2)"
            };

            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, i+1] = headers[i];
            }
            object[,] values = new object[Flats.Count, headers.Length]; //flats.count hány sorom van; headers.length hány oszlopom

            int szamlalo = 0;
            foreach (var s in Flats)
            {
                values[szamlalo, 0] = s.Code;
                values[szamlalo, 1] = s.Vendor;
                values[szamlalo, 2] = s.Side;
                values[szamlalo, 3] = s.District;
                if (s.Elevator == true)
                {
                    values[szamlalo, 4] = "Van";
                }
                else
                {
                    values[szamlalo, 4] = "Nincs";
                }
                values[szamlalo, 5] = s.NumberOfRooms;
                values[szamlalo, 6] = s.FloorArea;
                values[szamlalo, 7] = s.Price;
                values[szamlalo, 8] = "";
                szamlalo++;
            }
            //biztos nem ide kell, de fogalmam sincs hova kéne
            xlSheet.get_Range(
             GetCell(2, 1),
             GetCell(1 + values.GetLength(0), values.GetLength(1))).Value2 = values;
        }

        private void CreateExcel()
        {
            try
            {
                
                xlApp = new Excel.Application(); // Excel elindítása és az applikáció objektum betöltése
               
                xlWB = xlApp.Workbooks.Add(Missing.Value); // Új munkafüzet

                xlSheet = xlWB.ActiveSheet; // Új munkalap 

                //CreateTable();

                //Control átadása a felhasználónak
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                //Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }
        }
    }
}
