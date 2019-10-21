using System;    
using System.Collections.Generic;    
using System.Data;    
using System.Data.SqlClient;    
using System.Linq;    
using System.Threading.Tasks;    
    
namespace primer4.Models    
{    
    public class GostiDataAccessLayer    
    {    
        string connectionString = "Server=localhost;Database=Proslava;User Id=student2;Password=Student_2018";    
    
        //To View all employees details      
        public IEnumerable<Gosti> sviGosti()    
        {    
            List<Gosti> spisakGostiju = new List<Gosti>();    
    
            using (SqlConnection con = new SqlConnection(connectionString))    
            {    
                SqlCommand cmd = new SqlCommand("spSviGosti", con);    
                cmd.CommandType = CommandType.StoredProcedure;    
    
                con.Open();    
                SqlDataReader rdr = cmd.ExecuteReader();    
    
                while (rdr.Read())    
                {    
                    Gosti g = new Gosti ();    
    
                    g.GostID = Convert.ToInt32(rdr["GostId"]);    
                    g.Ime = rdr["Ime"].ToString();    
                    g.Email = rdr["Email"].ToString();    
                    g.Telefon = rdr["Telefon"].ToString();    
                    g.DolaziNaZurku = Convert.ToBoolean(rdr["Dolazi"]);
    
                    spisakGostiju.Add(g);    
                }    
                con.Close();    
            }    
            return spisakGostiju;    
        }    
    
        //To Add new employee record      
        public void dodajGosta(Gosti gost)    
        {    
            using (SqlConnection con = new SqlConnection(connectionString))    
            {    
                SqlCommand cmd = new SqlCommand("spDodajGosta", con);    
                cmd.CommandType = CommandType.StoredProcedure;    
    
                cmd.Parameters.AddWithValue("@Ime", gost.Ime);    
                cmd.Parameters.AddWithValue("@Email", gost.Email);    
                cmd.Parameters.AddWithValue("@Telefon", gost.Telefon);    
                cmd.Parameters.AddWithValue("@Dolazi", gost.DolaziNaZurku);    
    
                con.Open();    
                cmd.ExecuteNonQuery();    
                con.Close();    
            }    
        }       
    }    
}