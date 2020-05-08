<%@ Page Title="Visualizar requisición" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reqsVer.aspx.cs" Inherits="T259Prueba08.reqsVer" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
    <link href="css/StyleTable.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        function descargar(id) {
            //alert("descargar " + id);
            //var nombre = $('#' + id).id();
            //var folnam = id + '|' + nombre;
            var folnam = id;
            $.ajax({
                type: "POST",
                url: "catalogos.asmx/descarga",
                data: "{folnam:'" + folnam + "', eban:'" + getUrlParameter("banfn") + "'}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (response) { alert(response.d); },
                failure: function () { /*alert("ERROR ");*/ }
            });
        }

        function aprobar(banfn, user) {
            //var banfn = "6"; 
            var sPageURL = decodeURIComponent(window.location.toString());
            //alert("Crear orden");
            var param = { banfn: banfn, user: user, url: sPageURL };
            $.ajax({
                type: "POST",
                url: "catalogos.asmx/aprobar",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                beforeSend: function (response) {
                    document.getElementById("loading").style.visibility = 'visible';
                },
                success: function (response) {
                    document.getElementById("loading").style.visibility = 'hidden';
                    alert(response.d); open_page('Historial.aspx');
                },
                failure: function () { /*alert("ERROR ");*/
                    document.getElementById("loading").style.visibility = 'hidden';
                }
            });
        }

        function autorizar(banfn, user) {
            //var banfn = "6"; 
            var sPageURL = decodeURIComponent(window.location.toString());
            //alert("Crear orden");
            var param = { banfn: banfn, user: user, url: sPageURL };
            $.ajax({
                type: "POST",
                url: "catalogos.asmx/autorizar",
                data: JSON.stringify(param),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                beforeSend: function (response) {
                    document.getElementById("loading").style.visibility = 'visible';
                },
                success: function (response) {
                    document.getElementById("loading").style.visibility = 'hidden';
                    alert(response.d); open_page('Historial.aspx');
                },
                failure: function () { /*alert("ERROR ");*/
                    document.getElementById("loading").style.visibility = 'hidden';
                }
            });
        }


        function rechazar(banfn, user) {
            var tr = $("#comm").append("<div style='position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(128, 128, 128, 0.5);'>"
                                            + "<div style='position: absolute; top: 50%; left: 50%; width: 400px; margin-left: -200px; height: 200px; margin-top: -100px; padding: 5px; background-color: white;'>"
                                                + "<div style='width:100%;height:100%;'>"
                                                    + "Agregue un comentario:"
                                                    + "<textarea id='comentarios' style='position: absolute;width:97%;left:1%;resize:none;top:25px;height:120px;'></textarea>"
                                                    + "<input type='button' value='Aceptar' class=\"btnCrear\"/ style='position: absolute;left:10%;bottom:10%;'onclick='confirmar(" + banfn + ",\"" + user + "\")'>"
                                                    + "<input type='button' value='Cancelar' class=\"btnCrear\"style='position: absolute;right:10%;bottom:10%;' onclick='cancelarA()'/>"
                                                + "</div>"
                                            + "</div>"
                                        + "</div>");
        }

        function cancelarA() {
            document.getElementById('comm').textContent = "";
        }

        function confirmar(banfn, user) {
            //var banfn = "6"; 
            var comentarios = document.getElementById("comentarios").textContent;
            comentarios = $("#comentarios").val();
            if (comentarios != "") {
                document.getElementById('comm').textContent = "";
                var sPageURL = decodeURIComponent(window.location.toString());
                //alert("Crear orden");
                var param = { banfn: banfn, user: user, url: sPageURL, comm: comentarios };
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/rechazar",
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    beforeSend: function (response) {
                        document.getElementById("loading").style.visibility = 'visible';
                    },
                    success: function (response) {
                        document.getElementById("loading").style.visibility = 'hidden';
                        alert(response.d); open_page('Historial.aspx');
                    },
                    failure: function () {
                        alert("ERROR ");
                        document.getElementById("loading").style.visibility = 'hidden';
                    }
                });
            }

        }

        function cancelar(banfn, user) {
            var tr = $("#comm").append("<div style='position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(128, 128, 128, 0.5);'>"
                                            + "<div style='position: absolute; top: 50%; left: 50%; width: 400px; margin-left: -200px; height: 200px; margin-top: -100px; padding: 5px; background-color: white;'>"
                                                + "<div style='width:100%;height:100%;'>"
                                                    + "Agregue un comentario:"
                                                    + "<textarea id='comentarios' style='position: absolute;width:97%;left:1%;resize:none;top:25px;height:120px;'></textarea>"
                                                    + "<input type='button' value='Aceptar' class=\"btnCrear\"/ style='position: absolute;left:10%;bottom:10%;'onclick='confirmarCC(" + banfn + ",\"" + user + "\")'>"
                                                    + "<input type='button' value='Cancelar' class=\"btnCrear\"style='position: absolute;right:10%;bottom:10%;' onclick='cancelarC()'/>"
                                                + "</div>"
                                            + "</div>"
                                        + "</div>");
        }

        function cancelarC() {
            document.getElementById('comm').textContent = "";
        }

        function confirmarCC(banfn, user) {
            //var banfn = "6"; 
            var comentarios = document.getElementById("comentarios").textContent;
            comentarios = $("#comentarios").val();
            if (comentarios != "") {
                document.getElementById('comm').textContent = "";
                var sPageURL = decodeURIComponent(window.location.toString());
                //alert("Crear orden");
                var param = { banfn: banfn, user: user, url: sPageURL, comm: comentarios };
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/cancelar",
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    beforeSend: function (response) {
                        document.getElementById("loading").style.visibility = 'visible';
                    },
                    success: function (response) {
                        document.getElementById("loading").style.visibility = 'hidden';
                        alert(response.d); open_page('Historial.aspx');
                    },
                    failure: function () {
                        alert("ERROR ");
                        document.getElementById("loading").style.visibility = 'hidden';
                    }
                });
            }

        }

        function modificar() {
            var banfn = getUrlParameter("banfn");
            open_page('reqsMod.aspx?banfn=' + banfn);
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


        function focuss(i, rows) {
            //document.getElementById('lblPrestaciones').textContent = "";
            document.getElementById('tr' + i).style.backgroundColor = 'lightgray';
            for (var k = 1; k < i; k++) {
                var par = k % 2;
                if (par == 0) {
                    document.getElementById('tr' + k).style.backgroundColor = '#8ebded';
                } else {
                    document.getElementById('tr' + k).style.backgroundColor = 'white';
                }
            }
            for (var k = i + 1; k < rows + 1; k++) {
                var par = k % 2;
                if (par == 0) {
                    document.getElementById('tr' + k).style.backgroundColor = '#8ebded';
                } else {
                    document.getElementById('tr' + k).style.backgroundColor = 'white';
                }
            }

            var imp = $("#imp" + i).text();

            var tipo = $("#tipo" + i).text();
            //if (tipo == "F") {
            if (imp == "K") {
                if (tipo == "F") {

                    document.getElementById('lblPrestaciones').textContent = "";

                    var tr = $("#lblPrestaciones").append("Posición[" + (i * 10) + "]<br>"
                                                                      + "<div><div style='text-align:right;width:900px;'></div><div  class='CSSTableGenerator' style='width:900px;'><table id='tablesp' style='width:inherit;'><tbody><tr><td>Pos</td>" + "<td>Servicio</td>" +
                                                                      "<td>Texto breve</td>" + "<td>Cantidad</td>" + "<td>Precio</td>" + "<td>Centro Coste</td><td>Cta. Mayor</td></tr>");


                    tabla_servicios($("#pos" + i).text());

                    var tr = $("#lblPrestaciones").append("</tbody></table></div></div>");
                } else {
                    document.getElementById('lblPrestaciones').textContent = "";

                    var tr = $("#lblPrestaciones").append("Posición[" + (i * 10) + "]<br>"
                                                                      + "<div><table id='tablesp' style='width:inherit;'><tbody>");


                    servicios($("#pos" + i).text());

                    var tr = $("#lblPrestaciones").append("</tbody></table></div>");
                }
            } else {
                if (imp == "A") {
                    document.getElementById('lblPrestaciones').textContent = "";

                    var tr = $("#lblPrestaciones").append("Posición[" + (i * 10) + "]<br>"
                                                                      + "<div><div style='text-align:right;width:400px;'></div><div  class='CSSTableGenerator' style='width:400px;'><table id='tablesp' style='width:inherit;'><tbody><tr><td>Pos</td>" + "<td>Activo</td>" +
                                                                       "<td>Cantidad</td></tr>");


                    tabla_serviciosA($("#pos" + i).text());

                    var tr = $("#lblPrestaciones").append("</tbody></table></div></div>");
                }
            }
            //} else {
            //    if (imp == "K") {
            //        var tr = $("#lblPrestaciones").append("Posición[" + (i * 10) + "]<br>"
            //                                                          + "<div><table id='tablesp' style='width:auto;'><tbody>");


            //        tabla_imputacion($("#pos" + i).text());
            //        var tr = $("#lblPrestaciones").append("</tbody></table></div>");
            //    }
            //}
        }
        function tabla_servicios(bnfpo) {//Agrega arreglo de materiales que coinciden con lo escrito

            var banfn = getUrlParameter("banfn");
            var param = { banfn: banfn, bnfpo: bnfpo };
            $.ajax({
                url: "catalogos.asmx/servicios",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    //var embeddedJsonObj = $.parseJSON(data.d);
                    var lista = data.d.split('|');
                    for (var j = 0; j < lista.length - 1; j += 8) {
                        var tr = $("#tablesp tbody").append("<tr>" +
                                                    "<td>" + lista[j] + "0</td>" +
                                                    "<td>" + lista[j + 1] + "</td>" +
                                                    "<td>" + lista[j + 2] + "</td>" +
                                                    "<td>" + lista[j + 3] + "</td>" +
                                                    "<td>" + lista[j + 4] + "</td>" +
                                                    "<td>" + lista[j + 5] + "</td>" +
                                                    "<td>" + lista[j + 6] + "</td>" +
                                                    //"<td>" + lista[j + 7] + "</td>" +
                                                     "</tr>");

                        //document.getElementById('serv' + (j + 1)).addEventListener('focusout', servicios, false);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });
        }
        function tabla_serviciosA(bnfpo) {//Agrega arreglo de materiales que coinciden con lo escrito
            var banfn = getUrlParameter("banfn");
            var param = { banfn: banfn, bnfpo: bnfpo };
            $.ajax({
                url: "catalogos.asmx/serviciosA",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    //var embeddedJsonObj = $.parseJSON(data.d);
                    var lista = data.d.split('|');
                    for (var j = 0; j < lista.length - 1; j += 4) {
                        var pos = j / 4;
                        pos++;
                        //var tr = $("#tablesp tbody").append("<tr id='trP" + pos + "' onclick='focussI(" + pos + "," + bnfpo + ")'>" +
                        var tr = $("#tablesp tbody").append("<tr id='trP" + pos + "'>" +
                                                    "<td>" + lista[j] + "0</td>" +
                                                    "<td>" + lista[j + 1] + "</td>" +
                                                    //"<td>" + lista[j + 2] + "</td>" +
                                                    "<td>" + lista[j + 3] + "</td>" +
                                                    //"<td>" + lista[j + 7] + "</td>" +
                                                     "</tr>");

                        //document.getElementById('serv' + (j + 1)).addEventListener('focusout', servicios, false);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });
        }
        function servicios(bnfpo) {//Agrega arreglo de materiales que coinciden con lo escrito

            var banfn = getUrlParameter("banfn");
            var param = { banfn: banfn, bnfpo: bnfpo };
            $.ajax({
                url: "catalogos.asmx/imputacion",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    //var embeddedJsonObj = $.parseJSON(data.d);
                    var lista = data.d.split('|');
                    for (var j = 0; j < lista.length - 1; j += 4) {
                        var s = "<tr>" +
                                                    "<td>Centro de coste:</td>" +
                                                    "<td><input type='text' value='" + lista[j] + "' disabled='disabled'/></td>" +
                                                    "<td>" + lista[j + 1] + "</td></tr><tr>" +
                                                    "<td>Cta de mayor:</td>" +
                                                    "<td><input type='text' value='" + lista[j + 2] + "' disabled='disabled'/></td>" +
                                                    "<td>" + lista[j + 3] + "</td></tr><tr>" +
                                                    //"<td>" + lista[j + 7] + "</td>" +
                                                     "</tr>"
                        var tr = $("#tablesp tbody").append(s);

                        //document.getElementById('serv' + (j + 1)).addEventListener('focusout', servicios, false);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });
        }

        function focussI(pos, bnfpo) {//

            document.getElementById('lblImputaciones').textContent = "";
            var tr = $("#lblImputaciones").append("Posición[" + (pos * 10) + "]<br>"
                                                                    + "<div><div style='text-align:right;width:600px;'></div><div  class='CSSTableGenerator' style='width:600px;'><table id='tablesi' style='width:inherit;'><tbody><tr><td>Pos</td>" +
                                                                   "<td>Cantidad</td><td>Cta. Mayor</td><td>Activo fijo</td></tr>");


            var banfn = getUrlParameter("banfn");
            var param = { banfn: banfn, bnfpo: bnfpo, serial: pos };
            $.ajax({
                url: "catalogos.asmx/imputacionA",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    //var embeddedJsonObj = $.parseJSON(data.d);
                    var lista = data.d.split('|');
                    for (var j = 0; j < lista.length - 1; j += 6) {
                        var tr = $("#tablesi tbody").append("<tr>" +
                                                        "<td>" + lista[j + 2] + "0</td>" +
                                                        "<td>" + lista[j + 3] + "</td>" +
                                                        "<td>" + lista[j + 4] + "</td>" +
                                                        "<td>" + lista[j + 5] + "</td>" +
                                                        "</tr>");

                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        }

        function tabla_imputacion(bnfpo) {//

            var banfn = getUrlParameter("banfn");
            var param = { banfn: banfn, bnfpo: bnfpo };
            $.ajax({
                url: "catalogos.asmx/imputacion",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                dataFilter: function (data) { return data; },
                success: function (data) {
                    //var embeddedJsonObj = $.parseJSON(data.d);
                    var lista = data.d.split('|');
                    for (var j = 0; j < lista.length - 1; j += 2) {
                        var tr = $("#tablesp tbody").append("<tr>" +
                                                        "<td>Centro de coste:</td>" + "<td><input id='costeP" + (j + 1) + "' value='" + lista[j] + "' disabled  /></td>" +
                                                        "<tr>" +
                                                        "<td>Cuenta de mayor:</td>" + "<td><input id='ctaP" + (j + 1) + "' value='" + lista[j + 1] + "' disabled /></td>" +
                                                        "</tr>");


                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert(textStatus);
                }
            });
        }

        function agregar(banfn, user) {
            var tr = $("#comm").append("<div style='position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(128, 128, 128, 0.5);'>"
                                            + "<div style='position: absolute; top: 50%; left: 50%; width: 400px; margin-left: -200px; height: 200px; margin-top: -100px; padding: 5px; background-color: white;'>"
                                                + "<div style='width:100%;height:100%;'>"
                                                    + "Escriba el comentario:"
                                                    + "<textarea id='comentarios' style='position: absolute;width:97%;left:1%;resize:none;top:25px;height:120px;'></textarea>"
                                                    + "<input type='button' value='Aceptar' class=\"btnCrear\"/ style='position: absolute;left:10%;bottom:10%;'onclick='confirmarC(" + banfn + ",\"" + user + "\")'>"
                                                    + "<input type='button' value='Cancelar' class=\"btnCrear\"style='position: absolute;right:10%;bottom:10%;' onclick='cancelarC()'/>"
                                                + "</div>"
                                            + "</div>"
                                        + "</div>");
        }

        function confirmarC(banfn, user) {
            //var banfn = "6"; 
            var comentarios = document.getElementById("comentarios").textContent;
            comentarios = $("#comentarios").val();
            if (comentarios != "") {
                document.getElementById('comm').textContent = "";
                var sPageURL = decodeURIComponent(window.location.toString());
                //alert("Crear orden");
                var param = { banfn: banfn, user: user, url: sPageURL, comm: comentarios };
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/addComent",
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    beforeSend: function (response) {
                        document.getElementById("loading").style.visibility = 'visible';
                    },
                    success: function (response) {
                        document.getElementById("loading").style.visibility = 'hidden';
                        //alert(response.d);
                        //open_page(response.d);
                        location.reload();
                    },
                    failure: function () {
                        alert("ERROR ");
                        document.getElementById("loading").style.visibility = 'hidden';
                    }
                });
            }

        }

    </script>
    <style type="text/css">
        .submit {
            border: 0px solid #563d7c;
            color: black;
            padding: 5px 10px 5px 25px;
            background-color: transparent;
            height: 100%;
            width: 90%;
            left: 10%;
            text-align: left;
        }

            .submit hover input {
                cursor: pointer;
            }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div>
            <%--<div class='CSSTableGenerator' style="width: 100%;">--%>
            <asp:Label runat="server" ID="lblTabla"></asp:Label>
            <%--</div>--%>
            <br />
            <br />
            <div id="lblPrestaciones" style="width: 100%;"></div>

            <br />
            <br />
            <div id="lblImputaciones" style="width: 100%;"></div>

            <br />
            <asp:FileUpload ID="fileUp" runat="server" AllowMultiple="true" Width="300" />
            <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="Guardar adjuntos" CssClass="btnCrear" /><br />

            <br />
            <%--Lista de adjuntos:<br />--%>
            <%--<asp:Label runat="server" ID="lblAdj"></asp:Label>--%>
        </div>
        <%--<div style="background-color:white;">
            <input id="fileUpload" type="file" />
                <input id="btnUploadFile" type="button" value="Upload File" />
        </div>--%>
    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="width: 1000px; text-align: right;">
        <asp:Label runat="server" ID="lblBotones"></asp:Label>
    </div>
    <div id="loading" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(128, 128, 128, 0.5); visibility: hidden">
        <div style="position: absolute; top: 50%; left: 50%; width: 300px; margin-left: -150px; height: 200px; margin-top: -100px; padding: 5px; background-color: white;">
            Procesando...
                <img src="images/ajax_loader_blue_512.gif" style="top: 40%; left: 40%; position: inherit; width: 20%;" />
        </div>
    </div>
    <div id="comm"></div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">

    <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
    <h1>Visualizar solicitud de compra</h1>
    <br />
    <br />
    <asp:Label runat="server" ID="lblEban"></asp:Label>

</asp:Content>
