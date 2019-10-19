using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrzavaNaselje.Models
{
    public class DrzavaContext : DbContext
    {
        public DrzavaContext(DbContextOptions<DrzavaContext> options)
            : base(options)
        {
        }
        public DrzavaContext()
        {

        }
        public DbSet<Drzava> Drzave { get; set; }
        public DbSet<Naselje> Naselja { get; set; }
    }
}
