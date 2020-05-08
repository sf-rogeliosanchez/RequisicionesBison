using System;
using System.Collections.Generic;

using SAP.Middleware.Connector;
using System.Configuration;
using IEntities;
using IBusiness;

namespace T259Prueba08.Models
{
    public class Conexion
    {
        RfcDestination oDestino;
        private IRfcTable contenidoHEX;
        private IRfcTable contenido;

        //Keys en Web.config-------------------------------------------------------------
        private string sapName = ConfigurationManager.AppSettings["sapName"];
        private string sapUser = ConfigurationManager.AppSettings["sapUser"];
        private string sapPass = ConfigurationManager.AppSettings["sapPass"];
        private string sapClient = ConfigurationManager.AppSettings["MANDT"];
        private string sapServer = ConfigurationManager.AppSettings["sapServer"];
        private string sapNumber = ConfigurationManager.AppSettings["sapNumber"];
        private string sapID = ConfigurationManager.AppSettings["sapID"];
        private string sapRouter = ConfigurationManager.AppSettings["sapRouter"];
        //-------------------------------------------------------------------------------

        #region conexionSAP
        private bool conectar()
        {
            RfcConfigParameters oParametros = new RfcConfigParameters();
            oParametros.Add(RfcConfigParameters.Name, sapName);
            oParametros.Add(RfcConfigParameters.User, sapUser);
            oParametros.Add(RfcConfigParameters.Password, sapPass);
            oParametros.Add(RfcConfigParameters.Client, sapClient);
            //oParametros.Add(RfcConfigParameters.Language, "EN");
            oParametros.Add(RfcConfigParameters.Language, "ES");
            oParametros.Add(RfcConfigParameters.AppServerHost, sapServer);
            oParametros.Add(RfcConfigParameters.SystemNumber, sapNumber);
            if (!sapRouter.Equals("") && !sapRouter.Equals(null))
            {
                oParametros.Add(RfcConfigParameters.SAPRouter, sapRouter);
            }
            if (!sapID.Equals("") && !sapID.Equals(null))
            {
                oParametros.Add(RfcConfigParameters.SystemID, sapID);
            }
            oParametros.Add(RfcConfigParameters.PoolSize, "5");

            oDestino = RfcDestinationManager.GetDestination(oParametros);

            try
            {
                oDestino.Ping();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;

        }
        #endregion
        #region requisiciones
        public string[] bapi_pr_create(List<ZEBAN_BE> items, List<ZEBAN_I_BE> imputaciones, List<ZEBAN_S_BE> servicios, string user, string url)//Crear requisición en BD(Confirmar en SAP)
        {
            string[] ret = new string[2];
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZMM_PR_CREATE");//Módulo de función

                IRfcTable pos = bapi.GetTable("PRITEM");
                IRfcTable posx = bapi.GetTable("PRITEMX");
                IRfcTable acc = bapi.GetTable("PRACCOUNT");
                IRfcTable accx = bapi.GetTable("PRACCOUNTX");
                IRfcTable lines = bapi.GetTable("SERVICELINES");
                IRfcTable linesx = bapi.GetTable("SERVICELINESX");
                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].TIPO.Equals("F"))
                    {
                        if (items[i].IMP.Equals("K"))
                        {
                            //Llenado de posiciones de requisición
                            pos.Append();
                            pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                            pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                            pos.SetValue("SHORT_TEXT", items[i].DESCR);
                            pos.SetValue("PLANT", items[i].WERKS);
                            pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                            pos.SetValue("QUANTITY", items[i].MENGE);
                            pos.SetValue("UNIT", "ZSE");
                            pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                            pos.SetValue("ACCTASSCAT", items[i].IMP);


                            posx.Append();
                            posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                            posx.SetValue("PREQ_ITEMX", "X");
                            posx.SetValue("PUR_GROUP", "X");
                            posx.SetValue("SHORT_TEXT", "X");
                            posx.SetValue("PLANT", "X");
                            posx.SetValue("MATL_GROUP", "X");
                            posx.SetValue("QUANTITY", "X");
                            posx.SetValue("UNIT", "X");
                            posx.SetValue("DELIV_DATE", "X");
                            posx.SetValue("ACCTASSCAT", "X");

                            for (int j = 0; j < imputaciones.Count; j++)
                            {
                                int b1 = Int32.Parse(imputaciones[j].BNFPO);
                                int b2 = Int32.Parse(items[i].BNFPO);
                                if (b1 == b2)
                                {
                                    acc.Append();
                                    acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    acc.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                    acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);
                                    acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);

                                    accx.Append();
                                    accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    accx.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                    accx.SetValue("PREQ_ITEMX", "X");
                                    accx.SetValue("SERIAL_NOX", "X");
                                    accx.SetValue("GL_ACCOUNT", "X");
                                    accx.SetValue("COSTCENTER", "X");

                                    for (int k = 0; k < servicios.Count; k++)
                                    {
                                        int b3 = Int32.Parse(servicios[k].BNFPO);
                                        if (b3 == b2 && servicios[k].SERIAL == imputaciones[j].SERIAL)
                                        {
                                            lines.Append();
                                            lines.SetValue("DOC_ITEM", servicios[j].BNFPO);
                                            lines.SetValue("SRV_LINE", (servicios[j].SERIAL));
                                            lines.SetValue("SERVICE", servicios[j].SERVICIO);
                                            lines.SetValue("SHORT_TEXT", servicios[j].TEXTO);
                                            lines.SetValue("QUANTITY", servicios[j].MENGE);
                                            lines.SetValue("UOM", servicios[j].MEINS);
                                            lines.SetValue("GROSS_PRICE", servicios[j].PREIS);

                                            linesx.Append();
                                            linesx.SetValue("DOC_ITEM", servicios[j].BNFPO);
                                            linesx.SetValue("SRV_LINE", (servicios[j].SERIAL));
                                            linesx.SetValue("SERVICE", "X");
                                            linesx.SetValue("SHORT_TEXT", "X");
                                            linesx.SetValue("QUANTITY", "X");
                                            linesx.SetValue("UOM", "X");
                                            linesx.SetValue("GROSS_PRICE", "X");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (items[i].IMP.Equals("A"))
                            {
                                //Llenado de posiciones de requisición
                                pos.Append();
                                pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                                pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                                pos.SetValue("SHORT_TEXT", items[i].DESCR);
                                pos.SetValue("PLANT", items[i].WERKS);
                                pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                                pos.SetValue("QUANTITY", items[i].MENGE);
                                pos.SetValue("UNIT", "ZSE");
                                pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                                pos.SetValue("ACCTASSCAT", items[i].IMP);


                                posx.Append();
                                posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                posx.SetValue("PREQ_ITEMX", "X");
                                posx.SetValue("PUR_GROUP", "X");
                                posx.SetValue("SHORT_TEXT", "X");
                                posx.SetValue("PLANT", "X");
                                posx.SetValue("MATL_GROUP", "X");
                                posx.SetValue("QUANTITY", "X");
                                posx.SetValue("UNIT", "X");
                                posx.SetValue("DELIV_DATE", "X");
                                posx.SetValue("ACCTASSCAT", "X");

                                for (int j = 0; j < imputaciones.Count; j++)
                                {
                                    int b1 = Int32.Parse(imputaciones[j].BNFPO);
                                    int b2 = Int32.Parse(items[i].BNFPO);
                                    if (imputaciones[j].BNFPO == items[i].BNFPO)
                                    {
                                        acc.Append();
                                        acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        acc.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                        acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);
                                        //acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);
                                        acc.SetValue("ASSET_NO", imputaciones[j].ASSET_NUM);
                                        acc.SetValue("SUB_NUMBER", "0000");

                                        accx.Append();
                                        accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        accx.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                        accx.SetValue("PREQ_ITEMX", "X");
                                        accx.SetValue("SERIAL_NOX", "X");
                                        accx.SetValue("GL_ACCOUNT", "X");
                                        //accx.SetValue("COSTCENTER", "X");
                                        accx.SetValue("ASSET_NO", "X");
                                        accx.SetValue("SUB_NUMBER", "X");

                                    }

                                }
                                for (int k = 0; k < servicios.Count; k++)
                                {
                                    int b1 = Int32.Parse(servicios[k].BNFPO);
                                    int b2 = Int32.Parse(items[i].BNFPO);
                                    if (servicios[k].BNFPO == items[i].BNFPO)
                                    {
                                        lines.Append();
                                        lines.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                        lines.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                        lines.SetValue("SERVICE", servicios[k].SERVICIO);
                                        lines.SetValue("SHORT_TEXT", servicios[k].TEXTO);
                                        lines.SetValue("QUANTITY", servicios[k].MENGE);
                                        lines.SetValue("UOM", servicios[k].MEINS);
                                        lines.SetValue("GROSS_PRICE", servicios[k].PREIS);

                                        linesx.Append();
                                        linesx.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                        linesx.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                        linesx.SetValue("SERVICE", "X");
                                        linesx.SetValue("SHORT_TEXT", "X");
                                        linesx.SetValue("QUANTITY", "X");
                                        linesx.SetValue("UOM", "X");
                                        linesx.SetValue("GROSS_PRICE", "X");
                                    }
                                }
                            }
                        }

                        //
                    }
                    else
                    {
                        //Llenado de posiciones de requisición
                        pos.Append();
                        pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                        pos.SetValue("MATERIAL", items[i].MATNR);
                        //pos.SetValue("SHORT_TEXT", items[i].DESCR);
                        pos.SetValue("QUANTITY", items[i].MENGE);
                        pos.SetValue("PLANT", items[i].WERKS);
                        pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                        pos.SetValue("PREQ_PRICE", items[i].PREIS);
                        pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                        pos.SetValue("ACCTASSCAT", items[i].IMP);


                        posx.Append();
                        posx.SetValue("PREQ_ITEMX", "X");
                        posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                        posx.SetValue("MATERIAL", "X");
                        posx.SetValue("QUANTITY", "X");
                        posx.SetValue("PLANT", "X");
                        posx.SetValue("PUR_GROUP", "X");
                        posx.SetValue("PREQ_PRICE", "X");
                        posx.SetValue("DELIV_DATE", "X");



                        if (!items[i].IMP.Equals("")) //Si tiene imputación 
                        {
                            for (int j = 0; j < servicios.Count; j++)
                            {
                                if (servicios[j].BNFPO == items[i].BNFPO)
                                {
                                    posx.SetValue("ACCTASSCAT", "X");
                                    acc.Append();
                                    acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    acc.SetValue("GL_ACCOUNT", servicios[j].GL_ACCOUNT);
                                    acc.SetValue("COSTCENTER", servicios[j].COSTCENTER);
                                    acc.SetValue("SERIAL_NO", (servicios[j].SERIAL));

                                    accx.Append();
                                    accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    accx.SetValue("SERIAL_NO", (servicios[j].SERIAL));
                                    accx.SetValue("SERIAL_NOX", "X");
                                    accx.SetValue("PREQ_ITEMX", "X");
                                    accx.SetValue("GL_ACCOUNT", "X");
                                    accx.SetValue("COSTCENTER", "X");
                                }
                            }
                        }
                    }
                }

                IRfcStructure head = bapi.GetStructure("PRHEADER");
                head.SetValue("PR_TYPE", "NB");//Tipo de pedido

                IRfcStructure headx = bapi.GetStructure("PRHEADERX");
                headx.SetValue("PR_TYPE", "X");

                bapi.Invoke(oDestino);

                IRfcStructure number = bapi.GetStructure("NUMBER");
                ret[0] = number.GetString("MESSAGE");//Export de función: Número de requisición


                IRfcTable bapi_return = bapi.GetTable("RETURN");
                string msg = "";
                for (int i = 1; i < bapi_return.Count; i++)
                {
                    bapi_return.CurrentIndex = i;
                    msg = msg + bapi_return.GetString("MESSAGE") + "\n"; //Mensajes de BAPI

                }
                ret[1] = msg;



                if (!ret[0].Equals("") && !ret[0].Equals(null)) //Si se creó correctamente en SAP
                {
                    ZEBAN_P_BE header = new ZEBAN_P_BE();
                    List<ZEBAN_BE> requisiciones = new List<ZEBAN_BE>();

                    header.FK_USUARIO = user;
                    header.STATUS = getStatus(user);
                    header.F_APROBACION = DateTime.Now;

                    int conf = ZEBAN_P_BLL.INSERT(header); //Insertar registro en SQL
                    List<ZEBAN_P_BE> zeban_p = ZEBAN_P_BLL.GET();
                    int b = zeban_p[(zeban_p.Count - 1)].BANFN;
                    int banfn = b;

                    for (int i = 0; i < items.Count; i++)
                    {
                        items[i].BANFN = banfn;
                    }
                    conf += ZEBAN_BLL.INSERT(items); //Insertar registro en SQL

                    List<ZEBAN_I_BE> imputacioness = new List<ZEBAN_I_BE>();
                    foreach (ZEBAN_BE it in items)
                    {
                        foreach (ZEBAN_I_BE s in imputaciones)
                        {
                            if (it.BNFPO == s.BNFPO)
                            {
                                s.BANFN = banfn;
                                imputacioness.Add(s);
                            }
                        }
                    }
                    if (imputacioness.Count > 0)
                    {
                        conf += ZEBAN_I_BLL.INSERT(imputacioness); //Insertar registros en SQL
                    }

                    List<ZEBAN_S_BE> servicioss = new List<ZEBAN_S_BE>();
                    foreach (ZEBAN_BE it in items)
                    {
                        foreach (ZEBAN_S_BE s in servicios)
                        {
                            if (it.BNFPO == s.BNFPO)
                            {
                                s.BANFN = banfn;
                                if (it.TIPO.Equals("F"))
                                {
                                    servicioss.Add(s);
                                }
                            }
                        }
                    }

                    if (servicioss.Count > 0)
                    {
                        conf += ZEBAN_S_BLL.INSERT(servicioss); //Insertar registros en SQL
                    }

                    enviarC(user, banfn, url);

                }
                return ret;
            }
            else
            {
                ret[0] = "";
                ret[1] = "NO SE PUDO VALIDAR LA REQUISICIÓN";
                return ret;
            }
        }

        public string[] bapi_pr_create2(List<ZEBAN_BE> items, List<ZEBAN_I_BE> imputaciones, List<ZEBAN_S_BE> servicios, string user, string url, string referencia)//Crear requisición en BD(Confirmar en SAP)//27.01.2017
        {
            string[] ret = new string[2];
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction bapi = repo.CreateFunction("ZMM_PR_CREATE2");//Módulo de función

                IRfcTable pos = bapi.GetTable("PRITEM");
                IRfcTable posx = bapi.GetTable("PRITEMX");
                IRfcTable acc = bapi.GetTable("PRACCOUNT");
                IRfcTable accx = bapi.GetTable("PRACCOUNTX");
                IRfcTable lines = bapi.GetTable("SERVICELINES");
                IRfcTable linesx = bapi.GetTable("SERVICELINESX");
                IRfcTable servacc = bapi.GetTable("SERVICEACCOUNT");
                IRfcTable servaccx = bapi.GetTable("SERVICEACCOUNTX");
                IRfcTable pritemtext = bapi.GetTable("PRITEMTEXT");

                for (int i = 0; i < items.Count; i++)
                {
                    if (items[i].TIPO.Equals("F"))
                    {
                        if (items[i].IMP.Equals("K"))
                        {
                            //Llenado de posiciones de requisición
                            pos.Append();
                            pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                            pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                            pos.SetValue("SHORT_TEXT", items[i].DESCR);
                            pos.SetValue("PLANT", items[i].WERKS);
                            pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                            pos.SetValue("QUANTITY", items[i].MENGE);
                            pos.SetValue("UNIT", "ZSE");
                            pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                            pos.SetValue("ACCTASSCAT", items[i].IMP);
                            pos.SetValue("ITEM_CAT", "9");


                            posx.Append();
                            posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                            posx.SetValue("PREQ_ITEMX", "X");
                            posx.SetValue("PUR_GROUP", "X");
                            posx.SetValue("SHORT_TEXT", "X");
                            posx.SetValue("PLANT", "X");
                            posx.SetValue("MATL_GROUP", "X");
                            posx.SetValue("QUANTITY", "X");
                            posx.SetValue("UNIT", "X");
                            posx.SetValue("DELIV_DATE", "X");
                            posx.SetValue("ACCTASSCAT", "X");
                            posx.SetValue("ITEM_CAT", "X");
                            posx.SetValue("CURRENCY", "X");

                            for (int j = 0; j < imputaciones.Count; j++)
                            {
                                int b1 = Int32.Parse(imputaciones[j].BNFPO);
                                int b2 = Int32.Parse(items[i].BNFPO);
                                if (b1 == b2)
                                {
                                    acc.Append();
                                    acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    //acc.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                    acc.SetValue("SERIAL_NO", "01");
                                    acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);
                                    acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);

                                    accx.Append();
                                    accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    //accx.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                    accx.SetValue("SERIAL_NO", "01");
                                    accx.SetValue("PREQ_ITEMX", "X");
                                    accx.SetValue("SERIAL_NOX", "X");
                                    accx.SetValue("GL_ACCOUNT", "X");
                                    accx.SetValue("COSTCENTER", "X");

                                    for (int k = 0; k < servicios.Count; k++)
                                    {
                                        int b3 = Int32.Parse(servicios[k].BNFPO);
                                        if (b3 == b2 && servicios[k].SERIAL == imputaciones[j].SERIAL)
                                        {
                                            lines.Append();
                                            lines.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                            lines.SetValue("OUTLINE", "1");
                                            //lines.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                            lines.SetValue("SRV_LINE", "1");
                                            lines.SetValue("SERVICE", servicios[k].SERVICIO);
                                            lines.SetValue("SHORT_TEXT", servicios[k].TEXTO);
                                            lines.SetValue("QUANTITY", servicios[k].MENGE);
                                            lines.SetValue("UOM", "ZSE");
                                            //servicios[k].PREIS = servicios[k].PREIS * 1000;
                                            servicios[k].PREIS = decimal.Round(servicios[k].PREIS, 2);
                                            lines.SetValue("GROSS_PRICE", servicios[k].PREIS);
                                            //lines.SetValue("PRICE_UNIT", 1000);
                                            lines.SetValue("CURRENCY", servicios[k].WAERS);
                                            pos.SetValue("CURRENCY", servicios[k].WAERS);

                                            linesx.Append();
                                            linesx.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                            linesx.SetValue("OUTLINE", "1");
                                            //linesx.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                            linesx.SetValue("SRV_LINE", "1");
                                            linesx.SetValue("SERVICE", "X");
                                            linesx.SetValue("SHORT_TEXT", "X");
                                            linesx.SetValue("QUANTITY", "X");
                                            linesx.SetValue("UOM", "X");
                                            linesx.SetValue("GROSS_PRICE", "X");
                                            //linesx.SetValue("PRICE_UNIT", "X");
                                            linesx.SetValue("CURRENCY", "X");

                                            servacc.Append();
                                            servacc.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                            //servacc.SetValue("OUTLINE", (k + 1) * 10);
                                            servacc.SetValue("OUTLINE", "1");
                                            servacc.SetValue("SRV_LINE", "1");
                                            servacc.SetValue("SERIAL_NO", "1");
                                            servacc.SetValue("SERIAL_NO_ITEM", "1");

                                            servaccx.Append();
                                            servaccx.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                            //servaccx.SetValue("OUTLINE", (k + 1) * 10);
                                            servaccx.SetValue("OUTLINE", "1");
                                            servaccx.SetValue("SRV_LINE", "1");
                                            servaccx.SetValue("SERIAL_NO", "1");
                                            servaccx.SetValue("SERIAL_NO_ITEM", 'X');

                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (items[i].IMP.Equals("A"))
                            {
                                for (int k = 0; k < servicios.Count; k++)
                                {
                                    int b3 = Int32.Parse(servicios[k].BNFPO);
                                    if (servicios[k].BNFPO.Trim() == items[i].BNFPO.Trim())
                                    {

                                        //Llenado de posiciones de requisición
                                        pos.Append();
                                        pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                                        pos.SetValue("SHORT_TEXT", items[i].DESCR);
                                        pos.SetValue("PLANT", items[i].WERKS);
                                        pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                                        //pos.SetValue("QUANTITY", items[i].MENGE);¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿
                                        //pos.SetValue("UNIT", "ZSE");¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿
                                        pos.SetValue("QUANTITY", servicios[k].MENGE);
                                        pos.SetValue("UNIT", servicios[k].MEINS);
                                        servicios[k].PREIS = servicios[k].PREIS * 1000;
                                        pos.SetValue("PREQ_PRICE", servicios[k].PREIS);
                                        pos.SetValue("CURRENCY", servicios[k].WAERS);
                                        pos.SetValue("PRICE_UNIT", 1000);
                                        pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                                        pos.SetValue("ACCTASSCAT", items[i].IMP);
                                        //pos.SetValue("ITEM_CAT", "9");¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿


                                        posx.Append();
                                        posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        posx.SetValue("PREQ_ITEMX", "X");
                                        posx.SetValue("PUR_GROUP", "X");
                                        posx.SetValue("SHORT_TEXT", "X");
                                        posx.SetValue("PLANT", "X");
                                        posx.SetValue("MATL_GROUP", "X");
                                        posx.SetValue("QUANTITY", "X");
                                        posx.SetValue("UNIT", "X");
                                        posx.SetValue("PREQ_PRICE", "X");
                                        posx.SetValue("CURRENCY", "X");
                                        posx.SetValue("PRICE_UNIT", "X");
                                        posx.SetValue("DELIV_DATE", "X");
                                        posx.SetValue("ACCTASSCAT", "X");
                                        //posx.SetValue("ITEM_CAT", "X");¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿
                                    }
                                }

                                for (int j = 0; j < imputaciones.Count; j++)
                                {
                                    int b1 = Int32.Parse(imputaciones[j].BNFPO);
                                    int b2 = Int32.Parse(items[i].BNFPO);
                                    if (imputaciones[j].BNFPO.Trim() == items[i].BNFPO.Trim())
                                    {
                                        acc.Append();
                                        acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        acc.SetValue("SERIAL_NO", "01");
                                        //acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿
                                        //acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);
                                        acc.SetValue("ASSET_NO", imputaciones[j].ASSET_NUM);
                                        //acc.SetValue("SUB_NUMBER", "0000");

                                        accx.Append();
                                        accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        accx.SetValue("SERIAL_NO", "01");
                                        accx.SetValue("PREQ_ITEMX", "X");
                                        accx.SetValue("SERIAL_NOX", "X");
                                        //accx.SetValue("GL_ACCOUNT", "X");¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿¿
                                        //accx.SetValue("COSTCENTER", "X");
                                        accx.SetValue("ASSET_NO", "X");
                                        //accx.SetValue("SUB_NUMBER", "X");

                                    }

                                }
                                //////for (int k = 0; k < servicios.Count; k++)
                                //////{
                                //////    int b1 = Int32.Parse(servicios[k].BNFPO);
                                //////    int b2 = Int32.Parse(items[i].BNFPO);
                                //////    if (servicios[k].BNFPO.Trim() == items[i].BNFPO.Trim())
                                //////    {
                                //////        lines.Append();
                                //////        lines.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                //////       //lines.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                //////        lines.SetValue("SRV_LINE", "01");
                                //////        lines.SetValue("SERVICE", servicios[k].SERVICIO);
                                //////        lines.SetValue("SHORT_TEXT", servicios[k].TEXTO);
                                //////        lines.SetValue("QUANTITY", servicios[k].MENGE);
                                //////        lines.SetValue("UOM", servicios[k].MEINS);
                                //////        lines.SetValue("GROSS_PRICE", servicios[k].PREIS);

                                //////        linesx.Append();
                                //////        linesx.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                //////        //linesx.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                //////        linesx.SetValue("SRV_LINE", "01");
                                //////        linesx.SetValue("SERVICE", "X");
                                //////        linesx.SetValue("SHORT_TEXT", "X");
                                //////        linesx.SetValue("QUANTITY", "X");
                                //////        linesx.SetValue("UOM", "X");
                                //////        linesx.SetValue("GROSS_PRICE", "X");

                                //////        servacc.Append();
                                //////        servacc.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                //////        servacc.SetValue("OUTLINE", "1");
                                //////        servacc.SetValue("SRV_LINE", "1");
                                //////        servacc.SetValue("SERIAL_NO", "1");
                                //////        servacc.SetValue("SERIAL_NO_ITEM", "1");

                                //////        servaccx.Append();
                                //////        servaccx.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                //////        servaccx.SetValue("OUTLINE", "1");
                                //////        servaccx.SetValue("SRV_LINE", "1");
                                //////        servaccx.SetValue("SERIAL_NO", "1");
                                //////        servaccx.SetValue("SERIAL_NO_ITEM", 'X');
                                //////    }
                                //////}
                            }
                        }

                        //
                    }
                    else
                    {
                        if (items[i].IMP.Equals("K"))
                        {
                            //Llenado de posiciones de requisición
                            pos.Append();
                            pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                            pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                            pos.SetValue("SHORT_TEXT", items[i].DESCR);
                            pos.SetValue("PLANT", items[i].WERKS);
                            pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                            pos.SetValue("QUANTITY", items[i].MENGE);
                            pos.SetValue("UNIT", items[i].MEINS);
                            pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                            pos.SetValue("ACCTASSCAT", items[i].IMP);
                            items[i].PREIS = items[i].PREIS * 1000;
                            pos.SetValue("PREQ_PRICE", items[i].PREIS);
                            pos.SetValue("CURRENCY", items[i].WAERS);
                            pos.SetValue("PRICE_UNIT", 1000);
                            //pos.SetValue("ITEM_CAT", "9");


                            posx.Append();
                            posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                            posx.SetValue("PREQ_ITEMX", "X");
                            posx.SetValue("PUR_GROUP", "X");
                            posx.SetValue("SHORT_TEXT", "X");
                            posx.SetValue("PLANT", "X");
                            posx.SetValue("PREQ_PRICE", "X");
                            posx.SetValue("MATL_GROUP", "X");
                            posx.SetValue("QUANTITY", "X");
                            posx.SetValue("UNIT", "X");
                            posx.SetValue("DELIV_DATE", "X");
                            posx.SetValue("ACCTASSCAT", "X");
                            //posx.SetValue("ITEM_CAT", "X");
                            posx.SetValue("CURRENCY", "X");
                            posx.SetValue("PRICE_UNIT", "X");

                            for (int j = 0; j < imputaciones.Count; j++)
                            {
                                int b1 = Int32.Parse(imputaciones[j].BNFPO);
                                int b2 = Int32.Parse(items[i].BNFPO);
                                if (b1 == b2)
                                {
                                    acc.Append();
                                    acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    //acc.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                    acc.SetValue("SERIAL_NO", "01");
                                    acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);
                                    acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);

                                    accx.Append();
                                    accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    //accx.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                    accx.SetValue("SERIAL_NO", "01");
                                    accx.SetValue("PREQ_ITEMX", "X");
                                    accx.SetValue("SERIAL_NOX", "X");
                                    accx.SetValue("GL_ACCOUNT", "X");
                                    accx.SetValue("COSTCENTER", "X");

                                }
                            }
                        }
                        else
                        {
                            //Llenado de posiciones de requisición
                            pos.Append();
                            pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                            pos.SetValue("MATERIAL", items[i].MATNR);
                            //pos.SetValue("SHORT_TEXT", items[i].DESCR);
                            pos.SetValue("QUANTITY", items[i].MENGE);
                            pos.SetValue("PLANT", items[i].WERKS);
                            pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                            pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                            pos.SetValue("ACCTASSCAT", items[i].IMP);
                            pos.SetValue("CURRENCY", items[i].WAERS);
                            items[i].PREIS = items[i].PREIS * 1000;
                            pos.SetValue("PREQ_PRICE", items[i].PREIS);
                            pos.SetValue("PRICE_UNIT", 1000);


                            posx.Append();
                            posx.SetValue("PREQ_ITEMX", "X");
                            posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                            posx.SetValue("MATERIAL", "X");
                            posx.SetValue("QUANTITY", "X");
                            posx.SetValue("PLANT", "X");
                            posx.SetValue("PUR_GROUP", "X");
                            posx.SetValue("PREQ_PRICE", "X");
                            posx.SetValue("DELIV_DATE", "X");
                            posx.SetValue("CURRENCY", "X");
                            posx.SetValue("PRICE_UNIT", "X");



                            if (!items[i].IMP.Equals("")) //Si tiene imputación 
                            {
                                for (int j = 0; j < servicios.Count; j++)
                                {
                                    if (servicios[j].BNFPO == items[i].BNFPO.Trim())
                                    {
                                        posx.SetValue("ACCTASSCAT", "X");
                                        acc.Append();
                                        acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        acc.SetValue("GL_ACCOUNT", servicios[j].GL_ACCOUNT);
                                        acc.SetValue("COSTCENTER", servicios[j].COSTCENTER);
                                        acc.SetValue("SERIAL_NO", (servicios[j].SERIAL));

                                        accx.Append();
                                        accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        accx.SetValue("SERIAL_NO", (servicios[j].SERIAL));
                                        accx.SetValue("SERIAL_NOX", "X");
                                        accx.SetValue("PREQ_ITEMX", "X");
                                        accx.SetValue("GL_ACCOUNT", "X");
                                        accx.SetValue("COSTCENTER", "X");
                                    }
                                }
                            }
                        }
                    }
                    pritemtext.Append();
                    pritemtext.SetValue("PREQ_ITEM", items[i].BNFPO);
                    pritemtext.SetValue("TEXT_ID", "F06");


                    int banfn = items[0].BANFN;
                    int roo = rol(USUARIO_BLL.GET(user)[0]);
                    if (roo == 5)
                    {
                        pritemtext.SetValue("TEXT_LINE", "AUTORIZÓ: " + user + ". FOLIO: " + banfn);
                    }
                    else
                    {
                        string gerente = "";
                        List<ZEBAN_H_BE> his = ZEBAN_H_BLL.GET(banfn);
                        foreach (ZEBAN_H_BE h in his)
                        {
                            int r = rol(USUARIO_BLL.GET(h.NIPLOGIN)[0]);
                            if (r == 5)
                            {
                                gerente = h.NIPLOGIN;
                            }
                        }
                        pritemtext.SetValue("TEXT_LINE", "AUTORIZÓ: " + gerente + ", " + user + ". FOLIO: " + banfn);
                    }

                    //27.01.2017------------------------------------------------------------------------
                    pritemtext.Append();
                    pritemtext.SetValue("PREQ_ITEM", items[i].BNFPO);
                    pritemtext.SetValue("TEXT_ID", "F07");
                    pritemtext.SetValue("TEXT_LINE", referencia);

                    //27.01.2017------------------------------------------------------------------------

                }



                IRfcStructure head = bapi.GetStructure("PRHEADER");
                head.SetValue("PR_TYPE", "NB");//Tipo de pedido

                IRfcStructure headx = bapi.GetStructure("PRHEADERX");
                headx.SetValue("PR_TYPE", "X");



                bapi.Invoke(oDestino);

                IRfcStructure number = bapi.GetStructure("NUMBER");
                ret[0] = number.GetString("MESSAGE");//Export de función: Número de requisición


                IRfcTable bapi_return = bapi.GetTable("RETURN");
                string msg = "";
                for (int i = 1; i < bapi_return.Count; i++)
                {
                    bapi_return.CurrentIndex = i;
                    msg = msg + bapi_return.GetString("MESSAGE") + "\n"; //Mensajes de BAPI

                }
                ret[1] = msg;

                return ret;
            }
            else
            {
                ret[0] = "";
                ret[1] = "NO HAY CONEXIÓN A SAP";
                return ret;
            }
        }

        private void enviarC(string u, int b, string url)
        {
            Mail email = new Mail();
            int r = rol(USUARIO_BLL.GET(u)[0]) + 1;

            string[] dest = getDestinatarios(r, u);
            string asunto = "Se ha creado una requisición";
            string mensaje = "Se creó la requisición: " + b + "<br><br>";
            email.send(dest, asunto, mensaje, b, url);
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
                dest[cont] = USUARIO_BLL.GET(u)[0].eMail; //Agregar usuario que crea la requisición.
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
            //string[] contabilidad = ConfigurationManager.AppSettings["contabilidad"].Split(';');
            string contabilidad = ConfigurationManager.AppSettings["contabilidad"];

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
                if (contabilidad.Equals(u.NipLogin.Trim()))
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
        private string getStatus(string usuario)
        {
            List<USUARIO_BE> users = USUARIO_BLL.GET(usuario);
            USUARIO_BE u = new USUARIO_BE();

            string s = "";
            if (users.Count > 0)
            {
                u = users[0];
            }

            if (u.DIREC)
            {
                s = "4";
            }
            else if (u.GEREN)
            {
                s = "3";
            }
            else if (u.CONTA)
            {
                s = "2";
            }
            else
            {
                s = "1";
            }

            return s;
        }

        public string[] bapi_pr_change(List<ZEBAN_BE> items, List<ZEBAN_I_BE> imputaciones, List<ZEBAN_S_BE> servicios, string user, int banfn, List<ZEBAN_BE> items_a, string url)//Crear requisición
        {
            string[] ret = new string[2];
            string status = getStatus(user);
            if (status.Equals(""))
            {
                return ret;
            }
            else
            {
                if (conectar())
                {
                    RfcRepository repo = oDestino.Repository;
                    IRfcFunction bapi = repo.CreateFunction("ZMM_PR_CREATE");//Módulo de función

                    IRfcTable pos = bapi.GetTable("PRITEM");
                    IRfcTable posx = bapi.GetTable("PRITEMX");
                    IRfcTable acc = bapi.GetTable("PRACCOUNT");
                    IRfcTable accx = bapi.GetTable("PRACCOUNTX");
                    IRfcTable lines = bapi.GetTable("SERVICELINES");
                    IRfcTable linesx = bapi.GetTable("SERVICELINESX");
                    for (int i = 0; i < items.Count; i++)
                    {
                        if (items[i].TIPO.Equals("F"))
                        {
                            if (items[i].IMP.Equals("K"))
                            {
                                //Llenado de posiciones de requisición
                                pos.Append();
                                pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                                pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                                pos.SetValue("SHORT_TEXT", items[i].DESCR);
                                pos.SetValue("PLANT", items[i].WERKS);
                                pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                                pos.SetValue("QUANTITY", items[i].MENGE);
                                pos.SetValue("UNIT", "ZSE");
                                pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                                pos.SetValue("ACCTASSCAT", items[i].IMP);


                                posx.Append();
                                posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                posx.SetValue("PREQ_ITEMX", "X");
                                posx.SetValue("PUR_GROUP", "X");
                                posx.SetValue("SHORT_TEXT", "X");
                                posx.SetValue("PLANT", "X");
                                posx.SetValue("MATL_GROUP", "X");
                                posx.SetValue("QUANTITY", "X");
                                posx.SetValue("UNIT", "X");
                                posx.SetValue("DELIV_DATE", "X");
                                posx.SetValue("ACCTASSCAT", "X");

                                for (int j = 0; j < imputaciones.Count; j++)
                                {
                                    if (imputaciones[j].BNFPO == items[i].BNFPO)
                                    {
                                        acc.Append();
                                        acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        acc.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                        acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);
                                        acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);

                                        accx.Append();
                                        accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        accx.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                        accx.SetValue("PREQ_ITEMX", "X");
                                        accx.SetValue("SERIAL_NOX", "X");
                                        accx.SetValue("GL_ACCOUNT", "X");
                                        accx.SetValue("COSTCENTER", "X");

                                        for (int k = 0; k < servicios.Count; k++)
                                        {
                                            int b1 = Int32.Parse(servicios[k].BNFPO);
                                            int b2 = Int32.Parse(items[i].BNFPO);
                                            if (b1 == b2 && servicios[k].SERIAL == imputaciones[j].SERIAL)
                                            {
                                                lines.Append();
                                                lines.SetValue("DOC_ITEM", servicios[j].BNFPO);
                                                lines.SetValue("SRV_LINE", (servicios[j].SERIAL));
                                                lines.SetValue("SERVICE", servicios[j].SERVICIO);
                                                lines.SetValue("SHORT_TEXT", servicios[j].TEXTO);
                                                lines.SetValue("QUANTITY", servicios[j].MENGE);
                                                lines.SetValue("UOM", servicios[j].MEINS);
                                                lines.SetValue("GROSS_PRICE", servicios[j].PREIS);

                                                linesx.Append();
                                                linesx.SetValue("DOC_ITEM", servicios[j].BNFPO);
                                                linesx.SetValue("SRV_LINE", (servicios[j].SERIAL));
                                                linesx.SetValue("SERVICE", "X");
                                                linesx.SetValue("SHORT_TEXT", "X");
                                                linesx.SetValue("QUANTITY", "X");
                                                linesx.SetValue("UOM", "X");
                                                linesx.SetValue("GROSS_PRICE", "X");
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (items[i].IMP.Equals("A"))
                                {
                                    //Llenado de posiciones de requisición
                                    pos.Append();
                                    pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                                    pos.SetValue("SHORT_TEXT", items[i].DESCR);
                                    pos.SetValue("PLANT", items[i].WERKS);
                                    pos.SetValue("MATL_GROUP", items[i].MAT_GR);
                                    pos.SetValue("QUANTITY", items[i].MENGE);
                                    pos.SetValue("UNIT", "ZSE");
                                    pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                                    pos.SetValue("ACCTASSCAT", items[i].IMP);


                                    posx.Append();
                                    posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                    posx.SetValue("PREQ_ITEMX", "X");
                                    posx.SetValue("PUR_GROUP", "X");
                                    posx.SetValue("SHORT_TEXT", "X");
                                    posx.SetValue("PLANT", "X");
                                    posx.SetValue("MATL_GROUP", "X");
                                    posx.SetValue("QUANTITY", "X");
                                    posx.SetValue("UNIT", "X");
                                    posx.SetValue("DELIV_DATE", "X");
                                    posx.SetValue("ACCTASSCAT", "X");

                                    for (int j = 0; j < imputaciones.Count; j++)
                                    {
                                        int b1 = Int32.Parse(imputaciones[j].BNFPO);
                                        int b2 = Int32.Parse(items[i].BNFPO);
                                        if (b1 == b2)
                                        {
                                            acc.Append();
                                            acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                            acc.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                            acc.SetValue("GL_ACCOUNT", imputaciones[j].GL_ACCOUNT);
                                            //acc.SetValue("COSTCENTER", imputaciones[j].COSTCENTER);
                                            acc.SetValue("ASSET_NO", imputaciones[j].ASSET_NUM);
                                            acc.SetValue("SUB_NUMBER", "0000");

                                            accx.Append();
                                            accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                            accx.SetValue("SERIAL_NO", (imputaciones[j].SERIAL));
                                            accx.SetValue("PREQ_ITEMX", "X");
                                            accx.SetValue("SERIAL_NOX", "X");
                                            accx.SetValue("GL_ACCOUNT", "X");
                                            //accx.SetValue("COSTCENTER", "X");
                                            accx.SetValue("ASSET_NO", "X");
                                            accx.SetValue("SUB_NUMBER", "X");

                                        }

                                    }
                                    for (int k = 0; k < servicios.Count; k++)
                                    {

                                        int b1 = Int32.Parse(servicios[k].BNFPO);
                                        int b2 = Int32.Parse(items[i].BNFPO);
                                        if (b1 == b2)
                                        {
                                            lines.Append();
                                            lines.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                            lines.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                            lines.SetValue("SERVICE", servicios[k].SERVICIO);
                                            lines.SetValue("SHORT_TEXT", servicios[k].TEXTO);
                                            lines.SetValue("QUANTITY", servicios[k].MENGE);
                                            lines.SetValue("UOM", servicios[k].MEINS);
                                            lines.SetValue("GROSS_PRICE", servicios[k].PREIS);

                                            linesx.Append();
                                            linesx.SetValue("DOC_ITEM", servicios[k].BNFPO);
                                            linesx.SetValue("SRV_LINE", (servicios[k].SERIAL));
                                            linesx.SetValue("SERVICE", "X");
                                            linesx.SetValue("SHORT_TEXT", "X");
                                            linesx.SetValue("QUANTITY", "X");
                                            linesx.SetValue("UOM", "X");
                                            linesx.SetValue("GROSS_PRICE", "X");
                                        }
                                    }
                                }
                            }

                            //
                        }
                        else
                        {
                            //Llenado de posiciones de requisición
                            pos.Append();
                            pos.SetValue("PREQ_ITEM", items[i].BNFPO);
                            pos.SetValue("MATERIAL", items[i].MATNR);
                            //pos.SetValue("SHORT_TEXT", items[i].DESCR);
                            pos.SetValue("QUANTITY", items[i].MENGE);
                            pos.SetValue("PLANT", items[i].WERKS);
                            pos.SetValue("PUR_GROUP", items[i].PUR_GR);
                            pos.SetValue("PREQ_PRICE", items[i].PREIS);
                            pos.SetValue("DELIV_DATE", items[i].F_ENTREGA);
                            pos.SetValue("ACCTASSCAT", items[i].IMP);


                            posx.Append();
                            posx.SetValue("PREQ_ITEMX", "X");
                            posx.SetValue("PREQ_ITEM", items[i].BNFPO);
                            posx.SetValue("MATERIAL", "X");
                            posx.SetValue("QUANTITY", "X");
                            posx.SetValue("PLANT", "X");
                            posx.SetValue("PUR_GROUP", "X");
                            posx.SetValue("PREQ_PRICE", "X");
                            posx.SetValue("DELIV_DATE", "X");



                            if (!items[i].IMP.Equals("")) //Si tiene imputación 
                            {
                                for (int j = 0; j < servicios.Count; j++)
                                {
                                    int b1 = Int32.Parse(servicios[j].BNFPO);
                                    int b2 = Int32.Parse(items[i].BNFPO);
                                    if (b2 == b1)
                                    {
                                        posx.SetValue("ACCTASSCAT", "X");
                                        acc.Append();
                                        acc.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        acc.SetValue("GL_ACCOUNT", servicios[j].GL_ACCOUNT);
                                        acc.SetValue("COSTCENTER", servicios[j].COSTCENTER);
                                        acc.SetValue("SERIAL_NO", (servicios[j].SERIAL));

                                        accx.Append();
                                        accx.SetValue("PREQ_ITEM", items[i].BNFPO);
                                        accx.SetValue("SERIAL_NO", (servicios[j].SERIAL));
                                        accx.SetValue("SERIAL_NOX", "X");
                                        accx.SetValue("PREQ_ITEMX", "X");
                                        accx.SetValue("GL_ACCOUNT", "X");
                                        accx.SetValue("COSTCENTER", "X");
                                    }
                                }
                            }
                        }
                    }

                    IRfcStructure head = bapi.GetStructure("PRHEADER");
                    head.SetValue("PR_TYPE", "NB");//Tipo de pedido

                    IRfcStructure headx = bapi.GetStructure("PRHEADERX");
                    headx.SetValue("PR_TYPE", "X");

                    bapi.Invoke(oDestino);

                    IRfcStructure number = bapi.GetStructure("NUMBER");
                    ret[0] = number.GetString("MESSAGE");//Export de función: Número de requisición


                    IRfcTable bapi_return = bapi.GetTable("RETURN");
                    string msg = "";
                    for (int i = 1; i < bapi_return.Count; i++)
                    {
                        bapi_return.CurrentIndex = i;
                        msg = msg + bapi_return.GetString("MESSAGE") + "\n"; //Mensajes de BAPI

                    }
                    ret[1] = msg;


                    if (!ret[0].Equals("") && !ret[0].Equals(null)) //Si se creó correctamente en SAP
                    {
                        ZEBAN_P_BE header = ZEBAN_P_BLL.GET(banfn + "")[0];
                        List<ZEBAN_BE> requisiciones = new List<ZEBAN_BE>();

                        header.BANFN = banfn;
                        //header.FK_USUARIO = user;
                        header.STATUS = getStatus(user);
                        header.F_APROBACION = DateTime.Now;
                        //header.APROBO = "";
                        ZEBAN_P_BLL.UPDATE(header);

                        for (int i = 0; i < items.Count; i++)
                        {
                            items[i].BANFN = banfn;
                            int p = (i + 1) * 10;
                            items[i].BNFPO = p + "";
                        }
                        if (items_a.Count > 0)
                        {
                            int del = ZEBAN_BLL.DELETE(items_a);
                            foreach (ZEBAN_BE z in items_a)
                            {
                                List<ZEBAN_S_BE> l = ZEBAN_S_BLL.GET(z.BANFN, z.BNFPO);
                                if (l.Count > 0)
                                {
                                    ZEBAN_S_BLL.DELETE(l);
                                }
                                List<ZEBAN_I_BE> ii = ZEBAN_I_BLL.GET(z.BANFN, z.BNFPO);
                                if (ii.Count > 0)
                                {
                                    ZEBAN_I_BLL.DELETE(ii);
                                }

                            }
                        }
                        int conf = ZEBAN_BLL.INSERT(items); //Insertar registro en SQL

                        List<ZEBAN_I_BE> imputacioness = new List<ZEBAN_I_BE>();
                        foreach (ZEBAN_BE it in items)
                        {
                            foreach (ZEBAN_I_BE s in imputaciones)
                            {
                                int a = Int32.Parse(it.BNFPO);
                                int b = Int32.Parse(s.BNFPO);
                                if (a == b)
                                {
                                    s.BANFN = banfn;
                                    imputacioness.Add(s);
                                }
                            }
                        }
                        if (imputacioness.Count > 0)
                        {
                            conf += ZEBAN_I_BLL.INSERT(imputacioness); //Insertar registros en SQL
                        }

                        List<ZEBAN_S_BE> servicioss = new List<ZEBAN_S_BE>();
                        foreach (ZEBAN_BE it in items)
                        {
                            foreach (ZEBAN_S_BE s in servicios)
                            {
                                if (it.BNFPO == s.BNFPO)
                                {
                                    s.BANFN = banfn;
                                    if (it.TIPO.Equals("F"))
                                    {
                                        servicioss.Add(s);
                                    }
                                }
                            }
                        }

                        if (servicioss.Count > 0)
                        {
                            conf += ZEBAN_S_BLL.INSERT(servicioss); //Insertar registros en SQL
                        }

                        ZEBAN_H_BE his = new ZEBAN_H_BE();
                        his.BANFN = banfn + "";
                        his.FECHA = DateTime.Now;
                        his.NIPLOGIN = user;

                        ZEBAN_H_BLL.INSERT(his);

                        //enviarM(user, banfn, url);
                    }

                    return ret;
                }
                else
                {
                    ret[0] = "";
                    ret[1] = "NO SE PUDO VALIDAR LA REQUISICIÓN";
                    return ret;
                }

            }
        }
        private void enviarM(string u, int b, string url)
        {
            Mail email = new Mail();
            int r = rol(USUARIO_BLL.GET(u)[0]) + 1;
            string[] dest1 = getDestinatarios(r, u);
            string uu = ZEBAN_P_BLL.GET(b + "")[0].FK_USUARIO;

            string[] dest2 = getDestinatarios(1, uu);

            string[] dest = new string[dest1.Length + 1];
            for (int i = 0; i < dest1.Length; i++)
            {
                dest[i] = dest1[i];
            }
            dest[dest1.Length] = dest2[dest2.Length - 1];

            string asunto = "Se ha modificado una requisición";
            string mensaje = u + " modificó la requisición: " + b + "<br><br>";
            email.send(dest, asunto, mensaje, b, url);
        }

        public string bapi_pr_change(List<ZEBAN_P_BE> items, string ebeln)//Modificar requisición
        {
            //if (conectar())
            //{
            //    RfcRepository repo = oDestino.Repository;
            //    IRfcFunction bapi = repo.CreateFunction("ZMM_PR_CHANGE");

            //    IRfcTable pos = bapi.GetTable("PRITEM");
            //    IRfcTable posx = bapi.GetTable("PRITEMX");
            //    for (int i = 0; i < items.Count; i++)
            //    {
            //        pos.Append();
            //        pos.SetValue("PREQ_ITEM", items[i].Po_item);
            //        pos.SetValue("MATERIAL", items[i].Material);
            //        pos.SetValue("QUANTITY", items[i].Quantity);
            //        pos.SetValue("PLANT", items[i].Plant);

            //        posx.Append();
            //        posx.SetValue("PREQ_ITEMX", check(items[i].Po_item));
            //        posx.SetValue("PREQ_ITEM", items[i].Po_item);
            //        posx.SetValue("MATERIAL", check(items[i].Material));
            //        posx.SetValue("QUANTITY", check(items[i].Quantity));
            //        posx.SetValue("PLANT", check(items[i].Plant));
            //    }

            //    IRfcStructure head = bapi.GetStructure("PRHEADER");
            //    IRfcStructure headx = bapi.GetStructure("PRHEADERX");
            //    head.SetValue("PR_TYPE", "NB");
            //    head.SetValue("PREQ_NO", ebeln);
            //    headx.SetValue("PR_TYPE", "X");
            //    headx.SetValue("PREQ_NO", "X");
            //    bapi.SetValue("NUMBER", ebeln);


            //    bapi.Invoke(oDestino);

            //    IRfcTable bapi_return = bapi.GetTable("RETURN");
            //    string msg = "";
            //    for (int i = 0; i < bapi_return.Count; i++)
            //    {
            //        bapi_return.CurrentIndex = i;
            //        msg = msg + bapi_return.GetString("MESSAGE");

            //    }

            //    List<ZEBAN_BE> requisiciones_old = ZEBAN_BLL.GET(ebeln);
            //    List<ZEBAN_BE> requisiciones = new List<ZEBAN_BE>();

            //    for (int i = 0; i < pos.Count; i++)
            //    {
            //        ZEBAN_BE req = new ZEBAN_BE();

            //        //llenado de objeto
            //        pos.CurrentIndex = i;
            //        req.BANFN = ebeln;
            //        req.BNFPO = pos.GetString("PREQ_ITEM");
            //        req.MATNR = pos.GetString("MATERIAL");
            //        req.MEINS = pos.GetString("UNIT");
            //        req.MENGE = Decimal.Parse(pos.GetString("QUANTITY"));
            //        req.PREIS = Decimal.Parse(pos.GetString("PREQ_PRICE"));
            //        req.STATUS = "N";
            //        req.WERKS = pos.GetString("PLANT");

            //        requisiciones.Add(req);
            //    }

            //    int conf = ZEBAN_BLL.UPDATE(requisiciones);

            //    return msg;
            //}
            //else
            //{

            return "NO HAY CONEXIÓN A SAP";
            //}
        }
        public IRfcTable get_PR()//Trae tabla con todas las requisiciones creadas por el usuario
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getPO = repo.CreateFunction("ZMM_PR_GET");

                sapUser = sapUser.ToUpper();

                getPO.SetValue("USER", sapUser);

                getPO.Invoke(oDestino);
                IRfcTable it_ekko = getPO.GetTable("IT_EBAN");
                IRfcTable retu = getPO.GetTable("RETURN");
                getPO.Invoke(oDestino);

                if (retu.Count > 0)
                {
                    return retu;
                }
                else
                {
                    return it_ekko;
                }
            }
            else
            {
                return null;
            }


        }
        public IRfcTable getDetalles_PR(string banfn)//Trae detalles de las posiciones de requisición
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getPO = repo.CreateFunction("ZMM_PR_DETAIL");

                sapUser = sapUser.ToUpper();

                getPO.SetValue("BANFN", banfn);

                getPO.Invoke(oDestino);
                IRfcTable it_ekko = getPO.GetTable("IT_EBAN");
                IRfcTable retu = getPO.GetTable("RETURN");
                getPO.Invoke(oDestino);

                if (retu.Count > 0)
                {
                    return retu;
                }
                else
                {
                    return it_ekko;
                }
            }
            else
            {
                return null;
            }
        }
        #endregion
        #region adjuntos
        public string rfcFuncionAdjuntar(string key, string type, string file, byte[] content)//Adjunta archivo a SAP
        {
            string[] ext = file.Split('.');
            string desc = ext[0];
            string extension = ext[1];

            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction GOS = repo.CreateFunction("ZMM_ADJUNTAR");
                GOS.SetValue("P_KEY", key);
                GOS.SetValue("P_TYPE", type);
                GOS.SetValue("P_FILE", file);
                GOS.SetValue("P_DESC", desc);

                IRfcTable text = GOS.GetTable("text");
                //if (extension.Equals("TXT") || extension.Equals("txt"))
                //{
                //    string s = Encoding.UTF8.GetString(content, 0, content.Length);
                //    s = s.Replace("\r\n", "\n");
                //    string[] ss = s.Split(null);

                //    for (int i = 0; i < ss.Length;i++) {
                //        text.Append();
                //        text.SetValue("LINE", ss[i]);
                //    }
                //}else
                //{
                GOS.SetValue("p_raw", content);
                //}

                IRfcStructure messtab = GOS.GetStructure("BAPIRET");
                GOS.Invoke(oDestino);
                string msg = messtab.GetString("MESSAGE");
                if (msg.Equals(""))
                {
                    msg = "Se ha subido correctamente el archivo: " + file;
                }
                return msg;
            }
            else
            {
                return "No hay conexión a SAP";
            }
        }

        public IRfcTable rfcFuncionLista(string key, string type)//Lista de adjuntos de la requisición
        {
            IRfcTable datos;
            if (conectar())
            {
                RfcRepository lista = oDestino.Repository;
                IRfcFunction GOS = lista.CreateFunction("ZRSG_GOS02");
                GOS.SetValue("P_KEY", key);
                GOS.SetValue("P_TYPE", type);

                GOS.Invoke(oDestino);
                datos = GOS.GetTable("T_LIST");

                return datos;
            }
            return null;
        }

        public void rfcDescargar(string key, string type, string folder)//Trae contenido de adjunto para descargarse
        {
            if (conectar())
            {
                RfcRepository lista = oDestino.Repository;
                IRfcFunction GOS = lista.CreateFunction("ZRSG_GOS03");
                GOS.SetValue("P_KEY", key);
                GOS.SetValue("P_TYPE", type);
                GOS.SetValue("P_FOLD", folder);

                GOS.Invoke(oDestino);
                contenidoHEX = GOS.GetTable("LT_CONT_HEX");
                contenido = GOS.GetTable("LT_CONTENT");
            }
        }
        public IRfcTable getContenidoHEX()
        {
            return contenidoHEX;
        }
        public IRfcTable getContenido()
        {
            return contenido;
        }

        private string check(string valor)
        {
            if (valor.Equals("") || valor.Equals(null))
            {
                return "";
            }
            else
            {
                return "X";
            }
        }
        #endregion
        #region catalogos

        public IRfcTable get_servicios(string texto, string tipo)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_SERVICIOS");

                if (texto.Length > 10)
                {
                    texto = texto.Substring(0, 10);
                }

                get.SetValue("TEXTO", texto);
                get.SetValue("TIPO", tipo);

                get.Invoke(oDestino);
                IRfcTable materiales = get.GetTable("O_SERVICIO");

                return materiales;
            }
            else
            {
                return null;
            }
        }

        public string check_materialDesc(string matnr)//Trae descripción del material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getMat = repo.CreateFunction("ZMM_MATNR_CHECK");

                sapUser = sapUser.ToUpper();

                //getPO.SetValue("USER", sapUser);
                getMat.SetValue("MATNR", matnr);

                getMat.Invoke(oDestino);

                IRfcStructure desc = getMat.GetStructure("DESC");
                string des = desc.GetString("MESSAGE");

                return des;

            }
            else
            {
                return null;
            }
        }
        public string check_materialPrecio(string matnr)//Trae descripción del material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getMat = repo.CreateFunction("ZMM_MATNR_CHECK");

                sapUser = sapUser.ToUpper();

                //getPO.SetValue("USER", sapUser);
                getMat.SetValue("MATNR", matnr);

                getMat.Invoke(oDestino);

                IRfcStructure desc = getMat.GetStructure("MBEW");
                string prec = desc.GetString("STPRS");

                return prec;

            }
            else
            {
                return null;
            }
        }

        public string check_materialGrupo(string matnr)//Trae descripción del material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getMat = repo.CreateFunction("ZMM_MATNR_CHECK");

                sapUser = sapUser.ToUpper();

                //getPO.SetValue("USER", sapUser);
                getMat.SetValue("MATNR", matnr);

                getMat.Invoke(oDestino);

                IRfcStructure desc = getMat.GetStructure("MATKL");
                string prec = desc.GetString("MATKL");

                return prec;

            }
            else
            {
                return null;
            }
        }


        public IRfcTable check_servicio(string serv, string tipo)//Trae descripción del material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_SERV_CHECK");

                get.SetValue("NUM", serv);
                get.SetValue("TIPO", tipo);

                get.Invoke(oDestino);
                IRfcTable materiales = get.GetTable("O_SERVICIO");

                return materiales;
            }
            else
            {
                return null;
            }
        }
        public string check_servDesc(string serv)//Trae descripción del material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getMat = repo.CreateFunction("ZMM_ASNUM_CHECK");

                sapUser = sapUser.ToUpper();

                //getPO.SetValue("USER", sapUser);
                getMat.SetValue("ASNUM", serv);

                getMat.Invoke(oDestino);

                IRfcStructure desc = getMat.GetStructure("DESC");
                string prec = desc.GetString("MESSAGE");

                return prec;

            }
            else
            {
                return null;
            }
        }
        public IRfcTable get_matnr(string matnr)//Trae lista de materiales que complen: LIKE %matnr%
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_MATNR");

                if (matnr.Length > 10)
                {
                    matnr = matnr.Substring(0, 10);
                }

                get.SetValue("I_MATNR", matnr);

                get.Invoke(oDestino);
                IRfcTable materiales = get.GetTable("O_MATNR");

                return materiales;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable get_werks(string werks)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_WERKS");

                if (werks.Length > 10)
                {
                    werks = werks.Substring(0, 10);
                }
                get.SetValue("I_WERKS", werks);

                get.Invoke(oDestino);
                IRfcTable materiales = get.GetTable("O_WERKS");

                return materiales;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable get_ekgrp(string ekgrp)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_EKGRP");

                if (ekgrp.Length > 10)
                {
                    ekgrp = ekgrp.Substring(0, 10);
                }
                get.SetValue("I_EKGRP", ekgrp);

                get.Invoke(oDestino);
                IRfcTable grupos = get.GetTable("O_EKGRP");

                return grupos;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable get_saknr(string saknr)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_SAKNR");

                if (saknr.Length > 10)
                {
                    saknr = saknr.Substring(0, 10);
                }
                get.SetValue("I_SAKNR", saknr);

                get.Invoke(oDestino);
                IRfcTable cuentas = get.GetTable("O_SAKNR");

                return cuentas;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable get_kostl(string kostl)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_KOSTL");

                if (kostl.Length > 10)
                {
                    kostl = kostl.Substring(0, 10);
                }
                get.SetValue("I_KOSTL", kostl);

                get.Invoke(oDestino);
                IRfcTable costes = get.GetTable("O_KOSTL");

                return costes;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable get_matkl(string matkl)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_T023");

                if (matkl.Length > 10)
                {
                    matkl = matkl.Substring(0, 10);
                }
                get.SetValue("I_MATKL", matkl);

                get.Invoke(oDestino);
                IRfcTable matl = get.GetTable("O_MATKL");

                return matl;
            }
            else
            {
                return null;
            }
        }
        public IRfcTable get_asnum(string asnum)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_ASNUM");

                if (asnum.Length > 10)
                {
                    asnum = asnum.Substring(0, 10);
                }
                get.SetValue("I_ASNUM", asnum);

                get.Invoke(oDestino);
                IRfcTable matl = get.GetTable("O_ASNUM");

                return matl;
            }
            else
            {
                return null;
            }
        }

        public IRfcTable get_asset(string asset)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_ASSET");

                if (asset.Length > 10)
                {
                    asset = asset.Substring(0, 10);
                }
                get.SetValue("I_ASSET", asset);

                get.Invoke(oDestino);
                IRfcTable matl = get.GetTable("O_ASSET");

                return matl;
            }
            else
            {
                return null;
            }
        }

        public string get_Ebeln(string b)
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction get = repo.CreateFunction("ZMM_GET_EBELN");

                get.SetValue("I_BANFN", b);

                get.Invoke(oDestino);
                string ebeln = get.GetString("O_EBELN");

                return ebeln;
            }
            else
            {
                return "";
            }
        }

        public string get_UOM(string uom)//Trae tabla con todas las requisiciones creadas por el usuario
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getPO = repo.CreateFunction("ZMM_GET_T006");

                sapUser = sapUser.ToUpper();

                getPO.SetValue("IM_UOM", uom);

                getPO.Invoke(oDestino);
                string ex_oum = getPO.GetString("EX_UOM");
                getPO.Invoke(oDestino);

                return ex_oum;
            }
            else
            {
                return null;
            }


        }

        public string get_currency(string valor, string moneda)//Trae descripción del material
        {
            if (conectar())
            {
                RfcRepository repo = oDestino.Repository;
                IRfcFunction getCurr = repo.CreateFunction("ZMM_GET_CURRENCY");

                sapUser = sapUser.ToUpper();

                DateTime d = DateTime.Now;
                string dia;
                string mes;

                if (d.Day < 10)
                {
                    dia = "0" + d.Day;
                }
                else
                {
                    dia = d.Day + "";
                }
                if (d.Month < 10)
                {
                    mes = "0" + d.Month;
                }
                else
                {
                    mes = d.Month + "";
                }

                string fecha = d.Year + "" + mes + "" + dia + "";

                getCurr.SetValue("DATE", fecha);
                getCurr.SetValue("FOREIGN_AMOUNT", valor);
                getCurr.SetValue("FOREIGN_CURRENCY", moneda);
                getCurr.SetValue("LOCAL_CURRENCY", "MXN");

                getCurr.Invoke(oDestino);

                IRfcStructure desc = getCurr.GetStructure("LOCAL_AMOUNT");
                string des = desc.GetString("TFACT");

                return des;

            }
            else
            {
                return null;
            }
        }
    }
}
        #endregion