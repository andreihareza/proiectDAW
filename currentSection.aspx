<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="currentSection.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="well">
        <h1 class="text-center" id="TitleID" runat="server">Forum</h1>
    </div>

    <asp:Label ID="postsList" runat="server">
   

    </asp:Label>
   
    <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>
</asp:Content>
