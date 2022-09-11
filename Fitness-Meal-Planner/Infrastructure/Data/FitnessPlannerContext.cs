using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class FitnessPlannerContext : DbContext
    {
        public FitnessPlannerContext(DbContextOptions options) : base(options) { }
        public DbSet<Product> Products { get; set; }
    }
}
