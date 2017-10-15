using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semente.Models;

namespace SementeTests
{
    [TestClass]
    public class MedicamentoTest
    {
        [TestMethod]
        public void HasName()
        {
            string nome = "clavamox";

            Medicamento m = new Medicamento();

            m.Nome = nome;

            Assert.IsNotNull(m.Nome);

        }


        [TestMethod]
        public void HasId()
        {
            long id = 1;

            Medicamento m = new Medicamento();

            m.Id = id;

            Assert.IsNotNull(m.Id);


        }

    }
}
