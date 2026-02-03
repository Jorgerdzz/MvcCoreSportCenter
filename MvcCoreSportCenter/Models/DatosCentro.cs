namespace MvcCoreSportCenter.Models
{
    public class DatosCentro
    {
        public int IdCentro { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Ciudad { get; set; }
        public string TipoCentro { get; set; }
        public bool Estado { get; set; }
        public List<Pista> Pistas { get; set; }
    }
}
