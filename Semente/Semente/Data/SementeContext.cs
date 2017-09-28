using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Semente.Models;

namespace Semente.Models
{
    public class SementeContext : DbContext
    {
        public SementeContext (DbContextOptions<SementeContext> options)
            : base(options)
        {
        }

        public DbSet<Semente.Models.Medicamento> Medicamento { get; set; }

        public DbSet<Semente.Models.Farmaco> Farmaco { get; set; }
    }
}
