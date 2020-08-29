using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAppCore.Models;


namespace WebAppCore.Datos
{
    public class Emails
    {
        private readonly IConfiguration _configuration;
        public Emails(IConfiguration configuration)
        {
            
            _configuration = configuration;
        }

        public void EnvioEmails()
        {
            //var retorno = new Contactos();
            var conn = _configuration.GetConnectionString("DefaultConnection");

            using (SqlConnection con = new SqlConnection(conn))
            {
                con.Open();
                using (SqlCommand command = new SqlCommand("sp_ObtenerPersonas", con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Contactos> Usu = new List<Contactos>();
                        while (reader.Read())
                        {
                            var regis = new Contactos();
                            regis.Id = reader.GetInt32(reader.GetOrdinal("id"));
                            regis.Nombre = reader.GetString(reader.GetOrdinal("nombre"));
                            regis.Email = reader.GetString(reader.GetOrdinal("email"));

                            Usu.Add(regis);
                        }

                    }
                }
                
            }

            //return retorno;
        }
    }
    
}
