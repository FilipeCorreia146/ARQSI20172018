using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semente.Models;
using System;

namespace SementeTests
{
    [TestClass]
    public class ReceitaTest
    {
        [TestMethod]
        public void HasId()
        {
            int id = 1;

            Receita f = new Receita();

            f.Id = id;

            Assert.IsNotNull(f.Id);

        }

        [TestMethod]
        public void HasData()
        {
            DateTime data = DateTime.Today;

            Receita f = new Receita();

            f.Data = data;

            Assert.IsNotNull(f.Data);

        }
    }
}
