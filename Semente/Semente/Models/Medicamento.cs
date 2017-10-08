using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semente.Models
{
    public class Medicamento {
        public long Id { get; set; }
        public String Nome { get; set; }
        public int ApresentacaoId { get; set; }
        public Apresentacao Apresentacao { get; set; }
    }
}
