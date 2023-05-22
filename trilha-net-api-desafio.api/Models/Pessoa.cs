using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace TrilhaApiDesafio.Models
{
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        [JsonIgnore]
        [InverseProperty("Responsavel")]
        public virtual ICollection<Tarefa> Tarefas { get; set; }
    }
}