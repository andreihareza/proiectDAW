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
        <asp:TextBox ID="UserID" runat="server" required="required"></asp:TextBox>

        <br />

        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
        <asp:TextBox ID="PasswordID" runat="server" TextMode="Password" required="required"></asp:TextBox>
      
        <br />

        <asp:Label ID="Label3" runat="server" Text="Confirm Password"></asp:Label>
        <asp:TextBox ID="ConfirmID" runat="server" TextMode="Password" required="required"></asp:TextBox> 
        <asp:Label ID="PasswordConfirmFailedID" runat="server" Text=""></asp:Label>

        <br />

        <asp:Label ID="Label4" runat="server" Text="E-mail"></asp:Label>
        <asp:TextBox ID="EmailID" runat="server" TextMode="Email"></asp:TextBox>

        <br />

        <asp:Button ID="Button1" runat="server" Text="Register" OnClick="Button1_Click" />

        <br /> 

        <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
