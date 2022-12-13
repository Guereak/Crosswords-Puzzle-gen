using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace S3_Projet
{
    public class Joueur
    {
        private string nom;
        public string[] motsTrouves;
        private int score;

        public static int maxTime = 60;

        public string[] MotsTrouves
        {
            get { return motsTrouves; }
        }

        public int Score
        {
            get { return score; }
        }

        public string Nom
        {
            get { return nom; }
        }

        public Joueur(string nom)
        {
            this.nom = nom;
            score = 0;
            motsTrouves = null;
        }

        public Joueur(string nom, int score)
        {
            this.nom = nom;
            this.score = score;
            motsTrouves = null;
        }

        /// <summary>
        /// Permet d'ajouter un mot à la liste des mots trouvés par le joueur
        /// </summary>
        public void Add_Mot(string mot)
        {
            int l = motsTrouves == null ? 0 : motsTrouves.Length;

            string[] temp = new string[l + 1];

            for(int i = 0; i < l; i++)
            {
                temp[i] = motsTrouves[i];
            }

            temp[l] = mot;

            motsTrouves = temp;
        }

        public override string ToString()
        {
            return $"{nom} a trouvé {motsTrouves.Length} mots au cours de cette partie.\nSon score est de {score} points.";

            //Ajouter la liste des mots trouvés?
        }

        /// <summary>
        /// Ajoute une valeur à la méthode privée score
        /// </summary>
        public void Add_Score(int val)
        {
            score += val;
        }

        /// <summary>
        /// Génère un nouveau plateau pour la génération d'un nouveau mot
        /// </summary>
        public void Tour(int tourNum, int dimensionX, int dimensionY)
        {
            Plateau p = PlateauGenerator.GeneratePlateau(tourNum, dimensionX, dimensionY);
        }

        /// <summary>
        /// Gère une manche. Principlement utilisée pour assainir les entrées de l'utilisateur
        /// </summary>
        public static Plateau Manche(Joueur player, int difficulte, int taille)
        {

            Timer t = new Timer();

            //Thread
            Thread th = new Thread(new ThreadStart(t.StartTimer));
            th.Start();

            Plateau currentPlateau = PlateauGenerator.GeneratePlateau(difficulte, taille, taille);
            bool foundAllWords = false;

            currentPlateau.AfficherPlateau();

            while (t.Time <= maxTime && !foundAllWords)
            {
                string[] args = Console.ReadLine().Split(' ');

                if(args.Length >= 4)
                {
                    if (currentPlateau.MotsATrouver.Contains(args[0]))
                    {
                        if (currentPlateau.Test_Plateau(args[0], Int32.Parse(args[3]) - 1,Int32.Parse(args[2]) - 1, args[1]))
                        {
                            if (t.Time <= maxTime)
                            {
                                if(player.motsTrouves == null)
                                {
                                    player.motsTrouves = new string[0];
                                }

                                if (player.MotsTrouves.Length < currentPlateau.MotsATrouver.Length)
                                {
                                    //On rajoute le mot dans la liste des mots trouvés.
                                    if (!player.MotsTrouves.Contains(args[0]))
                                    {
                                        player.Add_Mot(args[0]);
                                        player.Add_Score(args[0].Length);
                                        Console.WriteLine("Bien joué à toi! Il reste encore des mots");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Tu as déjà trouvé ce mot. Essaye encore!");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Le mot n'est pas à cet endroit. Continue à chercher!");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ce mot n'est pas un mot à trouver :(");
                    }
                }
                else
                {
                    Console.WriteLine($"Il n'y a pas assez d'arguments: 4 attendus, {args.Length} lus.");
                }
            }

            Console.WriteLine($"Malheureusement le temps est écoulé! Ton score est maintenant de {player.Score}");
            Console.ReadKey();

            return currentPlateau;
        }
    }
}
