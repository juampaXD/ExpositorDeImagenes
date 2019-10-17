using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpositorDeImagenes;
using System.Collections.Generic;

namespace TestExpositor
{
    [TestClass]
    public class UnitTest1
    {
        FrmExpositor e;
        List<bool> ListTest = new List<bool>();
        [TestInitialize]
        public void InicioP()
        {
            e = new FrmExpositor();
        }
        [TestMethod]
        public void TestEscogerNumeroDiferente()
        {
            int Tamaño = 5;

            for (int i = 0; i < Tamaño; i++)
            {
                if (i == 1) { ListTest.Add(true); } else { ListTest.Add(false); }
            }
            ListTest.Add(false);

            for (int i = 0; i < Tamaño; i++)
            {
                e.EscogerNumero(ListTest, ListTest.Count, true);
                Assert.AreNotEqual(1, e.N);
            }
        }
        [TestMethod]
        public void TestEscogerUltimo()
        {//o una posición en especifico

            for (int i = 0; i < 5; i++)
            {
                if (i == 5) { ListTest.Add(false); } else { ListTest.Add(true); }
            }

            ListTest.Add(false);
            e.EscogerNumero(ListTest, ListTest.Count, true);

            Assert.AreEqual(5, e.N);
        }
        [TestMethod]
        public void TestEscogerConSoloUnNumero()
        {
            ListTest.Add(false);

            e.EscogerNumero(ListTest, ListTest.Count, true);

            Assert.AreEqual(0, e.N);
        }
        [TestMethod]
        public void TestEscogerCon0Numeros()
        {
            e.EscogerNumero(ListTest, ListTest.Count, true);

            Assert.AreEqual(0, e.N);
        }
    }
}