﻿<%@ Page Title="Crear requisición" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="ReqsCrea.aspx.cs" Inherits="T259Prueba08.ReqsCrea" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>Crear solicitud</title>
    <link href="css/Site.css" rel="stylesheet" />
    <link href="css/StyleBtn.css" rel="stylesheet" />
    <link href="css/StyleTable.css" rel="stylesheet" />
    <script src="//ajax.googleapis.com/ajax/libs/jquery/1.11.1/jquery.min.js"></script>
    <link href="aut/tautocomplete.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script>
        var servicio = "<input type='text' id='serv' onchange='borrarS()'/>";
        var activo = "<input type='text' id='acti' onchange='borrarA()'/>";
        var material = "<input id='mate' style='width:200px;' onchange='borrar()'/>";
        var imputa = "<input id='impu' type='text'/>";
        addEventListener('load', inicio, false); //Correr método inicio al cargar la página

        function inicio() {
            $('#label').append(servicio);
            autocompleta_s('serv');
            fecha('date');
        }

        function borrarS() {
            document.getElementById("desc").value = "";
            document.getElementById("preis").value = "";
            document.getElementById("gr_a").value = "";

            var val = document.getElementById("serv").value.replace(/^\s+|\s+$/g, '');
            //var arr = val.split(' - ');
            $('#serv').text = val;//arr[0];
            //document.getElementById('label2').textContent = "";
            //document.getElementById('label2').textContent = arr[1];
            llena_servicio(val);
        }

        function borrarA() {
            document.getElementById("desc").value = "";
            document.getElementById("preis").value = "";
            document.getElementById("gr_a").value = "";

            var val = document.getElementById("acti").value.replace(/^\s+|\s+$/g, '');
            //var arr = val.split(' - ');
            $('#acti').text = val;//arr[0];
            //document.getElementById('label2').textContent = "";
            //document.getElementById('label2').textContent = arr[1];
            llena_activo(val);
        }

        function cambiatipo(valor) {
            document.getElementById('label').textContent = "";
            document.getElementById("desc").value = "";
            document.getElementById("preis").value = "";
            document.getElementById("gr_a").value = "";

            if (valor == 's') {
                $('#label').append(servicio);
                autocompleta_s('serv');
                document.getElementById("preis").disabled = false;
                document.getElementById("waers").disabled = true;
                document.getElementById("gr_a").disabled = true;
                document.getElementById("uom").disabled = true;
                document.getElementById("waers2").hidden = true;
                document.getElementById("waers").hidden = false;
                document.getElementById("addLines").disabled = false;
            }
            if (valor == 'a') {
                $('#label').append(activo);
                autocompleta_a('acti');
                document.getElementById("preis").disabled = true;
                document.getElementById("waers").disabled = true;
                document.getElementById("gr_a").disabled = true;
                document.getElementById("uom").disabled = true;
                document.getElementById("waers2").hidden = true;
                document.getElementById("waers").hidden = false;
                document.getElementById("addLines").disabled = false;
            }
            if (valor == 'm') {
                $('#label').append(material);
                //autocompleta_m('mate');
            }
            if (valor == 'i') {
                $('#label').append(imputa);
                //autocompleta_i('impu');
                document.getElementById("preis").disabled = false;
                document.getElementById("waers").disabled = false;
                document.getElementById("gr_a").disabled = true;
                document.getElementById("uom").disabled = false;
                document.getElementById("waers2").hidden = false;
                document.getElementById("waers").hidden = true;
                document.getElementById("addLines").disabled = false;
                autocompleta_GR_A("gr_a");
            }
        }
        function cantidades(id) { //Verifica que el contenido sea numérico
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
        function fecha(id) {
            var d = new Date();
            var dia = d.getDate();
            var mes = d.getMonth() + 1;
            var anio = d.getFullYear();

            if (dia < 10) {
                dia = "0" + dia;
            }
            if (mes < 10) {
                mes = "0" + mes;
            }

            if (navigator.userAgent.toLowerCase().indexOf('chrome') > -1) {
                //if (dia < 10) {
                //    document.getElementById(id).value = anio + "-" + mes + "-0" + dia;
                //} else {
                document.getElementById(id).value = anio + "-" + mes + "-" + dia;
                //}
            } else {
                //if (dia < 10) {
                //    document.getElementById(id).value = "0" + dia + "/" + mes + "/" + anio;
                //} else {
                document.getElementById(id).value = dia + "/" + mes + "/" + anio;
                //}
            }
        }

        function crear() {
            if (pos > 0) {
                var referencia = document.getElementById("referencia").value;
                if (referencia.trim() != "") {
                    var comentario = document.getElementById("TextArea1").value;
                    var usuario = '<%=Session["UserName"] %>';
                    var sPageURL = decodeURIComponent(window.location.toString());
                    var param = { url: sPageURL, valores: tabla, comentario: comentario, r: referencia, us: usuario};
                    $.ajax({
                        type: "POST",
                        url: "ReqsCrea.aspx/crear2",
                        //data: data,
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
                            var mensaje = response.d;
                            var status = mensaje.charAt(0);
                            if (status == "B") {
                                alert("Requisición creada.");
                                //$(location).attr('href', '/Historial.aspx');
                                open_page('Historial.aspx');
                            } else {
                                if (confirm(response.d + "\n¿Desea crear aún así la requisición?")) {
                                    crearError();
                                    //open_page('/Historial.aspx');
                                } else {
                                }
                            }
                        },
                        failure: function (XMLHttpRequest, textStatus, errorThrown) {
                            document.getElementById("loading").style.visibility = 'hidden';
                            alert(textStatus);
                        }
                    });
                } else {
                    alert("El campo de referencia es obligatorio");
                }
            }
            ////var tipo = document.getElementById("Tipo").value;
            ////var id;

            ////if (tipo == 's') {
            ////    //SERVICIOS
            ////    var arr = document.getElementById('serv').value.split(' - ');
            ////    if (arr.length > 0) {
            ////        id = arr[0];
            ////    }
            ////} else if (tipo == 'a') {
            ////    //ACTIVOS
            ////    var arr = document.getElementById('acti').value.split(' - ');
            ////    if (arr.length > 0) {
            ////        id = arr[0];
            ////    }
            ////} else {
            ////    //MATERIAL
            ////    var arr = document.getElementById('mate').value.split(' - ');
            ////    if (arr.length > 0) {
            ////        id = arr[0];
            ////    }
            ////}
            ////var preis;
            ////var gr_a;
            ////var quan;
            ////var date;
            ////var waers;
            ////if (id != "") {
            ////    id = completa(id, 18);
            ////    descr = document.getElementById("desc").value;
            ////    preis = document.getElementById("preis").value;
            ////    gr_a = document.getElementById("gr_a").value;
            ////    quan = document.getElementById("quan").value;
            ////    date = document.getElementById("date").value;
            ////    waers = document.getElementById("waers").value;

            ////    var comentario = document.getElementById("TextArea1").value

            ////    var valores = descr + "|" + preis + "|" + gr_a + "|" + quan + "|" + date + "|" + waers + "|";

            ////    if (id != "" && preis != "" && gr_a != "") {

            ////        //if (tipo == 's') {
            ////        var sPageURL = decodeURIComponent(window.location.toString());
            ////        var param = { tipo: tipo, url: sPageURL, valores: valores, id: id, comentario: comentario };

            ////        $.ajax({
            ////            type: "POST",
            ////            url: "ReqsCrea.aspx/crear",
            ////            //data: data,
            ////            data: JSON.stringify(param),
            ////            contentType: "application/json; charset=utf-8",
            ////            dataType: "json",
            ////            async: true,
            ////            cache: false,
            ////            beforeSend: function (response) {
            ////                document.getElementById("loading").style.visibility = 'visible';
            ////            },
            ////            success: function (response) {
            ////                document.getElementById("loading").style.visibility = 'hidden';
            ////                var mensaje = response.d;
            ////                var status = mensaje.charAt(0);
            ////                if (status == "B") {
            ////                    alert("Requisición creada.");
            ////                    //$(location).attr('href', '/Historial.aspx');
            ////                    open_page('Historial.aspx');
            ////                } else {
            ////                    if (confirm(response.d + "\n¿Desea crear aún así la requisición?")) {
            ////                        crearError();
            ////                        //open_page('/Historial.aspx');
            ////                    } else {
            ////                    }
            ////                }
            ////            },
            ////            failure: function (XMLHttpRequest, textStatus, errorThrown) {
            ////                document.getElementById("loading").style.visibility = 'hidden';
            ////                alert(textStatus);
            ////            }
            ////        });
            ////        //}

            ////    }

            ////    //alert(id + "," + preis + "," + gr_a + "," + quan + "," + date);
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

        var pos = 0;
        var tabla = "";
        function addLine() {
            var tipo = document.getElementById("Tipo").value;
            var id;
            var uom = "-";
            var Vuom = "";

            if (tipo == 's') {
                //SERVICIOS
                var arr = document.getElementById('serv').value.split(' - ');
                if (arr.length > 0) {
                    id = arr[0];
                }
            } else if (tipo == 'a') {
                //ACTIVOS
                var arr = document.getElementById('acti').value.split(' - ');
                if (arr.length > 0) {
                    id = arr[0];
                }
            } else if (tipo == 'm') {
                //MATERIAL
                var arr = document.getElementById('mate').value.split(' - ');
                if (arr.length > 0) {
                    id = arr[0];
                }
            } else {
                //MATERIAL
                var arr = document.getElementById('impu').value.split(' - ');
                if (arr.length > 0) {
                    id = arr[0];
                    document.getElementById("desc").value = id;
                }
                uom = document.getElementById("uom").value.toUpperCase();
                //Vuom = verificaUOM(uom);
            }
            var preis;
            var gr_a;
            var quan;
            var date;
            var waers;

            if (uom != "") {
                if (id != "") {
                    id = completa(id, 18);
                    descr = document.getElementById("desc").value;
                    preis = document.getElementById("preis").value;
                    gr_a = document.getElementById("gr_a").value.toUpperCase().split("-")[0];
                    quan = document.getElementById("quan").value;
                    date = document.getElementById("date").value;
                    if (tipo == 'i') {
                        waers = document.getElementById("waers2").value.toUpperCase();
                    } else {
                        waers = document.getElementById("waers").value;
                    }
                    var unidad = document.getElementById("uom").value.toUpperCase();

                    var comentario = document.getElementById("TextArea1").value

                    var valores = tipo + "|" + id + "|" + descr + "|" + preis + "|" + gr_a + "|" + quan + "|" + date + "|" + waers + "|" + unidad + "|";

                    if (id != "" && preis != "" && quan != "") {
                        ////if (id != "" && preis != "" && gr_a != "" && quan != "") {
                        tabla += valores;
                        pos++;
                        if (pos == 1) {
                            var tr = $("#posiciones tbody").append("<tr><td>Pos</td>" +
                                                                        "<td>Id</rd><td>Precio</td><td>Mon.</td>" +
                                                                        "<td>Gpo.Articulo</rd><td>Cantidad</td><td>Fecha</td><tr>");
                        }
                        var tr = $("#posiciones tbody").append("<tr><td>" + (pos * 10) + "</td>" +
                        "<td>" + id + "</rd><td>" + preis + "</td><td>" + waers + "</td>" +
                            "<td>" + gr_a + "</rd><td>" + quan + "</td><td>" + date + "</td><tr>");


                        //document.getElementById('id').val = "";
                        document.getElementById('desc').value = "";
                        document.getElementById('preis').value = "";
                        document.getElementById('gr_a').value = "";
                        document.getElementById('quan').value = "";
                        document.getElementById('waers').value = "";
                        document.getElementById('uom').value = "";
                        if (tipo == 's') {
                            //SERVICIOS
                            document.getElementById('serv').value = "";

                        } else if (tipo == 'a') {
                            //ACTIVOS
                            document.getElementById('acti').value = "";

                        } else if (tipo == 'm') {
                            //MATERIAL
                            document.getElementById('mate').value = "";

                        } else if (tipo == 'i') {
                            //MATERIAL
                            document.getElementById('impu').value = "";

                        }
                    }

                }
            }
        }


        function verificaUOM(id) {
            var uom = document.getElementById(id).value;
            var resp = "";
            var param = { uom: uom.toUpperCase() };
            $.ajax({
                url: "catalogos.asmx/verificaUOM",
                data: JSON.stringify(param),
                dataType: "json",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    resp = response.d;
                    if (resp == "") {
                        alert("La unidad de medida no existe");
                        document.getElementById("addLines").disabled = true;
                        document.getElementById("uom").value = "";
                    } else {
                        document.getElementById("addLines").disabled = false;
                        document.getElementById("uom").value = resp;
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //alert(textStatus);
                }
            });

        }

    </script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css" />
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <%--<script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>--%>
    <script src="Scripts/jquery-ui-1.11.4.js"></script>
    <link rel="stylesheet" href="/resources/demos/style.css" />
    <script>

        function autocompleta_m(id) {
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
                            var embeddedJsonObj = $.parseJSON(data.d)
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


        function autocompleta_s(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { texto: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_servicio",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = $.parseJSON(data.d)
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
                        var val = ui.item.value;
                        var arr = val.split(' - ');
                        $('#serv').text = arr[0];
                        llena_servicio(arr[0]);
                    }
                },
                minLength: 2 //Longitud mínima para calcularse
            });
        }

        function autocompleta_a(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { texto: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_activo",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = $.parseJSON(data.d)
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
                        var val = ui.item.value;
                        var arr = val.split(' - ');
                        $('#serv').text = arr[0];
                        llena_activo(arr[0]);
                    }
                },
                minLength: 2 //Longitud mínima para calcularse
            });
        }

        function autocompleta_i(id) {
            $("#" + id).autocomplete({
                source: function (request, response) {
                    var para = $('#' + id).val();
                    var param = { texto: para.toUpperCase() };
                    $.ajax({
                        url: "catalogos.asmx/lista_imputa",
                        data: JSON.stringify(param),
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        dataFilter: function (data) { return data; },
                        success: function (data) {
                            var embeddedJsonObj = $.parseJSON(data.d)
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
                        var val = ui.item.value;
                        var arr = val.split(' - ');
                        $('#serv').text = arr[0];
                        llena_imputa(arr[0]);
                    }
                },
                minLength: 2 //Longitud mínima para calcularse
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
                            var embeddedJsonObj = $.parseJSON(data.d)
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


        function llena_material(valor) {

            var matnr = valor;
            document.getElementById('desc').val = "";
            document.getElementById('preis').val = "";
            document.getElementById('gr_a').val = "";
            document.getElementById('waers').val = "";

            if (matnr != "") {
                matnr = matnr.toUpperCase();
                matnr = completa(matnr, 18);
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/materialDesc",
                    data: "{matnr:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        document.getElementById('desc').val = "";
                        document.getElementById("desc").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/materialPrecio",
                    data: "{matnr:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById('preis').val = "";
                        document.getElementById("preis").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/materialGrupo",
                    data: "{matnr:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById('gr_a').val = "";
                        document.getElementById("gr_a").value = response.d;
                    },
                    failure: function () { }
                });


                document.getElementById('waers').val = "";
                document.getElementById("waers").value = "MXN";
            }

        }


        function llena_servicio(valor) {

            var matnr = valor;
            document.getElementById('desc').val = "";
            document.getElementById('preis').val = "";
            document.getElementById('gr_a').val = "";
            document.getElementById('waers').val = "";

            if (matnr != "") {
                matnr = matnr.toUpperCase();
                matnr = matnr.split('-')[0];
                matnr = completa(matnr, 18);
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/servicioDesc",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        document.getElementById("desc").value = response.d;
                    },
                    failure: function () {
                        alert("ERROR ");
                    }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/servicioPrecio",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("preis").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/servicioGrupo",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("gr_a").value = response.d;
                    },
                    failure: function () { }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/servicioMoneda",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("waers").value = response.d;
                    },
                    failure: function () { }
                });
            }
        }

        function llena_activo(valor) {

            var matnr = valor;
            document.getElementById('desc').val = "";
            document.getElementById('preis').val = "";
            document.getElementById('gr_a').val = "";
            document.getElementById('waers').val = "";


            if (matnr != "") {
                matnr = matnr.toUpperCase();
                matnr = completa(matnr, 18);
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/activoDesc",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        document.getElementById("desc").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/activoPrecio",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("preis").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/activoGrupo",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("gr_a").value = response.d;
                    },
                    failure: function () { }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/activoMoneda",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("waers").value = response.d;
                    },
                    failure: function () { }
                });
            }

        }

        function llena_imputa(valor) {

            var matnr = valor;
            document.getElementById('desc').val = "";
            document.getElementById('preis').val = "";
            document.getElementById('gr_a').val = "";
            document.getElementById('waers').val = "";


            if (matnr != "") {
                matnr = matnr.toUpperCase();
                matnr = completa(matnr, 18);
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/imputaDesc",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        document.getElementById("desc").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/imputaPrecio",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("preis").value = response.d;
                    },
                    failure: function () { /*alert("ERROR ");*/ }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/imputaGrupo",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("gr_a").value = response.d;
                    },
                    failure: function () { }
                });
                $.ajax({
                    type: "POST",
                    url: "catalogos.asmx/imputaMoneda",
                    data: "{serv:'" + matnr + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (response) {
                        //var id = evt.target.id;
                        //var len = id.length;
                        //var num = id.charAt(len - 1);
                        document.getElementById("waers").value = response.d;
                    },
                    failure: function () { }
                });
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
        <div>
            Referencia de proyecto:
            <input type="text" id="referencia" style="width:370px;"/>
            <br />
            <br />
            <hr />

            <table>
                <tr>
                    <td>
                        <select id="Tipo" onchange="cambiatipo(this.value)" style="width: 100%;">
                            <option value="s">Servicio</option>
                            <option value="a">Activo</option>
                            <option value="i">Material</option>
                            <%--<option value="m">Material</option>--%>
                        </select></td>
                    <td>
                        <label id="label"></label>
                    </td>
                    <td>
                        <label id="label2"></label>
                    </td>
                </tr>
                <tr>
                    <td>Precio:</td>
                    <td>
                        <%--<input id="Text1" disabled="disabled" onblur="cantidades(this.id)"  /></td>--%>
                        <input id="preis"  onblur="cantidades(this.id)"  /></td>
                    <td>Moneda:</td>
                    <td>
                        <input id="waers" disabled="disabled" style="width: 40px" />
                        <select id="waers2"  hidden="hidden" style="width: 60px" >
                            <option value="MXN">MXN</option>
                            <option value="USD">USD</option>
                            <option value="EUR">EUR</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Grupo de artículo:</td>
                    <td>
                        <input id="gr_a" disabled="disabled" />
                    </td>
                </tr>
                <tr>
                    <td>Cantidad:</td>
                    <td>
                        <input id="quan" type="text" onblur="cantidades(this.id)" /></td>
                    <td>Unidad:</td>
                    <td>
                        <%--<input id="uom" disabled="disabled" style="width: 40px" onblur="verificaUOM(this.id)"/>--%>
                        <select id="uom" disabled="disabled" style="width: 90px;">
                            <option value="">-</option>
                            <option value="PZ">PZ - Pieza</option>
                            <option value="M">M - Metro</option>
                            <option value="KG">KG - Kilogramo</option>
                            <option value="L">L - Litro</option>
                            <option value="CJ">CJ - Caja</option>
                            <option value="FT">FT - Pies</option>
                            <option value="IN">IN - Pulgada</option>
                            <option value="TO">TO - Tonelada</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>Fecha de entrega:</td>
                    <td>
                        <input id="date" type="date" />
                    </td>
                </tr>
            </table>

            <input id="desc" disabled="disabled" hidden="hidden" />


        </div>
        <div>
            <%--Referencia de proyecto:
            <input type="text" id="referenciaP2" />--%>
            <div>
            <div style="height:30px;text-align: right;">
                <input type="button" value="Agregar" onclick="addLine()" class="btnCrear" style="text-align: right;" id="addLines"/>
            </div>
                <div class="CSSTableGenerator" style="width: auto">
                    <table id="posiciones" style="width: 100%;">
                        <tbody></tbody>
                    </table>
                </div>
        </div>
        </div>
        <br />
        Comentarios:<br />
        <textarea id="TextArea1" rows="5" style="width: 100%; resize: none;"></textarea><br />
        
    </form>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <div style="text-align: right; width: auto;">
        <%--<input type="button" value="Agregar línea" onclick="addLine()" class="btnCrear" />--%>
        <input type="button" value="Crear" onclick="crear()" class="btnCrear" style="text-align: right" />
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
    <h1>Creación de solicitudes</h1>
    <br />
    <br />
</asp:Content>

