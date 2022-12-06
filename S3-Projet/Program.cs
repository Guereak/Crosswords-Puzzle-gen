using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace S3_Projet
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Jeu j = new Jeu();

            //save test
            j.SaveGame();

            j.StartGame();

            //PlateauGenerator.GeneratePlateau(1, 10, 10);
            //Console.WriteLine("Début du programme:");
            //Timer t = new Timer();
            //Thread th = new Thread(new ThreadStart(t.StartTimer));
            //Console.WriteLine(t.Time);
            //th.Start();
            //Console.ReadLine();
            //
            //Console.WriteLine(t.Time);



            Console.ReadLine();


            /*Plateau p = new Plateau();
            p.ToRead("Grid1.csv");

            Console.WriteLine(p.ToString());
            Console.ReadLine();

            p.ToFile("queue.csv");
            Console.ReadLine();
            Console.WriteLine(p.Test_Plateau("AGILITE", 0, 4, "S"));
            Console.ReadLine();*/
        }
    }
}

