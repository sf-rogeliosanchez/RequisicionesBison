<%@ Page Title="Historial" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historial.aspx.cs" Inherits="T259Prueba08.Historial" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
    <link href="css/StyleTable.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        var btn = true;

        addEventListener('load', inicio, false); //Correr método inicio al cargar la página

        function inicio() {
            //    $('#label').append(servicio);
            //    autocompleta_s('serv');
            //    fecha('date');
            //}

            //$(document).ready(function () {
            $("tbody tr").click(function () {
                var id = $(this).attr("id");
                if (btn) {
                    var status = $("#statu" + id).text();
                    //if (status != "B") {
                    //    if (id != "header") {
                    //        $(location).attr('href', '/Modificar.aspx?banfn=' + $(this).attr("id"));
                    //    }
                    //} else {
                    if (id != "header") {
                        if (id != undefined) {
                            open_page('reqsVer.aspx?banfn=' + $(this).attr("id"));
                            //btn = false;
                        }
                    }
                    //}
                }
            });
            //});
        }

        function open_page(comp) {
            var sPageURL = decodeURIComponent(window.location.toString());
            var url = sPageURL.split('/');
            var urls = '';
            for (var i = 0; i < url.length - 1; i++) {
                urls += url[i] + '/';
            }
            $(location).attr('href', urls + comp);
        }
        function click_on(id) {
            var id = $(id).attr('id');
            var banfn = id.replace('btn', '');
            //$.ajax({
            //    type: "POST",
            //    url: "Historial.aspx/modif",
            //    data: "{banfn:'" + banfn + "'}",
            //    contentType: "application/json; charset=utf-8",
            //    dataType: "json",
            //    async: true,
            //    cache: false,
            //    success: function (response) { alert(response.d); $(location).attr('href', '/Visualizar.aspx?banfn=' + banfn); },
            //    failure: function () { alert("ERROR "); }
            //});
            open_page('reqsMod.aspx?banfn=' + banfn);
            btn = false;
        }

        function clic_his(id) {

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div>
            <div style="height:100%; width:100%; position:inherit">
                <div class="CSSTableGenerator" style="width: 100%;height:95%;">
                    <%--<asp:Label runat="server" ID="lblTabla"></asp:Label><br /><br />--%>
                    <asp:Label runat="server" ID="lblTabla1"></asp:Label>
                </div>
            </div>
            <%--<p id="banfn"></p>

            <asp:Table ID="Table1" runat="server" BorderColor ="Black">
            <asp:TableHeaderRow runat="server" id ="id">
                <asp:TableHeaderCell runat="server" Text="Número de solicitud"></asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" Text="Solicitante"></asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" Text="Estatus"></asp:TableHeaderCell>
                <asp:TableHeaderCell runat="server" Text="Fecha de creación"></asp:TableHeaderCell>
            </asp:TableHeaderRow>

            <asp:TableRow>
                <asp:TableCell runat="server" Text="1"><div contenteditable="true">2</div></asp:TableCell>
                <asp:TableCell runat="server" Text="1"></asp:TableCell>
                <asp:TableCell runat="server" Text="1"></asp:TableCell>
                <asp:TableCell runat="server" Text="1"></asp:TableCell>
                
            </asp:TableRow>

            </asp:Table>--%>
        </div>

        <%--<asp:Button runat="server" OnClick="click_on" ID="button" Text="Botón"></asp:Button>--%>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div>
    <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
        <h1>Historial de requisiciones</h1>
        <br />
        <br />
    </div>
</asp:Content>
