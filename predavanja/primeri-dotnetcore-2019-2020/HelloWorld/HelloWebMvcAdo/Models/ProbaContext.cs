using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HelloWeb.Models
{
    public class ProbaContext
    {
        public string ConnectionString { get; set; }

        public ProbaContext(string connectionString)
        {
            this.ConnectionString = connectionString;
        }

        private MySqlConnection GetConnection()
        {
            return new MySqlConnection(ConnectionString);
        }

        public List<Skola> VratiSveSkole()
        {
            List<Skola> list = new List<Skola>();

            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from skola where id < 10", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(new Skola()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naziv = reader["naziv"].ToString(),
                            Adresa = reader["adresa"].ToString()
                        });
                    }
                }
            }
            return list;
        }

    }


}
