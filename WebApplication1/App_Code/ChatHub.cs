﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Data;
using System.Data.SqlClient;
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
        string cnnString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"|DataDirectory|\\Database1.mdf\";Integrated Security=True;User Instance=True;";
        SqlConnection connection = new SqlConnection(cnnString);
        string cmdText = "INSERT INTO videochat (username, pass, connid) VALUES ('"+user+"','"+pass+"','"+connid+"');";

        SqlCommand cmd = new SqlCommand(cmdText, connection);

        cmd.CommandType = CommandType.Text;
        
        connection.Open();

        int result = cmd.ExecuteNonQuery();
    }
    public void Deregister( string message,string connid)
    {
        try
        {
            // Connection string for a typical local MySQL installation
            string cnnString = "Data Source=.\\SQLEXPRESS;AttachDbFilename=\"|DataDirectory|\\Database1.mdf\";Integrated Security=True;User Instance=True;";
            // Create a connection object 
            SqlConnection connection = new SqlConnection(cnnString);

            // Create a SQL command object
            string cmdText = "DELETE FROM videochat WHERE [connid]='"+connid+"';";

            SqlCommand cmd = new SqlCommand(cmdText, connection);

            connection.Open();

            cmd.CommandType = CommandType.Text;

           
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
