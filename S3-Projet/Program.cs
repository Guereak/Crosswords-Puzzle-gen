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
            Menu(j);

            Console.ReadLine();
        }

        static void Menu(Jeu j)
        {
            bool validOption = false;
            while (!validOption)
            {
                Console.Clear();
                Console.WriteLine("Sélectionner une option");
                Console.WriteLine("1) Lancer une nouvelle partie");
                Console.WriteLine("2) Charger une partie depuis la sauvegarde");
                Console.WriteLine("3) Rechercher un mot dans le dictionnaire (recherche dichotomique)\n");

                string s = Console.ReadLine().Trim();

                switch (s)
                {
                    case "1":
                        validOption = true;
                        j.StartGame();
                        break;
                    case "2":
                        Jeu.ListSavedGames();
                        validOption = true;
                        break;
                    case "3":
                        SearchWord();
                        validOption = true;
                        break;
                    default:
                        Console.WriteLine("Vous n'avez pas entré un nombre entre 1 et 3. Réesayez");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void SearchWord()
        {
            Console.Write("Entrez un mot à rechercher >");
            string s = Console.ReadLine();

            Console.WriteLine("Dans quelle langue? Anglais: 'EN', Défaut: Français");
            string l = Console.ReadLine();
            Dictionnaire d = new Dictionnaire(l);
            if (d.Search(s))
            {
                Console.WriteLine("Ce mot existe.");
            }
            else
            {
                Console.WriteLine("Ce mot n'existe pas");
            }
            Console.ReadLine();
        }
    }
}

