using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using ControleDeFuncionarios.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using ControleDeFuncionarios.Helper;

namespace ControleDeFuncionarios.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        public string Login { get; set; }

        [Required(ErrorMessage = "Informe o perfil do usuário")]
        public PerfilEnum? Perfil { get; set; }

        public string Senha { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public virtual List<FuncionarioModel>? Funcionarios { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0,8);
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}