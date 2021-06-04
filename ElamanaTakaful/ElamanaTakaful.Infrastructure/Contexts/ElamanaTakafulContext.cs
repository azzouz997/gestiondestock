using ElamanaTakaful.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElamanaTakaful.Infrastructure.Contexts
{
    public class ElamanaTakafulContext : DbContext
    {
        public ElamanaTakafulContext(DbContextOptions<ElamanaTakafulContext> options): base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Function> Functions { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Proposition> Propositions { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder
                .Entity<Role>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder
                .Entity<Proposition>()
                .HasOne(e => e.Product)
                .WithMany(e => e.Propositions)
                .OnDelete(DeleteBehavior.ClientCascade);



        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(@"Data Source=161.97.173.185;Initial Catalog=ElamanaTakafulBD;User ID=SA;Password=ElamanaTakaful(123); Persist Security Info = True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
 

    }
}
