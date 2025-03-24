using System.ComponentModel.DataAnnotations;

namespace ControleDeFuncionarios.Models
{
    public class FuncionarioModel
    {
        public int Id { get; set; }

        //required: deixa o campo como obrigatorio para preencher.

        //[Required(ErrorMessage ="Digite o nome do contato!")]
        public string Nome { get; set; }

        //emailadress: verifica se o email é valido
        //[Required(ErrorMessage = "Digite o e-mail do contato!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; }

        //[Required(ErrorMessage = "Digite o celular do contato!")]
        [Phone(ErrorMessage = "O celular informado não é válido!")]
        public string Celular { get; set; }

        //[Required(ErrorMessage = "Digite o cargo do contato!")]
        public string Cargo { get; set; }

        //[Required(ErrorMessage = "Digite o setor do contato!")]
        public string Setor { get; set; }
        
        public int? UsuarioId { get; set; }

        public UsuarioModel? Usuario { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

    }
}
