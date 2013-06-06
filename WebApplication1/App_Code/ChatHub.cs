using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using MySql.Data.MySqlClient;
using System.Data;
public class ChatHub : Hub
{
    public void Send( string receiverconnectid, string message, string myconnectid)
    {
        // Call the broadcastMessage method to update clients.
        Clients.Client(receiverconnectid).broadcastMessage(message, myconnectid);
    }
    public void Sendtoall(string message, string myconnectid)
    {
        // Call the broadcastMessage method to update clients.
        Clients.Others.broadcastMessage(message, myconnectid);
    }
    public void Register(string user,string pass, string connid)
    {
        string cnnString = "Server=localhost;Port=3306;Database=tut;Uid=root;Pwd=1234;default command timeout=3600; Connection Timeout=5;";
        MySqlConnection connection = new MySqlConnection(cnnString);
        string cmdText = "INSERT INTO videochat (user, pass, connid) VALUES (?user ,?pass, ?connid);";

        MySqlCommand cmd = new MySqlCommand(cmdText, connection);

        cmd.CommandType = CommandType.Text;

        cmd.Parameters.Add("?user", MySqlDbType.VarChar).Value = user;
        cmd.Parameters.Add("?pass", MySqlDbType.VarChar).Value = pass;
        cmd.Parameters.Add("?connid", MySqlDbType.VarChar).Value = connid;

        connection.Open();

        int result = cmd.ExecuteNonQuery();
    }
    public void Deregister( string message,string connid)
    {
        try
        {
            // Connection string for a typical local MySQL installation
            string cnnString = "Server=localhost;Port=3306;Database=tut;Uid=root;Pwd=1234;default command timeout=3600; Connection Timeout=5;";

            // Create a connection object 
            MySqlConnection connection = new MySqlConnection(cnnString);

            // Create a SQL command object
            string cmdText = "DELETE FROM videochat WHERE connid=(?connid);";

            MySqlCommand cmd = new MySqlCommand(cmdText, connection);

            connection.Open();

            cmd.CommandType = CommandType.Text;

            cmd.Parameters.Add("?connid", MySqlDbType.VarChar).Value = connid;
            cmd.ExecuteNonQuery();
            
            //                result.Text = "Done...";
            connection.Close();

        }
        catch (Exception ex)
        {
            //                result.Text = ex.Message;
        }
    }
}
