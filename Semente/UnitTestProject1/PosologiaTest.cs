using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semente.Models;

namespace SementeTests
{
    [TestClass]
    public class PosologiaTest
    {
        [TestMethod]
        public void HasId()
        {
            int id = 1;

            Posologia f = new Posologia();

            f.Id = id;

            Assert.IsNotNull(f.Id);

        }

        [TestMethod]
        public void HasDescricao()
        {
            string desc = "desc";

            Posologia p = new Posologia();
            p.Descricao= desc;

            Assert.IsNotNull(p.Descricao);

        }

        [TestMethod]
        public void HasDose()
        {
            string dose = "dose";

            Posologia p = new Posologia();
            p.Dose = dose;

            Assert.IsNotNull(p.Dose);

        }

        [TestMethod]
        public void HasApresentacao()
        {
            int id = 1;
           
            Apresentacao a = new Apresentacao();

            a.Id = id;

            Posologia p = new Posologia();

            p.ApresentacaoId = id;

            p.Apresentacao = a;

            Assert.IsNotNull(p.ApresentacaoId);

            Assert.IsNotNull(p.Apresentacao);

            Assert.AreEqual(p.Apresentacao.Id,a.Id);

        }

    }
}
