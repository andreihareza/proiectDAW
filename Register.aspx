<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="Register.aspx.cs" Inherits="Register" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <div class="panel panel-primary">
                <div class="panel panel-heading">
                    <h3>Register</h3>
                </div>
                <div class="panel panel-body">

                        <div class="form-group">
                            <div class="input-group">
                                <asp:Label class="input-group-addon glyphicon glyphicon-user" ID="Label1" runat="server"></asp:Label>
                                <asp:TextBox ID="UserID" runat="server" class="form-control" required="required" placeholder="User Name"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:Label class="input-group-addon glyphicon glyphicon-lock" ID="Label2" runat="server"></asp:Label>
                                <asp:TextBox ID="PasswordID" class="form-control" runat="server" TextMode="Password" required="required" placeholder="Password"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:Label ID="Label3" class="input-group-addon glyphicon glyphicon-lock" runat="server"></asp:Label>
                                <asp:TextBox ID="ConfirmID" class="form-control" runat="server" TextMode="Password" placeholder="Confirm Password" required="required"></asp:TextBox>
                                <asp:Label ID="PasswordConfirmFailedID" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                        <div class="form-group">
                            <div class="input-group">
                                <asp:Label ID="Label4" class="input-group-addon glyphicon glyphicon-envelope" runat="server"></asp:Label>
                                <asp:TextBox ID="EmailID" class="form-control" runat="server" placeholder="E-mail" TextMode="Email" required="required"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="Button1" class="btn btn-success pull-right" runat="server" Text="Register" OnClick="Button1_Click" />
                    <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
