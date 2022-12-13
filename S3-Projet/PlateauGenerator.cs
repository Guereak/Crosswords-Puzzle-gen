using System;
using System.Threading;
using System.Text.RegularExpressions;

namespace S3_Projet
{
    class PlateauGenerator
    {
        //Ajustable

        public static Dictionnaire dico;

        static int trialThreshold = 400;

        static string[] directions = { "E", "S", "N", "O", "NE", "SO", "NO", "SE" };


        /// <summary>
        /// Génère un plateau avec des mots placés aléatoirement dans la grille
        /// </summary>
        /// <param name="difficulty">difficulté</param>
        /// <param name="nbrLignes">nombre de lignes du plateau</param>
        /// <param name="nbrColonnes">nombre de colonnes du plateau</param>
        public static Plateau GeneratePlateau(int difficulty, int nbrLignes, int nbrColonnes)
        {
            char[,] newPlateau = new char[nbrLignes, nbrColonnes];
            string[] motsATrouver;
            //On détermine le nombre de mots à placer en fonction de la taille
            int nbrMots = (int)Math.Sqrt(nbrLignes * nbrColonnes * 1.2);
            motsATrouver = new string[nbrMots];

            for (int i = 0; i < nbrMots; i++)
            {
                //On choisit la direction dans laquelle placer le mot
                Randomizer randomizer = new Randomizer();
                int seed = randomizer.Next();
                Random r = new Random(seed);
                string direction;

                switch (difficulty)
                {
                    case 1:
                        direction = directions[r.Next(2)];
                        break;
                    case 2:
                        direction = directions[r.Next(4)];
                        break;
                    case 3:
                        direction = directions[r.Next(6)];
                        break;
                    default:
                        direction = directions[r.Next(8)]; 
                        break;
                }

                int movX = 0;
                int movY = 0;

                switch (direction)
                {
                    case "N":
                        movX = -1;
                        break;
                    case "S":
                        movX = 1;
                        break;
                    case "E":
                        movY = 1;
                        break;
                    case "O":
                        movY = -1;
                        break;
                    case "NE":
                        movX = -1;
                        movY = 1;
                        break;
                    case "NO":
                        movX = -1;
                        movY = -1;
                        break;
                    case "SE":
                        movX = 1;
                        movY = 1;
                        break;
                    case "SO":
                        movX = 1;
                        movY = -1;
                        break;
                }

                bool motPlace = false;
                int motLength = 0;
                bool correctLength = false;

                //On choisit une longeur de mot adaptée en fonction de la direction
                while (!correctLength)
                {
                    if (movX != 0 && movY != 0)
                    {
                        motLength = r.Next(4, Math.Min(nbrLignes, nbrColonnes));
                    }
                    else if (movX != 0)
                    {
                        motLength = r.Next(4, nbrColonnes);
                    }
                    else if (movY != 0)
                    {
                        motLength = r.Next(4, nbrLignes);
                    }

                    if(motLength < 15)
                    {
                        correctLength = true;
                    }
                }
                

                int posX = 0;
                int posY = 0;

                while (!motPlace)
                {
                    //On génère une position de départ aléatoire sur le plateau
                    posX = r.Next(nbrColonnes);
                    posY = r.Next(nbrLignes);
                    //Console.WriteLine($"{posX}, {posY}");

                    //On vérifie qu'on est pas outOfBounds
                    if((posX + motLength * movX >= 0 && posX + motLength * movX < nbrColonnes) && (posY + motLength * movY >= 0 && posY + motLength * movY < nbrLignes))
                    {
                        motPlace = true;
                    }
                }

                //On parcourt ces positions sur la matrice de char pour vérifier si on a des contraintes
                string searchFor = "";

                for (int j = 0; j < motLength; j++)
                {
                    if (newPlateau[posY + j * movY, posX + j * movX] == '\0')
                    {
                        searchFor += '.';
                    }
                    else
                    {
                        searchFor += newPlateau[posY + j * movY, posX + j * movX];
                    }
                }

                //Regex
                Regex reg = new Regex(searchFor);
                if(dico.wordList.Count != 0)
                {
                    dico.wordList.Clear();
                }
                dico.ListFiller(motLength);

                int index = 0;
                bool foundword = false;

                string leMot = searchFor;

                while(index < trialThreshold && !foundword)
                {
                    int searchIndex = r.Next(dico.wordList.Count);

                    if (reg.IsMatch(dico.wordList[searchIndex]))
                    {
                        leMot = dico.wordList[searchIndex];
                        motsATrouver[i] = leMot;
                        foundword = true;
                    }
                    index++;
                }

                // Conséquence si on a pas trouvé de mot potable
                if (!foundword)
                {
                    i--;
                }
                else
                {
                    for (int j = 0; j < motLength; j++)
                    {
                        newPlateau[posY + j * movY, posX + j * movX] = leMot[j];
                    }
                }

            }

            //On génère des lettres au hasard pour remplir la grille
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random rand = new Random();

            for (int j = 0; j < newPlateau.GetLength(0); j++)
            {
                for(int k = 0; k < newPlateau.GetLength(1); k++)
                {
                    if(newPlateau[j,k] == '\0')
                    {
                        newPlateau[j, k] = alphabet[rand.Next(26)];
                    }
                }
            }


            //DEBUG
            string s1 = "";
            foreach (string s in motsATrouver)
            {
                s1 += s + ", ";
            }
            Console.WriteLine(s1);

            Plateau.AfficherPlateau(newPlateau);

            Plateau p = new Plateau(newPlateau, difficulty, motsATrouver);

            return p;
        }
    }
}