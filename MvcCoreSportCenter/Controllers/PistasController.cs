using Microsoft.AspNetCore.Mvc;
using MvcCoreSportCenter.Models;
using MvcCoreSportCenter.Repositories;

namespace MvcCoreSportCenter.Controllers
{
    public class PistasController : Controller
    {
        private RepositoryPistas repo;

        public PistasController()
        {
            this.repo = new RepositoryPistas();
        }

        public IActionResult Index(int id)
        {
            DatosCentro dc = this.repo.GetDatosCentro(id);
            return View(dc);
        }
    }
}
