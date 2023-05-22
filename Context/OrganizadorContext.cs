using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Context
{
    public class OrganizadorContext : DbContext
    {
        public DbSet<Tarefa> Tarefas { get; set; }
        public DbSet<Pessoa> Pessoas { get; set; }

        public OrganizadorContext(DbContextOptions options) : base(options)
        {
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