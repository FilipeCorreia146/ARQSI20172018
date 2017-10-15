using Microsoft.VisualStudio.TestTools.UnitTesting;
using Semente.Models;

namespace SementeTests
{
    [TestClass]
    public class ApresentacaoTest
    {
        [TestMethod]
        public void HasId()
        {

            long id = 1;

            Apresentacao a = new Apresentacao();

            a.Id = id;

            Assert.IsNotNull(a.Id);

        }

        [TestMethod]
        public void HasDescricao()
        {
            string descricao = "Descrição1";

            Apresentacao a = new Apresentacao();

            a.Descricao = descricao;

            Assert.IsNotNull(a.Descricao);

        }

        [TestMethod]
        public void HasForma()
        {
            string forma = "xarope";

            Apresentacao a = new Apresentacao();

            a.Forma = forma;

            Assert.IsNotNull(a.Forma);
        }

        [TestMethod]
        public void HasConcetracao()
        {
            string concentracao = "100";

            Apresentacao a = new Apresentacao();

            a.Concentracao = concentracao;

            Assert.IsNotNull(a.Concentracao);
        }


        [TestMethod]
        public void Hasqtd()
        {
            string qtd = "10";

            Apresentacao a = new Apresentacao();

            a.Qtd = qtd;

            Assert.IsNotNull(a.Qtd);

        }

        [TestMethod]
        public void HasMedicamento()
        {
            long id = 1;
            string nome = "n";
            Medicamento m = new Medicamento();

            m.Id = id;

            m.Nome = nome;

            Apresentacao a = new Apresentacao();

            a.Medicamento = m;

            a.MedicamentoId = id;

            Assert.IsNotNull(a.Medicamento);

            Assert.AreEqual(a.MedicamentoId,a.Medicamento.Id);

        }

        [TestMethod]
        public void HasFarmaco()
        {
            int id = 1;
            string nome = "n";
            Farmaco f = new Farmaco();

            f.Id = id;

            f.Nome = nome;

            Apresentacao a = new Apresentacao();

            a.Farmaco = f;

            a.FarmacoId = id;

            Assert.IsNotNull(a.Farmaco);

            Assert.AreEqual(a.FarmacoId, a.Farmaco.Id);

        }

    }
}
