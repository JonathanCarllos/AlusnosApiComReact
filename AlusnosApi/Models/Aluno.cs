using System.ComponentModel.DataAnnotations;

namespace AlusnosApi.Models
{
    public class Aluno
    {
        public int AlunoId { get; set; }

        [Required]
        [StringLength(80)]
        public string? Nome { get; set; }

        [Required]
        [StringLength(100)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required]
        public int Idade { get; set; }
    }
}
