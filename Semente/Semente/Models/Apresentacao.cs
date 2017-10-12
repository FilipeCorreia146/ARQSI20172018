using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semente.Models
{
    public class Apresentacao
    {
        public long Id { get; set; }
        public String Descricao { get; set; }
        public String Forma { get; set; }
        public String Concentracao { get; set; }
        public String Qtd { get; set; }
        public int MedicamentoId { get; set; }
        public Medicamento Medicamento { get; set; }
        public int FarmacoId { get; set; }
        public Farmaco Farmaco { get; set; }
    }
}
