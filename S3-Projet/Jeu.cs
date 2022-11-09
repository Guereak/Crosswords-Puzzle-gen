using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3_Projet
{
    class Jeu
    {
        Plateau currentPlateau;
        Plateau[] previousPlateaux;

        Joueur player1;
        Joueur player2;

        int nombreDeManches = 4;


        public void StartGame()
        {
            Console.WriteLine("Bonjour Joueur 1, entrez votre nom:");
            player1 = new Joueur(Console.ReadLine());
            Console.WriteLine("Bonjour Joueur 2, entrez votre nom:");
            player2 = new Joueur(Console.ReadLine());

            for (int i = 0; i < nombreDeManches; i++)
            {
                Console.WriteLine($"Au tour de {player1.Nom}");
                Console.ReadKey();
                currentPlateau = Joueur.Manche(player1, i+1);

                Console.WriteLine($"Au tour de {player2.Nom}");
                Console.ReadKey();
                currentPlateau = Joueur.Manche(player2, i + 1);
            }
        }
    }
}
