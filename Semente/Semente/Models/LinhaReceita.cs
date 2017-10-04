using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semente.Models
{
    public class LinhaReceita
    {
        public long Id { get; set; }
        public Receita receita { get; set; }
        public DateTime dataValidade { get; set; }
        public int quantidade { get; set; }
    }
}
