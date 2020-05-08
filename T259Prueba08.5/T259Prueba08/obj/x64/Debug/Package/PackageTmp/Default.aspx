<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="T259Prueba08.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <form id="form1" runat="server">
        <div style="text-align: center">
            <br /><br />
            Usuario :<br />
            <asp:TextBox ID="TextBox1" runat="server" Width="210px"></asp:TextBox><br /><br />
            Contraseña :<br />
            <asp:TextBox ID="TextBox2" runat="server" Width="210px" TextMode="Password"></asp:TextBox><br /><br />

            <%--<div style="width: 52%; text-align: right;">--%>
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Acceder" CssClass="btnCrear" />
            <%--</div>--%>
            <%--            <asp:TextBox ID="TextBox1" runat="server" Width="210px" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Usuario" Visible="false"/>--%>


            <br />
            <br />
           <%-- <asp:Button ID="btnCrear" runat="server" Text="Creación solicitudes" OnClick="btnCrear_Click" CssClass="button_Alex02" />
            <asp:Label runat="server" Width="30%"></asp:Label>
            <asp:Button ID="btnHistorial" runat="server" Text="Historial solicitudes" OnClick="btnHistorial_Click" CssClass="button_Alex02" />--%>
        </div>
    </form>
</asp:Content>
