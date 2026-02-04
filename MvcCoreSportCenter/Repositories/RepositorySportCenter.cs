using Microsoft.Data.SqlClient;
using MvcCoreSportCenter.Models;
using System.Data;

namespace MvcCoreSportCenter.Repositories
{
    public class RepositorySportCenter
    {
        private DataTable tablaCentros;
        private SqlConnection cn;
        private SqlCommand com;

        public RepositorySportCenter()
        {
            string connectionString = "Data Source=LOCALHOST\\DEVELOPER;Initial Catalog=SPORTCENTER;User ID=SA;Trust Server Certificate=True";
            string sql = "select * from Centros";
            SqlDataAdapter ad = new SqlDataAdapter(sql, connectionString);
            this.tablaCentros = new DataTable();
            ad.Fill(this.tablaCentros);
        }

        public List<Centro> GetCentros()
        {
            var consulta = from datos in this.tablaCentros.AsEnumerable()
                           select datos;
            List<Centro> centros = new List<Centro>();
            foreach(var row in consulta)
            {
                Centro c = new Centro
                {
                    IdCentro = row.Field<int>("IdCentro"),
                    Nombre = row.Field<string>("Nombre"),
                    Direccion = row.Field<string>("Direccion"),
                    Ciudad = row.Field<string>("Ciudad"),
                    TipoCentro = row.Field<string>("TipoCentro"),
                    Estado = row.Field<bool>("Estado"),
                };
                centros.Add(c);
            }
            return centros;
        }

        public List<string> GetTiposCentro()
        {
            var consulta = (from datos in this.tablaCentros.AsEnumerable()
                           select datos.Field<string>("TipoCentro")).Distinct();
            List<string> tiposCentro = consulta.ToList();
            return tiposCentro;
        }

        public List<Centro> GetCentrosPorTipo(string tipo)
        {
            var consulta = from datos in this.tablaCentros.AsEnumerable()
                           where datos.Field<string>("TipoCentro") == tipo
                           select datos;
            List<Centro> centros = new List<Centro>();
            foreach (var row in consulta)
            {
                Centro c = new Centro
                {
                    IdCentro = row.Field<int>("IdCentro"),
                    Nombre = row.Field<string>("Nombre"),
                    Direccion = row.Field<string>("Direccion"),
                    Ciudad = row.Field<string>("Ciudad"),
                    TipoCentro = row.Field<string>("TipoCentro"),
                    Estado = row.Field<bool>("Estado"),
                };
                centros.Add(c);
            }
            return centros;
        }

        public List<Centro> GetCentrosActivos()
        {
            var consulta = from datos in this.tablaCentros.AsEnumerable()
                            where datos.Field<bool>("Estado") == true
                            select datos;
            List<Centro> centros = new List<Centro>();
            foreach (var row in consulta)
            {
                Centro c = new Centro
                {
                    IdCentro = row.Field<int>("IdCentro"),
                    Nombre = row.Field<string>("Nombre"),
                    Direccion = row.Field<string>("Direccion"),
                    Ciudad = row.Field<string>("Ciudad"),
                    TipoCentro = row.Field<string>("TipoCentro"),
                    Estado = row.Field<bool>("Estado")
                };
                centros.Add(c);
            }
            return centros;
        }

    }
}
