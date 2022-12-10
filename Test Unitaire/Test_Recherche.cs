using Microsoft.VisualStudio.TestTools.UnitTesting;
using S3_Projet;
using System;

namespace Test_Unitaire
{
    [TestClass]
    public class Test_Recherche
    {
        [TestMethod]
        public void Test_1()
        {
            Dictionnaire d = new Dictionnaire("FR");

            bool b = d.Search("RECHERCHE");

            Assert.AreEqual(b, true);
        }

        [TestMethod]
        public void Test_2()
        {
            Dictionnaire d = new Dictionnaire("EN");

            bool b = d.Search("THEORIQUE");

            Assert.AreEqual(b, false);
        }

        [TestMethod]
        public void Test_3()
        {
            Dictionnaire d = new Dictionnaire("EN");

            bool b = d.Search("INDEED");

            Assert.AreEqual(b, true);
        }

        [TestMethod]
        public void Test_4()
        {
            Dictionnaire d = new Dictionnaire("FR");

            bool b = d.Search("OKLMZER");

            Assert.AreEqual(b, false);
        }
    }
}
