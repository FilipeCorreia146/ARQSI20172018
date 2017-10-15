using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semente.Models;
using System;

namespace SementeTests
{
    [TestClass]
    public class linhReceitaTest
    {
        [TestMethod]
        public void HasId()
        {
            int id = 1;

            LinhaReceita f = new LinhaReceita();

            f.Id = id;

            Assert.IsNotNull(f.Id);

        }

        [TestMethod]
        public void HasDataValidade()
        {
            DateTime data = DateTime.Today;

            LinhaReceita f = new LinhaReceita();

            f.DataValidade = data;

            Assert.IsNotNull(f.DataValidade);

        }

        [TestMethod]
        public void HasReceita()
        {
            DateTime d = DateTime.Today;
            int id = 1;
            Receita r = new Receita();
            r.Data = d;
            r.Id = id;

            LinhaReceita lr = new LinhaReceita();

            lr.ReceitaId = id;
            lr.Receita = r;

            Assert.IsNotNull(lr.ReceitaId);

            Assert.AreEqual(lr.ReceitaId, r.Id);

        }

        [TestMethod]
        public void HasQuantidade()
        {
            int quantidade = 10;

            LinhaReceita lr = new LinhaReceita();

            lr.Quantidade = quantidade;

            Assert.IsNotNull(lr.Quantidade);
        }
    }
}
