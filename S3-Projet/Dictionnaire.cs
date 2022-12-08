using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S3_Projet
{
    public class Dictionnaire
    {
        public List<string> wordList = new List<string>();
        public string fileName = "MotsFR.txt";


        public Dictionnaire(string langue)
        {
            if(langue == "EN")
            {
                fileName = "MotsEN.txt";
            }
        }

        /// <summary>
        /// Racherche dichotomique récursive / binary search
        /// </summary>
        private bool RechDichoRecursif(string mot, int bordureGauche, int bordureDroite)
        {

            int milieu = (bordureDroite + bordureGauche) / 2;
            if (wordList[milieu] != mot)
            {
                if (bordureDroite - bordureGauche <= 1)
                {
                    return false;
                }
                else
                {
                    if (string.Compare(wordList[milieu], mot) < 0)       //Si le mot est après
                    {
                        //Console.WriteLine(milieu);
                        return RechDichoRecursif(mot, milieu, bordureDroite);
                    }
                    else        //Si le mot est avant
                    {
                        //Console.WriteLine(milieu);
                        return RechDichoRecursif(mot, bordureGauche, milieu);
                    }
                }
            }
            else
            {
                return true;
            }


        }

        /// <summary>
        /// Remplissage de la liste avec les mots qui ont le nombre de lettres passés en paramètre
        /// </summary>
        public void ListFiller(int nbreDeLettres)
        {
            wordList = new List<string>();
            using (StreamReader file = new StreamReader($"../../Dictionnaires/{fileName}"))
            {
                string line;
                bool isNext = false;

                while ((line = file.ReadLine()) != null)
                {

                    if (isNext)
                    {
                        wordList.AddRange(line.Split(' '));
                        isNext = false;
                    }

                    if (line.Trim() == nbreDeLettres.ToString())
                    {
                        isNext = true;
                    }
                }
            }
        }

        /// <summary>
        /// Combine la recherche dichotomique et le remplissage pour une fonction de recherche complète
        /// </summary>
        public bool Search(string mot)
        {
            bool isEmpty = wordList.Count == 0 ? true : false;
            while (!isEmpty)
            {
                isEmpty = wordList.Count == 0 ? true : false;
            }

            ListFiller(mot.Length);
            return RechDichoRecursif(mot, 0, wordList.Count);
        }
    }
}
