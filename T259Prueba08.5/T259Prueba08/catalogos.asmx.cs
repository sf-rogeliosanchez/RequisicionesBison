using IBusiness;
using IEntities;
using SAP.Middleware.Connector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;
using T259Prueba08.Models;

namespace T259Prueba08
{
    /// <summary>
    /// Summary description for catalogos
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [ScriptService]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class catalogos : System.Web.Services.WebService
    {

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public string lista_material(string matnr)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.get_matnr(matnr);            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            for (int i = 0; i < materiales.Count; i++)
            {
                materiales.CurrentIndex = i;
                lista[i] = quitaCeros(materiales.GetString("MATNR"));
                lista[i] += " - " + materiales.GetString("MAKTX");
            }


            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }
        [WebMethod]
        public string lista_servicio(string texto)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.get_servicios(texto, "S");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            for (int i = 0; i < materiales.Count; i++)
            {
                //Llenado de lista 
                materiales.CurrentIndex = i;
                lista[i] = materiales.GetString("NUM");
                lista[i] += " - " + materiales.GetString("DESCR");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string lista_activo(string texto)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.get_servicios(texto, "A");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            for (int i = 0; i < materiales.Count; i++)
            {
                //Llenado de lista 
                materiales.CurrentIndex = i;
                lista[i] = materiales.GetString("NUM");
                lista[i] += " - " + materiales.GetString("DESCR");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        [WebMethod]
        public string lista_imputa(string texto)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.get_servicios(texto, "M");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            for (int i = 0; i < materiales.Count; i++)
            {
                //Llenado de lista 
                materiales.CurrentIndex = i;
                lista[i] = materiales.GetString("NUM");
                lista[i] += " - " + materiales.GetString("DESCR");
            }

            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);                      //Convertir arreglo a string para JavaScript
            return list;
        }

        private string quitaCeros(string s)
        {
            string r;
            try
            {
                int i = Int32.Parse(s);
                r = i + "";
            }
            catch
            {
                r = s;
            }
            return r;
        }
        [WebMethod]
        public string materialDesc(string matnr)
        {
            Conexion con = new Conexion();
            string descripcion = con.check_materialDesc(matnr);
            return descripcion;
        }

        [WebMethod]
        public string materialPrecio(string matnr)
        {
            Conexion con = new Conexion();
            string descripcion = con.check_materialPrecio(matnr);
            return descripcion;
        }


        [WebMethod]
        public string materialGrupo(string matnr)
        {
            Conexion con = new Conexion();
            string descripcion = con.check_materialGrupo(matnr);
            return descripcion;
        }

        [WebMethod]
        public string servicioDesc(string serv)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "S");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("DESCR");
            return desc;
        }

        [WebMethod]
        public string servicioPrecio(string serv)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "S");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("PREIS");
            return desc;
        }


        [WebMethod]
        public string servicioGrupo(string serv)
        {

            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "S");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("MATKL");
            return desc;
        }
        [WebMethod]
        public string servicioMoneda(string serv)
        {

            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "S");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("WAERS");
            return desc;
        }

        [WebMethod]
        public string activoDesc(string serv)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "A");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("DESCR");
            return desc;
        }

        [WebMethod]
        public string activoPrecio(string serv)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "A");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("PREIS");
            return desc;
        }


        [WebMethod]
        public string activoGrupo(string serv)
        {

            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "A");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("MATKL");
            return desc;
        }

        [WebMethod]
        public string activoMoneda(string serv)
        {

            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "A");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("WAERS");
            return desc;
        }

        [WebMethod]
        public string imputaDesc(string serv)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "M");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("DESCR");
            return desc;
        }

        [WebMethod]
        public string imputaPrecio(string serv)
        {
            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "M");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("PREIS");
            return desc;
        }


        [WebMethod]
        public string imputaGrupo(string serv)
        {

            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "M");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("MATKL");
            return desc;
        }

        [WebMethod]
        public string imputaMoneda(string serv)
        {

            Conexion con = new Conexion();                          //Conexión a SAP
            IRfcTable materiales = con.check_servicio(serv, "M");            //Llama módulo de función
            string[] lista = new string[materiales.Count];
            materiales.CurrentIndex = 0;
            string desc = materiales.GetString("WAERS");
            return desc;
        }


        [WebMethod]
        public string servDesc(string serv)
        {
            Conexion con = new Conexion();
            string descripcion = con.check_servDesc(serv);
            return descripcion;
        }

        [WebMethod]
        public string lista_centros(string werks)
        {
            Conexion con = new Conexion();
            IRfcTable centros = con.get_werks(werks);

            string[] lista = new string[centros.Count];
            for (int i = 0; i < centros.Count; i++)
            {
                centros.CurrentIndex = i;
                lista[i] = centros.GetString("WERKS");
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }

        [WebMethod(EnableSession = false)]
        public string lista_grupos(string ekgrp)
        {
            Conexion con = new Conexion();
            IRfcTable grupos = con.get_ekgrp(ekgrp);

            string[] lista = new string[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                grupos.CurrentIndex = i;
                lista[i] = grupos.GetString("EKGRP");
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }
        [WebMethod]
        public string lista_cuentas(string saknr)
        {
            Conexion con = new Conexion();
            IRfcTable cuentas = con.get_saknr(saknr);

            string[] lista = new string[cuentas.Count];
            for (int i = 0; i < cuentas.Count; i++)
            {
                cuentas.CurrentIndex = i;
                lista[i] = cuentas.GetString("SAKNR");
                lista[i] += " - " + cuentas.GetString("TXT50");
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }

        [WebMethod]
        public string lista_costes(string kostl)
        {
            Conexion con = new Conexion();
            IRfcTable costes = con.get_kostl(kostl);

            string[] lista = new string[costes.Count];
            for (int i = 0; i < costes.Count; i++)
            {
                costes.CurrentIndex = i;
                lista[i] = costes.GetString("KOSTL");
                lista[i] += " - " + costes.GetString("KTEXT");
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }
        [WebMethod(EnableSession = false)]
        public string lista_grupos_a(string matkl)
        {
            Conexion con = new Conexion();
            matkl = matkl.ToUpper();
            IRfcTable grupos = con.get_matkl(matkl);

            string[] lista = new string[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                grupos.CurrentIndex = i;
                lista[i] = grupos.GetString("MATKL");
                lista[i] += " | " + grupos.GetString("WGBEZ");
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }
        [WebMethod(EnableSession = false)]
        public string lista_asnum(string asnum)
        {
            Conexion con = new Conexion();
            asnum = asnum.ToUpper();
            IRfcTable grupos = con.get_asnum(asnum);

            string[] lista = new string[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                grupos.CurrentIndex = i;
                lista[i] = grupos.GetString("ASNUM");
            }
            for (int i = 0; i < lista.Length; i++)
            {
                int cont = 0;
                foreach (char c in lista[i])
                {
                    if (c.Equals('0'))
                    {
                        cont++;
                    }
                    else
                    {
                        break;
                    }
                }
                lista[i] = lista[i].Substring(cont, lista[i].Length - cont);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }

        [WebMethod(EnableSession = false)]
        public string lista_asset(string asset)
        {
            Conexion con = new Conexion();
            asset = asset.ToUpper();
            IRfcTable grupos = con.get_asset(asset);

            string[] lista = new string[grupos.Count];
            for (int i = 0; i < grupos.Count; i++)
            {
                grupos.CurrentIndex = i;
                lista[i] = grupos.GetString("ANLN1");
            }
            for (int i = 0; i < lista.Length; i++)
            {
                int cont = 0;
                foreach (char c in lista[i])
                {
                    if (c.Equals('0'))
                    {
                        cont++;
                    }
                    else
                    {
                        break;
                    }
                }
                lista[i] = lista[i].Substring(cont, lista[i].Length - cont);
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return list;
        }
        #region VER_REQ
        [WebMethod]
        public string servicios(string banfn, string bnfpo)
        {
            List<ZEBAN_S_BE> servicios = ZEBAN_S_BLL.GET(Int32.Parse(banfn), bnfpo);
            List<ZEBAN_I_BE> imputaciones = ZEBAN_I_BLL.GET(Int32.Parse(banfn), bnfpo);
            string listas = "";
            foreach (ZEBAN_S_BE s in servicios)
            {
                foreach (ZEBAN_I_BE i in imputaciones)
                {
                    if (s.SERIAL.Equals(i.SERIAL))
                    {
                        listas += s.SERIAL + "|" + s.SERVICIO.Trim() + "|" + s.TEXTO + "|" + s.MENGE + "|"
                            + s.PREIS + "|" + desc_costes(i.COSTCENTER.Trim()) + "|" + desc_cuentas(i.GL_ACCOUNT.Trim()) + "|" + i.ASSET_NUM.Trim() + "|";
                    }
                }
            }

            return listas;
        }
        private string desc_costes(string kostl)
        {
            Conexion con = new Conexion();
            IRfcTable costes = con.get_kostl(kostl);

            string lista = "";
            for (int i = 0; i < costes.Count; i++)
            {
                costes.CurrentIndex = i;
                lista = costes.GetString("KOSTL");
                lista += " - " + costes.GetString("KTEXT");
            }
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string list = js.Serialize(lista);
            return lista;
        }
        private string desc_cuentas(string saknr)
        {
            Conexion con = new Conexion();
            IRfcTable cuentas = con.get_saknr(saknr);

            string lista = "";
            for (int i = 0; i < cuentas.Count; i++)
            {
                cuentas.CurrentIndex = i;
                lista = cuentas.GetString("SAKNR");
                lista += " - " + cuentas.GetString("TXT50");
            }
            //JavaScriptSerializer js = new JavaScriptSerializer();
            //string list = js.Serialize(lista);
            return lista;
        }
        [WebMethod]
        public string serviciosA(string banfn, string bnfpo)
        {
            List<ZEBAN_S_BE> servicios = ZEBAN_S_BLL.GET(Int32.Parse(banfn), bnfpo);
            List<ZEBAN_I_BE> IMP = ZEBAN_I_BLL.GET(Int32.Parse(banfn), bnfpo);
            string listas = "";
            foreach (ZEBAN_S_BE s in servicios)
            {
                foreach (ZEBAN_I_BE i in IMP)
                {
                    if (s.BNFPO.Trim() == i.BNFPO.Trim())
                    {
                        listas += s.SERIAL + "|" + i.ASSET_NUM.Trim() + "|" + s.TEXTO + "|" + s.MENGE + "|";
                    }
                }

            }

            return listas;
        }
        //[WebMethod]
        //public string imputacionA(string banfn, string bnfpo, string serial)
        //{
        //    List<ZEBAN_I_BE> lista = ZEBAN_I_BLL.GET(Int32.Parse(banfn), bnfpo);
        //    string listas = "";
        //    foreach (ZEBAN_I_BE l in lista)
        //    {
        //        listas += l.BNFPO.Trim() + "|" + l.SERIAL + "|" + l.LINE + "|" + l.MENGE + "|" + l.GL_ACCOUNT.Trim() + "|" + l.ASSET_NUM.Trim() + "|";
        //    }
        //    JavaScriptSerializer js = new JavaScriptSerializer();
        //    string list = js.Serialize(lista);
        //    return listas;
        //}
        [WebMethod]
        public string imputacion(string banfn, string bnfpo)
        {
            List<ZEBAN_I_BE> lista = ZEBAN_I_BLL.GET(Int32.Parse(banfn), bnfpo.Trim());
            string listas = "";
            foreach (ZEBAN_I_BE l in lista)
            {
                string[] coste = desc_costes(l.COSTCENTER.Trim()).Split('-');
                string[] cuenta = desc_cuentas(l.GL_ACCOUNT.Trim()).Split('-');
                string desc_co = "";
                string desc_ct = "";
                if (coste.Length > 1)
                {
                    for (int o = 1; o < coste.Length; o++)
                    {
                        desc_co += coste[o];
                    }
                }

                if (cuenta.Length > 1)
                {
                    for (int o = 1; o < cuenta.Length; o++)
                    {
                        desc_ct += cuenta[o];
                    }
                }
                listas += l.COSTCENTER.Trim() + "|" + desc_co + "|" + l.GL_ACCOUNT.Trim() + "|" + desc_ct;
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return listas;
        }

        #endregion
        [WebMethod]
        public string serviciosK(string banfn, string bnfpo)
        {
            List<ZEBAN_I_BE> imputaciones = ZEBAN_I_BLL.GET(Int32.Parse(banfn), bnfpo);
            string listas = "";
            foreach (ZEBAN_I_BE i in imputaciones)
            {
                listas += i.SERIAL + "|" + "" + "|" + "" + "|" + "" + "|"
                    + "" + "|" + i.COSTCENTER.Trim() + "|" + i.GL_ACCOUNT.Trim() + "|" + i.ASSET_NUM.Trim() + "|";
            }


            return listas;
        }


        [WebMethod]
        public string imputacionA(string banfn, string bnfpo)
        {
            List<ZEBAN_I_BE> lista = ZEBAN_I_BLL.GET(Int32.Parse(banfn), bnfpo);
            string listas = "";
            foreach (ZEBAN_I_BE l in lista)
            {
                listas += l.BNFPO.Trim() + "|" + l.SERIAL + "|" + l.LINE + "|" + l.MENGE + "|" + l.GL_ACCOUNT.Trim() + "|" + l.ASSET_NUM.Trim() + "|";
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            string list = js.Serialize(lista);
            return listas;
        }
        [WebMethod]
        public string descarga(string folnam, string eban)
        {
            string[] split = folnam.Split('|');
            //string folder = split[0];
            //Conexion con = new Conexion();
            //con.rfcDescargar(eban, "BUS2105", folder);
            //IRfcTable contenidoHEX = con.getContenidoHEX();
            //byte[] linea;
            //Byte[] archivo = new Byte[contenidoHEX.RowCount * 255];

            //for (int i = 0; i < contenidoHEX.RowCount; i++)
            //{
            //    contenidoHEX.CurrentIndex = i;
            //    linea = (byte[])contenidoHEX.GetValue("LINE");
            //    for (int j = 0; j < linea.Length; j++)
            //    {
            //        archivo[(i * 255) + j] = linea[j];
            //    }
            //}
            string nombre = split[1];
            ////System.IO.File.WriteAllBytes("C:/APLICATIONS/"+nombre, archivo);
            //Visualizar v = new Visualizar();

            //v.StreamFileToBrowser(nombre, archivo);

            return nombre;
        }

        [WebMethod]
        public string aprobar(string banfn, string user, string url)
        {
            string[] ur = url.Split('/');
            string u = "";
            for (int i = 0; i < ur.Length - 1; i++)
            {
                u += ur[i] + "/";
            }
            List<ZEBAN_P_BE> requ = ZEBAN_P_BLL.GET(banfn);
            string referencia = "";//27.01.2017
            if (requ.Count > 0)
            {
                referencia = requ[0].REFERENCIA;//27.01.2017
                requ[0].STATUS = rol(USUARIO_BLL.GET(user)[0]) + "";
                if (requ[0].STATUS.Equals("5") | requ[0].STATUS.Equals("6"))
                {
                    Conexion con = new Conexion();
                    List<ZEBAN_BE> Items = ZEBAN_BLL.GET(Int32.Parse(banfn));
                    List<ZEBAN_I_BE> imps = new List<ZEBAN_I_BE>();
                    List<ZEBAN_S_BE> servs = new List<ZEBAN_S_BE>();
                    foreach (ZEBAN_BE z in Items)
                    {
                        List<ZEBAN_I_BE> ii = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO.Trim());
                        List<ZEBAN_S_BE> ss = ZEBAN_S_BLL.GET(z.BANFN, z.BNFPO.Trim());
                        foreach (ZEBAN_I_BE i in ii)
                        {
                            imps.Add(i);
                            if (z.TIPO.Trim() != "F")
                            {
                                ZEBAN_S_BE t = new ZEBAN_S_BE();
                                t.ASSET_NUM = i.ASSET_NUM.Trim();
                                t.COSTCENTER = i.COSTCENTER.Trim();
                                t.BANFN = i.BANFN;
                                t.BNFPO = i.BNFPO.Trim();
                                t.GL_ACCOUNT = i.GL_ACCOUNT.Trim();
                                t.SERIAL = i.SERIAL;
                                //t.
                                servs.Add(t);
                            }
                        }
                        if (z.TIPO.Trim() == "F")
                        {
                            foreach (ZEBAN_S_BE s in ss)
                            {
                                s.SERVICIO = completa(s.SERVICIO.Trim(), 18);
                                servs.Add(s);
                            }
                        }
                    }
                    string[] ret = con.bapi_pr_create2(Items, imps, servs, user, u, referencia);//Crea requisición//27.01.2017
                    string status;// = "E";
                    if (!ret[0].Equals(""))
                    {
                        status = "B";
                        requ[0].APROBO = user;
                        requ[0].SAP_BANFN = ret[0];
                        requ[0].STATUS = "6";
                        int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                        if (i > 0)
                        {
                            ZEBAN_H_BE his = new ZEBAN_H_BE();
                            his.BANFN = banfn;
                            his.FECHA = DateTime.Now;
                            his.NIPLOGIN = user;

                            ZEBAN_H_BLL.INSERT(his);

                            List<ZEBAN_D_BE> docs = ZEBAN_D_BLL.GET(Int32.Parse(banfn));
                            for (int y = 0; y < docs.Count; y++)
                            {
                                byte[] bytes = docs[y].INFO;
                                string name = docs[y].NOMBRE + "." + docs[y].EXT;

                                ret[i] = con.rfcFuncionAdjuntar(ret[0], "BUS2105", name, bytes);
                            }



                            enviarF(user, Int32.Parse(banfn), u, ret[0]);
                            //return "Se ha aprobado la requisición";
                        }
                        ret[1] = "Se ha creado la requisición " + ret[0] + " en SAP";
                    }
                    else
                    {
                        status = "Hubieron errores al crear la requisición:";
                    }

                    return "\n" + ret[1];
                }
                else
                {
                    requ[0].APROBO = user;
                    int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                    if (i > 0)
                    {
                        ZEBAN_H_BE his = new ZEBAN_H_BE();
                        his.BANFN = banfn;
                        his.FECHA = DateTime.Now;
                        his.NIPLOGIN = user;

                        ZEBAN_H_BLL.INSERT(his);
                        enviar(user, Int32.Parse(banfn), u);
                        return "Se ha aprobado la requisición";
                    }
                }
            }
            return "Hubo un error al aprobar la requisición";
        }
        [WebMethod]
        public string cancelar(string banfn, string user, string url, string comm)
        {
            string[] ur = url.Split('/');
            string u = "";
            for (int i = 0; i < ur.Length - 1; i++)
            {
                u += ur[i] + "/";
            }
            List<ZEBAN_P_BE> requ = ZEBAN_P_BLL.GET(banfn);
            if (requ.Count > 0)
            {
                //int status = Int32.Parse(requ[0].STATUS);
                int status = 0;
                status = rol(USUARIO_BLL.GET(requ[0].FK_USUARIO)[0]);
                //if (status > 0)
                //{
                //    status--;
                //}
                //requ[0].STATUS = status + "";
                requ[0].STATUS = "-2";
                requ[0].APROBO = "";
                int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                if (i > 0)
                {
                    ZEBAN_H_BE his = new ZEBAN_H_BE();
                    his.BANFN = banfn;
                    his.FECHA = DateTime.Now;
                    his.NIPLOGIN = user;
                    ZEBAN_H_BLL.INSERT(his);

                    ZEBAN_C_BE com = new ZEBAN_C_BE();
                    com.BANFN = Int32.Parse(banfn);
                    com.FK_USUARIO = user;
                    com.TEXTO = "CANCELADO POR: \n" + comm;
                    ZEBAN_C_BLL.INSERT(com);


                    //enviarR(user, Int32.Parse(banfn), u, comm);
                    return "Se ha cancelado la requisición";
                }
            }
            return "Hubo un error al cancelar la requisición";
        }

        [WebMethod]
        public string rechazar(string banfn, string user, string url, string comm)
        {
            string[] ur = url.Split('/');
            string u = "";
            for (int i = 0; i < ur.Length - 1; i++)
            {
                u += ur[i] + "/";
            }
            List<ZEBAN_P_BE> requ = ZEBAN_P_BLL.GET(banfn);
            if (requ.Count > 0)
            {
                int status = 0;
                status = rol(USUARIO_BLL.GET(user)[0]);
                if (status == 4 & !requ[0].STATUS.Trim().Equals("0") & !requ[0].STATUS.Trim().Equals("1"))
                {
                    requ[0].STATUS = "2";
                    int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                    List<ZEBAN_BE> pos = ZEBAN_BLL.GET(Int16.Parse(banfn));
                    foreach (ZEBAN_BE p in pos)
                    {
                        List<ZEBAN_I_BE> imp = ZEBAN_I_BLL.GET(p.BANFN, p.BNFPO);
                        foreach (ZEBAN_I_BE im in imp)
                        {
                            im.COSTCENTER = "";
                            im.GL_ACCOUNT = "";
                        }
                        ZEBAN_I_BLL.UPDATE(imp);
                    }

                    ZEBAN_H_BE his = new ZEBAN_H_BE();
                    his.BANFN = banfn;
                    his.FECHA = DateTime.Now;
                    his.NIPLOGIN = user;
                    ZEBAN_H_BLL.INSERT(his);

                    ZEBAN_C_BE com = new ZEBAN_C_BE();
                    com.BANFN = Int32.Parse(banfn);
                    com.FK_USUARIO = user;
                    com.TEXTO = comm;
                    ZEBAN_C_BLL.INSERT(com);

                    //ENVIAR CORREO A CONTABILIDAD.
                    enviarConta(Int32.Parse(banfn), u, comm);
                    return "Se ha rechazado la requisición";
                }
                else
                {
                    requ[0].STATUS = "-1";
                    requ[0].COMPRADOR = "";
                    int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                    if (i > 0)
                    {
                        ZEBAN_H_BE his = new ZEBAN_H_BE();
                        his.BANFN = banfn;
                        his.FECHA = DateTime.Now;
                        his.NIPLOGIN = user;
                        ZEBAN_H_BLL.INSERT(his);

                        ZEBAN_C_BE com = new ZEBAN_C_BE();
                        com.BANFN = Int32.Parse(banfn);
                        com.FK_USUARIO = user;
                        com.TEXTO = comm;
                        ZEBAN_C_BLL.INSERT(com);


                        enviarR(user, Int32.Parse(banfn), u, comm);
                        return "Se ha rechazado la requisición";
                    }
                }
            }
            return "Hubo un error al rechazar la requisición";
        }

        private void enviarConta(int b, string url, string comm)
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
            string asunto = "Se ha rechazado una requisición";
            string mensaje = ConfigurationManager.AppSettings["contabilidad"].ToUpper() + " rechazó la requisición: " + b + "<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br><br>";
            //}
            mensaje += comentarios(comm);

            string new_url = "";
            string[] urls = url.Split('/');
            for (int i = 0; i < urls.Length - 1; i++)
            {
                new_url += urls[i] + "/";
            }

            email.send(dest3, asunto, mensaje, b, new_url);
        }


        [WebMethod]
        public string autorizar(string banfn, string user, string url)
        {
            string[] ur = url.Split('/');
            string u = "";
            for (int i = 0; i < ur.Length - 1; i++)
            {
                u += ur[i] + "/";
            }
            List<ZEBAN_P_BE> requ = ZEBAN_P_BLL.GET(banfn);
            if (requ.Count > 0)
            {
                //int status = Int32.Parse(requ[0].STATUS);
                int status = 0;
                status = rol(USUARIO_BLL.GET(user)[0]);

                //if (status > 0)
                //{
                //    status--;
                //}
                //requ[0].STATUS = status + "";
                requ[0].STATUS = (Int16.Parse(requ[0].STATUS) + 1) + "";
                if (requ[0].STATUS.Equals("1"))
                {
                    requ[0].STATUS = "2";
                }
                if (status == 4)
                {
                    if (!requ[0].COMPRADOR.Equals("-") & !requ[0].COMPRADOR.Equals(""))
                    {
                        requ[0].STATUS = "4";
                    }
                }
                requ[0].APROBO = "";
                int i = ZEBAN_P_BLL.UPDATE(requ[0]);
                if (i > 0)
                {
                    ZEBAN_H_BE his = new ZEBAN_H_BE();
                    his.BANFN = banfn;
                    his.FECHA = DateTime.Now;
                    his.NIPLOGIN = user;
                    ZEBAN_H_BLL.INSERT(his);


                    if (requ[0].STATUS.Equals("2"))
                    {
                        enviarA(user, Int32.Parse(banfn), u);
                    }
                    else if (requ[0].STATUS.Equals("4"))
                    {
                        enviarGerente(Int16.Parse(banfn), url, 4);
                    }
                    else if (requ[0].STATUS.Equals("5"))
                    {
                        enviarGerente(Int16.Parse(banfn), url, 5);
                    }
                    return "Se ha autorizado la requisición";
                }
            }
            return "Hubo un error al autorizar la requisición";
        }

        private void enviarGerente(int b, string url, int r)
        {
            Mail email = new Mail();
            //int r = 0;
            //r = 4;

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


        [WebMethod]
        public string addComent(string banfn, string user, string url, string comm)
        {
            string[] ur = url.Split('/');
            string u = "";
            for (int i = 0; i < ur.Length - 1; i++)
            {
                u += ur[i] + "/";
            }

            ZEBAN_C_BE com = new ZEBAN_C_BE();
            com.BANFN = Int32.Parse(banfn);
            com.FK_USUARIO = user;
            com.TEXTO = comm;
            ZEBAN_C_BLL.INSERT(com);

            return u;
        }


        private string completa(string cadena, int longitud)
        {
            string ret = "";
            int len = cadena.Length;
            for (int i = 0; i < (longitud - len); i++)
            {
                ret += "0";
            }
            ret += cadena;
            return ret;
        }
        private void enviarF(string u, int b, string url, string SAP_N)
        {

            Mail email = new Mail();

            List<USUARIO_BE> gerencia = USUARIO_BLL.GET(USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].AREA); //Usuarios gerencia de area
            string[] dest3 = new string[gerencia.Count + 1];
            string[] names3 = new string[gerencia.Count + 1];

            for (int i = 0; i < gerencia.Count; i++)
            {
                dest3[i] = gerencia[i].eMail;
                names3[i] = gerencia[i].NipLogin;
            }

            dest3[gerencia.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].COMPRADOR)[0].eMail;
            names3[gerencia.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].COMPRADOR)[0].NipLogin;
            string asunto = "Se ha aprobado una requisición";
            string mensaje = u.ToUpper() + " aprobó la requisición: " + b + "<br><br>" +
                "Solicitud de compra en SAP: " + SAP_N +
                "<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}
            email.send(dest3, asunto, mensaje, b, url);
        }


        private void enviar(string u, int b, string url)
        {
            ////Mail email = new Mail();
            ////int r = rol(USUARIO_BLL.GET(u)[0]) + 1;
            ////if (r < 6)
            ////{
            ////    string[] dest = getDestinatarios(r, u);
            ////    USUARIO_BE usu = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0];
            ////    dest[dest.Length - 1] = usu.eMail;
            ////    string asunto = "Se ha aprobado una requisición: " + b;
            ////    string mensaje = u + " aprobó la requisición: " + b + "<br><br>";
            ////    email.send(dest, asunto, mensaje, b, url);
            ////}
            ////else
            ////{
            ////    string[] dest2 = new string[2];
            ////    string[] names2 = new string[2];
            ////    if (USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO).Count > 0)
            ////    {
            ////        USUARIO_BE usu = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0];
            ////        dest2[0] = usu.eMail;
            ////        names2[0] = usu.NipLogin;
            ////    }
            ////    dest2[1] = USUARIO_BLL.GET(u)[0].eMail;
            ////    names2[1] = USUARIO_BLL.GET(u)[0].NipLogin;
            ////    string asuntos = "Se ha aprobado una requisición: " + b;
            ////    string mensajes = u + " aprobó la requisición: " + b + "<br><br>";
            ////    for (int i = 0; i < names2.Length; i++)
            ////    {
            ////        mensajes += names2[i] + "<br><br>";
            ////    }
            ////    email.send(dest2, asuntos, mensajes, b, url);
            ////}
            Mail email = new Mail();
            //int r = 1;

            List<USUARIO_BE> gerencia = USUARIO_BLL.GET(USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].AREA); //Usuarios gerencia de area
            string[] dest3 = new string[gerencia.Count + 1];
            string[] names3 = new string[gerencia.Count + 1];

            for (int i = 0; i < gerencia.Count; i++)
            {
                dest3[i] = gerencia[i].eMail;
                names3[i] = gerencia[i].NipLogin;
            }

            dest3[gerencia.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].COMPRADOR)[0].eMail;
            names3[gerencia.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].COMPRADOR)[0].NipLogin;
            string asunto = "Se ha aprobado una requisición";
            string mensaje = u.ToUpper() + " aprobó la requisición: " + b + "<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}
            email.send(dest3, asunto, mensaje, b, url);
        }
        private void enviarR(string u, int b, string url, string com)
        {
            Mail email = new Mail();
            int r = rol(USUARIO_BLL.GET(u)[0]) + 1;

            List<ZEBAN_H_BE> hist = ZEBAN_H_BLL.GET(b);
            string[] dest3 = new string[1];
            string[] names3 = new string[1];
            ////for (int i = 0; i < hist.Count; i++)
            ////{
            ////    dest3[i] = USUARIO_BLL.GET(hist[i].NIPLOGIN)[0].eMail;
            ////    names3[i] = USUARIO_BLL.GET(hist[i].NIPLOGIN)[0].NipLogin;
            ////}

            ////dest3[hist.Count] = USUARIO_BLL.GET(u)[0].eMail;
            ////names3[hist.Count] = USUARIO_BLL.GET(u)[0].NipLogin;


            dest3[0] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].eMail;
            names3[0] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].NipLogin;
            string asunto = "Se ha rechazado una requisición";
            string mensaje = u.ToUpper() + " rechazó la requisición: " + b + "<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br><br>";
            //}
            mensaje += comentarios(com);
            email.send(dest3, asunto, mensaje, b, url);
            //}
            //else
            //{
            //    //string[] dest2 = new string[2];
            //    //USUARIO_BE usu = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0];
            //    //dest2[0] = usu.eMail;
            //    //dest2[1] = USUARIO_BLL.GET(u)[0].eMail;
            //    //string asuntos = "Se ha aprobado una requisición";
            //    //string mensajes = u + " aprobó la requisición: " + b + "<br><br>";
            //    //email.send(dest2, asuntos, mensajes, b, url);
            //}
        }
        private void enviarA(string u, int b, string url)
        {
            Mail email = new Mail();
            int r = 2;

            List<USUARIO_BE> compras = USUARIO_BLL.GET(r); //Usuarios de Compras
            string[] dest3 = new string[compras.Count + 1];
            string[] names3 = new string[compras.Count + 1];

            for (int i = 0; i < compras.Count; i++)
            {
                dest3[i] = compras[i].eMail;
                names3[i] = compras[i].NipLogin;
            }

            dest3[compras.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].eMail;
            names3[compras.Count] = USUARIO_BLL.GET(ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO)[0].NipLogin;
            string asunto = "Se ha autorizado una requisición";
            string mensaje = u.ToUpper() + " autorizó la requisición: " + b + "<br><br>";
            //for (int i = 0; i < names3.Length; i++)
            //{
            //    mensaje += names3[i] + "<br>";
            //}
            email.send(dest3, asunto, mensaje, b, url);
        }
        private string comentarios(string texto)
        {
            string ret = "";

            ret = texto.Replace("\n", "<br>");
            ret += "<br><br>";

            return ret;
        }
        private string[] getDestinatarios(int r, string u)
        {
            if (r > 0)
            {
                List<USUARIO_BE> temp22 = USUARIO_BLL.GET();
                int cont = 0;
                for (int i = 0; i < temp22.Count; i++)
                {
                    if (r.Equals(2))
                    {
                        if (temp22[i].CONTA)
                        {
                            cont++;
                        }
                    }
                    else if (r.Equals(3))
                    {
                        if (temp22[i].GEREN)
                        {
                            cont++;
                        }
                    }
                    else if (r.Equals(4))
                    {
                        if (temp22[i].DIREC)
                        {
                            cont++;
                        }
                    }
                }

                string[] dest = new string[cont + 1];
                cont = 0;
                for (int i = 0; i < temp22.Count; i++)
                {
                    if (r.Equals(2))
                    {
                        if (temp22[i].CONTA)
                        {
                            dest[cont] = temp22[i].eMail;
                            cont++;
                        }
                    }
                    else if (r.Equals(3))
                    {
                        if (temp22[i].GEREN)
                        {
                            dest[cont] = temp22[i].eMail;
                            cont++;
                        }
                    }
                    else if (r.Equals(4))
                    {
                        if (temp22[i].DIREC)
                        {
                            dest[cont] = temp22[i].eMail;
                            cont++;
                        }
                    }
                }
                //dest[cont] = USUARIO_BLL.GET(u)[0].eMail; //Agregar usuario que crea la requisición.

                return dest;
            }
            else
            {
                return new string[1];
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


        //[HttpPost]
        //public void subir()
        //{
        //    if (HttpContext.Current.Request.Files.AllKeys.Any())
        //    {
        //        // Get the uploaded image from the Files collection
        //        var httpPostedFile = HttpContext.Current.Request.Files["UploadedImage"];

        //        if (httpPostedFile != null)
        //        {
        //            // Validate the uploaded image(optional)

        //            // Get the complete file path
        //            var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedFiles"), httpPostedFile.FileName);

        //            // Save the uploaded file to "UploadedFiles" folder
        //            httpPostedFile.SaveAs(fileSavePath);
        //        }
        //    }
        //}

        [WebMethod]
        public string verificaUOM(string uom)
        {
            Conexion con = new Conexion();
            string ex_uom = con.get_UOM(uom);

            return ex_uom;
        }

        [WebMethod]
        public decimal getTipoCambio(string valor, string moneda)
        {
            Conexion con = new Conexion();
            string valores = con.get_currency(valor, moneda);

            int decimales = 0;

            string[] s = valor.Split('.');
            if (s.Length > 1)
            {
                decimales = s[1].Length;
            }

            valores = valores.Substring(0, valores.Length - decimales) + "." + valores.Substring(valores.Length - decimales,decimales);

            decimal valorD = decimal.Parse(valores);
            return valorD;
        }
    }

}

