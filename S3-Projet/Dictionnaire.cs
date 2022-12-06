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

        public void ListFiller(int nbreDeLettres)
        {
            wordList = new List<string>();
            using (StreamReader file = new StreamReader("../../Dictionnaires/MotsFR.txt"))
            {
                string line;
                bool isNext = false;

                while ((line = file.ReadLine()) != null)
                {

                    if (isNext)
                    {
                        //Pas convaincu par cette liste
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
