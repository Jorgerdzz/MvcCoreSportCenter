using Microsoft.AspNetCore.Mvc;
using MvcCoreSportCenter.Models;
using MvcCoreSportCenter.Repositories;

namespace MvcCoreSportCenter.Controllers
{
    public class PistasController : Controller
    {
        private RepositoryPistas repo;
        private RepositorySportCenter repoCentros;

        public PistasController()
        {
            this.repo = new RepositoryPistas();
            this.repoCentros = new RepositorySportCenter();
        }

        public IActionResult Index(int id)
        {
            DatosCentro dc = this.repo.GetDatosCentro(id);
            return View(dc);
        }

        public IActionResult Insert()
        {
            ViewData["CENTROS"] = this.repoCentros.GetCentrosActivos();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Insert(int idCentro, string nombrePista, double precioHora, bool esTechada)
        {
            await this.repo.InsertPista(idCentro, nombrePista, precioHora, esTechada);
            ViewData["CENTROS"] = this.repoCentros.GetCentrosActivos();
            return RedirectToAction("Index", new {id = idCentro});
            //return RedirectToAction("Index", "Centros");
        }

        public async Task<IActionResult> Update(int idPista)
        {
            Pista p = this.repo.FindPistaById(idPista);
            ViewData["CENTROS"] = this.repoCentros.GetCentrosActivos();
            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int idPista, int idCentro, string nombrePista, double precioHora, bool esTechada)
        {
            await this.repo.UpdatePista(idPista, idCentro, nombrePista, precioHora, esTechada);
            ViewData["CENTROS"] = this.repoCentros.GetCentrosActivos();
            return RedirectToAction("Index", new {id = idCentro});
        }

        public async Task<IActionResult> Delete(int idPista, int idCentro)
        {
            await this.repo.DeletePista(idPista);
            return RedirectToAction("Index", new { id = idCentro });
        }

    }
}
