using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semente.Models
{
    public class Posologia
    {
        public long Id { get; set; }
        public String Descricao { get; set; }
        public String Dose { get; set; }
        public long ApresentacaoId { get; set; }
        public Apresentacao Apresentacao { get; set; }
    }
}
