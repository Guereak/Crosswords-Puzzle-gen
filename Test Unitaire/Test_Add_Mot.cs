using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3_Projet;
using System;

namespace Test_Unitaire
{
    [TestClass]
    public class Test_Add_Mot
    {
        [TestMethod]
        public void Test_1()
        {
            Joueur j = new Joueur("Max");
            j.motsTrouves = new string[] { "Alpha", "Bravo", "Charlie" };

            CollectionAssert.AreEqual(j.motsTrouves, new string[] { "Alpha", "Bravo", "Charlie" });
        }

        [TestMethod]
        public void Test_2()
        {
            Joueur j = new Joueur("Max");
            j.motsTrouves = new string[] { "Alpha", "Bravo", "Charlie" };

            j.Add_Mot("Echo");

            CollectionAssert.AreEqual(j.motsTrouves, new string[] { "Alpha", "Bravo", "Charlie", "Echo" });
        }

        [TestMethod]
        public void Test_3()
        {
            Joueur j = new Joueur("Max");
            j.motsTrouves = new string[] { "Alpha", "Bravo", "Charlie" };

            j.Add_Mot("Echo");
            j.Add_Mot("Delta");
            j.Add_Mot("Foxtrot");

            CollectionAssert.AreEqual(j.motsTrouves, new string[] { "Alpha", "Bravo", "Charlie", "Echo", "Delta", "Foxtrot" });
        }
    }
}
