using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3_Projet;
using System;

namespace Test_Unitaire
{
    [TestClass]
    public class Test_Dico_Recherche
    {
        [TestMethod]
        public void Test_1()
        {
            Dictionnaire d = new Dictionnaire("FR");

            Assert.AreEqual(d.Search("AVION"), true);
        }

        [TestMethod]
        public void Test_2()
        {
            Dictionnaire d = new Dictionnaire("FR");

            Assert.AreEqual(d.Search("ZIZO"), false);
        }

        [TestMethod]
        public void Test_3()
        {
            Dictionnaire d = new Dictionnaire("FR");

            Assert.AreEqual(d.Search("AVIRON"), true);
        }

        [TestMethod]
        public void Test_4()
        {
            Dictionnaire d = new Dictionnaire("FR");

            Assert.AreEqual(d.Search("PLUSIEURSMOTS"), false);
        }

        [TestMethod]
        public void Test_5()
        {
            Dictionnaire d = new Dictionnaire("FR");

            Assert.AreEqual(d.Search("BATEAU"), true);
        }

        [TestMethod]
        public void Test_6()
        {
            Dictionnaire d = new Dictionnaire("FR");

            Assert.AreEqual(d.Search("15€4r4€\\r"), false);
        }
    }
}
