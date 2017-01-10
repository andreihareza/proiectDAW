<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="currentPost.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="well">
        <h1 class="text-center" id="TitleID" runat="server">Forum</h1>
    </div>
    <div class="alert alert-info">
        <asp:Label ID="currentPost" runat="server">
        </asp:Label>
    </div>

    <asp:Label ID="answersList" runat="server">
    </asp:Label>
    <div class='form-group' id="answerVisibility" runat="server">
        <label for='comment'>Answer</label>
        <textarea class="form-control" rows="5" id="textAnswer" required="required" maxlength="999" runat="server"></textarea>
        <asp:Button OnClick='AddAnswer' class='btn btn-success' runat='server' Text='Answer' ID='buttonPost'></asp:Button>
    </div>


    <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>
</asp:Content>
