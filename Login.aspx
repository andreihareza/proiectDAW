<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="Login.aspx.cs" Inherits="_Default" %>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="row">
        <div class="col-md-6 col-md-offset-3">
            <div class="panel panel-primary">
                <div class="panel panel-heading">
                    <h3>Login</h3>
                </div>
                <div class="panel panel-body">
                    <form id="form1" class="" runat="server">
                        <div class="form-group ">
                            <div class="input-group">
                                <asp:Label ID="Label1" class="input-group-addon glyphicon glyphicon-user" runat="server"></asp:Label>
                                <asp:TextBox ID="UserID" placeholder="User Name" class="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="form-group ">
                            <div class="input-group">
                                <asp:Label ID="Label2" class="input-group-addon glyphicon glyphicon-lock" runat="server"></asp:Label>
                                <asp:TextBox ID="PasswordID" placeholder="Password" class="form-control" TextMode="Password" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="Button1" class="btn btn-success pull-right" runat="server" Text="Login" OnClick="Button1_Click" />
                    </form>
                    <asp:Label ID="MessageID" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

