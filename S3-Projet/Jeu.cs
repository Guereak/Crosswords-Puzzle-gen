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
        int mancheActuelle = 0;

        int[] size = new int[] { 8, 10, 12, 14 };
        int[] timer = new int[] { 60, 90, 120, 150};

        public Jeu(int round, string p1Name, string p2Name, int p1Score, int p2Score)
        {
            player1 = new Joueur(p1Name, p1Score);
            player2 = new Joueur(p2Name, p2Score);
            mancheActuelle = round;
        }

        public Jeu() { }

        public void Game()
        {
            while (mancheActuelle < nombreDeManches)
            {
                Joueur.maxTime = timer[mancheActuelle];
                mancheActuelle++;
                
                Console.WriteLine($"Au tour de {player1.Nom}");
                Console.ReadKey();

                currentPlateau = Joueur.Manche(player1, mancheActuelle, size[mancheActuelle - 1]);
                previousPlateaux.Add(currentPlateau);

                Console.WriteLine($"Au tour de {player2.Nom}");
                Console.ReadKey();
                currentPlateau = Joueur.Manche(player2, mancheActuelle, size[mancheActuelle- 1]);
                previousPlateaux.Add(currentPlateau);

                //Ici on veut proposer la sauvegarde de la partie
                Console.WriteLine("Voulez vous sauvegarder la partie et quitter? (oui/non)");
                if (Console.ReadLine().ToLower() == "oui")
                {
                    SaveGame();
                    mancheActuelle = 1000;  //Dépasse le nombre de manches
                }

            }
        }


        /// <summary>
        /// Initialise le jeu
        /// </summary>
        public void StartGame()
        {
            Console.WriteLine("Quel dictionnaire utiliser? Anglais: 'EN', Français: 'FR' (par défaut) :");
            string s = Console.ReadLine();
            PlateauGenerator.dico = new Dictionnaire(s);
            Console.WriteLine($"Le dictionnaire sélectionné est le: {(s == "EN" ? "Anglais" : "Français")}");


            mancheActuelle = 0;
            Console.WriteLine("Bonjour Joueur 1, entrez votre nom:");
            player1 = new Joueur(Console.ReadLine());
            Console.WriteLine("Bonjour Joueur 2, entrez votre nom:");
            player2 = new Joueur(Console.ReadLine());

            Game();
        }

        public void SaveGame()
        {
            /* Pour effectuer la sauvegarde, on crée un répertoire de sauvegarde avec le nom de la sauvegarde
               Ce répertoire contient un fichier game.csv qui contient les données de sauvegarde du jeu
               Il contient également les fichers de plateau nommés comme:
               { J1 | J2 }_Manche_{ 1 | 2 | 3 | 4 }.csv

                La sauvegarde ne peut se faire qu'entre les manches.
             */
            string savePath = DateTime.Now.ToString("Save_dd_MM_yyyy_HH_mm");

            //On crée le fichier de sauvegarde
            Directory.CreateDirectory($"../../Sauvegardes/{savePath}");
            Directory.CreateDirectory($"../../Sauvegardes/{savePath}/Plateaux");

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

        /// <summary>
        /// Liste les jeux sauvegardés.
        /// </summary>
        public static void ListSavedGames()
        {
            string[] arr = Directory.GetFileSystemEntries("../../Sauvegardes/");
            Console.WriteLine("Choisissez la sauvegarde que vous souhaitez restaurer:");
            for(int i = 0; i < arr.Length; i++)
            {
                Console.WriteLine($"{i + 1}) " + arr[i].Substring(18));
            }
            Console.Write("\nNuméro de la sauvegarde > ");

            int outNum = 0;
            bool b = Int32.TryParse(Console.ReadLine(), out outNum);
            if(outNum > 0 && outNum <= arr.Length)
            {
                RestoreGame(arr[outNum-1]);
            }
            else
            {
                Console.WriteLine("Le nombre que vous avez entré n'est pas valide. Arrêt du programme");
                Console.ReadKey();
            }
        }

        /// <summary>
        /// Réstaurer le jeu depuis un chemin d'accès spécifique
        /// Seulement appelé depuis ListSavedGames
        /// </summary>
        /// <param name="path"></param>
        public static void RestoreGame(string path)
        {
            Console.WriteLine(path);
            string gameFilePath = $"{path}/game.csv";

            string[] gameData = File.ReadAllLines(gameFilePath);
            string[] p1Data = gameData[0].Split(',');
            string[] p2Data = gameData[1].Split(',');
            int round = Int32.Parse(gameData[2].Trim());

            string p1Name = p1Data[0];
            int p1Score = Int32.Parse(p1Data[1]);
            string p2Name = p2Data[0];
            int p2Score = Int32.Parse(p2Data[1]);

            Jeu j = new Jeu(round, p1Name, p2Name, p1Score, p2Score);

            //Restart the game
            j.Game();
        }

    }
}
