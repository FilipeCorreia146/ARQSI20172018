using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semente.Models;

namespace SementeTests
{
    [TestClass]
    public class FarmacoTest
    {
        [TestMethod]
        public void HasId()
        {
            int id = 1;

            Farmaco f = new Farmaco();

            f.Id = id;

            Assert.IsNotNull(f.Id);

        }

        [TestMethod]
        public void HasName()
        {
            string name = "nome";

            Farmaco f = new Farmaco();

            f.Nome = name;

            Assert.IsNotNull(f.Nome);

        }
    }
}
