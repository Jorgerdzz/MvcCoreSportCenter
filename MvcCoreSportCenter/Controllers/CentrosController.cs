using Microsoft.AspNetCore.Mvc;
using MvcCoreSportCenter.Models;
using MvcCoreSportCenter.Repositories;

namespace MvcCoreSportCenter.Controllers
{
    public class CentrosController : Controller
    {
        private RepositorySportCenter repo;

        public CentrosController()
        {
            this.repo = new RepositorySportCenter();
        }

        public IActionResult Index()
        {
            List<Centro> centros = this.repo.GetCentros();
            ViewData["TiposCentro"] = this.repo.GetTiposCentro();
            return View(centros);
        }

        [HttpPost]
        public IActionResult Index(string tipocentro)
        {
            List<Centro> centros = this.repo.GetCentrosPorTipo(tipocentro);
            ViewData["TiposCentro"] = this.repo.GetTiposCentro();
            return View(centros);
        }

    }
}
