using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Libary.Core.Test
{
    [TestClass]
    public class TestCPFCNPJ
    {

        [TestMethod]
        public void ValidaCpfCnpj()
        {
            var cpfValido = ValidaCPFCNPJ.TaxIdValidation.ValidateTaxId("000.000.000-00");
            

        }
    }
}
