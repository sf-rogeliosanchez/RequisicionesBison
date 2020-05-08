<%@ Page Title="Historial" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Historial2.aspx.cs" Inherits="T259Prueba08.Historial2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
    <link href="css/StyleTable.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        var btn = true;

        addEventListener('load', inicio, false); //Correr método inicio al cargar la página

        function inicio() {
            $("tbody tr").click(function () {
                var id = $(this).attr("id");
                if (btn) {
                    var status = $("#statu" + id).text();
                    //if (status != "B") {
                    //    if (id != "header") {
                    //        $(location).attr('href', '/Modificar.aspx?banfn=' + $(this).attr("id"));
                    //    }
                    //} else {
                    if (id != "header" & id != "header1") {
                        if (id != undefined) {
                            open_page('reqsVer.aspx?banfn=' + $(this).attr("id"));
                        }
                    }
                }
            });
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

        //function filtrar() {
        //    var tt = HISTORIAL;
        //    alert(document.getElementById("filtro").value);

        //}
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div>
            <input id="filtro1" type="search" placeholder="Filtrar" /><br />
            <div style="height: 100%; width: 100%; position: inherit">
                <div class="CSSTableGenerator2" style="width: 100%; height: 95%;">
                    <asp:Label runat="server" ID="lblTabla1"></asp:Label>
                </div>
                <br />
                <br />

                <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
                <script src="Scripts/jquery.filtertable.min.js"></script>
                <script>
                    $(document).ready(function () {
                        $('#tablas1').filterTable({
                            inputSelector: '#filtro',
                            ignoreColumns: [0, 1, 2, 4, 5, 6, 7, 8]
                        }); // apply filterTable to all tables on this page
                    });
                    $(document).ready(function () {
                        $('#tablas1').filterTable({
                            inputSelector: '#filtroS',
                            ignoreColumns: [0, 1, 3, 4, 5, 6, 7, 8]
                        }); // apply filterTable to all tables on this page
                    });
                    //$(document).ready(function () {
                    //    $('#tablas1').filterTable({
                    //        inputSelector: '#filtro1',
                    //}); // apply filterTable to all tables on this page
                    //});
                </script>
            </div>
        </div>
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
