using System;
using System.Threading;
using System.IO;

namespace S3_Projet
{
    public class Plateau
    {
        private char[,] matrice;
        private int difficultyLevel;
        private string[] motsATrouver;

        public string[] MotsATrouver
        {
            get { return motsATrouver; }
        }

        public Plateau(char[,] matrice, int difficultyLevel, string[] motsATrouver)
        {
            this.matrice = matrice;
            this.difficultyLevel = difficultyLevel;
            this.motsATrouver = motsATrouver;

        }

        public override string ToString()
        {
            string s1 = $"Niveau: {difficultyLevel}, Dimensions: {matrice.GetLength(0)}x{matrice.GetLength(1)} (LxC), Nombre de mots: {motsATrouver.Length}\n";

            foreach(string s in motsATrouver)
            {
                s1 += s + ", ";
            }
            s1 += "\n";

            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    s1 += matrice[i, j] + " ";
                }
                s1 += "\n";
            }

            return s1.TrimEnd('\n');
        }

        /// <summary>
        /// Lis un fichier plateau csv
        /// </summary>
        public void ToRead(string nomfile)
        {
            int[] firstLineParams = new int[4];


            using (var file = new StreamReader($"../../Plateaux/{nomfile}"))
            {
                string line;
                int index = 0;
                while ((line = file.ReadLine()) != null)
                {

                    //On s'assure du bon formattage du fichier CSV
                    line = line.Trim(';');
                    line += ';';

                    if (index == 0)
                    {
                        string s = "";

                        int i2 = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] != ';')
                            {
                                s += line[i];
                            }
                            else if (i2 < 4)
                            {
                                firstLineParams[i2] = Int32.Parse(s);

                                s = "";
                                i2++;
                            }
                        }

                        difficultyLevel = firstLineParams[0];
                        matrice = new char[firstLineParams[1], firstLineParams[2]];
                    }
                    else if (index == 1)
                    {
                        int wordNumber = firstLineParams[3];
                        string s = "";

                        motsATrouver = new string[wordNumber];

                        int i2 = 0;
                        for (int i = 0; i < line.Length; i++)
                        {
                            if (line[i] != ';')
                            {
                                s += line[i];
                            }
                            else
                            {
                                motsATrouver[i2] = s;
                                s = "";
                                i2++;
                            }
                        } 

                    }
                    else if(index <= firstLineParams[1] + 2)
                    {
                        int columnLength = firstLineParams[2];
                        string s = "";

                        int i2 = 0;
                        for(int i = 0; i < line.Length; i++)
                        {
                            if (line[i] != ';')
                            {
                                s += line[i];
                            }
                            else if (i2 < matrice.GetLength(1))
                            {
                                matrice[index - 2, i2] = s[0];
                                s = "";
                                i2++;
                            }
                        }
                    }

                    index++;
                }
            }
        }

        /// <summary>
        /// Enregristre un plateau dans un fichier csv
        /// </summary>
        public void ToFile(string nomfile)
        {
            string toWrite = $"{difficultyLevel};{matrice.GetLength(0)};{matrice.GetLength(1)};{motsATrouver.Length}\n";
            foreach(string s in motsATrouver)
            {
                toWrite += s + ';';
            }
            toWrite += '\n';
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for(int j = 0; j < matrice.GetLength(1); j++)
                {
                    toWrite += matrice[i, j].ToString() + ';';
                }
                toWrite += '\n';
            }
            toWrite.TrimEnd('\n');

            using (StreamWriter f = new StreamWriter(nomfile))
            {
                f.Write(toWrite);
            }
                //File.WriteAllText(nomfile, toWrite);
        }

        /// <summary>
        /// Cette méthode est appellée pour vérifier si un mot est contenu à la position passée en paramètre
        /// </summary>
        public bool Test_Plateau(string mot, int ligne, int colonne, string direction)
        {
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
                default:
                    Console.WriteLine("La direction ne fait pas partie des directions valides.");
                    return false;
            }



            int i = 0;
            bool doMatch = true;
            while(i < mot.Length && doMatch)
            {
                if(ligne >= matrice.GetLength(0) && colonne >= matrice.GetLength(1))
                {
                    Console.WriteLine("Le truc n'est pas dans le tableau");
                    return false;
                }

                if(mot[i] != matrice[ligne, colonne])
                {
                    doMatch = false;
                }

                if(ligne < matrice.GetLength(0) && movX > 0)
                {
                    ligne += movX;
                }
                else if (ligne > 0  && movX < 0)
                {
                    ligne += movX;
                }

                if (colonne < matrice.GetLength(1) && movY > 0)
                {
                    colonne += movY;
                }
                else if (colonne > 0 && movY < 0)
                {
                    colonne += movY;
                }

                i++;
            }

            return doMatch;
        }

        /// <summary>
        /// Affiche le plateau passé en paramètres dans la console, statique
        /// </summary>
        public static void AfficherPlateau(char[,] matrice)
        {
            for(int i = 0; i < matrice.GetLength(0); i++)
            {
                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write(matrice[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Afficher le plateau, méthode d'instance
        /// </summary>
        public void AfficherPlateau()
        {
            Console.Clear();

            Console.WriteLine("Entrez vos réponses sous la forme: MOT DIRECTION LIGNE COLONNE\n");

            string s1 = "";
            foreach (string s in motsATrouver)
            {
                s1 += s + ", ";
            }
            Console.WriteLine(s1);
            Console.Write("---");
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                Console.Write("----");
            }
            Console.WriteLine(" ");

            Console.Write("  | ");
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                Console.Write(i + 1);
                Console.Write(i < 9 ? " | " : "| ");
            }
            Console.WriteLine();

            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                Console.Write(i + 1);
                Console.Write(i < 9 ? " | " : "| ");

                for (int j = 0; j < matrice.GetLength(1); j++)
                {
                    Console.Write(matrice[i, j] + " | ");
                }
                Console.WriteLine();
            }

            Console.Write("---");
            for (int i = 0; i < matrice.GetLength(0); i++)
            {
                Console.Write("----");
            }
            Console.WriteLine(" ");
        }
    }
}
