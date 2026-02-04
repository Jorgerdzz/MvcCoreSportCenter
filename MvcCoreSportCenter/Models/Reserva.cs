namespace MvcCoreSportCenter.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int IdPista { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaReserva { get; set; }
        public int Horas { get; set; }
        public bool Pagada { get; set; }
    }
}
