namespace MvcCoreSportCenter.Models
{
    public class Pista
    {
        public int IdPista { get; set; }
        public int IdCentro { get; set; }
        public string NombrePista { get; set; }
        public decimal PrecioHora { get; set; }
        public bool EsTechada { get; set; }
    }
}
