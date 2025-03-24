    using ControleDeFuncionarios.Data;
using ControleDeFuncionarios.Models;
using Microsoft.EntityFrameworkCore;

namespace ControleDeFuncionarios.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoContext _bancoContext;

        public ContatoRepositorio(BancoContext bancoContext) 
        {
            this._bancoContext = bancoContext;
        }

        public FuncionarioModel ListarPorId(int id)
        {
            return _bancoContext.Funcionarios.FirstOrDefault(x => x.Id == id);
        }

        public List<FuncionarioModel> BuscarTodos(int usuarioId)
        {
            return _bancoContext.Funcionarios.Where(x => x.UsuarioId == usuarioId).ToList();
        }

        public FuncionarioModel Adicionar(FuncionarioModel contato)
        {
            contato.DataCadastro = DateTime.Now;

            // gravar no banco de dados
            _bancoContext.Funcionarios.Add(contato);
            // commitar
            _bancoContext.SaveChanges();

            return contato;
        }

        public FuncionarioModel Atualizar(FuncionarioModel contato)
        {
            // procura o ID no banco
            FuncionarioModel contatoDB = ListarPorId(contato.Id);
            
            if(contatoDB == null) throw new System.Exception("Houve um erro na atualização do contato!");

                contatoDB.Nome = contato.Nome;
                contatoDB.Email = contato.Email;
                contatoDB.Celular = contato.Celular;
                contatoDB.Cargo = contato.Cargo;
                contatoDB.Setor = contato.Setor;
                contatoDB.DataAtualizacao = DateTime.Now;

                _bancoContext.Funcionarios.Update(contatoDB);
                _bancoContext.SaveChanges();

            return contatoDB;
        }

        public bool Apagar(int id)
        {
            FuncionarioModel contadoDB = ListarPorId(id);

            if(contadoDB == null) throw new System.Exception("Houve um erro ao efetuar a exclusão do contato");
        
            _bancoContext.Funcionarios.Remove(contadoDB);
            _bancoContext.SaveChanges();
        
            return true;
        }

        public object BuscarTodos(object usuarioId)
        {
            throw new NotImplementedException();
        }

        public List<FuncionariosPorSetorViewModel> ObterFuncionariosPorSetor()
        {
            return _bancoContext.Funcionarios
                .GroupBy(f => f.Setor)
                .Select(g => new FuncionariosPorSetorViewModel
                {
                    Setor = g.Key,
                    Quantidade = g.Count()
                })
                .ToList();
        }
    }
}
