using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        protected readonly IConfiguration Configuration;

        public OrganizadorContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseInMemoryDatabase("TestDb");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarefa>(entity =>
            {
                entity.HasOne(d => d.Responsavel)
                    .WithMany(p => p.Tarefas)
                    .HasForeignKey(d => d.ResponsavelId)
                    .HasConstraintName("FK_Tarefa_TB_Grupos");
            });
        }
    }
}