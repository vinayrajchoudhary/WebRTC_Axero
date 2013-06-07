using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;

using System.Data;
using System.Data.SqlClient;
namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string cnnString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"|DataDirectory|\\Database1.mdf\";Integrated Security=True;User Instance=True;";
               
                 SqlConnection connection = new SqlConnection(cnnString);

                string cmdText = "SELECT username,connid FROM videochat;";
                SqlCommand cmd = new SqlCommand(cmdText, connection);

                connection.Open();

                cmd.CommandType = CommandType.Text;
                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    
                    users.Controls.Add(new LiteralControl("<br />"));
                    users.Controls.Add(new LiteralControl("<li id=" + dr["connid"].ToString() + ">" + dr["username"].ToString() + "</li>"));
                }
                dr.Close();
                connection.Close();

            }
            catch (Exception ex)
            {
                //error.Text = ex.Message;
            }
        }

 
    }
}