using Microsoft.Data.SqlClient;
using MvcCoreSportCenter.Models;
using System.Data;

namespace MvcCoreSportCenter.Repositories
{
    public class RepositoryPistas
    {
        private DataTable tablaPistas;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositoryPistas()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=SPORTCENTER;User ID=SA;Trust Server Certificate=True";
            string sql = "select * from VIEW_DATOS_CENTRO";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            this.tablaPistas = new DataTable();
            ad.Fill(this.tablaPistas);
        }

        public DatosCentro GetDatosCentro(int idCentro)
        {
            var consulta = from datos in this.tablaPistas.AsEnumerable()
                           where datos.Field<int>("IdCentro") == idCentro
                           select datos;
            if(consulta.Count() == 0)
            {
                DatosCentro dc = new DatosCentro();
                dc.IdCentro = 0;
                dc.Nombre = "";
                dc.Direccion = "";
                dc.Ciudad = "";
                dc.TipoCentro = "";
                dc.Estado = false;
                dc.Pistas = null;
                return dc;
            }
            else
            {
                List<Pista> pistas = new List<Pista>();
                foreach(var row in consulta)
                {
                    Pista p = new Pista
                    {
                        IdPista = row.Field<int>("IdPista"),
                        IdCentro = row.Field<int>("IdCentro"),
                        NombrePista = row.Field<string>("NombrePista"),
                        PrecioHora = row.Field<decimal>("PrecioPorHora"),
                        EsTechada = row.Field<bool>("EsTechada")
                    };
                    pistas.Add(p);
                }
                var row1 = consulta.First();
                DatosCentro dc = new DatosCentro();
                dc.IdCentro = row1.Field<int>("IdCentro");
                dc.Nombre = row1.Field<string>("Nombre");
                dc.Direccion = row1.Field<string>("Direccion");
                dc.Ciudad = row1.Field<string>("Ciudad");
                dc.TipoCentro = row1.Field<string>("TipoCentro");
                dc.Estado = row1.Field<bool>("Estado");
                dc.Pistas = pistas;
                return dc;
            }
            
            
        }
    }
}
