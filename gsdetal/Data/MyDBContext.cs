using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Models;
using Microsoft.EntityFrameworkCore;

namespace gsdetal.DBViewModel
{
    public class MyDBContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<Itemdetail> Itemdetails { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=gsdetal.db");
        }
    }
}
