// <copyright file="PruebasUnitariasTest.cs">Copyright ©  2019</copyright>

using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TestExpositor;

namespace TestExpositor.Tests
{
    [TestClass]
    [PexClass(typeof(PruebasUnitarias))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    public partial class PruebasUnitariasTest
    {

        [PexMethod(MaxBranches = 20000)]
        public void TestEscogerNumeroDiferente([PexAssumeUnderTest]PruebasUnitarias target)
        {
            target.TestEscogerNumeroDiferente();
            // TODO: agregar aserciones a método PruebasUnitariasTest.TestEscogerNumeroDiferente(PruebasUnitarias)
        }
    }
}
