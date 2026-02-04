using Microsoft.AspNetCore.Mvc;
using MvcCoreSportCenter.Models;
using MvcCoreSportCenter.Repositories;

namespace MvcCoreSportCenter.Controllers
{
    public class ReservasController : Controller
    {
        private RepositoryReservas repo;
        private RepositorySportCenter repoCentros;

        public ReservasController()
        {
            this.repo = new RepositoryReservas();
            this.repoCentros = new RepositorySportCenter();
        }

        public IActionResult Index(string tipoCentro)
        {
            List<Reserva> reservas = this.repo.GetReservasDeporte(tipoCentro);
            ViewData["CENTROS"] = this.repoCentros.GetTiposCentro();
            return View(reservas);
        }
    }
}
