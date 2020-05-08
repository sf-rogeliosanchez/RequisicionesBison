<%@ Page Title="Historial" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="T259Prueba08.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
    <link href="css/StyleTable.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        var btn = true;

        addEventListener('load', inicio, false); //Correr método inicio al cargar la página

        function inicio() {
            $('#tablas1 tbody').on('click', 'tr', function () {
                var id = this.id;
                if (btn) {
                    var status = $("#statu" + id).text();
                    if (id != "header" & id != "header1" & id != "") {
                        if (id != undefined) {
                            open_page('reqsVer.aspx?banfn=' + $(this).attr("id"));
                        }
                    }
                }
            });

            $('#numFilter').val(<% =Session["numFilter"] %>);
        }

        function open_page(comp) {
            var sPageURL = decodeURIComponent(window.location.toString());
            sPageURL = decodeURIComponent(window.location.origin) +decodeURIComponent(window.location.pathname);
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
            <div>
                <asp:TextBox ID="txtBuscar" runat="server" CssClass="search" placeholder="Buscar..."></asp:TextBox>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="button" OnClick="LinkButton1_Click">
                    <img src="images/ic_search_white_24dp_1x.png" style="top:7px;position:relative;" />
                </asp:LinkButton>
            </div>

            <div style="height: 100%; width: 100%; position: inherit">
                <br />

                <div style="width: 100%; height: 95%;">
                    <div style="width: 50%; float: left;">
                        Mostrar 
                    <select id="numFilter" onchange="cambiaNum(this.value)">
                        <option value="10">10</option>
                        <option value="25">25</option>
                        <option value="50">50</option>
                        <option value="100">100</option>
                    </select>
                        por página
                    </div>
                    <div style="width: 50%; float: left; text-align: right;">
                        Buscar: 
                        <input id="txtReqs" runat="server" type="text" placeholder="No. de Requisición" onkeyup="txtReqs_keyup(this.value)" />
                    </div>

                    <asp:Label runat="server" ID="lblTabla1"></asp:Label>
                </div>
                <br />
                <div style="width: 100%">
                    <div style="width: 50%; float: left; padding-top: 10px;">
                        Página
                        <asp:Label runat="server" ID="lblNumPag"></asp:Label>
                        de
                        <asp:Label runat="server" ID="lblNumTPag"></asp:Label>
                    </div>
                    <div style="width: 50%; float: left; text-align: right;">
                        <asp:Label runat="server" ID="lblControlPag"></asp:Label>
                    </div>
                </div>
                <br />

                <script src="Scripts/jquery.SortTable.js" type="text/javascript"></script>
                <link href="css/StyleTable2.css" rel="stylesheet" />

                <script type="text/javascript">

                    function cambiaNum(valor) {
                        var cadenaB = getUrlParameter("search");
                        if(cadenaB == undefined){
                            open_page('Historial.aspx?num=' + valor+"&pag=1");
                        }else{
                            cadenaB = getBusqueda(cadenaB);
                            open_page('Historial.aspx?search='+cadenaB+'&num=' + valor+"&pag=1");
                        }
                    }
                    function getBusqueda(cadenaBB){
                        var c = cadenaBB.replace('/','%2F');
                        return c;
                    }

                    var controles = "";
                    function txtReqs_keyup(valor) {
                        //alert(valor)
                        
                        var usuario = "<% =Session["UserName"]%>";
                        var num = "<% =Session["numFilter"]%>";
                        var numActual = "<% =Session["pagActual"]%>";
                        var totPags = <% =Session["totPags"]%>;

                        
                        document.getElementById('ContentPlaceHolder1_txtBuscar').value = "";
                        document.getElementById('ContentPlaceHolder1_lblTabla1').textContent = "";
                        document.getElementById('ContentPlaceHolder1_lblTabla1').innerHTML = " <table  class='CSSTableGenerator2' id=\"tablas1\" border=1 style=\"width:1150px;height:95%;text-align:center\" ><thead><tr id = \"header\"><th style='width:94px;'>Requisición</th><th style='width:71px;'>Fecha de <br>creación</th><th style='width:80px;' filter-type='ddl'>Solicitante</th><th style='width:80px;'>Estatus</th><th style='max-width:150px;'>Referencia</th><th style='width:77px;'>Solicitud<br>de pedido</th><th style='width:77px;'>Orden de<br> compra</th><th style='width:100px;'>Fecha de <br>modificación</th><th style='width:96px;' filter-type='ddl'>Última <br> modificación por</th><th>Comentarios</th></tr></thead><tbody>";
                        
                        if (controles == "") {
                            //controles = document.getElementById('ContentPlaceHolder1_lblControlPag').innerHTML
                            controles += '<a class="paginate_button current" data-dt-idx="1" tabindex="0">1</a>';
                            for(var i = 2;i<6;i++){
                                controles += '<a class="paginate_button" onclick="control('+i+')" data-dt-idx="1" tabindex="0">'+i+'</a>';
                            }
                            controles += '<a class="paginate_button disabled" data-dt-idx="1" tabindex="0">...</a>';
                            controles += '<a class="paginate_button" onclick="'+totPags+'" data-dt-idx="1" tabindex="0">'+totPags+'</a>';
                            controles += '<a class="paginate_button" onclick="control('+(numActual+1)+')" data-dt-idx="1" tabindex="0">Siguiente</a>';
                        }

                        //$("ContentPlaceHolder1_lblTabla1").text("");
                        var param = { valor: valor, userr: usuario, num:num, numActual:numActual };
                        $.ajax({
                            type: "POST",
                            url: "Historial.aspx/filtrarTodo",
                            //data: data,
                            data: JSON.stringify(param),
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            cache: false,
                            beforeSend: function (response) {
                                //document.getElementById("loading").style.visibility = 'visible';
                            },
                            success: function (response) {
                                //document.getElementById("loading").style.visibility = 'hidden';
                                var mensaje = response.d;
                                var status = mensaje.charAt(0);
                                if (status == "B") {
                                    alert("Búsqueda");
                                    //$(location).attr('href', '/Historial.aspx');
                                    //open_page('Historial.aspx');
                                } else {
                                    var tabll = document.getElementById('ContentPlaceHolder1_lblTabla1');
                                    tabll.innerHTML = response.d;
                                    //$("#ContentPlaceHolder1_lblTabla1").val(response.d);
                                }
                            },
                            failure: function (XMLHttpRequest, textStatus, errorThrown) {
                                //document.getElementById("loading").style.visibility = 'hidden';
                                alert(textStatus);
                            }
                        });
                        if (valor != "") {
                            document.getElementById('ContentPlaceHolder1_lblNumPag').innerHTML = "1";
                            document.getElementById('ContentPlaceHolder1_lblNumTPag').innerHTML = "1";
                            document.getElementById('ContentPlaceHolder1_lblControlPag').innerHTML = "";
                        } else {
                            document.getElementById('ContentPlaceHolder1_lblControlPag').innerHTML = controles;
                        }
                    }

                    var getUrlParameter = function getUrlParameter(sParam) {
                        var sPageURL = decodeURIComponent(window.location.search.substring(1)),
                            sURLVariables = sPageURL.split('&'),
                            sParameterName,
                            i;

                        for (i = 0; i < sURLVariables.length; i++) {
                            sParameterName = sURLVariables[i].split('=');

                            if (sParameterName[0] === sParam) {
                                return sParameterName[1] === undefined ? true : sParameterName[1];
                            }
                        }
                    };

                    function control(pagina) {
                        var cadenaB = getUrlParameter("search");
                        var sorts = getUrlParameter("sort");
                        if(sorts!=undefined){
                            sorts = "&sort="+sorts;
                        }else{
                            sorts = "";
                        }
                        if(cadenaB == undefined){
                            open_page('Historial.aspx?pag='+pagina+sorts);
                        }else{
                            cadenaB = getBusqueda(cadenaB);
                            open_page('Historial.aspx?search='+cadenaB+'&pag='+pagina+sorts);
                        }
                    }

                    function abre(id){
                        open_page('reqsVer.aspx?banfn=' + id);
                    }

                    function sort(id){
                        var cadenaB = getUrlParameter("search");
                        var sorts = getUrlParameter("sort");
                        if(sorts!=undefined){
                            if(sorts.charAt(0)=='A')
                                sorts = "D";
                            else
                                sorts = "A";
                        }else{
                            sorts = "D";
                        }
                        if(cadenaB == undefined){
                            open_page('Historial.aspx?pag=1&sort=' + sorts + id);
                        }else{
                            cadenaB = getBusqueda(cadenaB);
                            open_page('Historial.aspx?search='+ cadenaB +'&pag=1&sort='+ sorts + id);
                        }
                    }
                </script>
            </div>
        </div>
        <style type="text/css">
            .search {
                padding: 8px 15px;
                background: rgba(50, 50, 50, 0.2);
                border: 0px solid #dbdbdb;
                width: 578px;
            }

            .button {
                position: relative;
                padding: 6px 15px 7px;
                left: -8px;
                border: 2px solid #207cca;
                background-color: #207cca;
                color: #fafafa;
            }

                .button:hover {
                    background-color: #fafafa;
                    color: #207cca;
                }
        </style>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <div>
        <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
        <h1>Historial de requisiciones</h1>
    </div>
</asp:Content>
