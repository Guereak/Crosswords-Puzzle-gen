using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3_Projet;
using System;

namespace Test_Unitaire
{
    [TestClass]
    public class Test_To_String
    {
        [TestMethod]
        public void Test_1()
        {
            char[,] matrice = new char[,] { { 'A', 'B' }, { 'C', 'D' }, { 'E', 'F' }, { 'G', 'H' } };
            string[] aTrouver = new string[] { "BONJOUR", "AU", "REVOIR" };
            int difficulty = 2;

            Plateau p = new Plateau(matrice, difficulty, aTrouver);

            string expectedOutput = "Niveau: 2, Dimensions: 4x2 (LxC), Nombre de mots: 3\nBONJOUR, AU, REVOIR, \nA B \nC D \nE F \nG H ";
            Assert.AreEqual(p.ToString(), expectedOutput);
        }   
    }
}
