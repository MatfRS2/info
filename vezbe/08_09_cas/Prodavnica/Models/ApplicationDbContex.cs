using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Prodavnica.Models
{
    public class ApplicationDbContex : DbContext
    {
        public ApplicationDbContex(DbContextOptions<ApplicationDbContex> opts)
            : base(opts)
        { }

        public DbSet<Proizvod> Proizvodi { get; set; }
        public DbSet<Porudzbina> Porudzbine { get; set; }
    }
}