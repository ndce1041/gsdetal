using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gsdetal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Serilog;

namespace gsdetal.DBViewModel
{
    public class MyDBContext : DbContext
    {
        public DbSet<Url> Urls { get; set; }
        public DbSet<Itemdetail> Itemdetails { get; set; }
        public DbSet<Thumbnail> Thumbnails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\xjh\\source\\repos\\gsdetal\\gsdetal\\gsdetal.db");

            // 配置日志记录
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.File("logs\\efcore_log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder
                    .AddSerilog(dispose: true);
            });

            optionsBuilder.UseLoggerFactory(loggerFactory);



        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Url>().ToTable("Urls");
            modelBuilder.Entity<Itemdetail>().ToTable("Itemdetails");
            modelBuilder.Entity<Thumbnail>().ToTable("Thumbnails");
        }
    }
}
