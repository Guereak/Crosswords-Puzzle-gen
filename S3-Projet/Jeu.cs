using System;
using System.Collections.Generic;
using System.IO;

namespace S3_Projet
{
    public class Jeu
    {
        Plateau currentPlateau;
        List<Plateau> previousPlateaux = new List<Plateau>();

        Joueur player1;
        Joueur player2;

        int nombreDeManches = 4;
        int mancheActuelle;


        public void StartGame()
        {
            Console.WriteLine("Bonjour Joueur 1, entrez votre nom:");
            player1 = new Joueur(Console.ReadLine());
            Console.WriteLine("Bonjour Joueur 2, entrez votre nom:");
            player2 = new Joueur(Console.ReadLine());

            for (mancheActuelle = 0; mancheActuelle < nombreDeManches; mancheActuelle++)
            {
                Console.WriteLine($"Au tour de {player1.Nom}");
                Console.ReadKey();
                currentPlateau = Joueur.Manche(player1, mancheActuelle+1);
                previousPlateaux.Add(currentPlateau);

                Console.WriteLine($"Au tour de {player2.Nom}");
                Console.ReadKey();
                currentPlateau = Joueur.Manche(player2, mancheActuelle + 1);
                previousPlateaux.Add(currentPlateau);

                //Ici on veut proposer la sauvegarde de la partie
                Console.WriteLine("Voulez vous sauvegarder la partie? (o/n)");

            }
        }

        public void SaveGame()
        {
            /* Pour effectuer la sauvegarde, on crée un répertoire de sauvegarde avec le nom de la sauvegarde
               Ce répertoire contient un fichier game.csv qui contient les données de sauvegarde du jeu
               Il contient également les fichers de plateau nommés comme:
               { J1 | J2 }_Manche_{ 1 | 2 | 3 | 4 }.csv

                La sauvegarde ne peut se faire qu'entre les manches.
             */
            string savePath = DateTime.Now.ToString("Savce_dd_MM_yyyy_HH_mm");

            //On crée le fichier de sauvegarde
            Directory.CreateDirectory($"../../Sauvegardes/{savePath}");

            string gameData = "";
            gameData += player1.Nom + ',' + player1.Score + '\n';
            gameData += player2.Nom + ',' + player2.Score + '\n';

            gameData += mancheActuelle;

            File.WriteAllText($"../../Sauvegardes/{savePath}/game.csv", gameData);

            int joueur = 1;
            int manche = 1;

            foreach(Plateau plateau in previousPlateaux)
            {
                plateau.ToFile($"../../Sauvegardes/{savePath}/J{joueur}_Manche_{manche}.csv");

                if(joueur == 1)
                {
                    joueur++;
                }
                else
                {
                    manche++;
                    joueur = 1;
                }
            }
        }

    }
}
