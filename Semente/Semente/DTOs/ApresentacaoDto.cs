using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Semente.Models;

namespace Semente.DTOs
{
    public class ApresentacaoDto
    {
        public long Id { get; set; }
        public String Descricao { get; set; }
        public String Forma { get; set; }
        public String Concentracao { get; set; }
        public String Qtd { get; set; }
        public long MedicamentoId { get; set; }
        //public Medicamento Medicamento { get; set; }
        public int FarmacoId { get; set; }
        //public Farmaco Farmaco { get; set; }
    }
}
