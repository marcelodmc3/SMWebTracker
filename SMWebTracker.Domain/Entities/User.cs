using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace SMWebTracker.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        [MinLength(5, ErrorMessage = "Nome do usário precisa ter no mínimo 5 caracteres.")]
        [MaxLength(200, ErrorMessage = "Nome do usário precisa ter no máximo 200 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login é obrigatório.")]
        [MinLength(5, ErrorMessage = "Login precisa ter no mínimo 5.")]
        [MaxLength(50, ErrorMessage = "Login precisa no máximo 50 caracteres.")]
        public string Login { get; set; }

        [Required]
        [MinLength(88)]
        [MaxLength(88)]
        [JsonIgnore]
        public string Hash { get; set; }

        [Required]
        [MinLength(44)]
        [MaxLength(44)]
        [JsonIgnore]
        public string Salt { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        [JsonIgnore]
        public bool Active { get; set; }
    }
}
