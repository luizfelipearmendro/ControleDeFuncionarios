using ControleDeFuncionarios.Filters;
using Microsoft.AspNetCore.Mvc;

namespace ControleDeFuncionarios.Controllers
{
    public class RestritoController : Controller
    {
        [PaginaParaUsuarioLogado]

        public IActionResult Index()
        {
            return View();
        }
    }
}