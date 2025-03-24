using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ControleDeFuncionarios.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        public string SenhaAtual { get; set; }

        public string NovaSenha { get; set; }

        [Compare("NovaSenha",ErrorMessage ="Senha não confere com a nova senha.")]
        public string ConfirmarNovaSenha { get; set; }
    }
}
