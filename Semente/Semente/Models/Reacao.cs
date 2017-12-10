using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Semente.Models
{
    public class Reacao
    {
        public int Id { get; set; }
        public String Descricao { get; set; }
        public int FarmacoId { get; set; }
        public Farmaco Farmaco { get; set; }
    }
}
