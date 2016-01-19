using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace TEAM3OIE2S.Controllers
{
    public class AuditController : Controller
    {

        public static void CreateAuditEntry(string userID, string username, string table, string attribute, string access)
        {
            if (username == null)
            {
                username = "(visitor)";
            }

            string insert_string = "INSERT INTO Audit(userID,userName,date,DBtable,attribute,access) VALUES (@p1,@p2,CURRENT_TIMESTAMP,@p3,@p4,@p5);"; //insert statement
            SqlConnection db = new SqlConnection(@"Data Source="",1044;Initial Catalog="";User ID="";Password=""");
            SqlCommand command = new SqlCommand(insert_string, db);

            if (userID == null)
            {
                command.Parameters.AddWithValue("@p1", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@p1", userID);
            }

            command.Parameters.AddWithValue("@p2", username);

            command.Parameters.AddWithValue("@p3", table);

            if (attribute == null)
            {
                command.Parameters.AddWithValue("@p4", DBNull.Value);
            }
            else
            {
                command.Parameters.AddWithValue("@p4", attribute);
            }

            command.Parameters.AddWithValue("@p5", access);

            db.Open();
            command.ExecuteNonQuery();
            db.Close();
        }
    }
}
