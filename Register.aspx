<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="User"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>

        <br />

        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>

        <br />

        <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label>
        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>

        <br />

        <asp:Label ID="Label4" runat="server" Text="E-mail"></asp:Label>
        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>

        <br />

        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" />
    
    </div>
    </form>
</body>
</html>
