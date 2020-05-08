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
            $("tbody tr").click(function () {
                var id = $(this).attr("id");
                if (btn) {
                    var status = $("#statu" + id).text();
                    if (id != "header" & id != "header1" & id != "") {
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
            <div style="height: 100%; width: 100%; position: inherit">
                <input type="checkbox" id="onlyyes" hidden="hidden" />
                <input type="checkbox" id="onlyno" hidden="hidden" />
                <br />
                <div style="text-align:right">
                    <a id="cleanfilters" href="#">Limpiar filtros</a>
                    <input type="text" id="quickfind" placeholder="Buscar" />
                    <br />
                </div>
                <div class="CSSTableGenerator2" style="width: 100%; height: 95%;">
                    <asp:Label runat="server" ID="lblTabla1"></asp:Label>
                </div>
                <br />
                <br />

                <script src="http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js" type="text/javascript"></script>
                <script type="text/javascript" src="Scripts/picnet.table.filter.min.js"></script>


                <script type="text/javascript">
                    $(document).ready(function () {
                        // Initialise Plugin
                        var options1 = {
                            additionalFilterTriggers: [$('#onlyyes'), $('#onlyno'), $('#quickfind')],
                            clearFiltersControls: [$('#cleanfilters')],
                            matchingRow: function (state, tr, textTokens) {
                                if (!state || !state.id) { return true; }
                                var val = tr.children('td:eq(2)').text();
                                switch (state.id) {
                                    case 'onlyyes': return state.value !== 'true' || val === 'yes';
                                    case 'onlyno': return state.value !== 'true' || val === 'no';
                                    default: return true;
                                }
                            }
                        };
                        $('#tablas1').tableFilter(options1);
                    });
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
