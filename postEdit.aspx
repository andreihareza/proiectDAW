<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="postEdit.aspx.cs" Inherits="postEdit" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="alert alert-info">
        <h4>
            <asp:Label runat="server" Text="Titlu"></asp:Label></h4>
        <input type="text" runat="server" id="textTitle" maxlength="500" required="required" class="form-control" />
        <br />
        <h4>
            <asp:Label runat="server" Text="Descriere"></asp:Label></h4>
        <textarea class="form-control" rows="5" id="textContent" required="required" maxlength="1500" runat="server"></textarea>
        <br />
        <asp:Button OnClick='EditPost' class='btn btn-success' runat='server' Text='Edit' ID='buttonEdit'></asp:Button>
        <asp:Button OnClick='DeletePost' class='btn btn-danger pull-right' runat='server' Text='Delete' ID='buttonDelete'></asp:Button>
    </div>

    <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>

</asp:Content>
