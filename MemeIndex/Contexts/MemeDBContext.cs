using Microsoft.EntityFrameworkCore;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MemeIndex.Contexts
{
    public class MemeDBContext : DbContext
    {
        public DbSet<Meme> Memes { get; set; }
        public DbSet<MemeCategory> MemeCategorys { get; set; }

        public MemeDBContext()
        {

        }

        public MemeDBContext(DbContextOptions<MemeDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB; Initial Catalog=MemeDB");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }
    }
}
