using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpositorDeImagenes;
using System.Collections.Generic;

namespace TestExpositor
{
    [TestClass]
    public class PruebasUnitarias
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
        {//prueba unitaria para escoger números diferentes al que esta activo
            int Tamaño = 5;
            int num = 0;//número que no debe repetirse
            for (int i = 0; i < Tamaño; i++)
            {
                if (i == num) { ListTest.Add(true); } else { ListTest.Add(false); }
            }
            Assert.AreNotEqual(1, e.EscogerNumero(Tamaño, ListTest,true));
        }
        [TestMethod]
        public void TestEscogerUltimo()
        {
            for (int i = 0; i <= 5; i++)
            {
                if (i == 5) { ListTest.Add(false); } else { ListTest.Add(true); }
            }
            Assert.AreEqual(5, e.EscogerNumero(ListTest.Count, ListTest,true));
        }
        [TestMethod]
        public void TestEscogerConSoloElementoEnLaLista()
        {
            ListTest.Add(false);
            Assert.AreEqual(0, e.EscogerNumero(ListTest.Count, ListTest,true));
        }
        [TestMethod]
        public void TestEscogerConNingunElemento()
        {
            Assert.AreEqual(-1, e.EscogerNumero(ListTest.Count, ListTest,true));
        }
    }
}