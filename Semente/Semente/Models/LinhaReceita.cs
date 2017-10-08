using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semente.Models
{
    public class LinhaReceita
    {
        public long Id { get; set; }
        public Receita Receita { get; set; }
        public DateTime DataValidade { get; set; }
        public int Quantidade { get; set; }
    }
}
