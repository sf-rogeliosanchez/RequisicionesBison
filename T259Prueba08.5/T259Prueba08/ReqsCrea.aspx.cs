using IBusiness;
using IEntities;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using T259Prueba08.Models;

namespace T259Prueba08
{
    public partial class ReqsCrea : System.Web.UI.Page
    {
        //static string user;
        protected void Page_Load(object sender, EventArgs e)
        {
            bool post = IsPostBack;

            try
            {
                //Button4.Attributes.Add("onclick", "return false;");
                //user = Request.Cookies.Get("UserName").Value; //Consulta cookie con nombre de usuario
                string user = Session["UserName"].ToString();
                lblUsuario.Text = "<br><strong>Usuario</strong> > " + user.ToUpper() + "<br>";
                List<USUARIO_BE> usuario = USUARIO_BLL.GET(user);
                if (usuario.Count < 1)
                {
                    string urlDestino = ConfigurationManager.AppSettings["loginPage"]; //Página configurable de login
                    Response.Redirect(urlDestino, false);
                }
            }
            catch
            {
                string urlDestino = ConfigurationManager.AppSettings["loginPage"]; //Página configurable de login
                Response.Redirect(urlDestino, false);
            }
        }

        [WebMethod(EnableSession = false)]
        public static string crear(string tipo, string url, string valores, string id, string comentario)// Crea requisición
        {
            string res = "";

            if (tipo == "s")
            {
                //res = creaServicios(valores, id, comentario);
            }
            else if (tipo == "a")
            {
                //res = creaActivos(valores, id, comentario);
            }
            else
            {
                //res = creaMateriales(valores, id, comentario);
            }

            return res;
        }

        private static List<Object> creaServicios(string valores, string id, int cont)
        {
            string[] item = valores.Split('|');
            ////ZEBAN_P_BE header = new ZEBAN_P_BE();
            ZEBAN_BE items = new ZEBAN_BE();
            Conexion con = new Conexion();


            ////header.FK_USUARIO = user;
            ////header.STATUS = getStatus(user);
            ////header.F_APROBACION = DateTime.Now;

            ////int conf = ZEBAN_P_BLL.INSERT(header); //Insertar registro en SQL
            ////List<ZEBAN_P_BE> zeban_p = ZEBAN_P_BLL.GET();
            ////int b = zeban_p[(zeban_p.Count - 1)].BANFN;
            ////int banfn = b;

            ////items.BANFN = banfn;
            IRfcTable serv = con.check_servicio(id, "S");
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.DESCR = serv.GetString("DESCR");
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            serv.CurrentIndex = 0;
            //items.MEINS = serv.GetString("UM");
            items.MEINS = "SER";
            //items.MENGE = Decimal.Parse(item[3]);            
            items.MENGE = 1;
            //items.PREIS = Decimal.Parse(item[1]);
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            items.TIPO = "F";
            items.IMP = "K";
            items.WERKS = ConfigurationManager.AppSettings["werks"];
            items.WAERS = item[5];

            ////List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            ////Items.Add(items);
            ////conf += ZEBAN_BLL.INSERT(Items); //Insertar registro en SQL

            List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
            ZEBAN_S_BE temp = new ZEBAN_S_BE();
            //temp.BANFN = banfn;
            temp.BNFPO = "" + (cont * 10);
            temp.SERIAL = 1;
            temp.MEINS = "SER";
            temp.SERVICIO = serv.GetString("SERVICIO");
            temp.TEXTO = con.check_servDesc(temp.SERVICIO);
            temp.MENGE = Decimal.Parse(item[3]);
            temp.PREIS = Decimal.Parse(item[1]);
            temp.WAERS = item[5];

            servs.Add(temp);
            //conf += ZEBAN_S_BLL.INSERT(servs); //Insertar registro en SQL

            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            ZEBAN_I_BE tempi = new ZEBAN_I_BE();
            //tempi.BANFN = banfn;
            tempi.BNFPO = "" + (cont * 10);
            tempi.SERIAL = 1;
            tempi.LINE = 1;
            tempi.GL_ACCOUNT = servs[0].GL_ACCOUNT;
            tempi.COSTCENTER = servs[0].COSTCENTER;
            imps.Add(tempi);
            //conf += ZEBAN_I_BLL.INSERT(imps); //Insertar registro en SQL

            //if (!comentario.Equals(""))
            //{
            //    ZEBAN_C_BE com = new ZEBAN_C_BE();
            //    com.BANFN = banfn;
            //    com.FK_USUARIO = user;
            //    com.TEXTO = comentario;

            //    conf += ZEBAN_C_BLL.INSERT(com);
            //}

            //return "B";
            List<Object> lista = new List<object>();
            lista.Add(items);
            lista.Add(tempi);
            lista.Add(temp);
            return lista;
        }

        private static List<Object> creaActivos(string valores, string id, int cont)
        {
            string[] item = valores.Split('|');
            //ZEBAN_P_BE header = new ZEBAN_P_BE();
            ZEBAN_BE items = new ZEBAN_BE();
            Conexion con = new Conexion();


            //header.FK_USUARIO = user;
            //header.STATUS = getStatus(user);
            //header.F_APROBACION = DateTime.Now;

            ////int conf = ZEBAN_P_BLL.INSERT(header); //Insertar registro en SQL
            ////List<ZEBAN_P_BE> zeban_p = ZEBAN_P_BLL.GET();
            ////int b = zeban_p[(zeban_p.Count - 1)].BANFN;
            ////int banfn = b;

            ////items.BANFN = banfn;
            IRfcTable act = con.check_servicio(id, "A");
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.DESCR = act.GetString("DESCR");
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            act.CurrentIndex = 0;
            //items.MEINS = serv.GetString("UM");
            items.MEINS = "SER";
            //items.MENGE = Decimal.Parse(item[3]);            
            items.MENGE = 1;
            //items.PREIS = Decimal.Parse(item[1]);
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            items.TIPO = "F";
            items.IMP = "A";
            items.WERKS = ConfigurationManager.AppSettings["werks"];
            items.WAERS = item[5];


            //List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            //Items.Add(items);
            //conf += ZEBAN_BLL.INSERT(Items); //Insertar registro en SQL

            List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
            ZEBAN_S_BE temp = new ZEBAN_S_BE();
            //temp.BANFN = banfn;
            temp.BNFPO = "" + (cont * 10); ;
            temp.SERIAL = 1;

            temp.MEINS = act.GetString("UM");
            temp.SERVICIO = act.GetString("SERVICIO");
            temp.ASSET_NUM = act.GetString("ACTIVO");
            temp.TEXTO = con.check_servDesc(temp.SERVICIO);
            temp.MENGE = Decimal.Parse(item[3]);
            temp.PREIS = Decimal.Parse(item[1]);
            temp.WAERS = item[5];

            servs.Add(temp);
            //conf += ZEBAN_S_BLL.INSERT(servs); //Insertar registro en SQL

            //List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            ZEBAN_I_BE tempi = new ZEBAN_I_BE();
            //tempi.BANFN = banfn;
            tempi.BNFPO = "" + (cont * 10); ;
            tempi.SERIAL = servs[0].SERIAL;
            tempi.MENGE = Decimal.Parse(item[3]);
            tempi.LINE = 1;
            tempi.GL_ACCOUNT = servs[0].GL_ACCOUNT;
            tempi.COSTCENTER = servs[0].COSTCENTER;
            tempi.ASSET_NUM = servs[0].ASSET_NUM;
            //imps.Add(tempi);
            //conf += ZEBAN_I_BLL.INSERT(imps); //Insertar registro en SQL


            //if (!comentario.Equals(""))
            //{
            //    ZEBAN_C_BE com = new ZEBAN_C_BE();
            //    com.BANFN = banfn;
            //    com.FK_USUARIO = user;
            //    com.TEXTO = comentario;

            //    conf += ZEBAN_C_BLL.INSERT(com);
            //}

            //return "B";
            List<Object> lista = new List<object>();
            lista.Add(items);
            lista.Add(tempi);
            lista.Add(temp);
            return lista;
        }

        private static ZEBAN_BE creaMateriales(string valores, string id, int cont)//, string comentario)
        {
            string[] item = valores.Split('|');
            //ZEBAN_P_BE header = new ZEBAN_P_BE();
            ZEBAN_BE items = new ZEBAN_BE();
            //Conexion con = new Conexion();


            //header.FK_USUARIO = user;
            //header.STATUS = getStatus(user);
            //header.F_APROBACION = DateTime.Now;

            //int conf = ZEBAN_P_BLL.INSERT(header); //Insertar registro en SQL
            //List<ZEBAN_P_BE> zeban_p = ZEBAN_P_BLL.GET();
            //int b = zeban_p[(zeban_p.Count - 1)].BANFN;
            //int banfn = b;

            //items.BANFN = banfn;
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            items.MATNR = id;
            items.MENGE = Decimal.Parse(item[3]);
            items.PREIS = Decimal.Parse(item[1]);
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            //items.TIPO = "F";
            //items.IMP = "A";
            items.WERKS = ConfigurationManager.AppSettings["werks"];
            items.WAERS = item[5];


            ////List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            ////Items.Add(items);
            ////conf += ZEBAN_BLL.INSERT(Items); //Insertar registro en SQL

            //List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
            //ZEBAN_S_BE temp = new ZEBAN_S_BE();
            //temp.BANFN = banfn;
            //temp.BNFPO = "10";
            //temp.SERIAL = 1;

            //temp.MEINS = act.GetString("UM");
            //temp.SERVICIO = act.GetString("SERVICIO");
            //temp.ASSET_NUM = act.GetString("ACTIVO");
            //temp.TEXTO = item[0];
            //temp.MENGE = Decimal.Parse(item[3]);
            //temp.PREIS = Decimal.Parse(item[1]);

            //servs.Add(temp);
            //conf += ZEBAN_S_BLL.INSERT(servs); //Insertar registro en SQL

            //List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            //ZEBAN_I_BE tempi = new ZEBAN_I_BE();
            //tempi.BANFN = banfn;
            //tempi.BNFPO = "10";
            //tempi.SERIAL = servs[0].SERIAL;
            //tempi.MENGE = Decimal.Parse(item[3]);
            //tempi.LINE = 1;
            //tempi.GL_ACCOUNT = servs[0].GL_ACCOUNT;
            //tempi.COSTCENTER = servs[0].COSTCENTER;
            //imps.Add(tempi);
            //conf += ZEBAN_I_BLL.INSERT(imps); //Insertar registro en SQL

            ////if (!comentario.Equals(""))
            ////{
            ////    ZEBAN_C_BE com = new ZEBAN_C_BE();
            ////    com.BANFN = banfn;
            ////    com.FK_USUARIO = user;
            ////    com.TEXTO = comentario;

            ////    conf += ZEBAN_C_BLL.INSERT(com);
            ////}

            ////return "B";
            return items;
        }
        private static List<Object> creaImputa(string valores, string id, int cont)
        {
            string[] item = valores.Split('|');
            ////ZEBAN_P_BE header = new ZEBAN_P_BE();
            ZEBAN_BE items = new ZEBAN_BE();
            Conexion con = new Conexion();


            ////header.FK_USUARIO = user;
            ////header.STATUS = getStatus(user);
            ////header.F_APROBACION = DateTime.Now;

            ////int conf = ZEBAN_P_BLL.INSERT(header); //Insertar registro en SQL
            ////List<ZEBAN_P_BE> zeban_p = ZEBAN_P_BLL.GET();
            ////int b = zeban_p[(zeban_p.Count - 1)].BANFN;
            ////int banfn = b;

            ////items.BANFN = banfn;
            items.BNFPO = "" + (cont * 10);
            items.DESCR = item[0];
            items.F_ENTREGA = DateTime.Parse(item[4]);
            items.MAT_GR = item[2];
            ////////IRfcTable serv = con.check_servicio(id, "M");
            ////////serv.CurrentIndex = 0;
            //items.MEINS = serv.GetString("UM");
            ////////items.MEINS = serv.GetString("UM");           
            items.MEINS = item[6];
            items.MENGE = Decimal.Parse(item[3]);
            items.PREIS = Decimal.Parse(item[1]);
            items.PUR_GR = ConfigurationManager.AppSettings["purchaseGroup"];
            //items.TIPO = "F";
            items.IMP = "K";
            items.WERKS = ConfigurationManager.AppSettings["werks"];
            items.WAERS = item[5];

            ////List<ZEBAN_BE> Items = new List<ZEBAN_BE>();
            ////Items.Add(items);
            ////conf += ZEBAN_BLL.INSERT(Items); //Insertar registro en SQL

            List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
            ZEBAN_S_BE temp = new ZEBAN_S_BE();
            //temp.BANFN = banfn;
            temp.BNFPO = "" + (cont * 10);
            temp.SERIAL = 1;
            temp.MEINS = "SER";
            ////////temp.SERVICIO = serv.GetString("SERVICIO");
            ////////temp.TEXTO = con.check_servDesc(temp.SERVICIO);
            temp.SERVICIO = "";
            temp.TEXTO = item[0];
            temp.MENGE = Decimal.Parse(item[3]);
            temp.PREIS = Decimal.Parse(item[1]);
            temp.WAERS = item[5];

            servs.Add(temp);
            //conf += ZEBAN_S_BLL.INSERT(servs); //Insertar registro en SQL

            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();

            ZEBAN_I_BE tempi = new ZEBAN_I_BE();
            //tempi.BANFN = banfn;
            tempi.BNFPO = "" + (cont * 10);
            tempi.SERIAL = 1;
            tempi.LINE = 1;
            tempi.GL_ACCOUNT = servs[0].GL_ACCOUNT;
            tempi.COSTCENTER = servs[0].COSTCENTER;
            imps.Add(tempi);
            //conf += ZEBAN_I_BLL.INSERT(imps); //Insertar registro en SQL

            //if (!comentario.Equals(""))
            //{
            //    ZEBAN_C_BE com = new ZEBAN_C_BE();
            //    com.BANFN = banfn;
            //    com.FK_USUARIO = user;
            //    com.TEXTO = comentario;

            //    conf += ZEBAN_C_BLL.INSERT(com);
            //}

            //return "B";
            List<Object> lista = new List<object>();
            lista.Add(items);
            lista.Add(tempi);
            lista.Add(temp);
            return lista;
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
        [WebMethod(EnableSession = false)]
        public static string crear2(string url, string valores, string comentario, string r, string us)// Crea requisición
        {
            string tipo = "";
            string id = "";



            ZEBAN_P_BE header = new ZEBAN_P_BE();
            header.FK_USUARIO = us;
            header.STATUS = "0";//getStatus(user) + "";
            header.F_APROBACION = DateTime.Now;
            header.REFERENCIA = r;

            List<ZEBAN_BE> items = new List<ZEBAN_BE>();
            List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();
            List<ZEBAN_S_BE> serv = new List<ZEBAN_S_BE>();


            string[] tabla = valores.Split('|');
            string valores_temp = "";
            int cont = 0;
            for (int i = 0; i < tabla.Length - 1; i += 9)
            {
                if (cont > 0)
                {
                    cont = (i / 9) + 1;//27.01.2017 i/8
                }
                else
                {
                    cont = i + 1;
                }
                valores_temp = tabla[i + 2] + "|"
                    + tabla[i + 3] + "|" + tabla[i + 4] + "|" + tabla[i + 5] + "|" + tabla[i + 6] + "|" + tabla[i + 7] + "|" + tabla[i + 8] + "|";
                tipo = tabla[i];
                id = tabla[i + 1];

                if (tipo == "s")
                {
                    //res = creaServicios(valores, id, comentario);
                    List<Object> list = new List<object>();
                    list = creaServicios(valores_temp, id, cont);

                    ZEBAN_BE item_s = new ZEBAN_BE();
                    ZEBAN_I_BE imp_s = new ZEBAN_I_BE();
                    ZEBAN_S_BE serv_s = new ZEBAN_S_BE();

                    item_s = (ZEBAN_BE)list[0];
                    items.Add(item_s);
                    imp_s = (ZEBAN_I_BE)list[1];
                    imps.Add(imp_s);
                    serv_s = (ZEBAN_S_BE)list[2];
                    serv.Add(serv_s);
                }
                else if (tipo == "a")
                {
                    //res = creaActivos(valores, id, comentario);
                    List<Object> list = new List<object>();
                    list = creaActivos(valores_temp, id, cont);

                    ZEBAN_BE item_i = new ZEBAN_BE();
                    ZEBAN_I_BE imp_i = new ZEBAN_I_BE();
                    ZEBAN_S_BE serv_i = new ZEBAN_S_BE();

                    item_i = (ZEBAN_BE)list[0];
                    items.Add(item_i);
                    imp_i = (ZEBAN_I_BE)list[1];
                    imps.Add(imp_i);
                    serv_i = (ZEBAN_S_BE)list[2];
                    serv.Add(serv_i);
                }
                else if (tipo == "m")
                {
                    ZEBAN_BE item = new ZEBAN_BE();
                    item = creaMateriales(valores_temp, id, cont);
                    items.Add(item);
                }
                else
                {
                    //res = creaServicios(valores, id, comentario);
                    List<Object> list = new List<object>();
                    list = creaImputa(valores_temp, id, cont);

                    ZEBAN_BE item_s = new ZEBAN_BE();
                    ZEBAN_I_BE imp_s = new ZEBAN_I_BE();
                    ZEBAN_S_BE serv_s = new ZEBAN_S_BE();

                    item_s = (ZEBAN_BE)list[0];
                    items.Add(item_s);
                    imp_s = (ZEBAN_I_BE)list[1];
                    imps.Add(imp_s);
                }
            }


            int conf = ZEBAN_P_BLL.INSERT(header); //Insertar registro en SQL
            List<ZEBAN_P_BE> zeban_p = ZEBAN_P_BLL.GET();
            int b = zeban_p[(zeban_p.Count - 1)].BANFN;
            int banfn = b;

            for (int i = 0; i < items.Count; i++)
            {
                items[i].BANFN = b;
            }
            for (int i = 0; i < imps.Count; i++)
            {
                imps[i].BANFN = b;
            }
            for (int i = 0; i < serv.Count; i++)
            {
                serv[i].BANFN = b;
            }

            conf += ZEBAN_BLL.INSERT(items);
            if (imps.Count > 0)
            {
                conf += ZEBAN_I_BLL.INSERT(imps);
            }
            if (serv.Count > 0)
            {
                conf += ZEBAN_S_BLL.INSERT(serv);
            }
            if (!comentario.Equals(""))
            {
                ZEBAN_C_BE com = new ZEBAN_C_BE();
                com.BANFN = banfn;
                com.FK_USUARIO = us;
                com.TEXTO = comentario;

                conf += ZEBAN_C_BLL.INSERT(com);
            }

            //USUARIO_BE u = USUARIO_BLL.GET(us)[0];
            //enviar(us, banfn, url);
            USUARIO_BE usuarioBe = USUARIO_BLL.GET(us)[0];
            using (List<USUARIO_BE>.Enumerator enumerator = USUARIO_BLL.GET(USUARIO_BLL.GET(us)[0].AREA).GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    USUARIO_BE current = enumerator.Current;
                    if (current.GEREN | current.DIREC)
                    {
                        header.BANFN = (b);
                        header.STATUS = ("2");
                        conf += ZEBAN_P_BLL.UPDATE(header);
                        enviarA(usuarioBe.NipLogin, b, url);
                    }
                    else
                        enviar(us, b, url);
                }
            }

            return "B";
        }

        static private void enviar(string u, int b, string url)
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

            string asunto = "Se ha creado una requisición";
            string mensaje = u.ToUpper() + " creó la requisición: " + b + "<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}

            string[] uurl = url.Split('/');
            string url_final = "";
            for (int k = 0; k < uurl.Length - 1; k++)
            {
                url_final += uurl[k] + "/";
            }
            email.send(dest3, asunto, mensaje, b, url_final);
        }

        private static void enviarA(string u, int b, string url)
        {
            Mail mail = new Mail();
            List<USUARIO_BE> usuarioBeList = USUARIO_BLL.GET(2);
            string[] dest = new string[usuarioBeList.Count + 1];
            string[] strArray = new string[usuarioBeList.Count + 1];
            for (int index = 0; index < usuarioBeList.Count; ++index)
            {
                dest[index] = usuarioBeList[index].eMail;
                strArray[index] = usuarioBeList[index].NipLogin;
            }
            dest[usuarioBeList.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(string.Concat((object)b))[0].FK_USUARIO)[0].eMail;
            strArray[usuarioBeList.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(string.Concat((object)b))[0].FK_USUARIO)[0].NipLogin;
            string asunto = "Se ha creado una requisición";
            string mensaje = u.ToUpper() + " creó la requisición: " + (object)b + "<br><br>";

            string[] strArray2 = url.Split('/');
            string url1 = "";
            for (int index = 0; index < strArray2.Length - 1; ++index)
                url1 = url1 + strArray2[index] + "/";


            mail.send(dest, asunto, mensaje, b, url1);
        }
    }
}