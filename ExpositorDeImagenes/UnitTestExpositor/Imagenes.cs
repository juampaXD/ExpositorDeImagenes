using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ExpositorDeImagenes;

namespace UnitTestExpositor
{
    [TestClass]
    public class Imagenes
    {
        FrmExpositor expositor;
        
        [TestInitialize]
        private void Iniciar()
        {
            expositor = new FrmExpositor();
        }
        [TestMethod]
        public void TestMethod1()
        {
            //Assert.IsTrue();
        }
    }
}