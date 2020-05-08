using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using IEntities;

namespace IPersistence
{
    public class BaseDataLayer
    {
        
        /// <summary>
        /// Obtiene una lista de usuarios
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        /// 
        public List<ZEBAN_P_BE> ZEBAN_P_GET(IDataReader reader)
        {
            List<ZEBAN_P_BE> zeban_p = new List<ZEBAN_P_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_P_BE z = new ZEBAN_P_BE();

                    z.APROBO = reader["APROBO"].ToString();
                    z.BANFN = Int32.Parse(reader["BANFN"].ToString());
                    z.F_APROBACION = DateTime.Parse(reader["F_APROBACION"].ToString());
                    z.FK_USUARIO = reader["FK_USUARIO"].ToString();
                    z.SAP_BANFN = reader["SAP_BANFN"].ToString();
                    z.SAP_EBELN = reader["SAP_EBELN"].ToString();
                    z.STATUS = reader["STATUS"].ToString();
                    z.REFERENCIA = reader["REFERENCIA"].ToString();
                    z.COMPRADOR = reader["COMPRADOR"].ToString();

                    zeban_p.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban_p;
        }
        public List<ZEBAN_BE> ZEBAN_GET(IDataReader reader)
        {
            List<ZEBAN_BE> zeban = new List<ZEBAN_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_BE z = new ZEBAN_BE();

                    z.BANFN = Int32.Parse(reader["BANFN"].ToString());
                    z.BNFPO = reader["BNFPO"].ToString();
                    z.MATNR = reader["MATNR"].ToString();
                    z.DESCR = reader["DESCR"].ToString();
                    z.MEINS = reader["MEINS"].ToString();
                    z.MENGE = Decimal.Parse(reader["MENGE"].ToString());
                    z.WERKS = reader["WERKS"].ToString();
                    z.PREIS = Decimal.Parse(reader["PREIS"].ToString());
                    z.WAERS = reader["WAERS"].ToString();
                    z.IMP = reader["IMP"].ToString();
                    z.TIPO = reader["TIPO"].ToString();
                    z.PUR_GR = reader["PUR_GR"].ToString();
                    z.MAT_GR = reader["MAT_GR"].ToString();
                    z.F_ENTREGA = DateTime.Parse(reader["F_ENTREGA"].ToString());

                    zeban.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban;
        }

        public List<USUARIO_BE> USUARIO_GET(IDataReader reader)
        {
            List<USUARIO_BE> user = new List<USUARIO_BE>();

            try
            {
                while (reader.Read())
                {
                    USUARIO_BE z = new USUARIO_BE();

                    z.NipLogin = reader["NipLogin"].ToString();
                    z.UserKey = reader["User_Key"].ToString();
                    z.eMail = reader["eMail"].ToString();
                    z.USUA = bool.Parse(reader["USUA"].ToString());
                    z.CONTA = bool.Parse(reader["CONTA"].ToString());
                    z.GEREN = bool.Parse(reader["GEREN"].ToString());
                    z.DIREC = bool.Parse(reader["DIREC"].ToString());
                    z.AREA = reader["AREA"].ToString();

                    user.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return user;
        }
        public ZEBAN_H_BE ZEBAN_H_GET(IDataReader reader)
        {
            ZEBAN_H_BE z = new ZEBAN_H_BE();

            try
            {
                while (reader.Read())
                {

                    z.BANFN = reader["BANFN"].ToString();
                    z.FECHA = DateTime.Parse(reader["FECHA"].ToString());
                    z.NIPLOGIN = reader["NipLogin"].ToString();

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return z;
        }

        public List<ZEBAN_H_BE> ZEBAN_H_GET2(IDataReader reader)
        {
            List<ZEBAN_H_BE> zeban = new List<ZEBAN_H_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_H_BE z = new ZEBAN_H_BE();

                    z.BANFN = reader["BANFN"].ToString();
                    z.FECHA = DateTime.Parse(reader["FECHA"].ToString());
                    z.NIPLOGIN = reader["NipLogin"].ToString();
                    zeban.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban;
        }
        //public ZEBAN_D_BE ZEBAN_D_GET(IDataReader reader)
        //{
        //    ZEBAN_D_BE z = new ZEBAN_D_BE();

        //    try
        //    {
        //        while (reader.Read())
        //        {

        //            z.ID = Int32.Parse(reader["ID"].ToString());
        //            z.BANFN = Int32.Parse(reader["BANFN"].ToString());
        //            z.NOMBRE = reader["NOMBRE"].ToString();
        //            z.EXT = reader["EXT"].ToString();
        //            z.INFO = (byte[])reader["INFO"];

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    finally
        //    {
        //        reader.Close();
        //    }
        //    return z;
        //}

        public List<ZEBAN_D_BE> ZEBAN_D_GET(IDataReader reader)
        {
            List<ZEBAN_D_BE> zeban = new List<ZEBAN_D_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_D_BE z = new ZEBAN_D_BE();

                    z.ID = Int32.Parse(reader["ID"].ToString());
                    z.BANFN = Int32.Parse(reader["BANFN"].ToString());
                    z.NOMBRE = reader["NOMBRE"].ToString();
                    z.EXT = reader["EXT"].ToString();
                    z.INFO = (byte[])reader["INFO"];

                    zeban.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban;
        }

        public List<ZEBAN_I_BE> ZEBAN_I_GET(IDataReader reader)
        {
            List<ZEBAN_I_BE> zeban = new List<ZEBAN_I_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_I_BE z = new ZEBAN_I_BE();

                    z.BANFN = Int32.Parse(reader["BANFN"].ToString());
                    z.BNFPO = reader["BNFPO"].ToString();
                    z.SERIAL = Int32.Parse(reader["SERIAL"].ToString());
                    z.LINE = Int32.Parse(reader["LINE"].ToString());
                    z.COSTCENTER = reader["COSTCENTER"].ToString();
                    z.ASSET_NUM = reader["ASSET_NUM"].ToString();
                    z.GL_ACCOUNT = reader["GL_ACCOUNT"].ToString();
                    z.MENGE = Decimal.Parse(reader["MENGE"].ToString());
                    //z.MEINS = reader["MEINS"].ToString();
                    //z.PREIS = Decimal.Parse(reader["PREIS"].ToString());
                    //z.SERVICIO= reader["SERVICIO"].ToString();
                    //z.TEXTO = reader["TEXTO"].ToString();

                    zeban.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban;
        }
        public List<ZEBAN_S_BE> ZEBAN_S_GET(IDataReader reader)
        {
            List<ZEBAN_S_BE> zeban = new List<ZEBAN_S_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_S_BE z = new ZEBAN_S_BE();

                    z.BANFN = Int32.Parse(reader["BANFN"].ToString());
                    z.BNFPO = reader["BNFPO"].ToString();
                    z.SERIAL = Int32.Parse(reader["SERIAL"].ToString());
                    z.MENGE = Decimal.Parse(reader["MENGE"].ToString());
                    z.MEINS = reader["MEINS"].ToString();
                    z.PREIS = Decimal.Parse(reader["PREIS"].ToString());
                    z.WAERS = reader["WAERS"].ToString();
                    z.SERVICIO = reader["SERVICIO"].ToString();
                    z.TEXTO = reader["TEXTO"].ToString();

                    zeban.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban;
        }

        public List<ZEBAN_C_BE> ZEBAN_C_GET(IDataReader reader)
        {
            List<ZEBAN_C_BE> zeban = new List<ZEBAN_C_BE>();

            try
            {
                while (reader.Read())
                {
                    ZEBAN_C_BE z = new ZEBAN_C_BE();

                    z.ID = Int32.Parse(reader["ID"].ToString());
                    z.BANFN = Int32.Parse(reader["BANFN"].ToString());
                    z.FK_USUARIO = reader["FK_USUARIO"].ToString();
                    z.FECHA = DateTime.Parse(reader["FECHA"].ToString());
                    z.TEXTO = reader["TEXTO"].ToString();

                    zeban.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zeban;
        }

        public List<ZACCXMATG_BE> ZACCMATG_GET(IDataReader reader)
        {
            List<ZACCXMATG_BE> zacc = new List<ZACCXMATG_BE>();

            try
            {
                while (reader.Read())
                {
                    ZACCXMATG_BE z = new ZACCXMATG_BE();

                    z.GL_ACCOUNT = reader["GL_ACCOUNT"].ToString();
                    z.MAT_GR = reader["MAT_GR"].ToString();

                    zacc.Add(z);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                reader.Close();
            }
            return zacc;
        }
    }
}
