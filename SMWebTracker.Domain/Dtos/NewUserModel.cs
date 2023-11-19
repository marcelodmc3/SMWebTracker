using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMWebTracker.Domain.Dtos
{
    public class NewUserModel
    {
        [Required(ErrorMessage = "Nome do usuário é obrigatório.")]
        [MinLength(5, ErrorMessage = "Nome do usário precisa ter no mínimo 5 caracteres.")]
        [MaxLength(200, ErrorMessage = "Nome do usário precisa ter no máximo 200 caracteres.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Login é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Senha é obrigatória.")]
        [MinLength(8, ErrorMessage = "Senha precisa ter no mínimo 8 caracteres.")]
        [MaxLength(50, ErrorMessage = "Senha precisa ter no máximo 50 caracteres.")]
        public string Password { get; set; }

        public bool IsAdmin { get; set; }
    }
}
