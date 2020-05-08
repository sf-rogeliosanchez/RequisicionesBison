using IBusiness;
using IEntities;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using T259Prueba08.Models;

namespace T259Prueba08
{
    public partial class reqsVer : System.Web.UI.Page
    {
        //static string BANFN;
        //static string user;
        USUARIO_BE usuario = new USUARIO_BE();
        protected void Page_Load(object sender, EventArgs e)
        {
            string BANFN = Request.QueryString["banfn"];
            try
            {
                //user = Request.Cookies.Get("UserName").Value; //USUARIO ACTUAL
                string user = Session["UserName"].ToString();
                lblUsuario.Text = "<br><strong>Usuario</strong> > " + user.ToUpper() + "<br>";
                USUARIO_BE usuario = USUARIO_BLL.GET(user)[0];
                List<ZEBAN_P_BE> historial = ZEBAN_P_BLL.GET(usuario); //CREADAS POR EL USUARIO ACTUAL
                List<USUARIO_BE> UsuariosM = new List<USUARIO_BE>(); //USUARIOS DE MENOR RANGO


                //---------------------------------------------------OBTENER HISTORIAL---------------------------------------//
                int roo = rol(usuario); //ROL DEL USUARIO
                ////if (roo == 1) //SI ES GERENTE DE AREA
                ////{
                List<USUARIO_BE> areas = USUARIO_BLL.GET("", usuario.NipLogin);
                for (int i = 0; i < areas.Count; i++)
                {
                    List<ZEBAN_P_BE> tempA = ZEBAN_P_BLL.GET(areas[i]);
                    historial.AddRange(tempA);
                }
                List<ZEBAN_P_BE> tempB = ZEBAN_P_BLL.GET(1, usuario.NipLogin);
                //27.01.2017----------------------------------------------------------------
                for (int i = 0; i < tempB.Count; i++)
                {
                    if (historial.Any(f => f.BANFN == tempB[i].BANFN) == false)
                    {
                        historial.Add(tempB[i]);
                    }
                }
                //27.01.2017----------------------------------------------------------------
                ////}
                ////else 
                if (roo == 2)
                {
                    List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(roo); //POR ESTATUS
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (historial.Any(f => f.BANFN == temp[i].BANFN) == false)
                        {
                            historial.Add(temp[i]);
                        }
                    }
                }
                else if (roo == 3)
                {
                    List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(2); //POR ESTATUS
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (historial.Any(f => f.BANFN == temp[i].BANFN) == false)
                        {
                            if (!temp[i].COMPRADOR.Trim().Equals(""))
                            {
                                historial.Add(temp[i]);
                            }
                        }
                    }
                }
                else if (roo > 3)
                {
                    List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(roo - 1); //POR ESTATUS
                    for (int i = 0; i < temp.Count; i++)
                    {
                        if (historial.Any(f => f.BANFN == temp[i].BANFN) == false)
                        {
                            historial.Add(temp[i]);
                        }
                    }
                    if (roo == 6)
                    {
                        temp = ZEBAN_P_BLL.GET(roo); //POR ESTATUS
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (historial.Any(f => f.BANFN == temp[i].BANFN) == false)
                            {
                                historial.Add(temp[i]);
                            }
                        }
                    }
                }
                else
                {
                    for (int j = roo - 1; j >= 0; j--)
                    {
                        List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(j); //CREADAS RECIENTEMENTE POR USUARIOS CON MENOR RANGO
                        for (int i = 0; i < temp.Count; i++)
                        {
                            if (historial.Any(f => f.BANFN == temp[i].BANFN) == false)
                            {
                                historial.Add(temp[i]);
                            }
                        }
                        if (j != 1)
                        {
                            List<USUARIO_BE> tempUsuariosM = USUARIO_BLL.GET(j);
                            UsuariosM.AddRange(tempUsuariosM);
                        }
                    }

                    //int jo = rol(usuario);
                    //List<ZEBAN_P_BE> tempo = new List<ZEBAN_P_BE>(); ;
                    //if (jo > 0)
                    //{
                    //    tempo = ZEBAN_P_BLL.GET(jo); //CREADAS RECIENTEMENTE POR USUARIOS CON MENOR RANGO
                    //}
                    //for (int i = 0; i < tempo.Count; i++)
                    //{
                    //    if (historial.Any(f => f.BANFN == tempo[i].BANFN) == false)
                    //    {
                    //        historial.Add(tempo[i]);
                    //    }
                    //}
                }


                List<ZEBAN_P_BE> temp2 = ZEBAN_P_BLL.GET("1", user); //APROBADAS Y MODIFICADAS POR USUARIO ACTUAL
                for (int i = 0; i < temp2.Count; i++)
                {
                    if (historial.Any(f => f.BANFN == temp2[i].BANFN) == false)
                    {
                        historial.Add(temp2[i]);
                    }
                }


                historial = historial.OrderBy(o => o.BANFN).ToList();

                //-----------------------------------------------------FIN OBTENER HISTORIAL---------------------------------------//


                bool bandera = false;
                int B = Int32.Parse(BANFN);
                for (int i = 0; i < historial.Count; i++)
                {
                    if (historial[i].BANFN.Equals(B))
                    {
                        bandera = true;
                    }
                }

                if (bandera)
                {
                    decimal precio = 0; //SUMA EL TOTAL
                    string moneda = ""; //MONEDA DE CADA POSICIÓN

                    List<ZEBAN_BE> eban = ZEBAN_BLL.GET(Int32.Parse(BANFN));//Posiciones
                    //List<ZEBAN_BE> eban = ZEBAN_BLL.GET();
                    ZEBAN_P_BE zeban = ZEBAN_P_BLL.GET(BANFN)[0]; //Encabezado

                    lblEban.Text = "<table >" +
                                        "<tr><td>No. de requisición: </td>" +
                                            "<td><input id=\"Text1\" type=\"text\" value=\"" + BANFN + "\" disabled=\"true\"/></td>";

                    if (!zeban.SAP_BANFN.Trim().Equals("") && !zeban.SAP_BANFN.Equals(null)) //SI FUE CREADA EN SAP
                    {
                        lblEban.Text += "<td style='width:20px;'></td>" +
                                            "<td>SAP :  <input id=\"Text2\" type=\"text\" value=\"" + zeban.SAP_BANFN + "\" disabled=\"true\"/></td>";
                        if (zeban.SAP_EBELN.Trim().Equals(""))
                        {
                            buscarOCompra(zeban.SAP_BANFN, BANFN); //BUSCA ORDEN DE COMPRA Y LO ACTUALIZA EN BD
                            zeban = ZEBAN_P_BLL.GET(BANFN)[0];//ACTUALIZAR ENCABEZADO
                        }
                        lblEban.Text += "<td style='width:20px;'></td>" +
                                            "<td>OCompra : <input id=\"Text3\" type=\"text\" value=\"" + zeban.SAP_EBELN + "\" disabled=\"true\"/></td>" +
                                            "<br><br>";
                    }

                    lblEban.Text += "</tr>" +
                                        "<tr><td>Ref. de proyecto:</td>" +
                                            "<td colspan='6'><input id='referencia' type='text' value='" + zeban.REFERENCIA + "' disabled='true' style='width:606px;'/></td>";
                    lblEban.Text += "</tr></td>" +
                                    "</table>";

                    string tab = "<div class='CSSTableGenerator' style='width: 100%;'>";
                    tab += "<table border=1 width=100%><tbody>" +
                                        "<tr>" +
                                            "<td>Pos</td>" +
                                            "<td>Imp</td>" +
                                            "<td>Tipo</td>" +
                                            "<td>Material</td>" +
                                            "<td>Descripción</td>" +
                                            "<td>Precio</td>" +
                                            "<td>Mon</td>" +
                                            "<td>Cantidad</td>" +
                                            "<td>Entrega</td>" +
                                            "<td>Gpo.artíc</td>" +
                                            "<td>Centro</td>" +
                                            "<td>Gr. comp</td>" +
                                     "</tr>";

                    string tipoR = "";
                    string tipoI = "";
                    eban = eban.OrderBy(o => int.Parse(o.BNFPO)).ToList();
                    for (int i = 0; i < eban.Count; i++)    //Posiciones
                    {
                        tipoR = eban[i].IMP;
                        tipoI = eban[i].TIPO;
                        moneda = eban[i].WAERS;
                        ////}

                        if (tipoR.Equals("K") && tipoI.Equals("F"))//Servicios
                        {
                            ZEBAN_S_BE servs = ZEBAN_S_BLL.GET(Int32.Parse(BANFN), "" + ((i + 1) * 10))[0];
                            tab += "<tr  id='tr" + (i + 1) + "' onclick='focuss(" + (i + 1) + "," + eban.Count + ")'>";

                            tab += "<td><div id='pos" + (i + 1) + "' >" + eban[i].BNFPO + "</div></td>";
                            tab += "<td><div id='imp" + (i + 1) + "' >" + eban[i].IMP + "</div></td>";
                            tab += "<td><div id='tipo" + (i + 1) + "' >" + eban[i].TIPO + "</div></td>";
                            tab += "<td>" + eban[i].MATNR + "</td>";
                            tab += "<td>" + eban[i].DESCR + "</td>";
                            tab += "<td>" + servs.PREIS + "</td>";
                            tab += "<td>" + servs.WAERS + "</td>";
                            tab += "<td>" + servs.MENGE + "</td>";
                            tab += "<td>" + eban[i].F_ENTREGA.ToShortDateString() + "</td>";
                            tab += "<td>" + eban[i].MAT_GR + "</td>";
                            tab += "<td>" + eban[i].WERKS + "</td>";
                            tab += "<td>" + eban[i].PUR_GR + "</td>";

                            tab += "</tr>";

                            precio += servs.PREIS * servs.MENGE;
                            moneda = servs.WAERS;
                        }
                        else if (tipoR.Equals("A"))//Activos
                        {
                            ZEBAN_S_BE serv = ZEBAN_S_BLL.GET(Int32.Parse(BANFN), "" + ((i + 1) * 10))[0];
                            tab += "<tr  id='tr" + (i + 1) + "' onclick='focuss(" + (i + 1) + "," + eban.Count + ")'>" +
                                        "<td><div id='pos" + (i + 1) + "' >" + eban[i].BNFPO + "</div></td>" +
                                        "<td><div id='imp" + (i + 1) + "' >" + eban[i].IMP + "</div></td>" +
                                        "<td><div id='tipo" + (i + 1) + "' ></div></td>" +
                                        "<td>" + eban[i].MATNR + "</td>" +
                                        "<td>" + eban[i].DESCR + "</td>" +
                                        "<td>" + serv.PREIS + "</td>" +
                                        "<td>" + serv.WAERS + "</td>" +
                                        "<td>" + serv.MENGE + "</td>" +
                                        "<td>" + eban[i].F_ENTREGA.ToShortDateString() + "</td>" +
                                        "<td>" + eban[i].MAT_GR + "</td>" +
                                        "<td>" + eban[i].WERKS + "</td>" +
                                        "<td>" + eban[i].PUR_GR + "</td>" +
                            "</tr>";
                            precio += serv.PREIS * serv.MENGE;
                            moneda = serv.WAERS;
                        }
                        else if (tipoR.Equals("K") && !tipoI.Equals("F"))//Material
                        {
                            tab += "<tr  id='tr" + (i + 1) + "' onclick='focuss(" + (i + 1) + "," + eban.Count + ")'>" +
                                         "<td><div id='pos" + (i + 1) + "' >" + eban[i].BNFPO + "</div></td>" +
                                         "<td><div id='imp" + (i + 1) + "' >" + eban[i].IMP + "</div></td>" +
                                         "<td><div id='tipo" + (i + 1) + "' >" + eban[i].TIPO + "</div></td>" +
                                         "<td>" + eban[i].MATNR + "</td>" +
                                         "<td>" + eban[i].DESCR + "</td>" +
                                         "<td>" + eban[i].PREIS + "</td>" +
                                         "<td>" + eban[i].WAERS + "</td>" +
                                         "<td>" + eban[i].MENGE + "</td>" +
                                         "<td>" + eban[i].F_ENTREGA.ToShortDateString() + "</td>" +
                                         "<td>" + eban[i].MAT_GR + "</td>" +
                                         "<td>" + eban[i].WERKS + "</td>" +
                                         "<td>" + eban[i].PUR_GR + "</td>" +
                                    "</tr>";
                            precio += eban[i].PREIS * eban[i].MENGE;
                        }
                        else
                        {
                            tab += "<tr  id='tr" + (i + 1) + "' onclick='focuss(" + (i + 1) + "," + eban.Count + ")'>" +
                                         "<td><div id='pos" + (i + 1) + "' >" + eban[i].BNFPO + "</div></td>" +
                                         "<td><div id='imp" + (i + 1) + "' >" + eban[i].IMP + "</div></td>" +
                                         "<td><div id='tipo" + (i + 1) + "' >" + eban[i].TIPO + "</div></td>" +
                                         "<td>" + eban[i].MATNR + "</td>" +
                                         "<td>" + eban[i].DESCR + "</td>" +
                                         "<td>" + eban[i].PREIS + "</td>" +
                                         "<td>" + eban[i].WAERS + "</td>" +
                                         "<td>" + eban[i].MENGE + "</td>" +
                                         "<td>" + eban[i].F_ENTREGA.ToShortDateString() + "</td>" +
                                         "<td>" + eban[i].MAT_GR + "</td>" +
                                         "<td>" + eban[i].WERKS + "</td>" +
                                         "<td>" + eban[i].PUR_GR + "</td>" +
                                    "</tr>";
                            precio += eban[i].PREIS * eban[i].MENGE;
                        }
                    }
                    precio = decimal.Round(precio, 4);
                    tab += "<tr style='	background:-o-linear-gradient(bottom, #005fbf 5%, #003f7f 100%);	background:-webkit-gradient( linear, left top, left bottom, color-stop(0.05, #005fbf), color-stop(1, #003f7f) );" +
                                "background:-moz-linear-gradient( center top, #005fbf 5%, #003f7f 100% );	background: -o-linear-gradient(top,#005fbf,003f7f);" +
                                "background-color:#005fbf;" +
                                "border:0px solid #000000;" +
                                "text-align:center;" +
                                "border-width:0px 0px 1px 1px;" +
                                "font-size:14px;" +
                                "font-family:Arial;" +
                                "font-weight:bold;" +
                                "color:#ffffff;'>" +
                                   "<td></td>" +
                                   "<td></td>" +
                                   "<td></td>" +
                                   "<td></td>" +
                                   "<td style='color:white;font-size:14px;font-weight: bold;'>Subtotal</td>" +
                                   "<td style='color:white;font-size:14px;font-weight: bold;'>" + precio + "</td>" +
                                   "<td style='color:white;font-size:14px;font-weight: bold;'>" + moneda + "</td>" +
                                   "<td></td>" +
                                   "<td></td>" +
                                   "<td></td>" +
                                   "<td></td>" +
                                   "<td></td>" +
                           "</tr>";
                    tab += "</tbody></table>";

                    tab += "</div>";
                    //tab += "<div style='text-align:right; font-size:20px;'><br>Subtotal: " + precio + " " + moneda + "</div>";
                    lblTabla.Text = tab;


                    //----------------------------------------------------ADJUNTOS ---------------------------------------//
                    List<ZEBAN_D_BE> adjuntos = ZEBAN_D_BLL.GET(Int32.Parse(BANFN));
                    //string add = "<div><table>"; //style=\"background-color:white;\">";
                    if (adjuntos.Count > 0)
                    {
                        Label lbl1 = new Label();
                        lbl1.Text = "Lista de adjuntos:<br><table><tr>";
                        form1.Controls.Add(lbl1);
                        for (int i = 0; i < adjuntos.Count; i++)
                        {
                            string archivo = adjuntos[i].NOMBRE;
                            string ext = adjuntos[i].EXT;
                            string folder = adjuntos[i].ID + "";
                            string tipo = tipoImagen(ext);


                            Label lbl2 = new Label();
                            lbl2.Text = "<td><div style=\"border:1px solid gray;background-color: lightgray;text-align:left;width:170px;height:30px;padding:0px 0px 0px 5px\">";
                            form1.Controls.Add(lbl2);
                            //add += "<asp:Button runat='server' ID='btn" + i + "' OnClick='btn1_Click' Text='Boton1'/>";
                            ImageButton btni = new ImageButton();
                            btni.ImageUrl = "//ssl.gstatic.com/docs/doclist/images/mediatype/icon_1_" + tipo + "_x16.png";
                            btni.ToolTip = archivo + "." + ext;

                            Button btnt = new Button();
                            //btnt.ImageUrl = "//ssl.gstatic.com/docs/doclist/images/mediatype/icon_1_" + tipo + "_x16.png";
                            btnt.ID = folder + "|" + archivo + "." + ext + "2";
                            //btnt.Text = "&nbsp;" + archivo;
                            btnt.ToolTip = archivo + "." + ext;

                            if (archivo.Length > 15)
                            {
                                archivo = archivo.Substring(0, 15) + "...";
                            }

                            btni.ID = folder + "|" + archivo + "." + ext + "1";
                            //btn2.Text = archivo + "." + ext;
                            //btn2.Click += new EventHandler(btn1_Click);
                            //btni.CssClass = "submitI";
                            btni.ImageAlign = ImageAlign.Middle;
                            btni.Click += new ImageClickEventHandler(imagebtn_Click);
                            btni.BackColor = System.Drawing.Color.LightGray;
                            System.Web.UI.WebControls.Unit p16 = 16;
                            btni.Height = p16;
                            btni.Width = p16;
                            form1.Controls.Add(btni);

                            btnt.CssClass = "submit";
                            btnt.Text = archivo;
                            btnt.BackColor = System.Drawing.Color.LightGray;
                            btnt.Click += new EventHandler(btn_Click);
                            form1.Controls.Add(btnt);


                            Label lbl3 = new Label();
                            //lbl2.Text = "</div><br>";
                            lbl3.Text = "</div></td>";
                            form1.Controls.Add(lbl3);

                            if ((i % 4) == 0 && i > 0)
                            {
                                Label lbl5 = new Label();
                                //lbl2.Text = "</div><br>";
                                lbl5.Text = "<br></td></tr><tr>";
                                form1.Controls.Add(lbl5);

                            }

                            //if (archivo.Length > 15)
                            //{
                            //    archivo = archivo.Substring(0, 15);
                            //}

                            //add += "<tr><td><div id=\"" + folder + "|" + archivo + "." + ext + "\"style=\"background-color: white;text-align:left;width:160px;padding:10px 5px 5px 5px\"" +
                            //    " onmouseover=\"this.style.cursor='pointer';\"" +
                            //    " onclick=\"descargar(this.id)\">" +
                            //"<image src=\"//ssl.gstatic.com/docs/doclist/images/mediatype/icon_1_" + tipo + "_x16.png\" AlternateText=\"Texto\"/>&nbsp;&nbsp;" +

                            //archivo + "</div>" + "</td>";
                        }
                        Label lbl4 = new Label();
                        lbl4.Text = "</tr></table>";
                        form1.Controls.Add(lbl4);
                        //add += "</table>";
                        //add += "</div>";
                        //lblAdj.Text = add;
                    }

                    //---------------------------------------------------FIN ADJUNTOS ---------------------------------------//

                    //---------------------------------------------------BOTONES ---------------------------------------------//
                    //ZEBAN_P_BE zeban = ZEBAN_P_BLL.GET(BANFN)[0];
                    int status = Int32.Parse(zeban.STATUS);
                    List<ZEBAN_BE> pos = ZEBAN_BLL.GET(zeban.BANFN);

                    if (status == 0 | status == 1 | status == -1)
                    {
                        if (status == 0 | status == 1)
                        {
                            List<USUARIO_BE> areas2 = USUARIO_BLL.GET("", usuario.NipLogin);
                            for (int i = 0; i < areas2.Count; i++)
                            {
                                List<ZEBAN_P_BE> tempA = ZEBAN_P_BLL.GET(areas2[i]);
                                foreach (ZEBAN_P_BE t in tempA)
                                {
                                    if (BANFN.Equals(t.BANFN + ""))
                                    {
                                        lblBotones.Text = "<input type=\"button\" value=\"Autorizar\" class=\"btnCrear\" onclick=\"autorizar('" + BANFN + "','" + user + "')\"/>&nbsp";
                                        lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" class=\"btnCrear\" onclick=\"rechazar('" + BANFN + "','" + user + "')\" />&nbsp;";
                                        lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                                        continue;
                                    }
                                }
                            }
                        }
                        ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
                        if (header.FK_USUARIO.Trim().Equals(usuario.NipLogin))
                        {
                            lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                        }
                    }

                    if (roo == 0)
                    {
                        if (status == roo | status == -1)
                        {
                            lblBotones.Text = "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                        }
                    }
                    else if (roo == 1)
                    {
                        if (status == 0 | status == 1)
                        {
                            ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
                            if (!header.FK_USUARIO.Trim().Equals(usuario.NipLogin))
                            {
                                lblBotones.Text = "<input type=\"button\" value=\"Autorizar\" class=\"btnCrear\" onclick=\"autorizar('" + BANFN + "','" + user + "')\"/>&nbsp";
                                lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" class=\"btnCrear\" onclick=\"rechazar('" + BANFN + "','" + user + "')\" />&nbsp;";
                                lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                            }
                        }
                    }
                    else if (roo == 2)
                    {
                        if (status == roo)
                        {
                            lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                        }
                    }
                    else if (roo == 3)
                    {
                        if (status == roo | status == 2)
                        {
                            lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                        }
                    }
                    else if (roo == 4)
                    {
                        bool si = false;
                        if (status == 3 | (status == 2 & !zeban.COMPRADOR.Trim().Equals("") & !zeban.COMPRADOR.Trim().Equals("-")))
                        {
                            foreach (ZEBAN_BE z in pos)
                            {
                                if (z.IMP.Equals("K"))
                                {
                                    List<ZEBAN_I_BE> imp = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO);
                                    foreach (ZEBAN_I_BE i in imp)
                                    {
                                        if (z.MAT_GR.Trim().Equals("") | i.COSTCENTER.Trim().Equals("") | i.GL_ACCOUNT.Trim().Equals(""))
                                        {
                                            si = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (z.MAT_GR.Trim().Equals(""))
                                    {
                                        si = true;
                                    }
                                }
                            }
                            if (!si)
                            {
                                lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"autorizar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>&nbsp";
                                lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>&nbsp";
                            }
                        }
                    }
                    else if (roo == 5)
                    {
                        if (status == 4)
                        {
                            if (precios(precio, moneda))
                            {
                                lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>&nbsp";
                                lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>&nbsp";
                            }
                            else
                            {
                                lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"autorizar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>&nbsp";
                                lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>&nbsp";
                            }
                        }
                    }
                    else
                    {
                        if (status == 5 | (status == 4 & !precios(precio, moneda)))
                        {
                            lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>&nbsp";
                            lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>&nbsp";
                        }
                    }



                    if (status != -2 && status != 6)
                    {
                        lblBotones.Text += "<input type=\"button\" value=\"Cancelar\" onclick=\"cancelar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>&nbsp";
                    }
                    lblBotones.Text += "<br>";

                    //---------------------------------------------------FIN BOTONES ----------------------------------------//


                    ////if (zeban.STATUS.Trim() != "-2")
                    ////{
                    ////    if (status <= rol(usuario))
                    ////    {
                    ////        List<USUARIO_BE> temp = USUARIO_BLL.GET(zeban.APROBO);
                    ////        USUARIO_BE actual = USUARIO_BLL.GET(user)[0];
                    ////        if (temp.Count > 0)
                    ////        {
                    ////            USUARIO_BE aprobo = temp[0];

                    ////            if (rol(actual) > rol(aprobo))
                    ////            {
                    ////                if (rol(actual) > 3)
                    ////                {
                    ////                    if (precios(precio, moneda) && rol(actual) == 4)
                    ////                    {
                    ////                        lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                        lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                    }
                    ////                    else
                    ////                    {
                    ////                        if (rol(actual) > 4)
                    ////                        {
                    ////                            if (zeban.SAP_BANFN.Trim().Equals("") | zeban.SAP_BANFN.Equals(null))
                    ////                            {

                    ////                                lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                                lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";

                    ////                            }
                    ////                        }
                    ////                    }
                    ////                }
                    ////                else
                    ////                {
                    ////                    lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>";
                    ////                    lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                    lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                }
                    ////            }
                    ////        }
                    ////        else
                    ////        {
                    ////            if (rol(actual) > 3)
                    ////            {

                    ////                List<ZEBAN_BE> pos2 = ZEBAN_BLL.GET(zeban.BANFN);
                    ////                bool si = false;
                    ////                if (!rol(usuario).Equals(2) && !rol(usuario).Equals(0))
                    ////                {
                    ////                    if (rol(actual) == 4)//ES PARA CONTABILIDAD PRINCIPAL
                    ////                    {
                    ////                        foreach (ZEBAN_BE z in pos2)
                    ////                        {
                    ////                            if (z.IMP.Equals("K"))
                    ////                            {
                    ////                                List<ZEBAN_I_BE> imp = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO);
                    ////                                foreach (ZEBAN_I_BE i in imp)
                    ////                                {
                    ////                                    if (z.MAT_GR.Trim().Equals("") | i.COSTCENTER.Trim().Equals("") | i.GL_ACCOUNT.Trim().Equals(""))
                    ////                                    {
                    ////                                        si = true;
                    ////                                    }
                    ////                                }
                    ////                            }
                    ////                            else
                    ////                            {
                    ////                                if (z.MAT_GR.Trim().Equals(""))
                    ////                                {
                    ////                                    si = true;
                    ////                                }
                    ////                            }
                    ////                        }
                    ////                        if (!si)
                    ////                        {
                    ////                            lblBotones.Text += "<input type=\"button\" value=\"AprobarC\" onclick=\"autorizar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                            lblBotones.Text += "<input type=\"button\" value=\"RechazarC\" onclick=\"rechazarConta('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                        }
                    ////                    }
                    ////                    else if (precios(precio, moneda) && rol(actual) == 5) //ES PARA GERENTE GENERAL
                    ////                    {
                    ////                        foreach (ZEBAN_BE z in pos2)
                    ////                        {
                    ////                            if (z.IMP.Equals("K"))
                    ////                            {
                    ////                                List<ZEBAN_I_BE> imp = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO);
                    ////                                foreach (ZEBAN_I_BE i in imp)
                    ////                                {
                    ////                                    if (z.MAT_GR.Trim().Equals("") | i.COSTCENTER.Trim().Equals("") | i.GL_ACCOUNT.Trim().Equals(""))
                    ////                                    {
                    ////                                        si = true;
                    ////                                    }
                    ////                                }
                    ////                            }
                    ////                            else
                    ////                            {
                    ////                                if (z.MAT_GR.Trim().Equals(""))
                    ////                                {
                    ////                                    si = true;
                    ////                                }
                    ////                            }
                    ////                        }
                    ////                        if (!si)
                    ////                        {

                    ////                            lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                            lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                        }
                    ////                    }
                    ////                    else if (rol(actual) == 5)
                    ////                    {
                    ////                        if (zeban.STATUS.Trim().Equals("4"))
                    ////                        {
                    ////                            lblBotones.Text += "<input type=\"button\" value=\"AprobarG\" onclick=\"autorizar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                            lblBotones.Text += "<input type=\"button\" value=\"RechazarG\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";

                    ////                        }
                    ////                    }
                    ////                    else
                    ////                    {
                    ////                        if (rol(actual) > 5)
                    ////                        {
                    ////                            if (zeban.SAP_BANFN.Trim().Equals("") | zeban.SAP_BANFN.Equals(null))
                    ////                            {
                    ////                                foreach (ZEBAN_BE z in pos2)
                    ////                                {
                    ////                                    if (z.IMP.Equals("K"))
                    ////                                    {
                    ////                                        List<ZEBAN_I_BE> imp = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO);
                    ////                                        foreach (ZEBAN_I_BE i in imp)
                    ////                                        {
                    ////                                            if (z.MAT_GR.Trim().Equals("") | i.COSTCENTER.Trim().Equals("") | i.GL_ACCOUNT.Trim().Equals(""))
                    ////                                            {
                    ////                                                si = true;
                    ////                                            }
                    ////                                        }
                    ////                                    }
                    ////                                    else
                    ////                                    {
                    ////                                        if (z.MAT_GR.Trim().Equals(""))
                    ////                                        {
                    ////                                            si = true;
                    ////                                        }
                    ////                                    }
                    ////                                }
                    ////                                if (!si)
                    ////                                {
                    ////                                    lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                                    lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                                }
                    ////                            }
                    ////                        }
                    ////                    }
                    ////                    //lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                    //lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                }
                    ////            }
                    ////            else
                    ////            {
                    ////                if (rol(usuario) == 1)
                    ////                {
                    ////                    lblBotones.Text += "<input type=\"button\" value=\"Autorizar\" class=\"btnCrear\" onclick=\"autorizar('" + BANFN + "','" + user + "')\"/>&nbsp";
                    ////                    lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" class=\"btnCrear\" onclick=\"rechazar('" + BANFN + "','" + user + "')\" />&nbsp;";
                    ////                }
                    ////                lblBotones.Text += "<input type=\"button\" value=\"Modificar\" onclick=\"modificar()\" class=\"btnCrear\"/>&nbsp";
                    ////                //if (!rol(usuario).Equals(1) && !rol(usuario).Equals(0))
                    ////                //{
                    ////                //    lblBotones.Text += "<input type=\"button\" value=\"Aprobar\" onclick=\"aprobar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////                //    lblBotones.Text += "<input type=\"button\" value=\"Rechazar\" onclick=\"rechazar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
                    ////                //}
                    ////            }
                    ////        }
                    ////    }
                    ////}

                    ////if (zeban.STATUS.Trim() != "-2" && zeban.STATUS.Trim() != "6" && zeban.STATUS.Trim() != "6")
                    ////{
                    ////    lblBotones.Text += "<input type=\"button\" value=\"Cancelar\" onclick=\"cancelar('" + BANFN + "','" + user + "')\" class=\"btnCrear\"/>";
                    ////}


                    Label lblComentarios = new Label();
                    lblComentarios.Text = "<BR>" + obtenerComentarios(BANFN, user); //GENERAR TABLA DE COMENTARIOS
                    form1.Controls.Add(lblComentarios);
                }
                else
                {
                    string urlDestino = "Historial.aspx"; //SI NO TIENE ACCESO A LA REQUISICIÓN, MUESTRA EL HISTORIAL
                    Response.Redirect(urlDestino, false);
                }
            }
            catch
            {
                string urlDestino = ConfigurationManager.AppSettings["loginPage"];//DEBE INICIAR SESIÓN
                Response.Redirect(urlDestino, false);
            }
        }

        private int rol(USUARIO_BE u)
        {
            int r = 0;
            string[] contabilidad = ConfigurationManager.AppSettings["contabilidad"].Split(';');

            if (u.DIREC)
            {
                r = 6;
            }
            else if (u.GEREN)
            {
                r = 5;
            }
            else if (u.CONTA)
            {
                if (contabilidad.Contains(u.NipLogin.Trim()))
                {
                    r = 4;
                }
                else
                {
                    r = 3;
                }
            }
            else if (u.USUA)
            {
                r = 2;
            }
            else if (USUARIO_BLL.GET("", u.NipLogin).Count > 0)
            {
                r = 1;
            }
            else
            {
                r = 0;
            }
            return r;
        }

        protected void btnMod_Click(object sender, EventArgs e)
        {
            string urlDestino = "Modificar.aspx?ebeln=" + Request.QueryString["ebeln"];
            Response.Redirect(urlDestino, false);
        }


        [WebMethod(EnableSession = false)]
        public string aprobar(string banfn, string url, string user)
        {
            List<ZEBAN_P_BE> requ = ZEBAN_P_BLL.GET(banfn);
            if (requ.Count > 0)
            {
                requ[0].STATUS = rol(USUARIO_BLL.GET(user)[0]) + "";
                requ[0].APROBO = user;
                int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                if (i > 0)
                {
                    ZEBAN_H_BE his = new ZEBAN_H_BE();
                    his.BANFN = banfn;
                    his.FECHA = DateTime.Now;
                    his.NIPLOGIN = user;

                    ZEBAN_H_BLL.INSERT(his);
                    return "Se ha aprobado la requisición";
                }
            }
            return "Hubo un error al aprobar la requisición";
        }

        [WebMethod(EnableSession = false)]
        public string descarga(string folnam, string BANFN)
        {
            string[] split = folnam.Split('|');
            string folder = split[0];
            Conexion con = new Conexion();
            con.rfcDescargar(BANFN, "BUS2105", folder);
            IRfcTable contenidoHEX = con.getContenidoHEX();
            byte[] linea;
            Byte[] archivo = new Byte[contenidoHEX.RowCount * 255];

            for (int i = 0; i < contenidoHEX.RowCount; i++)
            {
                contenidoHEX.CurrentIndex = i;
                linea = (byte[])contenidoHEX.GetValue("LINE");
                for (int j = 0; j < linea.Length; j++)
                {
                    archivo[(i * 255) + j] = linea[j];
                }
            }
            string nombre = split[1];
            //System.IO.File.WriteAllBytes("C:/APLICATIONS/"+nombre, archivo);
            StreamFileToBrowser(nombre, archivo);

            return nombre;
        }

        public void StreamFileToBrowser(string sFileName, byte[] fileBytes)
        {
            Response.Clear();
            Response.ClearHeaders();
            Response.ClearContent();
            Response.AppendHeader("content-length", fileBytes.Length.ToString());
            Response.ContentType = GetMimeTypeByFileName(sFileName);
            Response.AppendHeader("content-disposition", "attachment; filename=" + sFileName);
            Response.BinaryWrite(fileBytes);


            Response.End();

            // use this instead of response.end to avoid thread aborted exception (known issue):
            // http://support.microsoft.com/kb/312629/EN-US
        }

        public static string GetMimeTypeByFileName(string sFileName)
        {
            string sMime = "application/octet-stream";

            string sExtension = System.IO.Path.GetExtension(sFileName);
            if (!string.IsNullOrEmpty(sExtension))
            {
                sExtension = sExtension.Replace(".", "");
                sExtension = sExtension.ToLower();

                if (sExtension == "xls" || sExtension == "xlsx")
                {
                    sMime = "application/ms-excel";
                }
                else if (sExtension == "doc" || sExtension == "docx")
                {
                    sMime = "application/msword";
                }
                else if (sExtension == "ppt" || sExtension == "pptx")
                {
                    sMime = "application/ms-powerpoint";
                }
                else if (sExtension == "rtf")
                {
                    sMime = "application/rtf";
                }
                else if (sExtension == "zip")
                {
                    sMime = "application/zip";
                }
                else if (sExtension == "mp3")
                {
                    sMime = "audio/mpeg";
                }
                else if (sExtension == "bmp")
                {
                    sMime = "image/bmp";
                }
                else if (sExtension == "gif")
                {
                    sMime = "image/gif";
                }
                else if (sExtension == "jpg" || sExtension == "jpeg")
                {
                    sMime = "image/jpeg";
                }
                else if (sExtension == "png")
                {
                    sMime = "image/png";
                }
                else if (sExtension == "tiff" || sExtension == "tif")
                {
                    sMime = "image/tiff";
                }
                else if (sExtension == "txt")
                {
                    sMime = "text/plain";
                }
            }

            return sMime;
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID.Substring(0, btn.ID.Length - 1);
            string name = btn.Text;

            //descarga(id + "|" + name);
            descargar(id);
        }
        protected void imagebtn_Click(object sender, EventArgs e)
        {
            ImageButton btn = (ImageButton)sender;
            string id = btn.ID.Substring(0, btn.ID.Length - 1);
            descargar(id);
        }

        public string descargar(string folnam)
        {
            string[] split = folnam.Split('|');
            string nombre = split[1];
            int id = Int32.Parse(split[0]);
            List<ZEBAN_D_BE> doc = ZEBAN_D_BLL.GET(0, id);
            if (doc.Count > 0)
            {
                //System.IO.File.WriteAllBytes("C:/APLICATIONS/"+nombre, archivo);
                StreamFileToBrowser(nombre, doc[0].INFO);
            }

            return nombre;
        }
        private string tipoImagen(string ext)
        {
            string exts = "";
            string e = ext.Substring(0, 3);
            e = e.ToLower(); ;

            switch (e)
            {
                case "doc":
                    exts = "word";
                    break;
                case "xls":
                    exts = "excel";
                    break;
                case "png":
                    exts = "image";
                    break;
                case "jpg":
                    exts = "image";
                    break;
                case "gif":
                    exts = "image";
                    break;
                case "pdf":
                    exts = "pdf";
                    break;
                case "ppt":
                    exts = "powerpoint";
                    break;
                case "rtf":
                    exts = "word";
                    break;
                case "rar":
                    exts = "archive";
                    break;
                case "zip":
                    exts = "archive";
                    break;
                default:
                    exts = "text";
                    break;

            }

            return exts;
        }
        //public IEnumerable<Control> GetAll(Control control, Type type)
        //{
        //    var controls = control.Controls.Cast<Control>();

        //    return controls.SelectMany(ctrl => GetAll(ctrl, type))
        //                              .Concat(controls)
        //                              .Where(c => c.GetType() == type);
        //}

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            string BANFN = this.Request.QueryString["banfn"];
            //ClientScript.RegisterStartupScript(GetType(), "Crear requsición", "mensaje();", true);    
            String csname1 = "PopupScript";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;

            if (fileUp.HasFiles)
            {
                //Conexion con = new Conexion();
                IList<HttpPostedFile> files = fileUp.PostedFiles;
                int[] ret = new int[files.Count];
                int cont = 0;
                for (int i = 0; i < files.Count; i++)
                {
                    Stream stream;
                    stream = files[i].InputStream;
                    int len = files[i].ContentLength;
                    byte[] bytes = new byte[len];
                    stream.Read(bytes, 0, len);
                    string[] name = files[i].FileName.Split('\\');
                    string filen = name[name.Length - 1];

                    ret[i] = adjuntar(BANFN, "BUS2105", filen, bytes);
                    cont++;
                }

                if (cont == files.Count)
                {
                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Se han cargado los archivos correctamente');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);


                        string urlDestino = "reqsVer.aspx?banfn=" + BANFN;
                        Response.Redirect(urlDestino, false);
                    }
                }
                else
                {
                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Hubo algún error al subir los documentos.');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                }
            }
            else
            {
                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('No hay archivos para subir.');";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
            }

        }


        private static int adjuntar(string banfn, string tipo, string nombre, byte[] bytes)
        {
            ZEBAN_D_BE doc = new ZEBAN_D_BE();
            doc.BANFN = Int32.Parse(banfn);
            doc.NOMBRE = nombre.Split('.')[0];
            doc.EXT = nombre.Split('.')[1];
            doc.INFO = bytes;

            int i = ZEBAN_D_BLL.INSERT(doc);
            return i;
        }

        private string obtenerComentarios(string banfn, string user)
        {
            String BANFN = banfn;
            List<ZEBAN_C_BE> comentarios = ZEBAN_C_BLL.GET(Int32.Parse(banfn));

            string tabla = "<div style='width: 80%; text-align: right;'>";
            tabla += "<input type=\"button\" value=\"Agregar comentario\" onclick=\"agregar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
            tabla += "<br>";


            if (comentarios.Count > 0)
            {
                tabla += "<input type='checkbox' id='onlyyes' hidden='hidden' />" +
                   "<input type='checkbox' id='onlyno' hidden='hidden' />" +
                   "<br />" +
                       // "<div style='text-align: right'>" +
                       "<a id='cleanfilters2' href='#'>Limpiar filtros</a>" +
                       "<input type='text' id='quickfind' placeholder='Buscar' />" +
                   //   "<br />" +
                   // "</div>" +

                   "<script src='http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js' type='text/javascript'></script>" +
                   "<script type='text/javascript' src='Scripts/picnet.table.filter.min.js'></script>" +


                   "<script type='text/javascript'>" +
                      " $(document).ready(function () {" +
                          // Initialise Plugin
                          " var options1 = {" +
                               "additionalFilterTriggers: [$('#onlyyes'), $('#onlyno'), $('#quickfind')]," +
                               "clearFiltersControls: [$('#cleanfilters2')]," +
                               "matchingRow: function (state, tr, textTokens) {" +
                                   "if (!state || !state.id) { return true; }" +
                                   "var val = tr.children('td:eq(2)').text();" +
                                   "switch (state.id) {" +
                                       "case 'onlyyes': return state.value !== 'true' || val === 'yes';" +
                                       "case 'onlyno': return state.value !== 'true' || val === 'no';" +
                                       "default: return true;" +
                                   "}" +
                               "}" +
                           "};" +
                           "$('#tablas2').tableFilter(options1);" +
                       "});" +
                   "</script>";
            }


            if (comentarios.Count > 0)
            {
                tabla += "<br><div  class='CSSTableGenerator2' style=' width:100%;'><table class='tablas2' id='tablas2' style=' width:96%;'><thead><tr><th>Fecha</th>"
                    + "<th filter-type='ddl'>Usuario</th><th>Comentario</th></tr></thead><tbody>";


                for (int i = 0; i < comentarios.Count; i++)
                {
                    string color = "white";
                    //if (user.Trim().Equals(comentarios[i].FK_USUARIO))
                    List<USUARIO_BE> esDirec = USUARIO_BLL.GET(comentarios[i].FK_USUARIO);
                    if (esDirec.Count > 0)
                        if (esDirec[0].DIREC)
                        {
                            color = "#8ebded";
                        }
                    tabla += "<tr><td style='padding: 3px 5px 3px 5px;background-color:" + color + ";vertical-align:top;width:120px;'>";
                    tabla += comentarios[i].FECHA.ToString() + "</td><td style='background-color:" + color + ";vertical-align:top;width:auto;width:70px;'>" + comentarios[i].FK_USUARIO;
                    tabla += "</td><td style='background-color:" + color + "'>";

                    string[] comentario = comentarios[i].TEXTO.Split('\n');
                    comentarios[i].TEXTO = "";
                    for (int j = 0; j < comentario.Length; j++)
                    {
                        string temp = "";
                        if (comentario[j].Length > 100)
                        {
                            for (int k = 0; k < comentario[j].Length; k = k + 100)
                            {
                                if ((k + 100) < comentario[j].Length)
                                {
                                    temp += comentario[j].Substring(k, 100) + "\n";
                                }
                                else
                                {
                                    temp += comentario[j].Substring(k, comentario[j].Length - k) + "\n";
                                }
                            }
                        }
                        else
                        {
                            temp += comentario[j] + "\n";
                        }
                        comentarios[i].TEXTO += temp;

                    }

                    string texto = comentarios[i].TEXTO.Replace("\n", "<br>");
                    tabla += texto + "</td>";
                    tabla += "</tr>";
                }
                tabla += "</tbody></table></div></div>";
            }


            return tabla;
        }

        private bool precios(decimal p, string waers)
        {
            catalogos cat = new catalogos();

            bool ret = false;

            if (cat.getTipoCambio(p + "", waers.Trim()) < 20000)
            {
                ret = true;
            }

            //if (waers.Equals("MXN"))
            //{
            //    if (p < 20000)
            //    {
            //        ret = true;
            //    }
            //}
            //else
            //{
            //    if (waers.Equals("USD"))
            //    {
            //        if ((p * decimal.Parse(ConfigurationManager.AppSettings["tipoCambioU"])) < 20000)
            //        {
            //            ret = true;
            //        }
            //    }
            //    else
            //    {
            //        if ((p * decimal.Parse(ConfigurationManager.AppSettings["tipoCambioE"])) < 20000)
            //        {
            //            ret = true;
            //        }
            //    }
            //}
            return ret;
        }

        //private void buscarOCompra(string B)
        //{
        //    String BANFN = B;
        //    Conexion con = new Conexion();
        //    string EBELN = con.get_Ebeln(B);
        //    if (!EBELN.Equals(""))
        //    {
        //        ZEBAN_P_BE p = ZEBAN_P_BLL.GET(BANFN)[0];
        //        p.SAP_EBELN = EBELN;
        //        ZEBAN_P_BLL.UPDATE(p);
        //    }

        //}

        private void buscarOCompra(string B, string BANFN)
        {
            string ebeln = new Conexion().get_Ebeln(B);
            if (ebeln.Equals(""))
                return;
            ZEBAN_P_BE zebanPBe = ZEBAN_P_BLL.GET(BANFN)[0];
            zebanPBe.SAP_EBELN = ebeln;
            ZEBAN_P_BLL.UPDATE(zebanPBe);
        }
    }

}