using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace Semente.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SementeContext(
                serviceProvider.GetRequiredService<DbContextOptions<SementeContext>>()))
            {
                // Look for any medicamentos.
                if (context.Medicamento.Any())
                {
                    return;   // DB has been seeded
                }

                context.Medicamento.AddRange(
                     new Medicamento
                     {
                         Nome = "Brufen"
                     },

                     new Medicamento
                     {
                         Nome = "Trifene"
                     },

                     new Medicamento
                     {
                         Nome = "Ben-u-ron"
                     },

                     new Medicamento
                     {
                         Nome = "Ib-u-ron"
                     },

                     new Medicamento
                     {
                         Nome = "Paracetamol"
                     }
                );

                // Look for any farmacos.
                if (context.Farmaco.Any())
                {
                    return;   // DB has been seeded
                }

                context.Farmaco.AddRange(
                     new Farmaco
                     {
                         Nome = "Ibuprofeno"
                     },

                     new Farmaco
                     {
                         Nome = "Paracetamol"
                     }
                );

                // Look for any apresentacoes.
                if (context.Apresentacao.Any())
                {
                    return;   // DB has been seeded
                }

                context.Apresentacao.AddRange(
                     new Apresentacao
                     {
                         Descricao = "Brufen 600mg comprimidos revestidos por pelicula",
                         Forma = "comprimidos",
                         Concentracao = "600 mg",
                         Qtd = "20 comprimidos",
                         MedicamentoId = 1,
                         FarmacoId = 1
                     },

                     new Apresentacao
                     {
                         Descricao = "Brufen 400mg comprimidos revestidos por pelicula",
                         Forma = "comprimidos",
                         Concentracao = "400 mg",
                         Qtd = "60 comprimidos",
                         MedicamentoId = 1,
                         FarmacoId = 1
                     },

                     new Apresentacao
                     {
                         Descricao = "Brufen Sem Acucar 40 mg/ml suspensao oral",
                         Forma = "xarope",
                         Concentracao = "40 mg/ml",
                         Qtd = "100 ml",
                         MedicamentoId = 1,
                         FarmacoId = 1
                     }
                );

                // Look for any posologias.
                if (context.Posologia.Any())
                {
                    return;   // DB has been seeded
                }

                context.Posologia.AddRange(
                     new Posologia
                     {
                         Descricao = "Adulto e criancas com idade superior a 12 anos",
                         Dose = "2 comprimidos/dia com um intervalo de 12 horas",
                         ApresentacaoId = 1
                     },

                     new Posologia
                     {
                         Descricao = "Adulto e criancas com idade superior a 12 anos",
                         Dose = "2 a 3 comprimidos/dia com um intervalo de 8 horas",
                         ApresentacaoId = 2
                     },

                     new Posologia
                     {
                         Descricao = "Criancas com idade inferior a 12 anos",
                         Dose = "40 mg/kg/dia",
                         ApresentacaoId = 1
                     },

                     new Posologia
                     {
                         Descricao = "Criancas com idade inferior a 12 anos",
                         Dose = "40 mg/kg/dia",
                         ApresentacaoId = 2
                     },

                     new Posologia
                     {
                         Descricao = "Bebes/Criancas 1-3 anos",
                         Dose = "7,5 ml de suspensao/dia",
                         ApresentacaoId = 3
                     },

                     new Posologia
                     {
                         Descricao = "Criancas 4-5 anos",
                         Dose = "11,25 ml de suspensao/dia",
                         ApresentacaoId = 3
                     },

                     new Posologia
                     {
                         Descricao = "Criancas 6-9 anos",
                         Dose = "15 ml de suspensao/dia",
                         ApresentacaoId = 3
                     },

                     new Posologia
                     {
                         Descricao = "Criancas 10-11 anos",
                         Dose = "20 ml de suspensao/dia",
                         ApresentacaoId = 3
                     },

                     new Posologia
                     {
                         Descricao = "Adolescentes >= 12 anos e adultos",
                         Dose = "30 ml de suspensao/dia",
                         ApresentacaoId = 3
                     }
                );

                context.SaveChanges();
            }
        }
        //    public static void Initialize(SementeContext context)
        //    {
        //        context.Database.EnsureCreated();

        //        if (!context.Medicamento.Any() && !context.Farmaco.Any())
        //        {

        //            Medicamento m1 = new Medicamento();
        //            m1.Nome = "Brufen";
        //            Medicamento m2 = new Medicamento();
        //            m2.Nome = "Trifene";
        //            Medicamento m3 = new Medicamento();
        //            m3.Nome = "Ben-u-ron";
        //            Medicamento m4 = new Medicamento();
        //            m4.Nome = "Ib-u-ron";
        //            Medicamento m5 = new Medicamento();
        //            m5.Nome = "Paracetamol";

        //            context.Add(m1);
        //            context.Add(m2);
        //            context.Add(m3);
        //            context.Add(m4);
        //            context.Add(m5);

        //            Farmaco f1 = new Farmaco();
        //            f1.Nome = "Ibuprofeno";
        //            Farmaco f2 = new Farmaco();
        //            f2.Nome = "Paracetamol";

        //            context.Add(f1);
        //            context.Add(f2);

        //            Apresentacao a1 = new Apresentacao();
        //            a1.Descricao = "Brufen 600mg comprimidos revestidos por pelicula";
        //            a1.Forma = "comprimidos";
        //            a1.Concentracao = "600 mg";
        //            a1.Qtd = "20 comprimidos";
        //            a1.MedicamentoId = 1;
        //            a1.FarmacoId = 1;
        //            Apresentacao a2 = new Apresentacao();
        //            a2.Descricao = "Brufen 400mg comprimidos revestidos por pelicula";
        //            a2.Forma = "comprimidos";
        //            a2.Concentracao = "400 mg";
        //            a2.Qtd = "60 comprimidos";
        //            a2.MedicamentoId = 1;
        //            a2.FarmacoId = 1;
        //            Apresentacao a3 = new Apresentacao();
        //            a3.Descricao = "Brufen Sem Acucar 40 mg/ml suspensao oral";
        //            a3.Forma = "xarope";
        //            a3.Concentracao = "40 mg/ml";
        //            a3.Qtd = "100 ml";
        //            a3.MedicamentoId = 1;
        //            a3.FarmacoId = 1;

        //            context.Add(a1);
        //            context.Add(a2);
        //            context.Add(a3);
        //        }

        //        context.SaveChanges();

        //    }
    }
}
