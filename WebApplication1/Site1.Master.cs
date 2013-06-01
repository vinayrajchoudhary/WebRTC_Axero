using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Routing;
using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using System.Data;
namespace WebApplication1
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string cnnString = "Server=localhost;Port=3306;Database=tut;Uid=root;Pwd=1234;";
                MySqlConnection connection = new MySqlConnection(cnnString);

                string cmdText = "SELECT user,connid FROM videochat;";
                MySqlCommand cmd = new MySqlCommand(cmdText, connection);

                connection.Open();

                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    
                    users.Controls.Add(new LiteralControl("<br />"));
                    users.Controls.Add(new LiteralControl("<li id=" + dr["connid"].ToString() + ">" + dr["user"].ToString() + "</li>"));
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