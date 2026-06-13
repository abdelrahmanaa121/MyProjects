using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWalletSystem.Models
{
    internal class AppDbContext : DbContext
    {
        //Define Tables Created in DB
        public DbSet<User> users { get; set; }
        public DbSet<Wallet> wallets { get; set; }
        //connection and Configuring
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source=.; initial catalog=MiniWalletDB; integrated security=true; TrustServerCertificate=true");
        }
    }
}
