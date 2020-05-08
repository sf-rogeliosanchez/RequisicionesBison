<%@ Page Title="Modificar requisición" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="reqsMod.aspx.cs" Inherits="T259Prueba08.reqsMod" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--<title>Crear solicitud</title>--%>
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
    <link href="css/StyleTable.css" rel="stylesheet" />
    <link href="css/StyleTable2.css" rel="stylesheet" />
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
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        var materiales = [];

        addEventListener('load', inicio, false); //Correr método inicio al cargar la página

        function inicio(e) {

        }

        function cantidades(id) {//Verifica que el contenido sea numérico
            var patron = /^[0-9]*\.?[0-9]+$/;
            //var id = evt.target.id;
            var valor = $("#" + id).val();
            if (valor.length > 0) {
                if (patron.test(valor))
                    valor = valor;
                else {
                    document.getElementById(id).value = "";
                }
            }

        }

        function completa(campo, tamanio) {//completa el material con ceros si es númerico
            var patron = /^[0-9]*\.?[0-9]+$/;
            var valor = campo;
            if (valor.length > 0) {
                if (patron.test(valor)) {
                    valor = valor;
                    var len = valor.length;
                    for (var i = len ; i < tamanio; i++) {
                        valor = "0" + valor;
                    }
                }
                else {
                    //alert('No se ingresó un valor entero');
                }
                return valor;
            } else {
                return '';
            }
        }

        function fecha(id) {
            var d = new Date();
            var dia = d.getDate();
            var mes = d.getMonth() + 1;
            var anio = d.getFullYear();

            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                if (dia < 10) {
                    document.getElementById(id).value = anio + "-" + mes + "-0" + dia;
                } else {
                    document.getElementById(id).value = anio + "-" + mes + "-" + dia;
                }
            } else {
                if (dia < 10) {
                    document.getElementById(id).value = "0" + dia + "/" + mes + "/" + anio;
                } else {
                    document.getElementById(id).value = dia + "/" + mes + "/" + anio;
                }
            }
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

        function fechas(fecha) {
            //var nav = navigator.appName;
            //alert(nav);
            var fechaa = fecha.split("-");
            var fechab = fecha.split("/");
            var fechac = fecha.split(".");
            var fechaFull = "";
            if (fechaa.length == 3) {
                fechaFull = fechaa[2] + "-" + fechaa[1] + "-" + fechaa[0];
            } else if (fechab.length == 3) {
                fechaFull = fechab[2] + "-" + fechab[1] + "-" + fechab[0];
            } else {
                fechaFull = fechac[2] + "-" + fechac[1] + "-" + fechac[0];
            }
            return fechaFull;
        }

        function crear(user, cont) {
            var tabla = "";
            var referencia = document.getElementById("referencia").value;
            var banfn = getUrlParameter('banfn');
            if (referencia.trim() != "") {
                for (var i = 1; i <= cont; i++) {

                    var tipo = document.getElementById("tipo" + i).value;
                    var id;

                    if (tipo == 'S') {
                        //SERVICIOS
                        var arr = document.getElementById('serv' + i).value.split(' - ');
                        if (arr.length > 0) {
                            id = arr[0];
                        }
                    } else if (tipo == 'A') {
                        //ACTIVOS
                        var arr = document.getElementById('acti' + i).value.split(' - ');
                        if (arr.length > 0) {
                            id = arr[0];
                        }
                    } else if (tipo == 'M') {
                        //MATERIAL
                        var arr = document.getElementById('mate' + i).value.split(' - ');
                        if (arr.length > 0) {
                            id = arr[0];
                        }
                    } else {
                        var arr = document.getElementById('impu' + i).value.split(' - ');
                        if (arr.length > 0) {
                            id = arr[0];
                        }
                    }
                    var preis;
                    var gr_a;
                    var quan;
                    var date;
                    var coste = "";
                    var cuenta = "";
                    var waers;
                    var comprador = "";
                    //if (id != "") {
                    descr = document.getElementById("desc" + i).value;
                    preis = document.getElementById("preis" + i).value;
                    gr_a = document.getElementById("gr_a" + i).value;
                    quan = document.getElementById("quan" + i).value;
                    date = document.getElementById("date" + i).value;
                    waers = document.getElementById("waers" + i).value;

                    if (document.getElementById("comp2") != null) {
                        comprador = document.getElementById("comp2").value;
                    }

                    var comentario = document.getElementById("TextArea1").value

                    if (tipo == "S" | tipo == "I") {
                        coste = document.getElementById("coste" + i).value;
                        var iii = coste.split(" - ");
                        coste = iii[0];
                        cuenta = document.getElementById("cta" + i).value;
                        var ii = cuenta.split(" - ");
                        cuenta = ii[0];
                    }
                    if (tipo == "A") {
                        //cuenta = document.getElementById("cta").value;
                        //var ii = cuenta.split(" - ");
                        //cuenta = ii[0];
                    }
                    gr_a = document.getElementById("gr_a" + i).value;
                    var iiii = gr_a.split(" | ");
                    gr_a = iiii[0];

                    //if (document.getElementById("coste").value == undefined) {
                    //    coste = "";
                    //} else {
                    //    coste = document.getElementById("coste").value;
                    //}
                    //if (document.getElementById("cta").value == undefined) {
                    //    cuenta = "";
                    //} else {
                    //    cuenta = document.getElementById("cta").value;
                    //}

                    var valores = tipo + "|" + id + "|" + descr + "|" + preis + "|" + gr_a + "|" + quan + "|" + date + "|" + coste + "|" + cuenta + "|" + waers + "|";
                    tabla += valores;
                }
                //if (tipo == 's') {
                var sPageURL = decodeURIComponent(window.location.toString());
                var param = { b: banfn, url: sPageURL, valores: tabla, us: user, comentario: comentario, comp: comprador, refer: referencia };

                $.ajax({
                    type: "POST",
                    url: "reqsMod.aspx/crear2",
                    //data: data,
                    data: JSON.stringify(param),
                    contentType: "application/json; charset=utf-8",
                    dataType: false,
                    processData: false,
                    async: true,
                    cache: false,
                    beforeSend: function (response) {
                        document.getElementById("loading").style.visibility = 'visible';
                    },
                    success: function (response) {
                        document.getElementById("loading").style.visibility = 'hidden';
                        try {
                            var mensaje = response.d;
                            var status = mensaje.charAt(0);
                        } catch (e) {
                            response = JSON.parse(response)
                            var mensaje = response.d;
                            var status = mensaje.charAt(0);
                        }
                        if (status == "B") {
                            //subir();
                            alert("Requisición modificada.");
                            //$(location).attr('href', '/Historial.aspx');
                            open_page('Historial.aspx');
                        } else {
                            //if (confirm(response.d + "\n¿Desea crear aún así la requisición?")) {
                            if (alert(response.d)) {
                                //crearError();
                                //open_page('/Historial.aspx');
                            } else {
                            }
                        }
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
            } else {
                alert("El campo de referencia es obligatorio");
            }
            //}

            //alert(id + "," + preis + "," + gr_a + "," + quan + "," + date);
            //}
        }
        function subir() {
            //var data = new FormData();

            //var files = $("#fileUpload").get(0).files;

            //// Add the uploaded image content to the form data collection
            //if (files.length > 0) {
            //    data.append("UploadedImage", files[0]);
            //    //data.append("")
            //}

            //// Make Ajax request with the contentType = false, and procesDate = false
            //var ajaxRequest = $.ajax({
            //    type: "POST",
            //    url: "/api/FileUploadController/uploadfile",
            //    contentType: false,
            //    processData: false,
            //    data: data
            //});

            //ajaxRequest.done(function (xhr, textStatus) {
            //    // Do other operation

            //});
            //ajaxRequest.error(function (xhr, textStatus) {
            //    // Do other operation
            //    alert(textStatus);
            //});
        }

    </script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <%--<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>
    <script src="Scripts/jquery-ui-1.11.4.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script>

        function autocompleta_matnr(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { matnr: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_material",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = JSON.parse(data.d)
                            response($.map(embeddedJsonObj, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                        //cambiar(ui.item.value, id);
                        var val = ui.item.value;
                        var arr = val.split(' - ');
                        $('#mate').text = arr[0];
                        //document.getElementById('label2').textContent = "";
                        //document.getElementById('label2').textContent = arr[1];
                        llena_material(arr[0]);
                    }
                },
                minLength: 2 //Longitud mínima para calcularse
            });
        }



        function autocompleta_cta(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { saknr: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_cuentas",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            try{
                                var embeddedJsonObj = JSON.parse(data.d);
                            } catch (e) {
                                var embeddedJsonObj = data.d;
                            }
                            response($.map(embeddedJsonObj, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                        //cambiar(ui.item.value, id);
                    }
                },
                minLength: 2
            });
        }

        function autocompleta_coste(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { kostl: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_costes",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = JSON.parse(data.d)
                            response($.map(embeddedJsonObj, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    if (ui.item) {
                    }
                },
                minLength: 2
            });
        }

        function autocompleta_GR_A(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { matkl: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_grupos_a",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = JSON.parse(data.d)
                            response($.map(embeddedJsonObj, function (item) {
                                return {
                                    value: item
                                }
                            }))
                        },
                        error: function (XMLHttpRequest, textStatus, errorThrown) {
                            //alert(textStatus);
                        }
                    });
                },
                select: function (event, ui) {
                    ////if (ui.item) {
                    ////    //cambiar(ui.item.value, id);
                    ////    var val = ui.item.value;
                    ////    var arr = val.split(' - ');
                    ////    $('#mate').text = arr[0];
                    ////    //document.getElementById('label2').textContent = "";
                    ////    //document.getElementById('label2').textContent = arr[1];
                    ////    llena_material(arr[0]);
                    ////}
                },
                minLength: 2 //Longitud mínima para calcularse
            });
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

        function subir1() {
            var files = document.getElementById('files').files;
            var fill = "";
            for (var q = 0; q < files.length; q++) {
                fill += files[q].name + "\n";
            }
            alert(fill);

            files = document.getElementById('fileUp').files;
            fill = "";
            for (var q = 0; q < files.length; q++) {
                fill += files[q].name + "\n";
            }
            alert(fill);
        }

        function mensaje() {
            alert("MENsaje desde ASP.");
        }
        function cambiaSel(i, count) {
            var sel = document.getElementById("waers" + i).value;
            for (var j = 1; j <= count; j++) {
                document.getElementById("waers" + j).value = sel;
            }
        }

        function copiarCoste(cont) {
            //var check = document.getElementById("chkCoste").checked;
            //if (check == true) {
                var costeGral = document.getElementById("coste1").value;

                for (var i = 2; i <= cont; i++) {
                    //var costeTemp = document.getElementById("coste" + i).value;
                    //if (costeTemp.trim() == "") {
                        document.getElementById("coste" + i).value = costeGral;
                    //}
                }
            //}
        }

        function copiarCta(cont) {
            //var check = document.getElementById("chkCta").checked;
            //if (check == true) {
                var ctaGral = document.getElementById("cta1").value;

                for (var i = 2; i <= cont; i++) {
                    //var ctaTemp = document.getElementById("cta" + i).value;
                    //if (ctaTemp.trim() == "") {
                        document.getElementById("cta" + i).value = ctaGral;
                    //}
                }
            //}
        }

    </script>

    <style>
        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto;
            /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }

        * html .ui-autocomplete {
            height: 100px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="form1" runat="server">
        <div style="width: 98%;">
            <%--<table>
                <tr>
                    <td>--%>
            <asp:Label runat="server" ID="lblTabla"></asp:Label><br />
            <%--<br />
                    </td>
                    <td>
                        <asp:TextBox ID="txtMate" runat="server" Enabled="false"></asp:TextBox><br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Precio:</td>
                    <td>
                        <asp:TextBox ID="txtPreis" runat="server" Enabled="false"></asp:TextBox></td>

                    <td>
                        <asp:Label runat="server" ID="lblCost"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCost" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Grupo de artículo:</td>
                    <td>
                        <asp:TextBox ID="txtGrupo" runat="server" Enabled="false"></asp:TextBox></td>

                    <td>
                        <asp:Label runat="server" ID="lblCta"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCta" runat="server" Visible="false"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Cantidad:</td>
                    <td>
                        <asp:TextBox ID="txtCant" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>Fecha de entrega:</td>
                    <td>
                        <asp:TextBox ID="txtFecha" runat="server"></asp:TextBox></td>
                </tr>
            </table>--%>
            Comentarios:<br />
            <textarea id="TextArea1" rows="5" style="width: 100%; resize: none;"></textarea><br />

            <div>
                <br />
                <br />
                <%--<select  id="sel1" onchange="cambiaSel()">
                    <option value="MXN" selected="selected">MXN</option>
                    <option value="USD">USD</option>
                </select>
                <select id="sel2">
                    <option selected="selected" value="MXN">MXN</option>
                    <option value="USD">USD</option>
                </select>--%>
                <asp:FileUpload ID="fileUp" runat="server" AllowMultiple="true" Width="300" />
                <asp:Button runat="server" ID="btnUpload" OnClick="btnUpload_Click" Text="Guardar adjuntos" CssClass="btnCrear" /><br />
                <br />
                <asp:Label runat="server" ID="lblReturn" Text=""></asp:Label>
            </div>
            <asp:Label runat="server" ID="lblAdj"></asp:Label>
        </div>

    </form>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="width: 98%; text-align: right;">
        <asp:Label runat="server" ID="lblBotones"></asp:Label>
    </div>
    <div style="width: 98%;">
        <asp:Label runat="server" ID="lblComprador"></asp:Label>
    </div>
    <div id="loading" style="position: absolute; top: 0; left: 0; width: 100%; height: 100%; background-color: rgba(128, 128, 128, 0.5); visibility: hidden">
        <div style="position: absolute; top: 50%; left: 50%; width: 300px; margin-left: -150px; height: 200px; margin-top: -100px; padding: 5px; background-color: white;">
            Procesando...
                <img src="images/ajax_loader_blue_512.gif" style="top: 40%; left: 40%; position: inherit; width: 20%;" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolder3" runat="server">
    <asp:Label ID="lblUsuario" runat="server" Text="Label"></asp:Label>
    <h1>Modificar requisición</h1>
    <br />
    <br />
    <asp:Label runat="server" ID="lblEban"></asp:Label>
</asp:Content>
