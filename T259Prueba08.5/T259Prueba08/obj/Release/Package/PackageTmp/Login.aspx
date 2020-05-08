<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="T259Prueba08.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="form1" runat="server">
        <div style="text-align: left;width:900px;" class="main">
            <br /><br />

            Usuario :<br />
            <asp:TextBox ID="txtUser" runat="server"  CssClass="textEntry"></asp:TextBox><br /><br />
            Contraseña :<br />
            <asp:TextBox ID="txtPass" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox><br /><br />

            <asp:Button ID="btnInicio" runat="server" OnClick="btnInicio_Click" Text="Acceder" CssClass="btnCrear" />
            <br />
            <br />
            <%--<asp:Login ID="Login1" runat="server"></asp:Login>--%>
        </div>
    </form>
</asp:Content>
