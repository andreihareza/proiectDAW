<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Layout.master" CodeFile="answerEdit.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="well">
        <div class="text-primary">
            <h4><asp:label runat="server" text="Raspuns: "></asp:label></h4>

        </div>
        <textarea class="form-control" rows="5" id="textAnswer" required="required" maxlength="999" runat="server"></textarea>
        <br />
        <asp:button onclick='EditAnswer' class='btn btn-success' runat='server' text='Edit' id='buttonEdit'></asp:button>
        <asp:button onclick='DeleteAnswer' class='btn btn-danger pull-right' runat='server' text='Delete' id='buttonDelete'></asp:button>
    </div>

    <asp:label id="MessageID" runat="server" text=""></asp:label>

</asp:Content>
