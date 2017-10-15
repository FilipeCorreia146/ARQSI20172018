using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Semente.Models;

namespace Semente.DTOs
{
    public class PosologiaDto
    {
        public String Descricao { get; set; }
        public String Dose { get; set; }
        public Apresentacao Apresentacao { get; set; }
    }
}
