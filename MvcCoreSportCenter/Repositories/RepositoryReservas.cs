using Microsoft.Data.SqlClient;
using MvcCoreSportCenter.Models;
using System.Data;
using System.Diagnostics.Metrics;

#region
//alter view VIEW_RESERVAS_DEPORTE
//as
//	select R.IdReserva, R.IdPista, R.IdCliente, R.FechaReserva, R.Horas, R.Pagada, C.TipoCentro
//	from Centros as C
//	inner join Pistas as P
//	on C.IdCentro = p.IdCentro
//	inner join Reservas as R
//	on R.IdPista = P.IdPista
//go
#endregion

namespace MvcCoreSportCenter.Repositories
{
    public class RepositoryReservas
    {
        private DataTable tablaReservas;
        
        public RepositoryReservas()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=SPORTCENTER;User ID=SA;Encrypt=True;Trust Server Certificate=True";
            string sql = "select * from VIEW_RESERVAS_DEPORTE";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            this.tablaReservas = new DataTable();
            ad.Fill(this.tablaReservas);
        }

        public List<Reserva> GetReservasDeporte(string tipoCentro)
        {
            var consulta = from datos in this.tablaReservas.AsEnumerable()
                           where datos.Field<string>("TipoCentro") == tipoCentro
                           select datos;
            if (consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                List<Reserva> reservas = new List<Reserva>();
                foreach(var row in consulta)
                {
                    Reserva r = new Reserva
                    {
                        IdReserva = row.Field<int>("IdReserva"),
                        IdPista = row.Field<int>("IdPista"),
                        IdCliente = row.Field<int>("IdCliente"),
                        FechaReserva = row.Field<DateTime>("FechaReserva"),
                        Horas = row.Field<int>("Horas"),
                        Pagada = row.Field<bool>("Pagada")
                    };
                    reservas.Add(r);
                }
                return reservas;
            }
        }

    }
}
