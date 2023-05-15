using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

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

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public int? ResponsavelId { get; set; }

        [ForeignKey(nameof(ResponsavelId))]
        [InverseProperty(nameof(Pessoa.Tarefas))]
        public virtual Pessoa Responsavel { get; set; }
    }
}