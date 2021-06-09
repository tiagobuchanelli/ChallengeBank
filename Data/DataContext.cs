using System;
using FuncionalHealthChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace FuncionalHealthChallenge.Data
{

    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ContaCorrente> ContasCorrentes { get; set; }

        public DbSet<OperacoesFinanceirasContaCorrente> OperacoesFinanceirasContasCorrentes { get; set; }

    }

}
