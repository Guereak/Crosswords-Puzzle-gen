﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace S3_Projet
{
    class Joueur
    {
        private string nom;
        private string[] motsTrouves;
        private int score;

        public static int dimX;
        public static int dimY;

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

        public void Add_Score(int val)
        {
            score += val;
        }

        public void Tour(int tourNum, int dimensionX, int dimensionY)
        {
            Plateau p = PlateauGenerator.GeneratePlateau(tourNum, dimensionX, dimensionY);
        }

        public static Plateau Manche(Joueur player, int difficulte)
        {

            Timer t = new Timer();

            Thread th = new Thread(new ThreadStart(t.StartTimer));
            th.Start();

            Plateau currentPlateau = PlateauGenerator.GeneratePlateau(difficulte, 10, 10);
            bool foundAllWords = false;

            currentPlateau.AfficherPlateau();


            while (t.Time <= 60 && !foundAllWords)
            {
                string[] args = Console.ReadLine().Split(' ');

                if (currentPlateau.MotsATrouver.Contains(args[0]))
                {
                    if (currentPlateau.Test_Plateau(args[0], Int32.Parse(args[2]), Int32.Parse(args[3]), args[1]))
                    {
                        if (t.Time <= 60)
                        {
                            player.Add_Mot(args[0]);
                            player.Add_Score(args[0].Length);

                            if (player.MotsTrouves.Length < currentPlateau.MotsATrouver.Length)
                            {
                                Console.WriteLine("Bien joué à toi! Il reste encore des mots");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Malheureusement le temps est écoulé! Ton score est maintenant de {player.Score}");
                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Le mot n'est pas à cet endroit. Continue à chercher!");
                    }

                    player.Add_Mot(args[1]);
                }
                else
                {
                    Console.WriteLine("Ce mot n'est pas un mot à trouver :(");
                }

            }

            return currentPlateau;

        }
    }
}