using ControleDeFuncionarios.Filters;
using ControleDeFuncionarios.Models;
using ControleDeFuncionarios.Repositorio;
using ControleDeFuncionarios.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;

namespace ControleDeFuncionarios.Controllers
{
    public class HomeController : Controller
    {
        [PaginaParaUsuarioLogado]

        /*private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }*/

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}