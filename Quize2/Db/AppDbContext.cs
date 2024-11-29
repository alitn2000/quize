using Microsoft.EntityFrameworkCore;
using Quize2.Entites;
using Quize2.InferaStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quize2.Db;

public class AppDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-URA992G\SQLEXPRESS; Initial Catalog=BankManager; Integrated Security=true ;TrustServerCertificate=True;");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new TransactionConfig());
        base.OnModelCreating(modelBuilder);
    }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
