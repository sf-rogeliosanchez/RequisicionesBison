using IBusiness;
using IEntities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using T259Prueba08.Models;

namespace T259Prueba08
{
    public partial class Default : System.Web.UI.Page
    {
        public string numFilter = "10";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string user = Session["UserName"].ToString();
                lblUsuario.Text = "<br><strong>Usuario</strong> > " + user.ToUpper() + "<br>";
                USUARIO_BE usuario = USUARIO_BLL.GET(user)[0];
                List<ZEBAN_P_BE> historial = ZEBAN_P_BLL.GET(usuario); //CREADAS POR EL USUARIO ACTUAL
                List<USUARIO_BE> UsuariosM = new List<USUARIO_BE>(); //USUARIOS DE MENOR RANGO

                int roo = rol(usuario); //ROL DEL USUARIO
                List<USUARIO_BE> areas = USUARIO_BLL.GET("", usuario.NipLogin);
                for (int i = 0; i < areas.Count; i++)
                {
                    List<ZEBAN_P_BE> tempA = ZEBAN_P_BLL.GET(areas[i]);
                    historial.AddRange(tempA);
                }

                List<ZEBAN_P_BE> tempB = ZEBAN_P_BLL.GET(1, usuario.NipLogin);
                for (int i = 0; i < tempB.Count; i++)
                {
                    if (historial.Any(f => f.BANFN == tempB[i].BANFN) == false)
                    {
                        historial.Add(tempB[i]);
                    }
                }
                if (roo == 1)
                {
                }
                else if (roo == 2)
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

                List<ZEBAN_P_BE> temp2 = ZEBAN_P_BLL.GET("1", user); //APROBADAS Y MODIFICADAS POR USUARIO ACTUAL
                for (int i = 0; i < temp2.Count; i++)
                {
                    if (historial.Any(f => f.BANFN == temp2[i].BANFN) == false)
                    {
                        historial.Add(temp2[i]);
                    }
                }


                historial = historial.OrderBy(o => o.BANFN).ToList();

                string tab = "";


                //02.02.2017 INICIO-----------------------------------------------------------------------------------------------------------


                string cadenaBusqueda;
                if (!IsPostBack)
                {
                    cadenaBusqueda = Request.QueryString["search"];
                    if (cadenaBusqueda == null)
                    {
                        cadenaBusqueda = txtBuscar.Text;
                    }
                }
                else
                {
                    cadenaBusqueda = txtBuscar.Text;
                }
                txtBuscar.Text = setBusqueda(cadenaBusqueda);

                List<ZEBAN_P_BE> historialH = new List<ZEBAN_P_BE>();
                List<string> listEstatus = new List<string>();
                if (!cadenaBusqueda.Trim().Equals(""))
                {
                    for (int i = 0; i < historial.Count; i++)
                    {
                        if (!IsPostBack)
                        {
                            cadenaBusqueda = setBusqueda(cadenaBusqueda);


                            if (historial[i].BANFN.ToString().IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                              | historial[i].APROBO.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                              | historial[i].FK_USUARIO.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                              | historial[i].F_APROBACION.ToShortDateString().IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                              | historial[i].REFERENCIA.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                              | historial[i].SAP_BANFN.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                              | historial[i].SAP_EBELN.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                                ////| est.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0
                                )
                            {
                                historialH.Add(historial[i]);
                                listEstatus.Add("");
                            }
                            else
                            {
                                string est = getStatus(historial[i]);
                                if (est.IndexOf(cadenaBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                                {
                                    historialH.Add(historial[i]);
                                    listEstatus.Add(est);
                                }
                            }
                        }
                    }
                    historial = historialH;
                }



                string filter = Request.QueryString["num"];//Requisiciones por página
                if (filter == null)
                {
                    try
                    {
                        filter = Session["numFilter"].ToString();
                    }
                    catch
                    {
                        filter = "10";
                    }
                }
                Session["numFilter"] = filter;

                int num = int.Parse(filter);

                int numPag = historial.Count / num; // Total de páginas

                int mod = historial.Count % num;

                if (mod > 0)
                    numPag++;

                lblNumTPag.Text = numPag + "";
                Session["totPags"] = numPag;

                string pagActual = Request.QueryString["pag"];
                if (pagActual == null)
                {
                    try
                    {
                        pagActual = Session["pagActual"].ToString();
                    }
                    catch
                    {
                        pagActual = "1";
                    }
                }
                int numActual = int.Parse(pagActual);
                if (numActual > numPag)
                    numActual = numPag;


                Session["pagActual"] = pagActual;
                lblNumPag.Text = "" + numActual;

                lblControlPag.Text = "";
                if (numActual > 1)
                {
                    //lblControlPag.Text += "Anterior";
                    lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual - 1) + ")' data-dt-idx='1' tabindex='0'>Anterior</a>";
                }
                if (numActual == 1 | numActual == 0)
                {
                    lblControlPag.Text += "<a class='paginate_button current' data-dt-idx='1' tabindex='0'>1</a>";
                }
                else
                {
                    lblControlPag.Text += "<a class='paginate_button' onclick='control(1)' data-dt-idx='1' tabindex='0'>1</a>";
                }

                if (numPag > 6)
                {
                    if (numActual < 5)
                    {
                        //lblControlPag.Text += "   2    3    4    5  ...   ";
                        string current = "";
                        for (int i = 2; i < 6; i++)
                        {
                            if (numActual == i)
                                current = " current";
                            else
                                current = "";
                            lblControlPag.Text += "<a class='paginate_button" + current + "' onclick='control(" + i + ")' data-dt-idx='1' tabindex='0'>" + i + "</a>";
                        }

                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(3)' data-dt-idx='1' tabindex='0'>3</a>";
                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(4)' data-dt-idx='1' tabindex='0'>4</a>";
                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(5)' data-dt-idx='1' tabindex='0'>5</a>";
                        lblControlPag.Text += "<a class='paginate_button disabled' data-dt-idx='1' tabindex='0'>...</a>";
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + numPag + ")' data-dt-idx='1' tabindex='0'>" + numPag + "</a>";
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual + 1) + ")' data-dt-idx='1' tabindex='0'>Siguiente</a>";
                    }
                    else if (numActual > numPag - 4)
                    {
                        //lblControlPag.Text += "  ...   " + (numPag - 4) + "    " + (numPag - 3) + "    " + (numPag - 2) + "    " + (numPag - 1) + "  ";

                        lblControlPag.Text += "<a class='paginate_button disabled' data-dt-idx='1' tabindex='0'>...</a>";

                        string current = "";
                        for (int i = 4; i > 0; i--)
                        {
                            if (numActual == (numPag - i))
                                current = " current";
                            else
                                current = "";
                            lblControlPag.Text += "<a class='paginate_button" + current + "' onclick='control(" + (numPag - i) + ")' data-dt-idx='1' tabindex='0'>" + (numPag - i) + "</a>";
                        }


                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numPag - 4) + ")' data-dt-idx='1' tabindex='0'>" + (numPag - 4) + "</a>";
                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numPag - 3) + ")' data-dt-idx='1' tabindex='0'>" + (numPag - 3) + "</a>";
                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numPag - 2) + ")' data-dt-idx='1' tabindex='0'>" + (numPag - 2) + "</a>";
                        //lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numPag - 1) + ")' data-dt-idx='1' tabindex='0'>" + (numPag - 1) + "</a>";

                        if (numActual == numPag)
                        {
                            lblControlPag.Text += "<a class='paginate_button current' data-dt-idx='1' tabindex='0'>" + (numPag) + "</a>";
                        }
                        else
                        {
                            lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numPag) + ")' data-dt-idx='1' tabindex='0'>" + (numPag) + "</a>";
                            lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual + 1) + ")' data-dt-idx='1' tabindex='0'>Siguiente</a>";
                        }
                        //lblControlPag.Text += numPag;
                    }
                    else
                    {
                        //lblControlPag.Text += "  ...   " + (numActual - 1) + "    " + numActual + "    " + (numActual + 1) + "   ....";

                        lblControlPag.Text += "<a class='paginate_button disabled' data-dt-idx='1' tabindex='0'>...</a>";
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual - 1) + ")' data-dt-idx='1' tabindex='0'>" + (numActual - 1) + "</a>";
                        lblControlPag.Text += "<a class='paginate_button  current' data-dt-idx='1' tabindex='0'>" + (numActual) + "</a>";
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual + 1) + ")' data-dt-idx='1' tabindex='0'>" + (numActual + 1) + "</a>";
                        lblControlPag.Text += "<a class='paginate_button disabled' data-dt-idx='1' tabindex='0'>...</a>";
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numPag) + ")' data-dt-idx='1' tabindex='0'>" + (numPag) + "</a>";
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual + 1) + ")' data-dt-idx='1' tabindex='0'>Siguiente</a>";
                        //lblControlPag.Text += numPag;
                        //lblControlPag.Text += "Siguiente";
                    }
                }
                else
                {
                    string current = "";
                    for (int i = 2; i <= numPag; i++)
                    {
                        if (numActual == i)
                            current = " current";
                        else
                            current = "";
                        lblControlPag.Text += "<a class='paginate_button" + current + "' onclick='control(" + i + ")' data-dt-idx='1' tabindex='0'>" + i + "</a>";
                    }
                    if (numPag != numActual)
                    {
                        lblControlPag.Text += "<a class='paginate_button'  onclick='control(" + (numActual + 1) + ")' data-dt-idx='1' tabindex='0'>Siguiente</a>";
                    }
                }


                //////////string sort = Request.QueryString["sort"];
                //////////if (sort == "A1")
                //////////    historial = historial.OrderBy(o => o.BANFN).ToList();
                //////////if (sort == "D1")
                //////////    historial = historial.OrderByDescending(o => o.BANFN).ToList();
                //////////if (sort == "A2")
                //////////    historial = historial.OrderBy(o => o.F_APROBACION).ToList();
                //////////if (sort == "D2")
                //////////    historial = historial.OrderByDescending(o => o.F_APROBACION).ToList();
                //02.02.2017 FIN-----------------------------------------------------------------------------------------------------------


                List<HISTORIAL> HIS = new List<HISTORIAL>();
                for (int i = (numActual - 1) * num; i < historial.Count; i++)
                {
                    if (i > (numActual * num) - 1)
                    {
                        continue;
                    }
                    int status = Int32.Parse(historial[i].STATUS);
                    HISTORIAL h = new HISTORIAL();

                    if (historial[i].SAP_BANFN.Trim() == "")
                    {
                        h.ORDEN = historial[i].SAP_EBELN;


                        h.BANFN = historial[i].BANFN;
                        h.F_CREACION = historial[i].F_APROBACION.ToShortDateString();
                        h.SOLICITANTE = historial[i].FK_USUARIO.ToUpper();
                        h.ESTATUS = new ESTATUS();
                        if (listEstatus.Count > 0)
                        {
                            if (!listEstatus[i].Trim().Equals(""))
                                h.ESTATUS.TEXTO = listEstatus[i];
                            else
                                h.ESTATUS.TEXTO = getStatus(historial[i]);
                        }
                        else
                        {
                            h.ESTATUS.TEXTO = getStatus(historial[i]);
                        }
                        h.REFERENCIA = historial[i].REFERENCIA;
                        if (roo == 0)
                        {
                            if (status == 0 | status == -1)
                            {
                                h.MODIF = true;
                            }
                            else
                            {
                                h.MODIF = false;
                            }
                        }
                        else if (roo == 1)
                        {
                            if (status == 0 | status == 1)// | status == -1)
                            {
                                h.MODIF = true;
                            }
                            else
                            {
                                h.MODIF = false;
                            }
                        }
                        else if (roo == 2)
                        {
                            if (status == roo)
                            {
                                h.MODIF = true;
                            }
                            else
                            {
                                h.MODIF = false;
                            }
                        }
                        else if (roo == 3)
                        {
                            if (status == 2 | status == 3)
                            {
                                h.MODIF = true;
                            }
                            else
                            {
                                h.MODIF = false;
                            }
                        }
                    }
                    else
                    {
                        h.BANFN = historial[i].BANFN;
                        h.F_CREACION = historial[i].F_APROBACION.ToShortDateString();
                        h.SOLICITANTE = historial[i].FK_USUARIO.ToUpper();
                        h.ESTATUS = new ESTATUS();
                        if (listEstatus.Count > 0)
                        {
                            if (!listEstatus[i].Trim().Equals(""))
                                h.ESTATUS.TEXTO = listEstatus[i];
                            else
                                h.ESTATUS.TEXTO = getStatus(historial[i]);
                        }
                        else
                        {
                            h.ESTATUS.TEXTO = getStatus(historial[i]);
                        }
                        h.ORDEN = historial[i].SAP_EBELN;
                        h.REFERENCIA = historial[i].REFERENCIA;
                    }

                    h.SOLICITUD = historial[i].SAP_BANFN;

                    ZEBAN_H_BE hh = ZEBAN_H_BLL.GET(historial[i].BANFN + "");
                    if (!hh.BANFN.Equals(""))
                    {
                        h.F_MODIFICA = hh.FECHA.ToShortDateString();
                    }
                    else
                    {
                        h.F_MODIFICA = "";
                    }
                    h.MODIFICA = hh.NIPLOGIN.ToUpper();
                    List<ZEBAN_C_BE> cs = ZEBAN_C_BLL.GET(historial[i].BANFN);
                    DateTime date = new DateTime();
                    int ultimo = 0;
                    for (int j = 0; j < cs.Count; j++)
                    {
                        if (j == 0)
                        {
                            date = cs[j].FECHA;
                        }
                        else
                        {
                            if (date < cs[j].FECHA)
                            {
                                date = cs[j].FECHA;
                                ultimo = j;
                            }
                        }
                    }
                    if (cs.Count > 0)
                    {
                        string[] comentario = cs[ultimo].TEXTO.Split('\n');

                        for (int j = 0; j < comentario.Length; j++)
                        {
                            string temp = "";
                            if (comentario[j].Length > 60)
                            {
                                for (int k = 0; k < comentario[j].Length; k = k + 60)
                                {
                                    if ((k + 60) < comentario[j].Length)
                                    {
                                        temp += comentario[j].Substring(k, 60) + "\n";
                                    }
                                    else
                                    {
                                        temp += comentario[j].Substring(k, comentario[j].Length - k) + "\n";
                                    }
                                }
                            }
                            else
                            {
                                temp = comentario[j];
                            }
                            h.COMENTARIO += temp;
                        }
                    }

                    if (!IsPostBack)
                    {
                        if (h.BANFN.ToString().Contains("1"))
                        {
                            string a = "ads";
                        }
                    }

                    HIS.Add(h);
                }



                //27.01.2016-------------------------------------------------------------------------------
                //tab += " <table class='tablas1' id=\"tablas1\" border=1 style=\"width:1050px;height:95%;text-align:center\" ><thead>" +
                //        "<tr id = \"header\">" +
                //            "<th>Requisición</th>" +
                //            "<th>Fecha de <br>creación</th>" +
                //            "<th filter-type='ddl'>Solicitante</th>" +
                //            "<th>Estatus</th>" +
                //            "<th>Referencia</th>" +
                //            "<th>Solicitud<br>de pedido</th>" +
                //            "<th>Orden de<br> compra</th>" +
                //            "<th>Fecha de <br>modificación</th>" +
                //            "<th filter-type='ddl'>Última <br> modificación por</th>" +
                //            "<th>Comentarios</th>" +
                //        "</tr>" +
                //        "</thead>" +
                //        "<tbody>";
                //////////tab += " <table  class='CSSTableGenerator2' id=\"tablas1\" border=1 style=\"width:1150px;height:95%;text-align:center\" ><thead><tr id = \"header\"><th style='width:94px;' onclick='sort(1)'>Requisición</th><th style='width:71px;' onclick='sort(2)'>Fecha de <br>creación</th><th style='width:80px;' filter-type='ddl'>Solicitante</th><th style='width:80px;'>Estatus</th><th style='max-width:150px;'>Referencia</th><th style='width:77px;'>Solicitud<br>de pedido</th><th style='width:77px;'>Orden de<br> compra</th><th style='width:100px;'>Fecha de <br>modificación</th><th style='width:96px;' filter-type='ddl'>Última <br> modificación por</th><th>Comentarios</th></tr></thead><tbody>";
                tab += " <table  class='CSSTableGenerator2' id=\"tablas1\" border=1 style=\"width:1150px;height:95%;text-align:center\" ><thead><tr id = \"header\"><th style='width:94px;'>Requisición</th><th style='width:71px;'>Fecha de <br>creación</th><th style='width:80px;' filter-type='ddl'>Solicitante</th><th style='width:80px;'>Estatus</th><th style='max-width:150px;'>Referencia</th><th style='width:77px;'>Solicitud<br>de pedido</th><th style='width:77px;'>Orden de<br> compra</th><th style='width:100px;'>Fecha de <br>modificación</th><th style='width:96px;' filter-type='ddl'>Última <br> modificación por</th><th>Comentarios</th></tr></thead><tbody>";
                //27.01.2016-------------------------------------------------------------------------------
                for (int i = 0; i < HIS.Count; i++)
                {
                    tab += "<tr id='" + HIS[i].BANFN + "'>";
                    if (HIS[i].MODIF)
                    {
                        tab += "<td style='text-align:left;'>" + HIS[i].BANFN + "  <input type=\"button\" id=\"btn" + HIS[i].BANFN
                                             + "\" runat=\"server\" title=\"Modificar\" onclick=\"click_on(this)\" value=\"Modificar\" style='width:60px'></button></td>";

                    }
                    else
                    {
                        tab += "<td style='text-align:left;'>" + HIS[i].BANFN + "</td>";

                    }
                    //tab += "<td>" + HIS[i].BANFN + "</td>";
                    tab += "<td>" + HIS[i].F_CREACION + "</td>";
                    tab += "<td>" + HIS[i].SOLICITANTE + "</td>";
                    tab += "<td>" + HIS[i].ESTATUS.TEXTO + "</td>";
                    tab += "<td>" + HIS[i].REFERENCIA.ToUpper() + "</td>";
                    tab += "<td>" + HIS[i].SOLICITUD + "</td>";
                    tab += "<td>" + HIS[i].ORDEN + "</td>";
                    tab += "<td>" + HIS[i].F_MODIFICA + "</td>";
                    tab += "<td>" + HIS[i].MODIFICA + "</td>";
                    tab += "<td style='max-width:300px;width:300px;'>" + HIS[i].COMENTARIO + "</td>";
                    tab += "</tr>";
                }

                tab += "</tbody></table>";
                lblTabla1.Text = tab;
            }
            catch (NullReferenceException nullex)
            {
                string urlDestino = ConfigurationManager.AppSettings["loginPage"];//DEBE INICIAR SESIÓN
                Response.Redirect(urlDestino, false);
            }
            catch (Exception ex)
            {

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

        private string getStatus(ZEBAN_P_BE principal)
        {
            string status = "";
            decimal total = 0;
            bool datos = false;
            if (!principal.APROBO.Equals(""))
            {
                status = "Aprobado por " + principal.APROBO.ToUpper();
            }
            else
            {

                if (principal.STATUS.Equals("-1"))
                {
                    status = "RECHAZADO";
                }
                else if (principal.STATUS.Trim().Equals("-2"))
                {
                    status = "CANCELADO";
                }
                else
                {
                    List<ZEBAN_BE> posiciones = ZEBAN_BLL.GET(principal.BANFN);
                    List<ZEBAN_S_BE> servicios = new List<ZEBAN_S_BE>();

                    for (int i = 0; i < posiciones.Count; i++)
                    {
                        if (principal.COMPRADOR.Trim().Equals("") | principal.COMPRADOR.Trim().Equals("-"))
                        {
                            datos = true;
                        }
                        if (posiciones[i].TIPO.Equals("F"))
                        {
                            if (posiciones[i].IMP.Equals("K"))
                            {
                                List<ZEBAN_I_BE> imp = new List<ZEBAN_I_BE>();
                                imp = ZEBAN_I_BLL.GET(principal.BANFN, posiciones[i].BNFPO.Trim());
                                servicios = ZEBAN_S_BLL.GET(principal.BANFN, posiciones[i].BNFPO.Trim());
                                for (int j = 0; j < servicios.Count; j++)
                                {
                                    total += servicios[j].PREIS * servicios[j].MENGE;
                                }
                                for (int j = 0; j < imp.Count; j++)
                                {
                                    if (imp[j].COSTCENTER.Trim().Equals("") | imp[j].GL_ACCOUNT.Trim().Equals("") | posiciones[i].MAT_GR.Trim().Equals(""))
                                    {
                                        datos = true;
                                    }
                                }
                            }
                            else
                            {
                                total += posiciones[i].PREIS * posiciones[i].MENGE;
                            }
                        }
                        else
                        {
                            total += posiciones[i].PREIS * posiciones[i].MENGE;
                            if (posiciones[i].IMP.Equals("K"))
                            {
                                List<ZEBAN_I_BE> imp = new List<ZEBAN_I_BE>();
                                imp = ZEBAN_I_BLL.GET(principal.BANFN, posiciones[i].BNFPO.Trim());

                                for (int j = 0; j < imp.Count; j++)
                                {
                                    if (imp[j].COSTCENTER.Trim().Equals("") | imp[j].GL_ACCOUNT.Trim().Equals("") | posiciones[i].MAT_GR.Trim().Equals(""))
                                    {
                                        datos = true;
                                    }
                                }
                            }

                        }
                    }

                    status = "Por aprobar por: ";
                    if (principal.STATUS.Trim().Equals("3"))// | principal.STATUS.Trim().Equals("2"))
                    {
                        string[] conta = ConfigurationManager.AppSettings["contabilidad"].Split(';');
                        foreach (string st in conta)
                        {
                            if (conta.Length > 1)
                            {
                                status += st + ", ";
                            }
                            else
                            {
                                status += st;
                            }
                        }
                    }
                    else
                    {
                        //if (total >= 20000)
                        //{
                        //    List<USUARIO_BE> uu = USUARIO_BLL.GET(5);
                        //    for (int j = 0; j < uu.Count; j++)
                        //    {
                        //        status += uu[j].NipLogin + ", ";
                        //    }
                        //}
                        //else
                        //{
                        //    List<USUARIO_BE> uu = USUARIO_BLL.GET(4);
                        //    for (int j = 0; j < uu.Count; j++)
                        //    {
                        //        status += uu[j].NipLogin + ", ";
                        //    }
                        //}

                        if (principal.STATUS.Trim().Equals("4"))
                        {
                            List<USUARIO_BE> uu = USUARIO_BLL.GET(4);
                            for (int j = 0; j < uu.Count; j++)
                            {
                                status += uu[j].NipLogin + ", ";
                            }
                        }
                        else
                        {
                            List<USUARIO_BE> uu = USUARIO_BLL.GET(5);
                            for (int j = 0; j < uu.Count; j++)
                            {
                                status += uu[j].NipLogin + ", ";
                            }
                        }
                    }
                }
            }
            if (principal.STATUS.Trim().Equals("0") || principal.STATUS.Trim().Equals("1"))
            {
                datos = true;
            }
            if (datos)
            {
                if (principal.STATUS.Trim().Equals("0") || principal.STATUS.Trim().Equals("1"))
                {
                    status = "Por autorizar por: ";

                    status += USUARIO_BLL.GET(ZEBAN_P_BLL.GET(principal.BANFN + "")[0].FK_USUARIO)[0].AREA;

                }
                else if (principal.STATUS.Trim().Equals("2"))
                {
                    if (principal.COMPRADOR.Trim().Equals("-") || principal.COMPRADOR.Trim().Equals(""))
                    {
                        status = "Falta comprador asignado.";
                    }
                    else
                    {
                        status = "Espera datos contables";
                    }
                }
                else
                {
                    status = "Espera datos contables";
                }
            }
            else if (principal.STATUS.Trim().Equals("2"))
            {
                status = "Por autorizar por: ";
                string[] conta = ConfigurationManager.AppSettings["contabilidad"].Split(';');
                foreach (string st in conta)
                {
                    if (conta.Length > 1)
                    {
                        status += st + ", ";
                    }
                    else
                    {
                        status += st;
                    }
                }
            }

            return status;
        }

        [WebMethod(EnableSession = false)]
        public static string filtrarTodo(string valor, string userr, string num, string numActual)
        {
            string BANFN = valor;
            string tab = "";
            tab += " <table  class='CSSTableGenerator2' id=\"tablas1\" border=1 style=\"width:1150px;height:95%;text-align:center\" ><thead><tr id = \"header\"><th style='width:94px;'>Requisición</th><th style='width:71px;'>Fecha de <br>creación</th><th style='width:80px;' filter-type='ddl'>Solicitante</th><th style='width:80px;'>Estatus</th><th style='max-width:150px;'>Referencia</th><th style='width:77px;'>Solicitud<br>de pedido</th><th style='width:77px;'>Orden de<br> compra</th><th style='width:100px;'>Fecha de <br>modificación</th><th style='width:96px;' filter-type='ddl'>Última <br> modificación por</th><th>Comentarios</th></tr></thead><tbody>";

            int nums = int.Parse(num);
            try
            {
                //user = Request.Cookies.Get("UserName").Value; //USUARIO ACTUAL
                string user = userr;
                USUARIO_BE usuario = USUARIO_BLL.GET(user)[0];
                List<ZEBAN_P_BE> historial = ZEBAN_P_BLL.GET(usuario); //CREADAS POR EL USUARIO ACTUAL
                List<USUARIO_BE> UsuariosM = new List<USUARIO_BE>(); //USUARIOS DE MENOR RANGO

                //---------------------------------------------------OBTENER HISTORIAL---------------------------------------//
                int roo = rol2(usuario); //ROL DEL USUARIO
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
                if (roo == 1)
                {
                }
                else if (roo == 2)
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



                List<ZEBAN_P_BE> zeban = new List<ZEBAN_P_BE>();

                bool bandera = false;

                if (valor.Trim().Equals(""))
                {
                    zeban.AddRange(historial);
                    bandera = true;

                }
                else
                {

                    int B = Int32.Parse(BANFN);
                    for (int i = 0; i < historial.Count; i++)
                    {
                        if (historial[i].BANFN.Equals(B))
                        {
                            zeban.Add(historial[i]);
                            bandera = true;
                        }
                    }
                }

                if (bandera)
                {
                    List<HISTORIAL> HIS = new List<HISTORIAL>();
                    for (int i = 0; i < zeban.Count; i++)
                    {
                        if (i > (nums - 1))
                        {
                            continue;
                        }
                        int status = Int32.Parse(zeban[i].STATUS);
                        HISTORIAL h = new HISTORIAL();

                        if (zeban[i].SAP_BANFN.Trim() == "")
                        {
                            h.ORDEN = zeban[i].SAP_EBELN;


                            h.BANFN = zeban[i].BANFN;
                            h.F_CREACION = zeban[i].F_APROBACION.ToShortDateString();
                            h.SOLICITANTE = zeban[i].FK_USUARIO.ToUpper();
                            h.ESTATUS = new ESTATUS();
                            h.ESTATUS.TEXTO = getStatus2(zeban[i].BANFN);
                            h.REFERENCIA = zeban[i].REFERENCIA;
                            if (roo == 0)
                            {
                                if (status == 0 | status == -1)
                                {
                                    h.MODIF = true;
                                }
                                else
                                {
                                    h.MODIF = false;
                                }
                            }
                            else if (roo == 1)
                            {
                                if (status == 0 | status == 1)// | status == -1)
                                {
                                    h.MODIF = true;
                                }
                                else
                                {
                                    h.MODIF = false;
                                }
                            }
                            else if (roo == 2)
                            {
                                if (status == roo)
                                {
                                    h.MODIF = true;
                                }
                                else
                                {
                                    h.MODIF = false;
                                }
                            }
                            else if (roo == 3)
                            {
                                if (status == 2 | status == 3)
                                {
                                    h.MODIF = true;
                                }
                                else
                                {
                                    h.MODIF = false;
                                }
                            }
                        }
                        else
                        {
                            h.BANFN = zeban[i].BANFN;
                            h.F_CREACION = zeban[i].F_APROBACION.ToShortDateString();
                            h.SOLICITANTE = zeban[i].FK_USUARIO.ToUpper();
                            h.ESTATUS = new ESTATUS();
                            h.ESTATUS.TEXTO = getStatus2(zeban[i].BANFN);
                            h.ORDEN = zeban[i].SAP_EBELN;
                            h.REFERENCIA = zeban[i].REFERENCIA;
                        }

                        h.SOLICITUD = zeban[i].SAP_BANFN;

                        ZEBAN_H_BE hh = ZEBAN_H_BLL.GET(zeban[i].BANFN + "");
                        if (!hh.BANFN.Equals(""))
                        {
                            h.F_MODIFICA = hh.FECHA.ToShortDateString();
                        }
                        else
                        {
                            h.F_MODIFICA = "";
                        }
                        h.MODIFICA = hh.NIPLOGIN.ToUpper();
                        List<ZEBAN_C_BE> cs = ZEBAN_C_BLL.GET(zeban[i].BANFN);
                        DateTime date = new DateTime();
                        int ultimo = 0;
                        for (int j = 0; j < cs.Count; j++)
                        {
                            if (j == 0)
                            {
                                date = cs[j].FECHA;
                            }
                            else
                            {
                                if (date < cs[j].FECHA)
                                {
                                    date = cs[j].FECHA;
                                    ultimo = j;
                                }
                            }
                        }
                        if (cs.Count > 0)
                        {
                            string[] comentario = cs[ultimo].TEXTO.Split('\n');

                            for (int j = 0; j < comentario.Length; j++)
                            {
                                string temp = "";
                                if (comentario[j].Length > 60)
                                {
                                    for (int k = 0; k < comentario[j].Length; k = k + 60)
                                    {
                                        if ((k + 60) < comentario[j].Length)
                                        {
                                            temp += comentario[j].Substring(k, 60) + "\n";
                                        }
                                        else
                                        {
                                            temp += comentario[j].Substring(k, comentario[j].Length - k) + "\n";
                                        }
                                    }
                                }
                                else
                                {
                                    temp = comentario[j];
                                }
                                h.COMENTARIO += temp;
                            }
                        }

                        HIS.Add(h);

                    }
                    //27.01.2016-------------------------------------------------------------------------------
                    //tab += " <table class='tablas1' id=\"tablas1\" border=1 style=\"width:1050px;height:95%;text-align:center\" ><thead>" +
                    //        "<tr id = \"header\">" +
                    //            "<th>Requisición</th>" +
                    //            "<th>Fecha de <br>creación</th>" +
                    //            "<th filter-type='ddl'>Solicitante</th>" +
                    //            "<th>Estatus</th>" +
                    //            "<th>Referencia</th>" +
                    //            "<th>Solicitud<br>de pedido</th>" +
                    //            "<th>Orden de<br> compra</th>" +
                    //            "<th>Fecha de <br>modificación</th>" +
                    //            "<th filter-type='ddl'>Última <br> modificación por</th>" +
                    //            "<th>Comentarios</th>" +
                    //        "</tr>" +
                    //        "</thead>" +
                    //        "<tbody>";
                    //27.01.2016-------------------------------------------------------------------------------
                    for (int i = 0; i < HIS.Count; i++)
                    {
                        tab += "<tr id='" + HIS[i].BANFN + "' onclick='abre(this.id)'>";
                        if (HIS[i].MODIF)
                        {
                            tab += "<td style='text-align:left;'>" + HIS[i].BANFN + "  <input type=\"button\" id=\"btn" + HIS[i].BANFN
                                                 + "\" runat=\"server\" title=\"Modificar\" onclick=\"click_on(this)\" value=\"Modificar\" style='width:60px'></button></td>";

                        }
                        else
                        {
                            tab += "<td style='text-align:left;'>" + HIS[i].BANFN + "</td>";

                        }
                        //tab += "<td>" + HIS[i].BANFN + "</td>";
                        tab += "<td>" + HIS[i].F_CREACION + "</td>";
                        tab += "<td>" + HIS[i].SOLICITANTE + "</td>";
                        tab += "<td>" + HIS[i].ESTATUS.TEXTO + "</td>";
                        tab += "<td>" + HIS[i].REFERENCIA.ToUpper() + "</td>";
                        tab += "<td>" + HIS[i].SOLICITUD + "</td>";
                        tab += "<td>" + HIS[i].ORDEN + "</td>";
                        tab += "<td>" + HIS[i].F_MODIFICA + "</td>";
                        tab += "<td>" + HIS[i].MODIFICA + "</td>";
                        tab += "<td style='max-width:300px;width:300px;'>" + HIS[i].COMENTARIO + "</td>";
                        tab += "</tr>";
                    }

                    tab += "</tbody></table>";
                    return tab;
                }
            }
            catch
            {

            }
            tab += "<tr><td colspan='10' style='text-align:center;'>No hay datos para mostrar</td></tr>";
            return tab;
        }
        private static int rol2(USUARIO_BE u)
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
        private static string getStatus2(int b)
        {
            string status = "";
            ZEBAN_P_BE principal = ZEBAN_P_BLL.GET(b + "")[0];
            List<ZEBAN_BE> posiciones = ZEBAN_BLL.GET(b);
            List<ZEBAN_S_BE> servicios = new List<ZEBAN_S_BE>();
            decimal total = 0;
            bool datos = false;
            if (!principal.APROBO.Equals(""))
            {
                status = "Aprobado por " + principal.APROBO.ToUpper();
            }
            else
            {
                if (principal.STATUS.Equals("-1"))
                {
                    status = "RECHAZADO";
                }
                else if (principal.STATUS.Trim().Equals("-2"))
                {
                    status = "CANCELADO";
                }
                else
                {
                    for (int i = 0; i < posiciones.Count; i++)
                    {
                        if (principal.COMPRADOR.Trim().Equals("") | principal.COMPRADOR.Trim().Equals("-"))
                        {
                            datos = true;
                        }
                        if (posiciones[i].TIPO.Equals("F"))
                        {
                            if (posiciones[i].IMP.Equals("K"))
                            {
                                List<ZEBAN_I_BE> imp = new List<ZEBAN_I_BE>();
                                imp = ZEBAN_I_BLL.GET(b, posiciones[i].BNFPO.Trim());
                                servicios = ZEBAN_S_BLL.GET(b, posiciones[i].BNFPO.Trim());
                                for (int j = 0; j < servicios.Count; j++)
                                {
                                    total += servicios[j].PREIS * servicios[j].MENGE;
                                }
                                for (int j = 0; j < imp.Count; j++)
                                {
                                    if (imp[j].COSTCENTER.Trim().Equals("") | imp[j].GL_ACCOUNT.Trim().Equals("") | posiciones[i].MAT_GR.Trim().Equals(""))
                                    {
                                        datos = true;
                                    }
                                }
                            }
                            else
                            {
                                total += posiciones[i].PREIS * posiciones[i].MENGE;
                            }
                        }
                        else
                        {
                            total += posiciones[i].PREIS * posiciones[i].MENGE;
                            if (posiciones[i].IMP.Equals("K"))
                            {
                                List<ZEBAN_I_BE> imp = new List<ZEBAN_I_BE>();
                                imp = ZEBAN_I_BLL.GET(b, posiciones[i].BNFPO.Trim());

                                for (int j = 0; j < imp.Count; j++)
                                {
                                    if (imp[j].COSTCENTER.Trim().Equals("") | imp[j].GL_ACCOUNT.Trim().Equals("") | posiciones[i].MAT_GR.Trim().Equals(""))
                                    {
                                        datos = true;
                                    }
                                }
                            }

                        }
                    }

                    status = "Por aprobar por: ";
                    if (principal.STATUS.Trim().Equals("3"))// | principal.STATUS.Trim().Equals("2"))
                    {
                        string[] conta = ConfigurationManager.AppSettings["contabilidad"].Split(';');
                        foreach (string st in conta)
                        {
                            if (conta.Length > 1)
                            {
                                status += st + ", ";
                            }
                            else
                            {
                                status += st;
                            }
                        }
                    }
                    else
                    {
                        //if (total >= 20000)
                        //{
                        //    List<USUARIO_BE> uu = USUARIO_BLL.GET(5);
                        //    for (int j = 0; j < uu.Count; j++)
                        //    {
                        //        status += uu[j].NipLogin + ", ";
                        //    }
                        //}
                        //else
                        //{
                        //    List<USUARIO_BE> uu = USUARIO_BLL.GET(4);
                        //    for (int j = 0; j < uu.Count; j++)
                        //    {
                        //        status += uu[j].NipLogin + ", ";
                        //    }
                        //}

                        if (principal.STATUS.Trim().Equals("4"))
                        {
                            List<USUARIO_BE> uu = USUARIO_BLL.GET(4);
                            for (int j = 0; j < uu.Count; j++)
                            {
                                status += uu[j].NipLogin + ", ";
                            }
                        }
                        else
                        {
                            List<USUARIO_BE> uu = USUARIO_BLL.GET(5);
                            for (int j = 0; j < uu.Count; j++)
                            {
                                status += uu[j].NipLogin + ", ";
                            }
                        }
                    }
                }
            }
            if (principal.STATUS.Trim().Equals("0") || principal.STATUS.Trim().Equals("1"))
            {
                datos = true;
            }
            if (datos)
            {
                if (principal.STATUS.Trim().Equals("0") || principal.STATUS.Trim().Equals("1"))
                {
                    status = "Por autorizar por: ";

                    status += USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].AREA;

                }
                else if (principal.STATUS.Trim().Equals("2"))
                {
                    if (principal.COMPRADOR.Trim().Equals("-") || principal.COMPRADOR.Trim().Equals(""))
                    {
                        status = "Falta comprador asignado.";
                    }
                    else
                    {
                        status = "Espera datos contables";
                    }
                }
                else
                {
                    status = "Espera datos contables";
                }
            }
            else if (principal.STATUS.Trim().Equals("2"))
            {
                status = "Por autorizar por: ";
                string[] conta = ConfigurationManager.AppSettings["contabilidad"].Split(';');
                foreach (string st in conta)
                {
                    if (conta.Length > 1)
                    {
                        status += st + ", ";
                    }
                    else
                    {
                        status += st;
                    }
                }
            }

            return status;
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string cadena = txtBuscar.Text;
            string urlDestino = "Historial.aspx";

            if (!cadena.Trim().Equals(""))
                urlDestino += "?search=" + getBusqueda(cadena) + "&pag=1";
            else
                urlDestino += "?pag=1";

            Response.Redirect(urlDestino, false);
        }

        private string getBusqueda(string cadena)
        {
            string busqueda = cadena;
            busqueda = busqueda.Replace("%", "%25");
            busqueda = busqueda.Replace("/", "%2F");

            return busqueda;
        }

        private string setBusqueda(string cadena)
        {
            string busqueda = cadena;

            busqueda = busqueda.Replace("%2F", "/");
            //busqueda = busqueda.Replace('�', '/');

            return busqueda;
        }
    }
}