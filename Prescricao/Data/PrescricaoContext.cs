using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Prescricao.Models
{
    public class PrescricaoContext : DbContext
    {
        public PrescricaoContext (DbContextOptions<PrescricaoContext> options)
            : base(options)
        {
        }

        public DbSet<Prescricao.Models.Medicamento> Medicamento { get; set; }
    }
}
