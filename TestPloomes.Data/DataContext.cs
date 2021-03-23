using System;
using Microsoft.EntityFrameworkCore;
using TestPloomes.Core.Business;

namespace TestPloomes.Data
{
  public class DataContext : DbContext
  {
    private readonly string _connectionString;
    public DataContext(string connectionString)
    {
      _connectionString = connectionString;

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer(_connectionString);
      }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

      builder.Entity<User>().ToTable("Users");
      builder.Entity<HQ>().ToTable("HQ");
      
      builder.Entity<User>().HasKey(x => x.Id);
      builder.Entity<HQ>().HasKey(x => x.Id);
      

      base.OnModelCreating(builder);
    }
  }
}