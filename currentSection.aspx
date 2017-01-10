<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="currentSection.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="well">
        <h1 class="text-center" id="TitleID" runat="server">Forum</h1>
    </div>
        <asp:Button tyApe="button" runat="server" class="btn btn-success" ToolTip="add new post" Text="Add new post" OnClick="Button1_Click"></asp:Button>
        <div id="postTemplate" visible="false" runat="server">
            <asp:Label ID="FormPost" runat="server"></asp:Label>
            <div class='form-group'>
                <label for='titlu'>Titlu</label>
            </div>
            <input type='text' class='form-control' id='titlu' runat='server' required="required" minlength="5" maxlength="50"/>
            <div class='form-group'>
                <label for='comment'>Continut</label>
                <textarea class='form-control' rows='5' id='continut' required="required" maxlength="999" runat="server"></textarea>
            </div>
            <asp:Button OnClick='AddPost' class='btn btn-success' runat='server' Text='Post' ID='buttonPost'></asp:Button>
        </div>
    <br />
    <asp:Label ID="postsList" runat="server">
   

    </asp:Label>

    <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>
</asp:Content>
