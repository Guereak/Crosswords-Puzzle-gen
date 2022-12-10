using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3_Projet;
using System;

namespace Test_Unitaire
{
    [TestClass]
    public class Test_Add_Score
    {
        [TestMethod]
        public void Test_1()
        {
            Joueur j = new Joueur("Elyess");
            j.Add_Score(5);
            Assert.AreEqual(5, j.Score);
        }
        [TestMethod]
        public void Test_2()
        {
            Joueur j = new Joueur("Elyess");
            Assert.AreEqual(0, j.Score);
        }
        [TestMethod]
        public void Test_3()
        {
            Joueur j = new Joueur("Elyess");
            j.Add_Score(-100);
            Assert.AreEqual(-100, j.Score);
        }
    }
}
