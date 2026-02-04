using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Data.SqlClient;
using MvcCoreSportCenter.Models;
using System.Data;
using System.Diagnostics.Metrics;
using System.Threading.Tasks;

#region
//create view VIEW_DATOS_CENTRO
//as
//	select C.IdCentro, C.Nombre, C.Direccion, C.Ciudad, C.TipoCentro, C.Estado,
//    P.IdPista, P.NombrePista, P.PrecioPorHora, P.EsTechada
//	from Centros as C
//	inner join Pistas as P
//	on C.IdCentro = P.IdCentro
//go

//create procedure SP_INSERT_PISTA
//(@idCentro int, @nombrePista nvarchar(50), @precioHora decimal, @esTechada bit)
//as
//	insert into Pistas values(@idCentro, @nombrePista, @precioHora, @esTechada)
//go

//alter procedure SP_UPDATE_PISTA
//(@idPista int, @idCentro int, @nombrePista nvarchar(50), @precioHora decimal, @esTechada bit)
//as
//	update Pistas set IdCentro = @idCentro, NombrePista = @nombrePista, PrecioPorHora = @precioHora,
//    EsTechada = @esTechada where IdPista = @idPista
//go

//alter procedure SP_DELETE_PISTA
//(@idPista int)
//as
//	delete from Pistas where IdPista = @idPista
//go

#endregion

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
            this.cn = new SqlConnection(connectionString);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
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

        

        public async Task InsertPista(int idCentro, string nombrePista, double precioHora, bool esTechada)
        {
            string sql = "SP_INSERT_PISTA";
            this.com.Parameters.Clear();
            this.com.Parameters.AddWithValue("@idCentro", idCentro);
            this.com.Parameters.AddWithValue("@nombrePista", nombrePista);
            this.com.Parameters.AddWithValue("@precioHora", precioHora);
            this.com.Parameters.AddWithValue("@esTechada", esTechada);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public Pista FindPistaById(int idPista)
        {
            var consulta = from datos in this.tablaPistas.AsEnumerable()
                           where datos.Field<int>("IdPista") == idPista
                           select datos;
            if(consulta.Count() == 0)
            {
                return null;
            }
            else
            {
                var row = consulta.First();
                Pista p = new Pista
                {
                    IdPista = row.Field<int>("IdPista"),
                    IdCentro = row.Field<int>("IdCentro"),
                    NombrePista = row.Field<string>("NombrePista"),
                    PrecioHora = row.Field<decimal>("PrecioPorHora"),
                    EsTechada = row.Field<bool>("EsTechada")
                };
                return p;
            }
        }

        public async Task UpdatePista(int idPista, int idCentro, string nombrePista, double precioHora, bool esTechada)
        {
            string sql = "SP_UPDATE_PISTA";
            this.com.Parameters.AddWithValue("@idPista", idPista);
            this.com.Parameters.AddWithValue("@idCentro", idCentro);
            this.com.Parameters.AddWithValue("@nombrePista", nombrePista);
            this.com.Parameters.AddWithValue("@precioHora", precioHora);
            this.com.Parameters.AddWithValue("@esTechada", esTechada);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

        public async Task DeletePista(int idPista)
        {
            string sql = "SP_DELETE_PISTA";
            this.com.Parameters.AddWithValue("@idPista", idPista);
            this.com.CommandType = CommandType.StoredProcedure;
            this.com.CommandText = sql;
            await this.cn.OpenAsync();
            await this.com.ExecuteNonQueryAsync();
            await this.cn.CloseAsync();
            this.com.Parameters.Clear();
        }

    }
}
