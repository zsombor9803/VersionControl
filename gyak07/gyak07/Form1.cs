using gyak07.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyak07
{
    public partial class Form1 : Form
    {
        Random rnd = new Random(1234);

        List<Person> Population = new List<Person>();
        List<BirthProbability> BirthProbabilities = new List<BirthProbability>();
        List<DeathProbability> deathProbabilities = new List<DeathProbability>();

        public Form1()
        {
            InitializeComponent();

            Population = GetPopulation(@"C:\Temp\nép.csv");
            BirthProbabilities = GetBirthProbabilities(@"C:\Temp\születés.csv");
            deathProbabilities = GetDeathProbabilities(@"C:\Temp\halál.csv");

            for (int year = 2015; year < 2024; year++)
            {
                for (int i = 0; i < Population.Count; i++)
                {

                }

                int NumberOfMales = (from x in Population
                                     where x.Gender == Gender.Male
                                     && x.IsAlive
                                     select x).Count();

                int NumberOfFemales = (from x in Population
                                    where x.Gender == Gender.Female && x.IsAlive
                                    select x).Count();

                Console.WriteLine(string.Format("Év:{0} Fiúk:{1} Lányok:{2}", year, NumberOfMales, NumberOfFemales))
            }
        }
        public List<Person> GetPopulation(string csvpath)
        {
            List<Person> population = new List<Person>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    population.Add(new Person()
                    {
                        BirthYear = int.Parse(line[0]),
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[1]),
                        NumberOfChildren = byte.Parse(line[2])
                    });
                }
            }
            return population;
        }

        public List<BirthProbability> GetBirthProbabilities(string csvpath)
        {
            List<BirthProbability> birthProbabilities = new List<BirthProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    birthProbabilities.Add(new BirthProbability()
                    {
                        Age = int.Parse(line[0]),
                        NumberOfChildren = byte.Parse(line[1]),
                        Death = double.Parse(line[2])
                    });
                }
            }
            return birthProbabilities;
        }

        public List<DeathProbability> GetDeathProbabilities(string csvpath)
        {
            List<DeathProbability> deathProbabilities = new List<DeathProbability>();

            using (StreamReader sr = new StreamReader(csvpath, Encoding.Default))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(';');
                    deathProbabilities.Add(new DeathProbability()
                    {
                        Gender = (Gender)Enum.Parse(typeof(Gender), line[0]),
                        Age = int.Parse(line[1]),
                        Death = double.Parse(line[2])
                    });
                }
            }
            return deathProbabilities;
        }
    }
}
