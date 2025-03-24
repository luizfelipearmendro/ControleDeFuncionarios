using ControleDeFuncionarios.Data;
using ControleDeFuncionarios.Filters;
using ControleDeFuncionarios.Helper;
using ControleDeFuncionarios.Models;
using ControleDeFuncionarios.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.Identity.Client;
using System.Diagnostics.Metrics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Http;

namespace ControleDeFuncionarios.Controllers
{
    [PaginaParaUsuarioLogado]
    public class FuncionarioController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;


        public FuncionarioController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
            List<FuncionarioModel> contatos = _contatoRepositorio.BuscarTodos(usuarioLogado.Id);

            return View(contatos);
        }

        public IActionResult Grafico()
        {
            var funcionariosPorSetor = _contatoRepositorio.ObterFuncionariosPorSetor();
            ViewBag.FuncionariosPorSetor = funcionariosPorSetor;

            return View();
        }

        public IActionResult Criar()
        {
            return View();
        }

        public IActionResult Editar(int id)
        {
            FuncionarioModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacaoContato(int id)
        {
            FuncionarioModel contato = _contatoRepositorio.ListarPorId(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);

                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Funcionário apagado com sucesso!";
                }
                else
                {
                    TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu funcionário.";
                }
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar seu funcionário, tente novamente, detalhes do erro: {erro.Message}";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Criar(FuncionarioModel contato)
        {
            try
            {
                //verifica se o contato cadastrado é valido
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Funcionário cadastrado com sucesso!";

                    //por fim, precisa retornar algo..
                    return RedirectToAction("Index", "Funcionario");
                }

                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar seu funcionário, tente novamente, detalhes do erro: {erro.Message}";

                //por fim, precisa retornar algo..
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult Editar(FuncionarioModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoDoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Funcionário editado com sucesso!";

                    //por fim, precisa retornar algo..
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos editar seu funcionário, tente novamente, detalhes do erro: {erro.Message}";
                return RedirectToAction("Index"); //vai cair na view editar
            }

        }
    }
}
