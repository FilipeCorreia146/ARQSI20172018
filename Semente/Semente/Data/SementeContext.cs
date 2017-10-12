using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Semente.Models;

namespace Semente.Models
{
    public class SementeContext : IdentityDbContext<UserEntity>
    {
        public SementeContext (DbContextOptions<SementeContext> options)
            : base(options)
        {
        }

        public DbSet<Semente.Models.Medicamento> Medicamento { get; set; }

        public DbSet<Semente.Models.Farmaco> Farmaco { get; set; }

        public DbSet<Semente.Models.Apresentacao> Apresentacao { get; set; }

        public DbSet<Semente.Models.Posologia> Posologia { get; set; }
    }
}
