using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using ControleDeFuncionarios.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ControleDeFuncionarios.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        public string Login { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

    }
}