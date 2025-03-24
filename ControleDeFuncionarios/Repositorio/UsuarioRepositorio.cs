    using ControleDeFuncionarios.Data;
using ControleDeFuncionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFuncionarios.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;

        public UsuarioRepositorio(BancoContext bancoContext) 
        {
            this._bancoContext = bancoContext;
        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios
                .Include(x => x.Funcionarios)
                .ToList();
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.Email.ToUpper() == email.ToUpper() && x.Login.ToUpper() == login.ToUpper());
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();

            // gravar no banco de dadoss
            _bancoContext.Usuarios.Add(usuario);
            // commitar
            _bancoContext.SaveChanges();

            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            // procura o ID no banco
            UsuarioModel usuarioDB = ListarPorId(usuario.Id);
            
            if(usuarioDB == null) throw new System.Exception("Houve um erro na atualização do usuário!");

                usuarioDB.Name = usuario.Name;
                usuarioDB.Login = usuario.Login;
                usuarioDB.Email = usuario.Email;
                //usuarioDB.DataAtualizacao = usuario.DataAtualizacao;
                usuarioDB.Perfil = usuario.Perfil;
                usuarioDB.DataAtualizacao = DateTime.Now;

                _bancoContext.Usuarios.Update(usuarioDB);
                _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = ListarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null) throw new System.Exception("Houve um erro na atualização da senha, usuário não encontrado.");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) throw new System.Exception("Senha atual não confere.");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) throw new System.Exception("Nova senha deve ser diferente da senha atual.");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDB);
            _bancoContext.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDB = ListarPorId(id);

            if(usuarioDB == null) throw new System.Exception("Houve um erro ao efetuar a exclusão do usuário");
        
            _bancoContext.Usuarios.Remove(usuarioDB);
            _bancoContext.SaveChanges();
        
            return true;
        }
    }
}
