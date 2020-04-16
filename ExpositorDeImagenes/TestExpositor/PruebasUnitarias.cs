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
        {//prueba unitaria para escoger números diferentes a los activos
            int Tamaño = 5;
            int num = 0;//número que no debe repetirse
            for (int i = 0; i <= Tamaño; i++)
            {
                if (i == num) { ListTest.Add(true); } else { ListTest.Add(false); }
            }

            for (int i = 0; i < Tamaño; i++)
            {
                num = e.EscogerNumero(Tamaño, ListTest);
            }
            Assert.AreNotEqual(1, num);
        }
        [TestMethod]
        public void TestEscogerUltimo()
        {
            for (int i = 0; i <= 5; i++)
            {
                if (i == 5) { ListTest.Add(false); } else { ListTest.Add(true); }
            }
            Assert.AreEqual(5, e.EscogerNumero(ListTest.Count, ListTest));
        }
        [TestMethod]
        public void TestEscogerConSoloElementoEnLaLista()
        {
            ListTest.Add(false);
            e.EscogerNumero(ListTest.Count,ListTest);
            Assert.AreEqual(0, 0);
        }
        [TestMethod]
        public void TestEscogerConNingunElemento()
        {
            e.EscogerNumero(ListTest.Count,ListTest);
            Assert.AreEqual(-1, -1);
        }
    }
}