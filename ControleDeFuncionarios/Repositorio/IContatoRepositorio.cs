using ControleDeFuncionarios.Models;

namespace ControleDeFuncionarios.Repositorio
{
    public interface IContatoRepositorio
    {
        List<FuncionarioModel> BuscarTodos(int usuarioId);

        FuncionarioModel ListarPorId(int id);

        FuncionarioModel Adicionar(FuncionarioModel contato);

        FuncionarioModel Atualizar(FuncionarioModel contato);

        List<FuncionariosPorSetorViewModel> ObterFuncionariosPorSetor(); // Adicionar este método

        bool Apagar(int id);
    }
}
