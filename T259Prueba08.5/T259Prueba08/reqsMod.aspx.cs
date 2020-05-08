using IBusiness;
using IEntities;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using T259Prueba08.Models;

namespace T259Prueba08
{
    public partial class reqsMod : System.Web.UI.Page
    {
        //static string BANFN;
        //static string user;
        static List<ZEBAN_BE> Items_a;
        string typ;

        protected void Page_Load(object sender, EventArgs e)
        {
            string BANFN = Request.QueryString["banfn"];
            //////try
            //////{
            //////    string user = Request.Cookies.Get("UserName").Value; //USUARIO ACTUAL
            //////    lblUsuario.Text = "<br><strong>Usuario</strong> > " + user.ToUpper() + "<br>";
            //////    USUARIO_BE usuario = USUARIO_BLL.GET(user)[0];
            //////    List<ZEBAN_P_BE> historial = ZEBAN_P_BLL.GET(usuario); //CREADAS POR EL USUARIO ACTUAL

            //////    for (int j = rol(usuario); j >= 0; j--)
            //////    {
            //////        List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(j); //CREADAS POR USUARIOS CON MENOR RANGO
            //////        for (int i = 0; i < temp.Count; i++)
            //////        {
            //////            if (historial.Any(f => f.BANFN == temp[i].BANFN) == false)
            //////            {
            //////                historial.Add(temp[i]);
            //////            }
            //////        }
            //////    }

            //////    List<ZEBAN_P_BE> temp2 = ZEBAN_P_BLL.GET("", user); //APROBADAS Y MODIFICADAS POR USUARIO ACTUAL
            //////    for (int i = 0; i < temp2.Count; i++)
            //////    {
            //////        if (historial.Any(f => f.BANFN == temp2[i].BANFN) == false)
            //////        {
            //////            historial.Add(temp2[i]);
            //////        }
            //////    }

            //////    bool bandera = false;
            //////    for (int i = 0; i < historial.Count; i++)
            //////    {
            //////        int status = Int32.Parse(historial[i].STATUS);
            //////        if (status <= rol(usuario))
            //////        {
            //////            List<USUARIO_BE> temp = USUARIO_BLL.GET(historial[i].APROBO);
            //////            if (temp.Count > 0)
            //////            {
            //////                USUARIO_BE aprobo = temp[0];
            //////                USUARIO_BE actual = USUARIO_BLL.GET(user)[0];

            //////                if (rol(actual) > rol(aprobo))
            //////                {

            //////                    if (historial[i].BANFN.Equals(Int32.Parse(BANFN)))
            //////                    {
            //////                        bandera = true;
            //////                    }
            //////                }
            //////            }
            //////            else
            //////            {
            //////                if (historial[i].BANFN.Equals(Int32.Parse(BANFN)))
            //////                {
            //////                    bandera = true;
            //////                }
            //////            }

            //////        }
            //////    }

            try
            {
                //user = Request.Cookies.Get("UserName").Value;
                //usuario = USUARIO_BLL.GET(user)[0];

                //List<ZEBAN_P_BE> historialt = new List<ZEBAN_P_BE>();
                //List<ZEBAN_P_BE> historial = new List<ZEBAN_P_BE>();

                //historialt = ZEBAN_P_BLL.GET(usuario);
                //for (int i = 0; i < historialt.Count; i++)
                //{
                //    if (historialt[i].FK_USUARIO.Equals(user))
                //    {
                //        historial.Add(historialt[i]);
                //    }
                //}

                //user = Request.Cookies.Get("UserName").Value; //USUARIO ACTUAL
                string user = Session["UserName"].ToString();
                lblUsuario.Text = "<br><strong>Usuario</strong> > " + user.ToUpper() + "<br>";
                USUARIO_BE usuario = USUARIO_BLL.GET(user)[0];
                List<ZEBAN_P_BE> historial = ZEBAN_P_BLL.GET(usuario); //CREADAS POR EL USUARIO ACTUAL
                List<USUARIO_BE> UsuariosM = new List<USUARIO_BE>();

                int roo = rol(usuario);
                if (roo == 1)
                {
                    List<USUARIO_BE> areas = USUARIO_BLL.GET("", usuario.NipLogin);
                    for (int i = 0; i < areas.Count; i++)
                    {
                        List<ZEBAN_P_BE> temp2 = ZEBAN_P_BLL.GET(areas[i]); //CREADAS POR USUARIOS CON MENOR RANGO
                        historial.AddRange(temp2);
                    }
                }
                else
                {
                    if (roo == 2)
                    {
                        List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(2); //CREADAS POR USUARIOS CON MENOR RANGO
                        for (int i = 0; i < temp.Count; i++)
                        {
                            historial.Add(temp[i]);
                        }
                    }

                    for (int j = rol(usuario) - 1; j >= 0; j--)
                    {
                        List<ZEBAN_P_BE> temp = ZEBAN_P_BLL.GET(j); //CREADAS POR USUARIOS CON MENOR RANGO
                        for (int i = 0; i < temp.Count; i++)
                        {
                            historial.Add(temp[i]);
                        }

                        List<USUARIO_BE> tempUsuariosM = USUARIO_BLL.GET(j);
                        UsuariosM.AddRange(tempUsuariosM);
                    }

                    List<ZEBAN_P_BE> temp2 = ZEBAN_P_BLL.GET("", user); //APROBADAS POR USUARIO ACTUAL
                    for (int i = 0; i < temp2.Count; i++)
                    {
                        historial.Add(temp2[i]);
                    }


                    List<ZEBAN_P_BE> temp3 = new List<ZEBAN_P_BE>(); //CREADAS POR USUARIOS CON MENOR RANGO
                    for (int i = 0; i < UsuariosM.Count; i++)
                    {
                        temp3.AddRange(ZEBAN_P_BLL.GET(UsuariosM[i]));

                    }
                    for (int i = 0; i < temp3.Count; i++)
                    {
                        if (historial.Any(f => f.BANFN == temp3[i].BANFN) == false)
                        {
                            historial.Add(temp3[i]);
                        }
                    }
                }
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
                    Conexion con = new Conexion();

                    List<ZEBAN_BE> eban = ZEBAN_BLL.GET(Int32.Parse(BANFN));
                    eban = eban.OrderBy(o => int.Parse(o.BNFPO)).ToList();
                    Items_a = eban;
                    ZEBAN_P_BE SAP = ZEBAN_P_BLL.GET(BANFN)[0]; //Encabezado
                    SAP = ZEBAN_P_BLL.GET(BANFN)[0];

                    string tab = "<hr>";
                    lblEban.Text = "No. de requisición :  <input id=\"Text1\" type=\"text\" value=\"" + BANFN + "\" disabled=\"true\"/><br><br>";
                    lblEban.Text += "Refer. de proyecto : <input id=\"referencia\" type=\"text\" value=\"" + SAP.REFERENCIA + "\" style='width:550px;'><br><br> ";

                    string tipos = "";

                    int r = rol(USUARIO_BLL.GET(user)[0]);


                    int id = 0;
                    for (int j = 0; j < eban.Count; j++)
                    {
                        id++;
                        tipos = tipo(eban[j].IMP, eban[j].TIPO);
                        typ = tipos;
                        tab += "<br><input id='desc" + id + "' value='" + eban[j].DESCR + "' hidden='hidden'/>";

                        if (tipos.Equals("S"))
                        {
                            tab += "<br><input id='tipo" + id + "' value='S' hidden='hidden'/>";
                            ZEBAN_I_BE imp = ZEBAN_I_BLL.GET(Int32.Parse(BANFN), "" + (id * 10))[0];
                            ZEBAN_S_BE ser = ZEBAN_S_BLL.GET(Int32.Parse(BANFN), "" + (id * 10))[0];

                            lblTabla.Text = "Servicio:";
                            tab += "<table><tr><td  style='width:130px;text-align:right;'>";
                            tab += "Servicio:<br><br> </td><td>";
                            tab += "<input id='serv" + id + "' disabled='disabled' value='";
                            tab += eban[j].DESCR + "' /><br><br></td></tr>";
                            tab += "<tr><td>";
                            tab += "Precio:</td><td>";

                            if (r > 2)
                            {
                                tab += "<input id='preis" + id + "' value='";
                                tab += ser.PREIS + "' />";
                                tab += "</td>";
                                tab += "<td style='width:130px;text-align:right;'>";
                                tab += "Moneda: </td><td>";
                                //tab += "<input id='waers" + id + "'  value='";
                                //tab += ser.WAERS + "'/>";
                                tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                    "<option value='MXN' ";
                                if (ser.WAERS == "MXN")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">MXN</option>" +
                                    "<option value='USD'";
                                if (ser.WAERS == "USD")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">USD</option>" +
                                   "<option value='EUR'";
                                if (ser.WAERS == "EUR")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">EUR</option>" +
                                "</select>";

                                tab += "</td></tr><tr><td>";
                                tab += "Grupo de artículo: </td><td>";
                                tab += "<input id='gr_a" + id + "'  value='";
                                tab += eban[j].MAT_GR.Trim() + "' /><script>autocompleta_GR_A('gr_a" + id + "');</script>";
                                //tab += eban[j].MAT_GR + "' disabled='disabled' />";
                                tab += "<td style='width:130px;text-align:right;'>";
                                tab += "Centro de coste: </td><td>";
                                tab += "<input id='coste" + id + "'  value='";
                                tab += imp.COSTCENTER.Trim() + "'/><script>autocompleta_coste('coste" + id + "');</script></td>";

                                //27.01.2017-----------------------------------------------------------------------------
                                if (eban[j].BNFPO.Trim().Equals("10"))
                                {
                                    tab += "<td><input id='chkCoste' type='button' value='Copiar' onclick='copiarCoste(" + eban.Count + ")'/></td>";
                                }
                                tab += "</td>";
                                //27.01.2017-----------------------------------------------------------------------------

                                tab += "<tr><td>";
                                tab += "Cantidad: </td><td>";
                                tab += "<input id='quan" + id + "'  value='";
                                tab += ser.MENGE + "' />";

                                tab += "</td>";
                                tab += "<td style='text-align:right;'>";
                                tab += "Cta de mayor: </td><td>";
                                tab += "<input type= 'text' id='cta" + id + "'  value='";
                                tab += imp.GL_ACCOUNT.Trim() + "' /><script>autocompleta_cta('cta" + id + "');</script></td>";

                                //27.01.2017-----------------------------------------------------------------------------
                                if (eban[j].BNFPO.Trim().Equals("10"))
                                {
                                    tab += "<td><input id='chkCta' type='button' value='Copiar' onclick='copiarCta(" + eban.Count + ")'/></td>";
                                }
                                tab += "</tr>";
                                //27.01.2017-----------------------------------------------------------------------------

                            }
                            else
                            {
                                if (r == 2)
                                {
                                    tab += "<input id='preis" + id + "' value='";
                                    tab += ser.PREIS + "' />";
                                    tab += "</td>";
                                    tab += "<td style='width:130px;text-align:right;'>";
                                    tab += "Moneda: </td><td>";
                                    //tab += "<input id='waers" + id + "'  value='";
                                    tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                    "<option value='MXN' ";
                                    if (ser.WAERS == "MXN")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">MXN</option>" +
                                        "<option value='USD'";
                                    if (ser.WAERS == "USD")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">USD</option>" +
                                   "<option value='EUR'";
                                    if (ser.WAERS == "EUR")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">EUR</option>" +
                                    "</select>";
                                }
                                else
                                {
                                    tab += "<input id='preis" + id + "' disabled='disabled' value='";
                                    tab += ser.PREIS + "' />";
                                    tab += "</td>";
                                    tab += "<td style='width:80px;text-align:center;'>";
                                    tab += "Moneda: </td><td>";
                                    tab += "<input id='waers" + id + "' disabled='disabled' value='";
                                    tab += ser.WAERS + "'/>";
                                }


                                tab += "</td></tr><tr><td>";
                                tab += "Grupo de artículo: </td><td>";
                                tab += "<input id='gr_a" + id + "'  value='";
                                tab += eban[j].MAT_GR + "' disabled='disabled' />";
                                tab += "<td style='width:130px;text-align:right;' hidden='hidden'>";
                                tab += "Centro de coste: </td><td>";
                                tab += "<input id='coste" + id + "'  hidden='hidden' value='";
                                tab += imp.COSTCENTER.Trim() + "'/><script>autocompleta_coste('coste" + id + "');</script>";

                                //////////27.01.2017-----------------------------------------------------------------------------
                                ////////if (eban[j].BNFPO.Trim().Equals("10"))
                                ////////{
                                ////////    tab += "<td><input id='chkCoste' type='button' value='Copiar' onclick='copiarCoste(" + eban.Count + ")'/></td>";
                                ////////}
                                ////////tab += "</td>";
                                //////////27.01.2017-----------------------------------------------------------------------------


                                tab += "<tr><td>";
                                tab += "Cantidad: </td><td>";
                                tab += "<input id='quan" + id + "'  value='";
                                tab += ser.MENGE + "' />";

                                tab += "</td>";
                                tab += "<td style='text-align:right;' hidden='hidden'>";
                                tab += "Cta de mayor: </td><td>";
                                tab += "<input type= 'text' id='cta" + id + "'  hidden='hidden' value='";
                                tab += imp.GL_ACCOUNT.Trim() + "' /><script>autocompleta_cta('cta" + id + "');</script></td>";
                                //////////27.01.2017-----------------------------------------------------------------------------
                                ////////if (eban[j].BNFPO.Trim().Equals("10"))
                                ////////{
                                ////////    tab += "<td><input id='chkCta' type='button' value='Copiar' onclick='copiarCta(" + eban.Count + ")'/></td>";
                                ////////}
                                tab += "</tr>";
                                //27.01.2017-----------------------------------------------------------------------------
                            }
                        }
                        else if (tipos.Equals("A"))
                        {

                            tab += "<br><input id='tipo" + id + "' value='A' hidden='hidden'/>";
                            ZEBAN_I_BE imp = ZEBAN_I_BLL.GET(Int32.Parse(BANFN), "" + (id * 10))[0];
                            ZEBAN_S_BE ser = ZEBAN_S_BLL.GET(Int32.Parse(BANFN), "" + (id * 10))[0];

                            lblTabla.Text = "Activo:";
                            tab += "<table><tr><td  style='width:130px;text-align:right;'>";
                            tab += "Activo: <br><br> </td><td>";
                            tab += "<input id='acti" + id + "' disabled='disabled' value='";
                            tab += eban[j].DESCR + "' /><br><br></td></tr>";
                            tab += "<tr><td>";
                            tab += "Precio:</td><td>";

                            if (r > 2)
                            {
                                //tab += "<input id='preis' value='";
                                //tab += ser.PREIS + "' />";
                                ////txtPreis.Text = ser.PREIS + "";
                                ////txtPreis.Enabled = true;
                                //tab += "</td><td style='width:130px;text-align:right;'>";
                                //tab += "Cta de mayor: </td><td>";
                                //tab += "<input id='cta'  value='";
                                //tab += imp.GL_ACCOUNT.Trim() + "' /><script>autocompleta_cta('cta');</script>";
                                ////txtCta.Text = imp.GL_ACCOUNT.Trim();
                                ////txtCta.Visible = true;
                                ////lblCta.Text = "Cta de mayor:";

                                tab += "<input id='preis" + id + "' value='";
                                tab += ser.PREIS + "' />";
                                tab += "</td>";
                                tab += "<td style='width:130px;text-align:right;'>";
                                tab += "Moneda: </td><td>";
                                //tab += "<input id='waers" + id + "'  value='";
                                //tab += ser.WAERS + "'/>";
                                tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                    "<option value='MXN' ";
                                if (ser.WAERS == "MXN")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">MXN</option>" +
                                    "<option value='USD'";
                                if (ser.WAERS == "USD")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">USD</option>" +
                                   "<option value='EUR'";
                                if (ser.WAERS == "EUR")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">EUR</option>" +
                                "</select>";

                                tab += "</td></tr><tr><td>";
                                tab += "Grupo de artículo: </td><td>";
                                tab += "<input id='gr_a" + id + "'  value='";
                                //tab += eban[j].MAT_GR + "' disabled='disabled' /></td></tr>";
                                tab += eban[j].MAT_GR.Trim() + "'  /><script>autocompleta_GR_A('gr_a" + id + "');</script></td></tr>";
                                //tab += "<td style='width:130px;text-align:right;'>";
                                //tab += "Centro de coste: </td><td>";
                                //tab += "<input id='coste'  value='";
                                //tab += imp.COSTCENTER.Trim() + "'/><script>autocompleta_coste('coste');</script>";

                                tab += "<tr><td>";
                                tab += "Cantidad: </td><td>";
                                tab += "<input id='quan" + id + "'  value='";
                                tab += ser.MENGE + "' />";

                                tab += "</td>";
                                //tab += "<td style='text-align:right;'>";
                                //tab += "Cta de mayor: </td><td>";
                                //tab += "<input id='cta'  value='";
                                //tab += imp.GL_ACCOUNT.Trim() + "' /><script>autocompleta_cta('cta');</script></td></tr>";
                                tab += "</tr>";
                            }
                            else
                            {
                                if (r == 2)
                                {
                                    tab += "<input id='preis" + id + "' value='";
                                    tab += ser.PREIS + "' />";
                                    tab += "</td>";
                                    tab += "<td style='width:130px;text-align:right;'>";
                                    tab += "Moneda: </td><td>";
                                    //tab += "<input id='waers" + id + "' value='";
                                    tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                    "<option value='MXN' ";
                                    if (ser.WAERS == "MXN")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">MXN</option>" +
                                        "<option value='USD'";
                                    if (ser.WAERS == "USD")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">USD</option>" +
                                   "<option value='EUR'";
                                    if (ser.WAERS == "EUR")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">EUR</option>" +
                                    "</select>";
                                }
                                else
                                {
                                    tab += "<input id='preis" + id + "' disabled='disabled' value='";
                                    tab += ser.PREIS + "' />";
                                    tab += "</td>";
                                    tab += "<td style='width:130px;text-align:right;'>";
                                    tab += "Moneda: </td><td>";
                                    tab += "<input id='waers" + id + "' disabled='disabled' value='";
                                    tab += ser.WAERS + "'/>";
                                }


                                tab += "</td></tr><tr><td>";
                                tab += "Grupo de artículo: </td><td>";
                                tab += "<input id='gr_a" + id + "'  value='";
                                tab += eban[j].MAT_GR + "' disabled='disabled' /></td></tr>";

                                tab += "<tr><td>";
                                tab += "Cantidad: </td><td>";
                                tab += "<input id='quan" + id + "'  value='";
                                tab += ser.MENGE + "' />";

                                tab += "</td>";
                                tab += "<td style='text-align:right;' hidden='hidden'>";
                                tab += "Cta de mayor: </td><td>";
                                tab += "<input type= 'text' id='cta" + id + "' hidden='hidden' value='";
                                tab += imp.GL_ACCOUNT.Trim() + "' /></td></tr>";
                            }
                        }
                        else if (tipos.Equals("M"))
                        {
                            tab += "<br><input id='tipo" + id + "' value='M' hidden='hidden'/>";
                            lblTabla.Text = "Material:";
                            tab += "<table><tr><td style='width:130px;'>";
                            tab += "Material:<br><br> </td><td>";
                            tab += "<input id='mate" + id + "' style='width:200px;'  disabled='disabled' value='";
                            tab += eban[j].MATNR + " - " + eban[j].DESCR + "' /><br><br></td></tr>";
                            tab += "<tr><td>";
                            tab += "Precio:</td><td>";

                            if (r > 0)
                            {
                                tab += "<input id='preis" + id + "' value='";
                                tab += eban[j].PREIS + "' />";
                                tab += "</td>";
                                tab += "<td style='width:80px;text-align:right;'>";
                                tab += "Moneda: </td><td>";
                                //tab += "<input id='waers" + id + "'  value='";
                                //tab += eban[j].WAERS + "'/>";
                                tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                   "<option value='MXN' ";
                                if (eban[j].WAERS == "MXN")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">MXN</option>" +
                                    "<option value='USD'";
                                if (eban[j].WAERS == "USD")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">USD</option>" +
                                    "<option value='EUR'";
                                if (eban[j].WAERS == "EUR")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">EUR</option>" +
                                "</select>";
                            }
                            else
                            {
                                tab += "<input id='preis" + id + "' value='";
                                tab += eban[j].PREIS + "' disabled='disabled'/>";
                                tab += "</td>";
                                tab += "<td style='width:80px;text-align:right;'>";
                                tab += "Moneda: </td><td>";
                                tab += "<input id='waers" + id + "'  value='";
                                tab += eban[j].WAERS + "' disabled='disabled'/>";
                            }

                            tab += "</td></tr><tr><td>";
                            tab += "Grupo de artículo: </td><td>";
                            tab += "<input id='gr_a" + id + "'   value='";
                            tab += eban[j].MAT_GR + "' disabled='disabled' />";
                            tab += "</td></tr><tr><td>";
                            tab += "Cantidad: </td><td>";
                            tab += "<input id='quan" + id + "'   value='";
                            tab += eban[j].MENGE + "' />";
                        }
                        else
                        {
                            tab += "<br><input id='tipo" + id + "' value='I' hidden='hidden'/>";
                            ZEBAN_I_BE imp = ZEBAN_I_BLL.GET(Int32.Parse(BANFN), "" + (id * 10))[0];
                            //ZEBAN_S_BE ser = ZEBAN_S_BLL.GET(Int32.Parse(BANFN), "" + (id * 10))[0];

                            lblTabla.Text = "Material:";
                            tab += "<table><tr><td  style='width:130px;text-align:right;'>";
                            tab += "Material:<br><br> </td><td>";
                            tab += "<input id='impu" + id + "' disabled='disabled' value='";
                            tab += eban[j].DESCR + "' /><br><br></td></tr>";
                            tab += "<tr><td>";
                            tab += "Precio:</td><td>";

                            if (r > 2)
                            {
                                tab += "<input id='preis" + id + "' value='";
                                tab += eban[j].PREIS + "' />";
                                tab += "</td>";
                                tab += "<td style='width:130px;text-align:right;'>";
                                tab += "Moneda: </td><td>";
                                //tab += "<input id='waers" + id + "'  value='";
                                //tab += ser.WAERS + "'/>";
                                tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                    "<option value='MXN' ";
                                if (eban[j].WAERS == "MXN")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">MXN</option>" +
                                    "<option value='USD'";
                                if (eban[j].WAERS == "USD")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">USD</option>" +
                                   "<option value='EUR'";
                                if (eban[j].WAERS == "EUR")
                                {
                                    tab += "selected='selected'";
                                }
                                tab += ">EUR</option>" +
                                "</select>";

                                tab += "</td></tr><tr><td>";
                                tab += "Grupo de artículo: </td><td>";
                                //tab += "<input id='gr_a" + id + "'  value='";
                                tab += "<input id='gr_a" + id + "'  disabled='disabled' value='";
                                //tab += eban[j].MAT_GR + "' disabled='disabled' />";
                                tab += eban[j].MAT_GR.Trim() + "' /><script>autocompleta_GR_A('gr_a" + id + "');</script>";
                                tab += "<td style='width:130px;text-align:right;'>";
                                tab += "Centro de coste: </td><td>";
                                tab += "<input id='coste" + id + "'  value='";
                                tab += imp.COSTCENTER.Trim() + "'/><script>autocompleta_coste('coste" + id + "');</script></td>";

                                //27.01.2017-----------------------------------------------------------------------------
                                if (eban[j].BNFPO.Trim().Equals("10"))
                                {
                                    tab += "<td><input id='chkCoste' type='button' value='Copiar' onclick='copiarCoste(" + eban.Count + ")'/></td>";
                                }
                                tab += "</td>";
                                //27.01.2017-----------------------------------------------------------------------------


                                tab += "<tr><td>";
                                tab += "Cantidad: </td><td>";
                                tab += "<input id='quan" + id + "'  value='";
                                tab += eban[j].MENGE + "' />";

                                tab += "</td>";
                                tab += "<td style='text-align:right;'>";
                                tab += "Cta de mayor: </td><td>";
                                tab += "<input type= 'text' id='cta" + id + "'  value='";
                                tab += imp.GL_ACCOUNT.Trim() + "' /><script>autocompleta_cta('cta" + id + "');</script></td>";

                                //27.01.2017-----------------------------------------------------------------------------
                                if (eban[j].BNFPO.Trim().Equals("10"))
                                {
                                    tab += "<td><input id='chkCta' type='button' value='Copiar' onclick='copiarCta(" + eban.Count + ")'/></td>";
                                }
                                tab += "</tr>";
                                //27.01.2017-----
                                
                            }
                            else
                            {
                                if (r == 2)
                                {
                                    tab += "<input id='preis" + id + "' value='";
                                    tab += eban[j].PREIS + "' />";
                                    tab += "</td>";
                                    tab += "<td style='width:130px;text-align:right;'>";
                                    tab += "Moneda: </td><td>";
                                    //tab += "<input id='waers" + id + "'  value='";
                                    tab += "<select  id='waers" + id + "' onchange='cambiaSel(" + id + "," + eban.Count + ")'>" +
                                    "<option value='MXN' ";
                                    if (eban[j].WAERS == "MXN")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">MXN</option>" +
                                        "<option value='USD'";
                                    if (eban[j].WAERS == "USD")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">USD</option>" +
                                   "<option value='EUR'";
                                    if (eban[j].WAERS == "EUR")
                                    {
                                        tab += "selected='selected'";
                                    }
                                    tab += ">EUR</option>" +
                                    "</select>";
                                }
                                else
                                {
                                    tab += "<input id='preis" + id + "' disabled='disabled' value='";
                                    tab += eban[j].PREIS + "' />";
                                    tab += "</td>";
                                    tab += "<td style='width:80px;text-align:center;'>";
                                    tab += "Moneda: </td><td>";
                                    tab += "<input id='waers" + id + "' disabled='disabled' value='";
                                    tab += eban[j].WAERS + "'/>";
                                }


                                tab += "</td></tr><tr><td>";
                                tab += "Grupo de artículo: </td><td>";
                                tab += "<input id='gr_a" + id + "'  value='";
                                tab += eban[j].MAT_GR + "' disabled='disabled' />";
                                tab += "<td style='width:130px;text-align:right;' hidden='hidden'>";
                                tab += "Centro de coste: </td><td>";
                                tab += "<input id='coste" + id + "'  hidden='hidden' value='";
                                tab += imp.COSTCENTER.Trim() + "'/><script>autocompleta_coste('coste" + id + "');</script>";

                                //////////27.01.2017-----------------------------------------------------------------------------
                                ////////if (eban[j].BNFPO.Trim().Equals("10"))
                                ////////{
                                ////////    tab += "<td><input id='chkCoste' type='button' value='Copiar' onclick='copiarCoste(" + eban.Count + ")'/></td>";
                                ////////}
                                ////////tab += "</td>";
                                //////////27.01.2017-----------------------------------------------------------------------------

                                tab += "<tr><td>";
                                tab += "Cantidad: </td><td>";
                                tab += "<input id='quan" + id + "'  value='";
                                tab += eban[j].MENGE + "' />";

                                tab += "</td>";
                                tab += "<td style='text-align:right;' hidden='hidden'>";
                                tab += "Cta de mayor: </td><td>";
                                tab += "<input type= 'text' id='cta" + id + "'  hidden='hidden' value='";
                                tab += imp.GL_ACCOUNT.Trim() + "' /><script>autocompleta_cta('cta" + id + "');</script></td>";


                                //////////27.01.2017-----------------------------------------------------------------------------
                                ////////if (eban[j].BNFPO.Trim().Equals("10"))
                                ////////{
                                ////////    tab += "<td><input id='chkCta' type='button' value='Copiar' onclick='copiarCta(" + eban.Count + ")'/></td>";
                                ////////}
                                tab += "</tr>";
                                //////////27.01.2017-----------------------------------------------------------------------------

                            }
                        }

                        tab += "</td></tr><tr><td>";

                        string browser = Request.Browser.Type;
                        string fecha = "";
                        int dia = eban[j].F_ENTREGA.Day;
                        int mes = eban[j].F_ENTREGA.Month;
                        string day = "";
                        string mon = "";

                        if (dia < 10)
                        {
                            day = "0" + eban[j].F_ENTREGA.Day;
                        }
                        else
                        {
                            day = eban[j].F_ENTREGA.Day + "";
                        }
                        if (mes < 10)
                        {
                            mon = "0" + eban[j].F_ENTREGA.Month;
                        }
                        else
                        {
                            mon = eban[j].F_ENTREGA.Month + "";
                        }

                        if (browser.ToCharArray()[0].Equals('C'))
                        {
                            fecha = eban[j].F_ENTREGA.Year + "-" + mon + "-" + day;
                        }
                        else
                        {
                            fecha = day + "/" + mon + "/" + eban[j].F_ENTREGA.Year;
                        }

                        tab += "Fecha de entrega: </td><td>";
                        tab += "<input id='date" + id + "' type='date' value='";
                        tab += fecha + "' /></td></tr>";
                        //txtFecha.Text = fecha;
                        if (tipos.Equals("S"))
                        {

                        }

                        tab += "</td></tr><table><br><hr />";

                    }

                    lblTabla.Text = tab;

                    lblBotones.Text = "<input type=\"button\" value=\"Guardar\" onclick=\"crear('" + user + "','" + id + "')\" class=\"btnCrear\" style=\"text-align: right\" />";

                    if (r == 2)
                    {
                        lblComprador.Text = "Comprador: <select id='comp2' >";
                        string[] compradores_temp = ConfigurationManager.AppSettings["compradores"].Split(';');
                        List<string> compradores = new List<string>();

                        foreach (string c in compradores_temp)
                        {
                            List<USUARIO_BE> uss = USUARIO_BLL.GET(c);
                            if (uss.Count > 0)
                            {
                                compradores.Add(c.ToUpper());
                            }
                        }
                        lblComprador.Text += "<option selected= 'selected' value='-'>-</option>";

                        for (int p = 0; p < compradores.Count; p++)
                        {
                            if (compradores[p].Equals(ZEBAN_P_BLL.GET(BANFN)[0].COMPRADOR))
                            {
                                lblComprador.Text += "<option selected='selected' value='" + compradores[p] + "'>" + compradores[p] + "</option>";
                            }
                            else
                            {
                                lblComprador.Text += "<option value='" + compradores[p] + "'>" + compradores[p] + "</option>";
                            }
                        }
                        lblComprador.Text += "</select>";

                    }


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

                    //add += "</table>";
                    //add += "</div>";
                    //lblAdj.Text = add;

                    //if (archivo.Length > 15)
                    //{
                    //    archivo = archivo.Substring(0, 15);
                    //}

                    //add += "<tr><td><div id=\"" + folder + "|" + archivo + "." + ext + "\"style=\"background-color: white;text-align:left;width:160px;padding:10px 5px 5px 5px\"" +
                    //    " onmouseover=\"this.style.cursor='pointer';\"" +
                    //    " onclick=\"descargar(this.id)\">" +
                    //"<image src=\"//ssl.gstatic.com/docs/doclist/images/mediatype/icon_1_" + tipo + "_x16.png\" AlternateText=\"Texto\"/>&nbsp;&nbsp;" +

                    //archivo + "</div>" + "</td>";
                    //////}


                    //Label lblComentarios = new Label();
                    //lblComentarios.Text = obtenerComentarios(BANFN);
                    //form1.Controls.Add(lblComentarios);

                }
                else
                {
                    string urlDestino = "Historial.aspx";
                    Response.Redirect(urlDestino, false);
                }
            }
            catch (Exception error)
            {
                string ei = error.Message;


                String csname1 = "PopupScript";
                Type cstype = this.GetType();

                ClientScriptManager cs = Page.ClientScript;
                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('" + ei + "');";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
                string urlDestino = ConfigurationManager.AppSettings["loginPage"];
                Response.Redirect(urlDestino, false);
            }

        }

        private string tipo(string imputacion, string tipo)
        {
            if (imputacion.Equals("K") && tipo.Equals("F"))
            {
                return "S";
            }
            else if (imputacion.Equals("A") && tipo.Equals("F"))
            {
                return "A";
            }
            else if (imputacion.Equals("") && imputacion.Equals(""))
            {
                return "M";
            }
            else if (imputacion.Equals("K") && !tipo.Equals("F"))
            {
                return "I";
            }
            else
            {
                return "";
            }
        }

        private static int rol(USUARIO_BE u)
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

        //private int rol(USUARIO_BE u)
        //{
        //    int r = 0;
        //    if (u.DIREC)
        //    {
        //        r = 5;
        //    }
        //    else if (u.GEREN)
        //    {
        //        r = 4;
        //    }
        //    else if (u.CONTA)
        //    {
        //        r = 3;
        //    }
        //    else if (u.USUA)
        //    {
        //        r = 2;
        //    }
        //    else if (USUARIO_BLL.GET("", u.NipLogin).Count > 0)
        //    {
        //        r = 1;
        //    }
        //    else
        //    {
        //        r = 0;
        //    }
        //    return r;
        //}

        [WebMethod(EnableSession = false)]
        public static string crear(string tipo, string url, string valores, string id, string us, string comentario)// Crea requisición
        {
            string res = "";

            if (tipo == "S")
            {
                //res = modiServicios(valores, id, us, comentario);
            }
            else if (tipo == "A")
            {
                //res = modiActivos(valores, id, us, comentario);
            }
            else
            {
                //res = modifMateriales(valores, id, us, comentario);
            }

            //subirImagen();

            return res;
        }


        private static string modiServicios(string b, string valores, string id, string us, int cont)
        {
            string[] item = valores.Split('|');
            int banfn = Int32.Parse(b);
            ZEBAN_BE items = ZEBAN_BLL.GET(banfn)[0];

            //ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
            ////header.BANFN = banfn;
            ////header.FK_USUARIO = user;
            //header.STATUS = getStatus(us);
            //header.F_APROBACION = DateTime.Now;
            //int conf = ZEBAN_P_BLL.UPDATE(header); //Insertar registro en SQL

            ////string mat_gr;
            ////if (!item[6].Trim().Equals(""))
            ////{
            ////    mat_gr = getMat_gr(item[6]);
            ////    if (mat_gr.Equals(""))
            ////    {
            ////        return "M";
            ////    }
            ////    else
            ////    {
            ////        item[2] = mat_gr;
            ////    }
            ////}

            int conf = 0;

            items.BANFN = banfn;
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            items.MATNR = "";
            items.MENGE = Decimal.Parse(item[3]);
            items.PREIS = Decimal.Parse(item[1]);
            items.WAERS = item[7].ToUpper();
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            items.TIPO = "F";
            items.IMP = "K";
            items.WERKS = ConfigurationManager.AppSettings["werks"];

            List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            Items.Add(items);
            conf = ZEBAN_BLL.UPDATE(Items); //Insertar registro en SQL

            List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
            ZEBAN_S_BE temp = ZEBAN_S_BLL.GET(banfn, "" + (cont * 10))[0];
            //temp.BANFN = banfn;
            //temp.BNFPO = "10";
            //temp.SERIAL = 1;
            //temp.MEINS = "SER";
            //temp.SERVICIO = serv.GetString("SERVICIO");
            //temp.TEXTO = con.check_servDesc(temp.SERVICIO);
            temp.MENGE = Decimal.Parse(item[3]);
            temp.PREIS = Decimal.Parse(item[1]);
            temp.WAERS = item[7].ToUpper();

            servs.Add(temp);
            conf += ZEBAN_S_BLL.UPDATE(servs); //Insertar registro en SQL

            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            ZEBAN_I_BE tempi = new ZEBAN_I_BE();
            tempi.BANFN = banfn;
            tempi.BNFPO = "" + (cont * 10);
            tempi.SERIAL = servs[0].SERIAL;
            tempi.LINE = 1;
            tempi.GL_ACCOUNT = item[6];
            tempi.COSTCENTER = item[5];
            imps.Add(tempi);
            conf += ZEBAN_I_BLL.UPDATE(imps); //Insertar registro en SQL

            //ZEBAN_H_BE his = new ZEBAN_H_BE();
            //his.BANFN = banfn + "";
            //his.FECHA = DateTime.Now;
            //his.NIPLOGIN = us;

            //ZEBAN_H_BLL.INSERT(his);

            //if (!comentario.Equals(""))
            //{
            //    ZEBAN_C_BE com = new ZEBAN_C_BE();
            //    com.BANFN = banfn;
            //    com.FK_USUARIO = us;
            //    com.TEXTO = comentario;

            //    conf += ZEBAN_C_BLL.INSERT(com);
            //}


            return "B";
        }

        private static string modiActivos(string b, string valores, string id, string us, int cont)
        {
            string[] item = valores.Split('|');
            ZEBAN_BE items = new ZEBAN_BE();
            int banfn = Int32.Parse(b);
            items = ZEBAN_BLL.GET(banfn)[0];
            //ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
            ////header.BANFN = banfn;
            ////header.FK_USUARIO = user;
            //header.STATUS = getStatus(us);
            //header.F_APROBACION = DateTime.Now;
            //int conf = ZEBAN_P_BLL.UPDATE(header); //Insertar registro en SQL
            int conf = 0;


            ////string mat_gr;
            ////if (!item[6].Trim().Equals(""))
            ////{
            ////    mat_gr = getMat_gr(item[6]);
            ////    if (mat_gr.Equals(""))
            ////    {
            ////        return "M";
            ////    }
            ////    else
            ////    {
            ////        item[2] = mat_gr;
            ////    }
            ////}

            items.BANFN = banfn;
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            items.MATNR = "";
            items.MENGE = Decimal.Parse(item[3]);
            items.PREIS = Decimal.Parse(item[1]);
            items.WAERS = item[7].ToUpper();
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            items.TIPO = "F";
            items.IMP = "A";
            items.WERKS = ConfigurationManager.AppSettings["werks"];

            List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            Items.Add(items);
            conf = ZEBAN_BLL.UPDATE(Items); //Insertar registro en SQL

            List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
            ZEBAN_S_BE temp = ZEBAN_S_BLL.GET(banfn, "" + (cont * 10))[0];
            //temp.BANFN = banfn;
            //temp.BNFPO = "10";
            //temp.SERIAL = 1;
            //temp.MEINS = "SER";
            //temp.SERVICIO = serv.GetString("SERVICIO");
            //temp.TEXTO = con.check_servDesc(temp.SERVICIO);
            temp.MENGE = Decimal.Parse(item[3]);
            temp.PREIS = Decimal.Parse(item[1]);
            temp.WAERS = item[7].ToUpper();

            servs.Add(temp);
            conf += ZEBAN_S_BLL.UPDATE(servs); //Insertar registro en SQL

            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            ZEBAN_I_BE tempi = ZEBAN_I_BLL.GET(banfn, "" + (cont * 10))[0];
            tempi.BANFN = banfn;
            tempi.BNFPO = "" + (cont * 10);
            tempi.SERIAL = servs[0].SERIAL;
            tempi.LINE = 1;
            if (item[6] != "")
            {
                tempi.GL_ACCOUNT = item[6];
            }
            if (item[5] != "")
            {
                tempi.COSTCENTER = item[5];
            }
            tempi.MENGE = Decimal.Parse(item[3]);
            imps.Add(tempi);
            conf += ZEBAN_I_BLL.UPDATE(imps); //Insertar registro en SQL

            //ZEBAN_H_BE his = new ZEBAN_H_BE();
            //his.BANFN = banfn + "";
            //his.FECHA = DateTime.Now;
            //his.NIPLOGIN = us;

            //ZEBAN_H_BLL.INSERT(his);

            //if (!comentario.Equals(""))
            //{
            //    ZEBAN_C_BE com = new ZEBAN_C_BE();
            //    com.BANFN = banfn;
            //    com.FK_USUARIO = us;
            //    com.TEXTO = comentario;

            //    conf += ZEBAN_C_BLL.INSERT(com);
            //}

            return "B";
        }

        private static ZEBAN_BE modifMateriales(string b, string valores, string id, string us, int cont)
        {
            string[] item = valores.Split('|');
            ZEBAN_BE items = new ZEBAN_BE();
            int banfn = Int32.Parse(b);

            ////ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
            //////header.BANFN = banfn;
            //////header.FK_USUARIO = user;
            ////header.STATUS = getStatus(us);
            ////header.F_APROBACION = DateTime.Now;

            ////int conf = ZEBAN_P_BLL.UPDATE(header); //Insertar registro en SQL

            items.BANFN = banfn;
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            items.MATNR = id;
            items.MENGE = Decimal.Parse(item[3]);
            items.PREIS = Decimal.Parse(item[1]);
            items.WAERS = item[7].ToUpper();
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            //items.TIPO = "F";
            //items.IMP = "A";
            items.WERKS = ConfigurationManager.AppSettings["werks"];

            List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            Items.Add(items);
            int conf = ZEBAN_BLL.UPDATE(Items); //Insertar registro en SQL

            ZEBAN_H_BE his = new ZEBAN_H_BE();
            his.BANFN = banfn + "";
            his.FECHA = DateTime.Now;
            his.NIPLOGIN = us;

            ZEBAN_H_BLL.INSERT(his);

            ////if (!comentario.Equals(""))
            ////{
            ////    ZEBAN_C_BE com = new ZEBAN_C_BE();
            ////    com.BANFN = banfn;
            ////    com.FK_USUARIO = us;
            ////    com.TEXTO = comentario;

            ////    conf += ZEBAN_C_BLL.INSERT(com);
            ////}

            return items;
        }

        private static string modiImpu(string b, string valores, string id, string us, int cont)
        {
            string[] item = valores.Split('|');
            int banfn = Int32.Parse(b);
            ZEBAN_BE items = ZEBAN_BLL.GET(banfn)[0];

            //ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
            ////header.BANFN = banfn;
            ////header.FK_USUARIO = user;
            //header.STATUS = getStatus(us);
            //header.F_APROBACION = DateTime.Now;
            //int conf = ZEBAN_P_BLL.UPDATE(header); //Insertar registro en SQL
            int conf = 0;

            string mat_gr;
            if (!item[6].Trim().Equals(""))
            {
                mat_gr = getMat_gr(item[6]);
                if (mat_gr.Equals(""))
                {
                    return "M";
                }
                else
                {
                    item[2] = mat_gr;
                }
            }

            items.BANFN = banfn;
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            items.MATNR = "";
            items.MENGE = Decimal.Parse(item[3]);
            decimal m = Decimal.Parse(item[1]);
            items.PREIS = m;
            items.WAERS = item[7].ToUpper();
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            items.TIPO = "";
            items.IMP = "K";
            items.WERKS = ConfigurationManager.AppSettings["werks"];

            List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            Items.Add(items);
            conf = ZEBAN_BLL.UPDATE(Items); //Insertar registro en SQL

            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            ZEBAN_I_BE tempi = new ZEBAN_I_BE();
            tempi.BANFN = banfn;
            tempi.BNFPO = "" + (cont * 10);
            tempi.SERIAL = 1;
            tempi.LINE = 1;
            tempi.GL_ACCOUNT = item[6];
            tempi.COSTCENTER = item[5];
            imps.Add(tempi);
            conf += ZEBAN_I_BLL.UPDATE(imps); //Insertar registro en SQL

            //ZEBAN_H_BE his = new ZEBAN_H_BE();
            //his.BANFN = banfn + "";
            //his.FECHA = DateTime.Now;
            //his.NIPLOGIN = us;

            //ZEBAN_H_BLL.INSERT(his);

            //if (!comentario.Equals(""))
            //{
            //    ZEBAN_C_BE com = new ZEBAN_C_BE();
            //    com.BANFN = banfn;
            //    com.FK_USUARIO = us;
            //    com.TEXTO = comentario;

            //    conf += ZEBAN_C_BLL.INSERT(com);
            //}

            return "B";
        }

        private static int getStatus(string usuario)
        {
            List<USUARIO_BE> users = USUARIO_BLL.GET(usuario);
            USUARIO_BE u = new USUARIO_BE();

            int s = 0;
            if (users.Count > 0)
            {
                u = users[0];
            }

            //if (u.DIREC)
            //{
            //    s = "4";
            //}
            //else if (u.GEREN)
            //{
            //    s = "3";
            //}
            //else if (u.CONTA)
            //{
            //    s = "2";
            //}
            //else if (u.USUA)
            //{
            //    s = "1";
            //}
            //else
            //{
            //    s = "0"; //Básico   
            //}
            if (u.DIREC)//Dirección
            {
                s = 5;
            }
            else if (u.GEREN)//Gerencia Gral.
            {
                s = 4;
            }
            else if (u.CONTA)//Contabilidad
            {
                s = 3;
            }
            else if (u.USUA)//Compras
            {
                s = 2;
            }
            else if (USUARIO_BLL.GET("", u.NipLogin).Count > 0)//Gerente de área
            {
                s = 1;
            }
            else
            {
                s = 0; //Básico   
            }

            return s;
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
        protected void btn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string id = btn.ID.Substring(0, btn.ID.Length - 1);
            string name = btn.Text;

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

        //private string obtenerComentarios(string banfn)
        //{
        //    string tabla = "";
        //    List<ZEBAN_C_BE> comentarios = ZEBAN_C_BLL.GET(Int32.Parse(banfn));
        //    if (comentarios.Count > 0)
        //    {
        //        tabla = "<br><div  class='CSSTableGenerator'><table style='width:100%;'><tr><td>Comentarios</td></tr>";
        //        for (int i = 0; i < comentarios.Count; i++)
        //        {
        //            tabla += "<tr><td><strong>";
        //            tabla += comentarios[i].FECHA.ToString() + "  " + comentarios[i].FK_USUARIO;
        //            tabla += "</strong><br>";

        //            string[] comentario = comentarios[i].TEXTO.Split('\n');
        //            comentarios[i].TEXTO = "\n";
        //            for (int j = 0; j < comentario.Length; j++)
        //            {
        //                string temp = "";
        //                if (comentario[j].Length > 100)
        //                {
        //                    for (int k = 0; k < comentario[j].Length; k = k + 100)
        //                    {
        //                        if ((k + 100) < comentario[j].Length)
        //                        {
        //                            temp += comentario[j].Substring(k, 100) + "\n";
        //                        }
        //                        else
        //                        {
        //                            temp += comentario[j].Substring(k, comentario[j].Length - k) + "\n";
        //                        }
        //                    }
        //                }
        //                else
        //                {
        //                    temp += comentario[j] + "\n";
        //                }
        //                comentarios[i].TEXTO += temp;

        //            }

        //            string texto = comentarios[i].TEXTO.Replace("\n", "<br>");
        //            tabla += texto;
        //            tabla += "</tr></td>";
        //        }
        //        tabla += "</table></div>";
        //    }


        //    return tabla;
        //}
        //////private string obtenerComentarios(string banfn)
        //////{

        //////    List<ZEBAN_C_BE> comentarios = ZEBAN_C_BLL.GET(Int32.Parse(banfn));

        //////    string tabla = "<div style='width: 100%; text-align: right;'>";
        //////    //tabla += "<input type=\"button\" value=\"Agregar comentario\" onclick=\"agregar('" + BANFN + "','" + user + "')\"  class=\"btnCrear\"/>";
        //////    tabla += "<br>";


        //////    if (comentarios.Count > 0)
        //////    {
        //////        tabla += "<input type='checkbox' id='onlyyes' hidden='hidden' />" +
        //////           "<input type='checkbox' id='onlyno' hidden='hidden' />" +
        //////           "<br />" +
        //////               // "<div style='text-align: right'>" +
        //////               "<a id='cleanfilters2' href='#'>Limpiar filtros</a>" +
        //////               "<input type='text' id='quickfind' placeholder='Buscar' />" +
        //////           //   "<br />" +
        //////           // "</div>" +

        //////           "<script src='http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js' type='text/javascript'></script>" +
        //////           "<script type='text/javascript' src='Scripts/picnet.table.filter.min.js'></script>" +


        //////           "<script type='text/javascript'>" +
        //////              " $(document).ready(function () {" +
        //////                  // Initialise Plugin
        //////                  " var options1 = {" +
        //////                       "additionalFilterTriggers: [$('#onlyyes'), $('#onlyno'), $('#quickfind')]," +
        //////                       "clearFiltersControls: [$('#cleanfilters2')]," +
        //////                       "matchingRow: function (state, tr, textTokens) {" +
        //////                           "if (!state || !state.id) { return true; }" +
        //////                           "var val = tr.children('td:eq(2)').text();" +
        //////                           "switch (state.id) {" +
        //////                               "case 'onlyyes': return state.value !== 'true' || val === 'yes';" +
        //////                               "case 'onlyno': return state.value !== 'true' || val === 'no';" +
        //////                               "default: return true;" +
        //////                           "}" +
        //////                       "}" +
        //////                   "};" +
        //////                   "$('#tablas2').tableFilter(options1);" +
        //////               "});" +
        //////           "</script>";
        //////    }


        //////    if (comentarios.Count > 0)
        //////    {
        //////        tabla += "<br><div class='CSSTableGenerator2' style=' width:100%;'><table class='tablas2' id='tablas2' style=' width:96%;'><thead><tr><th>Fecha</th>"
        //////            + "<th filter-type='ddl'>Usuario</th><th>Comentario</th></tr></thead><tbody>";


        //////        for (int i = 0; i < comentarios.Count; i++)
        //////        {
        //////            string color = "white";
        //////            //if (user.Trim().Equals(comentarios[i].FK_USUARIO))
        //////            if (USUARIO_BLL.GET(comentarios[i].FK_USUARIO)[0].DIREC)
        //////            {
        //////                color = "#8ebded";
        //////            }
        //////            tabla += "<tr><td style='padding: 3px 5px 3px 5px;background-color:" + color + ";vertical-align:top;width:120px;'>";
        //////            tabla += comentarios[i].FECHA.ToString() + "</td><td style='background-color:" + color + ";vertical-align:top;width:auto;width:70px;'>" + comentarios[i].FK_USUARIO;
        //////            tabla += "</td><td style='background-color:" + color + "'>";

        //////            string[] comentario = comentarios[i].TEXTO.Split('\n');
        //////            comentarios[i].TEXTO = "";
        //////            for (int j = 0; j < comentario.Length; j++)
        //////            {
        //////                string temp = "";
        //////                if (comentario[j].Length > 100)
        //////                {
        //////                    for (int k = 0; k < comentario[j].Length; k = k + 100)
        //////                    {
        //////                        if ((k + 100) < comentario[j].Length)
        //////                        {
        //////                            temp += comentario[j].Substring(k, 100) + "\n";
        //////                        }
        //////                        else
        //////                        {
        //////                            temp += comentario[j].Substring(k, comentario[j].Length - k) + "\n";
        //////                        }
        //////                    }
        //////                }
        //////                else
        //////                {
        //////                    temp += comentario[j] + "\n";
        //////                }
        //////                comentarios[i].TEXTO += temp;

        //////            }

        //////            string texto = comentarios[i].TEXTO.Replace("\n", "<br>");
        //////            tabla += texto + "</td>";
        //////            tabla += "</tr>";
        //////        }
        //////        tabla += "</tbody></table></div></div>";
        //////    }


        //////    return tabla;
        //////}
        private string obtenerComentarios(string banfn)
        {
            List<ZEBAN_C_BE> zebanCBeList = ZEBAN_C_BLL.GET(int.Parse(banfn));
            string str1 = "<div style='width: 100%; text-align: right;'>" + "<br>";
            if (zebanCBeList.Count > 0)
                //str1 += "<input type='button' value='Copiar' id='onlyyes' hidden='hidden' /><input type='button' value='Copiar' id='onlyno' hidden='hidden' /><br /><a id='cleanfilters2' href='#'>Limpiar filtros</a><input type='text' id='quickfind' placeholder='Buscar' /><script src='http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js' type='text/javascript'></script><script type='text/javascript' src='Scripts/picnet.table.filter.min.js'></script><script type='text/javascript'> $(document).ready(function () { var options1 = {additionalFilterTriggers: [$('#onlyyes'), $('#onlyno'), $('#quickfind')],clearFiltersControls: [$('#cleanfilters2')],matchingRow: function (state, tr, textTokens) {if (!state || !state.id) { return true; }var val = tr.children('td:eq(2)').text();switch (state.id) {case 'onlyyes': return state.value !== 'true' || val === 'yes';case 'onlyno': return state.value !== 'true' || val === 'no';default: return true;}}};$('#tablas2').tableFilter(options1);});</script>";
                str1 += "<br /><a id='cleanfilters2' href='#'>Limpiar filtros</a><input type='text' id='quickfind' placeholder='Buscar' /><script src='http://ajax.microsoft.com/ajax/jquery/jquery-1.3.2.min.js' type='text/javascript'></script><script type='text/javascript' src='Scripts/picnet.table.filter.min.js'></script><script type='text/javascript'> $(document).ready(function () { var options1 = {additionalFilterTriggers: [$('#onlyyes'), $('#onlyno'), $('#quickfind')],clearFiltersControls: [$('#cleanfilters2')],matchingRow: function (state, tr, textTokens) {if (!state || !state.id) { return true; }var val = tr.children('td:eq(2)').text();switch (state.id) {case 'onlyyes': return state.value !== 'true' || val === 'yes';case 'onlyno': return state.value !== 'true' || val === 'no';default: return true;}}};$('#tablas2').tableFilter(options1);});</script>";
            if (zebanCBeList.Count > 0)
            {
                string str2 = str1 + "<br><div  class='CSSTableGenerator2' style=' width:100%;'><table class='tablas2' id='tablas2' style=' width:96%;'><thead><tr><th>Fecha</th><th filter-type='ddl'>Usuario</th><th>Comentario</th></tr></thead><tbody>";
                for (int index1 = 0; index1 < zebanCBeList.Count; ++index1)
                {
                    string str3 = "white";
                    if (USUARIO_BLL.GET(zebanCBeList[index1].FK_USUARIO)[0].DIREC)
                        str3 = "#8ebded";
                    string str4 = str2 + "<tr><td style='padding: 3px 5px 3px 5px;background-color:" + str3 + ";vertical-align:top;width:120px;'>" + zebanCBeList[index1].FECHA.ToString() + "</td><td style='background-color:" + str3 + ";vertical-align:top;width:auto;width:70px;'>" + zebanCBeList[index1].FK_USUARIO + "</td><td style='background-color:" + str3 + "'>";
                    string[] strArray = zebanCBeList[index1].TEXTO.Split('\n');
                    zebanCBeList[index1].TEXTO = "";
                    for (int index2 = 0; index2 < strArray.Length; ++index2)
                    {
                        string str5 = "";
                        if (strArray[index2].Length > 100)
                        {
                            int startIndex = 0;
                            while (startIndex < strArray[index2].Length)
                            {
                                str5 = startIndex + 100 >= strArray[index2].Length ? str5 + strArray[index2].Substring(startIndex, strArray[index2].Length - startIndex) + "\n" : str5 + strArray[index2].Substring(startIndex, 100) + "\n";
                                startIndex += 100;
                            }
                        }
                        else
                            str5 = str5 + strArray[index2] + "\n";
                        zebanCBeList[index1].TEXTO += str5;
                    }
                    string str6 = zebanCBeList[index1].TEXTO.Replace("\n", "<br>");
                    str2 = str4 + str6 + "</td>" + "</tr>";
                }
                str1 = str2 + "</tbody></table></div></div>";
            }
            return str1;
        }



        [WebMethod(EnableSession = false)]
        public static string crear2(string b, string url, string valores, string us, string comentario, string comp, string refer)// Crea requisición
        {
            string tipo = "";
            string id = "";
            string[] tabla = valores.Split('|');
            string BANFN = b;

            ZEBAN_P_BE header = ZEBAN_P_BLL.GET(BANFN)[0];
            int banfn = Int32.Parse(BANFN);
            if (!comp.Equals(""))
            {
                header.COMPRADOR = comp;
            }
            header.REFERENCIA = refer;
            //header.BANFN = banfn;
            //header.FK_USUARIO = user;
            if (us.Equals(header.FK_USUARIO.Trim()))
            {
                if (header.STATUS.Trim().Equals(getStatus(us) + ""))
                {
                    header.STATUS = getStatus(us) + "";
                }
                else
                {
                    if (!(header.STATUS.Trim().Equals("2") & getStatus(us).Equals(3)))
                    {
                        header.STATUS = "0";
                    }
                    else
                    {
                        if (!header.COMPRADOR.Equals("") & !header.COMPRADOR.Equals("-"))
                        {
                            for (int i = 0; i < tabla.Length - 1; i += 10)
                            {
                                if (tabla[i + 7].Equals("") | tabla[i + 8].Equals(""))
                                {
                                    header.STATUS = getStatus(us) - 1 + "";
                                }
                                else
                                {
                                    header.STATUS = getStatus(us) + "";
                                }
                            }
                            //List<ZEBAN_I_BE> imps = ZEBAN_I_BLL.GET(
                        }
                    }
                }
            }
            else
            {
                string st = header.STATUS;
                header.STATUS = getStatus(us) + "";

                List<USUARIO_BE> subs = USUARIO_BLL.GET("", us);
                foreach (USUARIO_BE s in subs)
                {
                    if (s.NipLogin.Equals(header.FK_USUARIO.Trim()))
                    {
                        header.STATUS = st;
                    }
                }
                if (header.STATUS.Equals(st))
                {
                    if (!header.COMPRADOR.Equals("") & !header.COMPRADOR.Equals("-"))
                    {
                        for (int i = 0; i < tabla.Length - 1; i += 10)
                        {
                            if (tabla[i + 7].Equals("") | tabla[i + 8].Equals(""))
                            {
                                header.STATUS = getStatus(us) - 1 + "";
                            }
                            else
                            {
                                header.STATUS = getStatus(us) + "";
                            }
                        }
                    }
                }
            }

            //header.F_APROBACION = DateTime.Now;


            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();
            List<ZEBAN_S_BE> serv = new List<ZEBAN_S_BE>();


            string valores_temp = "";

            ////////if(!tabla[8].Trim().Equals("")){
            ////////    List<ZACCXMATG_BE> zacc = ZACCXMATG_BLL.GET(tabla[8]);
            ////////    if(zacc.Count > 0){

            ////////    }
            ////////}


            int cont = 0;
            for (int i = 0; i < tabla.Length - 1; i += 10)
            {
                if (cont > 0)
                {
                    cont = (i / 10) + 1;
                }
                else
                {
                    cont = i + 1;
                }
                valores_temp = tabla[i + 2] + "|"
                    + tabla[i + 3] + "|" + tabla[i + 4] + "|" + tabla[i + 5] + "|" + tabla[i + 6] + "|" + tabla[i + 7] + "|" + tabla[i + 8] + "|" + tabla[i + 9] + "|";
                tipo = tabla[i];
                id = tabla[i + 1];
                string rs_matn = "";

                if (tipo == "S")
                {
                    //res = creaServicios(valores, id, comentario);
                    //List<Object> list = new List<object>();
                    rs_matn = modiServicios(b,valores_temp, id, us, cont);

                    ////ZEBAN_BE item_s = new ZEBAN_BE();
                    ////ZEBAN_I_BE imp_s = new ZEBAN_I_BE();
                    ////ZEBAN_S_BE serv_s = new ZEBAN_S_BE();

                    ////item_s = (ZEBAN_BE)list[0];
                    ////items.Add(item_s);
                    ////imp_s = (ZEBAN_I_BE)list[1];
                    ////imps.Add(imp_s);
                    ////serv_s = (ZEBAN_S_BE)list[2];
                    ////serv.Add(serv_s);
                }
                else if (tipo == "A")
                {
                    //res = creaActivos(valores, id, comentario);
                    //List<Object> list = new List<object>();
                    //list = modifActivos(valores_temp, id, us, cont);
                    rs_matn = modiActivos(b,valores_temp, id, us, cont);

                    ////ZEBAN_BE item_i = new ZEBAN_BE();
                    ////ZEBAN_I_BE imp_i = new ZEBAN_I_BE();
                    ////ZEBAN_S_BE serv_i = new ZEBAN_S_BE();

                    ////item_i = (ZEBAN_BE)list[0];
                    ////items.Add(item_i);
                    ////imp_i = (ZEBAN_I_BE)list[1];
                    ////imps.Add(imp_i);
                    ////serv_i = (ZEBAN_S_BE)list[2];
                    ////serv.Add(serv_i);
                }
                else if (tipo == "M")
                {
                    ZEBAN_BE item = new ZEBAN_BE();
                    item = modifMateriales(b,valores_temp, id, us, cont);
                    //items.Add(item);
                }
                else
                {
                    rs_matn = modiImpu(b,valores_temp, id, us, cont);
                }

                if (rs_matn.Equals("M"))
                {
                    return "La cta. de mayor no tiene configurado un grupo de materiales predeterminado.";
                }


            }



            int conf = ZEBAN_P_BLL.UPDATE(header);
            bool si = false;
            decimal suma = 0;
            List<ZEBAN_BE> items = ZEBAN_BLL.GET(header.BANFN);
            foreach (ZEBAN_BE z in items)
            {
                if (z.MAT_GR.Trim().Equals(""))
                {
                    si = true;
                }
                if (z.IMP.Equals("K"))
                {
                    List<ZEBAN_I_BE> imp = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO);
                    foreach (ZEBAN_I_BE im in imp)
                    {
                        if (im.GL_ACCOUNT.Trim().Equals("") | im.COSTCENTER.Trim().Equals(""))
                        {
                            si = true;
                        }
                        if (z.TIPO.Trim().Equals("F"))
                        {
                            ZEBAN_S_BE ser = ZEBAN_S_BLL.GET(z.BANFN, z.BNFPO)[0];
                            suma = suma + (ser.MENGE * ser.PREIS);
                        }

                    }
                }
                else
                {
                    suma = suma + (z.MENGE * z.PREIS);
                }
            }
            if (header.STATUS == "3")
            {
                if (!si)
                {
                    enviarContaPrin(banfn, url);
                }
            }
            ZEBAN_H_BE his = new ZEBAN_H_BE();
            his.BANFN = banfn + "";
            his.FECHA = DateTime.Now;
            his.NIPLOGIN = us;

            ZEBAN_H_BLL.INSERT(his);

            if (!comentario.Equals(""))
            {
                ZEBAN_C_BE com = new ZEBAN_C_BE();
                com.BANFN = banfn;
                com.FK_USUARIO = us;
                com.TEXTO = comentario;

                conf += ZEBAN_C_BLL.INSERT(com);
            }

            if (getStatus(us).Equals(2) && comp != "-")
            {
                if (!si)
                {
                    enviarContaPrin(banfn, url);
                }
                else
                {
                    enviarConta(banfn, url);
                }
            }

            if (header.STATUS.Trim().Equals("0"))
            {
                enviarArea(us, banfn, url);
            }


            return "B";
        }

        private static void enviarConta(int b, string url)
        {
            Mail email = new Mail();
            int r = 3;

            List<USUARIO_BE> conta = USUARIO_BLL.GET(r); //Usuarios de contabilidad
            r = 0;
            for (int i = 0; i < conta.Count; i++)
            {
                if (rol(conta[i]) == 3)
                {
                    r++;
                }
            }
            string[] dest3 = new string[r];
            string[] names3 = new string[r];
            r = 0;

            for (int i = 0; i < conta.Count; i++)
            {
                if (rol(conta[i]) == 3)
                {
                    dest3[r] = conta[i].eMail;
                    names3[r] = conta[i].NipLogin;
                    r++;
                }
            }

            string asunto = "Requisición en espera de datos contables";
            string mensaje = "La requisición: " + b + " está en espera de datos contables<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}

            string new_url = "";
            string[] urls = url.Split('/');
            for (int i = 0; i < urls.Length - 1; i++)
            {
                new_url += urls[i] + "/";
            }

            email.send(dest3, asunto, mensaje, b, new_url);
        }

        private static void enviarContaPrin(int b, string url)
        {
            Mail email = new Mail();
            int r = 3;

            List<USUARIO_BE> conta =
                USUARIO_BLL.GET(ConfigurationManager.AppSettings["contabilidad"]); //Usuarios de contabilidad

            string[] dest3 = new string[1];
            string[] names3 = new string[1];
            r = 0;

            for (int i = 0; i < 1; i++)
            {
                dest3[i] = conta[i].eMail;
                names3[i] = conta[i].NipLogin;
            }

            string asunto = "Requisición en espera de aprobación";
            string mensaje = "La requisición: " + b + " está en espera aprobación<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}

            string new_url = "";
            string[] urls = url.Split('/');
            for (int i = 0; i < urls.Length - 1; i++)
            {
                new_url += urls[i] + "/";
            }

            email.send(dest3, asunto, mensaje, b, new_url);
        }

        private static void enviarGerente(int b, string url, decimal suma)
        {
            Mail email = new Mail();
            int r = 0;
            if (suma < 20000)
            {
                r = 4;
            }
            else
            {
                r = 5;
            }

            List<USUARIO_BE> compras = USUARIO_BLL.GET(r); //Usuarios de contabilidad
            string[] dest3 = new string[compras.Count];
            string[] names3 = new string[compras.Count];

            for (int i = 0; i < compras.Count; i++)
            {
                dest3[i] = compras[i].eMail;
                names3[i] = compras[i].NipLogin;
            }

            string asunto = "Requisición en espera de aprobación";
            string mensaje = "La requisición: " + b + " está en espera de aprobación<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}

            string new_url = "";
            string[] urls = url.Split('/');
            for (int i = 0; i < urls.Length - 1; i++)
            {
                new_url += urls[i] + "/";
            }

            email.send(dest3, asunto, mensaje, b, new_url);
        }

        static private void enviarArea(string u, int b, string url)
        {
            Mail email = new Mail();
            //int r = 1;

            List<USUARIO_BE> gerencia = USUARIO_BLL.GET(USUARIO_BLL.GET(u)[0].AREA); //Usuarios gerencia de area
            string[] dest3 = new string[gerencia.Count];
            string[] names3 = new string[gerencia.Count];

            for (int i = 0; i < gerencia.Count; i++)
            {
                dest3[i] = gerencia[i].eMail;
                names3[i] = gerencia[i].NipLogin;
            }

            string asunto = "Se ha modificado una requisición";
            string mensaje = u.ToUpper() + " modificó la requisición: " + b + "<br>para su aprobación.<br><br>";
            for (int i = 0; i < names3.Length; i++)
            {
                mensaje += names3[i] + "<br>";
            }

            string[] uurl = url.Split('/');
            string url_final = "";
            for (int k = 0; k < uurl.Length - 1; k++)
            {
                url_final += uurl[k] + "/";
            }
            email.send(dest3, asunto, mensaje, b, url_final);
        }

        public static string getMat_gr(string GL_ACCOUNT)
        {
            List<ZACCXMATG_BE> zacc = ZACCXMATG_BLL.GET(GL_ACCOUNT);

            if (zacc.Count > 0)
            {
                return zacc[0].MAT_GR;
            }
            else
            {
                return "";
            }
        }
    }
}