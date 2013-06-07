<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="start.aspx.cs" Inherits="WebApplication1.start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
       <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>Username</td>
                <td><asp:TextBox ID="user" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>Password</td>
                <td><asp:TextBox ID ="pass" runat="server"></asp:TextBox></td>
            </tr>
                <td><asp:TextBox ID ="connid" runat="server" type="hidden"></asp:TextBox></td>
           
         
               <tr>
                <td>&nbsp;</td>
                <td><asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" /></td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td><asp:Label ID="lblError" runat="server" Text=""></asp:Label></td>
            </tr>    
        </table>
    </div>
    </form>
    <div>
    
    </div>
</body>
</html>
