using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using TrilhaApiDesafio.Common.Enums;

namespace TrilhaApiDesafio.Models
{
    public class Tarefa
    {
        [Key]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
        public int? Responsavel { get; set; }

        [ForeignKey(nameof(Responsavel))]
        [InverseProperty(nameof(Pessoa.Tarefas))]
        public virtual Pessoa ResponsavelNavigation { get; set; }
    }
}