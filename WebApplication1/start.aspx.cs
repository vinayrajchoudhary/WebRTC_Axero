using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApplication1
{
    public partial class start : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Connection string for a typical local MySQL installation
                string cnnString = "Server=localhost;Port=3306;Database=tut;Uid=root;Pwd=1234;default command timeout=3600; Connection Timeout=5;";

                // Create a connection object 
                MySqlConnection connection = new MySqlConnection(cnnString);

                // Create a SQL command object
                string cmdText = "INSERT INTO videochat (user, pass, connid) VALUES (?user ,?pass, ?connid);";

                MySqlCommand cmd = new MySqlCommand(cmdText, connection);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.Add("?user", MySqlDbType.VarChar).Value = user.Text;
                cmd.Parameters.Add("?pass", MySqlDbType.VarChar).Value = pass.Text;
                cmd.Parameters.Add("?connid", MySqlDbType.VarChar).Value = connid.Text;

                connection.Open();

                int result = cmd.ExecuteNonQuery();

                lblError.Text = "Data Saved";

            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    
    }
}