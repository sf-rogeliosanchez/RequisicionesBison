using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using System.Data;
using System.Collections;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Configuration;
using System.Data.Common;
using System.Transactions;
using System.Data.Odbc;
using System.Data.SqlClient;


namespace IPersistence.SQL
{
    public class SQLDataLayer : AbstractDataAccessLayer
    {

        #region ZEBAN_P
        public override int ZEBAN_P_INSERT(ZEBAN_P_BE z)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {



                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                    DB.AddInParameter(Command, "@BANFN", DbType.String, z.BANFN);
                    DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, z.FK_USUARIO);
                    DB.AddInParameter(Command, "@STATUS", DbType.String, z.STATUS);
                    DB.AddInParameter(Command, "@APROBO", DbType.String, z.APROBO);
                    DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, z.SAP_BANFN);
                    DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, z.SAP_EBELN);
                    DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, z.F_APROBACION);
                    DB.AddInParameter(Command, "@REFERENCIA", DbType.String, z.REFERENCIA);
                    DB.AddInParameter(Command, "@COMPRADOR", DbType.String, z.COMPRADOR);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                    returnValue = DB.ExecuteNonQuery(Command);

                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override List<ZEBAN_P_BE> ZEBAN_P_GET()
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                DB.AddInParameter(Command, "@BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@STATUS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@APROBO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@REFERENCIA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COMPRADOR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_P_BE> ZEBANFN_P_GET(string banfn)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@STATUS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@APROBO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@REFERENCIA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COMPRADOR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_P_BE> ZEBANFN_P_GET(string banfn, string aprobo)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@STATUS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@APROBO", DbType.String, aprobo);
                DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@REFERENCIA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COMPRADOR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, 24);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_P_BE> ZEBANFN_P_GET(int status)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                DB.AddInParameter(Command, "@BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@STATUS", DbType.String, status + "");
                DB.AddInParameter(Command, "@APROBO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@REFERENCIA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COMPRADOR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_C);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_P_BE> ZEBANFN_P_GET(int iss, string comprador)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                DB.AddInParameter(Command, "@BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@STATUS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@APROBO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@REFERENCIA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COMPRADOR", DbType.String, comprador);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, 25);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_P_BE> ZEBAN_P_GET(USUARIO_BE usuario)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                DB.AddInParameter(Command, "@BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, usuario.NipLogin);
                DB.AddInParameter(Command, "@STATUS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@APROBO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@REFERENCIA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COMPRADOR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesZEBAN.GET_USER_HISTORICAL);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_P_BE> ZEBAN_P_GET(String consulta)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = new SqlCommand(consulta);

                return ZEBAN_P_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override int ZEBAN_P_UPDATE(ZEBAN_P_BE z)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    //foreach (ZEBAN_P_BE z in requisiciones)
                    //{
                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                    DB.AddInParameter(Command, "@BANFN", DbType.String, z.BANFN);
                    DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, z.FK_USUARIO);
                    DB.AddInParameter(Command, "@STATUS", DbType.String, z.STATUS);
                    DB.AddInParameter(Command, "@APROBO", DbType.String, z.APROBO);
                    DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, z.SAP_BANFN);
                    DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, z.SAP_EBELN);
                    DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, z.F_APROBACION);
                    DB.AddInParameter(Command, "@REFERENCIA", DbType.String, z.REFERENCIA);
                    DB.AddInParameter(Command, "@COMPRADOR", DbType.String, z.COMPRADOR);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.UPDATE);

                    returnValue = DB.ExecuteNonQuery(Command);
                    //}
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override int ZEBAN_P_DELETE(List<ZEBAN_P_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_P_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_P);

                        DB.AddInParameter(Command, "@BANFN", DbType.String, z.BANFN);
                        DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, z.F_APROBACION);
                        DB.AddInParameter(Command, "@STATUS", DbType.String, z.STATUS);
                        DB.AddInParameter(Command, "@APROBO", DbType.String, z.APROBO);
                        DB.AddInParameter(Command, "@SAP_BANFN", DbType.String, z.SAP_BANFN);
                        DB.AddInParameter(Command, "@SAP_EBELN", DbType.String, z.SAP_EBELN);
                        DB.AddInParameter(Command, "@F_APROBACION", DbType.DateTime, z.F_APROBACION);
                        DB.AddInParameter(Command, "@REFERENCIA", DbType.String, z.REFERENCIA);
                        DB.AddInParameter(Command, "@COMPRADOR", DbType.String, z.COMPRADOR);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.DELETE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        #endregion
        #region ZEBAN
        public override int ZEBAN_INSERT(List<ZEBAN_BE> requisiciones)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@MATNR", DbType.String, z.MATNR);
                        DB.AddInParameter(Command, "@DESCR", DbType.String, z.DESCR);
                        DB.AddInParameter(Command, "@WERKS", DbType.String, z.WERKS);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        DB.AddInParameter(Command, "@WAERS", DbType.String, z.WAERS);
                        DB.AddInParameter(Command, "@IMP", DbType.String, z.IMP);
                        DB.AddInParameter(Command, "@PUR_GR", DbType.String, z.PUR_GR);
                        DB.AddInParameter(Command, "@MAT_GR", DbType.String, z.MAT_GR);
                        DB.AddInParameter(Command, "@TIPO", DbType.String, z.TIPO);
                        DB.AddInParameter(Command, "@F_ENTREGA", DbType.DateTime, z.F_ENTREGA);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override List<ZEBAN_BE> ZEBAN_GET()
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN);

                DB.AddInParameter(Command, "@BANFN", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@BNFPO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MATNR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@DESCR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@WERKS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MENGE", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@MEINS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@PREIS", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@WAERS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@IMP", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@PUR_GR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MAT_GR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TIPO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_ENTREGA", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET);

                return ZEBAN_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_BE> ZEBAN_GET(int banfn)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN);

                DB.AddInParameter(Command, "@BANFN", DbType.Int32, banfn);
                DB.AddInParameter(Command, "@BNFPO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MATNR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@DESCR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@WERKS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MENGE", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@MEINS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@PREIS", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@WAERS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@IMP", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@PUR_GR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MAT_GR", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TIPO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@F_ENTREGA", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_BE> ZEBAN_GET(String consulta)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = new SqlCommand(consulta);

                return ZEBAN_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override int ZEBAN_UPDATE(List<ZEBAN_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@MATNR", DbType.String, z.MATNR);
                        DB.AddInParameter(Command, "@DESCR", DbType.String, z.DESCR);
                        DB.AddInParameter(Command, "@WERKS", DbType.String, z.WERKS);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        DB.AddInParameter(Command, "@WAERS", DbType.String, z.WAERS);
                        DB.AddInParameter(Command, "@IMP", DbType.String, z.IMP);
                        DB.AddInParameter(Command, "@PUR_GR", DbType.String, z.PUR_GR);
                        DB.AddInParameter(Command, "@MAT_GR", DbType.String, z.MAT_GR);
                        DB.AddInParameter(Command, "@TIPO", DbType.String, z.TIPO);
                        DB.AddInParameter(Command, "@F_ENTREGA", DbType.DateTime, z.F_ENTREGA);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.UPDATE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override int ZEBAN_DELETE(List<ZEBAN_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@MATNR", DbType.String, z.MATNR);
                        DB.AddInParameter(Command, "@DESCR", DbType.String, z.DESCR);
                        DB.AddInParameter(Command, "@WERKS", DbType.String, z.WERKS);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        DB.AddInParameter(Command, "@WAERS", DbType.String, z.WAERS);
                        DB.AddInParameter(Command, "@IMP", DbType.String, z.IMP);
                        DB.AddInParameter(Command, "@PUR_GR", DbType.String, z.PUR_GR);
                        DB.AddInParameter(Command, "@MAT_GR", DbType.String, z.MAT_GR);
                        DB.AddInParameter(Command, "@TIPO", DbType.String, z.TIPO);
                        DB.AddInParameter(Command, "@F_ENTREGA", DbType.DateTime, z.F_ENTREGA);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.DELETE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        #endregion
        #region ZEBAN_H
        public override int ZEBAN_H_INSERT(ZEBAN_H_BE z)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {



                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_H);

                    DB.AddInParameter(Command, "@BANFN", DbType.String, z.BANFN);
                    DB.AddInParameter(Command, "@NipLogin", DbType.String, z.NIPLOGIN);
                    DB.AddInParameter(Command, "@FECHA", DbType.DateTime, z.FECHA);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                    returnValue = DB.ExecuteNonQuery(Command);

                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }

        public override ZEBAN_H_BE ZEBANFN_H_GET(string banfn)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_H);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@NipLogin", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FECHA", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET);

                return ZEBAN_H_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }

        public override List<ZEBAN_H_BE> ZEBANFN_H_GET(int banfn)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_H);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@NipLogin", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FECHA", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_H_GET2(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        #endregion
        #region ZEBAN_D
        public override int ZEBAN_D_INSERT(ZEBAN_D_BE z)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {



                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_D);

                    DB.AddInParameter(Command, "@ID", DbType.Int32, z.ID);
                    DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                    DB.AddInParameter(Command, "@NOMBRE", DbType.String, z.NOMBRE);
                    DB.AddInParameter(Command, "@EXT", DbType.String, z.EXT);
                    DB.AddInParameter(Command, "@INFO", DbType.Binary, z.INFO);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                    returnValue = DB.ExecuteNonQuery(Command);

                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override List<ZEBAN_D_BE> ZEBANFN_D_GET(int banfn)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_D);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@ID", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@NOMBRE", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@EXT", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@INFO", DbType.Binary, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_D_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_D_BE> ZEBANFN_D_GET(int banfn, int id)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_D);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@ID", DbType.Int32, id);
                DB.AddInParameter(Command, "@NOMBRE", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@EXT", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@INFO", DbType.Binary, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, 24);

                return ZEBAN_D_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override int ZEBAN_D_DELETE(ZEBAN_D_BE z)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {

                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_D);

                    DB.AddInParameter(Command, "@ID", DbType.Int32, z.ID);
                    DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                    DB.AddInParameter(Command, "@NOMBRE", DbType.String, z.NOMBRE);
                    DB.AddInParameter(Command, "@EXT", DbType.String, z.EXT);
                    DB.AddInParameter(Command, "@INFO", DbType.Binary, z.INFO);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.DELETE);

                    returnValue = DB.ExecuteNonQuery(Command);

                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        #endregion
        #region ZEBAN_I
        public override int ZEBAN_I_INSERT(List<ZEBAN_I_BE> requisiciones)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_I_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_I);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@SERIAL", DbType.Int32, z.SERIAL);
                        DB.AddInParameter(Command, "@LINE", DbType.Int32, z.LINE);
                        DB.AddInParameter(Command, "@GL_ACCOUNT", DbType.String, z.GL_ACCOUNT);
                        DB.AddInParameter(Command, "@COSTCENTER", DbType.String, z.COSTCENTER);
                        DB.AddInParameter(Command, "@ASSET_NUM", DbType.String, z.ASSET_NUM);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        //DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        //DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        //DB.AddInParameter(Command, "@SERVICIO", DbType.String, z.SERVICIO);
                        //DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override List<ZEBAN_I_BE> ZEBAN_I_GET()
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_I);

                DB.AddInParameter(Command, "@BANFN", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@BNFPO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SERIAL", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@LINE", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@GL_ACCOUNT", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COSTCENTER", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ASSET_NUM", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MENGE", DbType.Decimal, DBNull.Value);
                //DB.AddInParameter(Command, "@MEINS", DbType.String, DBNull.Value);
                //DB.AddInParameter(Command, "@PREIS", DbType.Decimal, DBNull.Value);
                //DB.AddInParameter(Command, "@SERVICIO", DbType.String, DBNull.Value);
                //DB.AddInParameter(Command, "@TEXTO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET);

                return ZEBAN_I_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_I_BE> ZEBAN_I_GET(int banfn, string bnfpo)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_I);

                DB.AddInParameter(Command, "@BANFN", DbType.Int32, banfn);
                DB.AddInParameter(Command, "@BNFPO", DbType.String, bnfpo);
                DB.AddInParameter(Command, "@SERIAL", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@LINE", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@GL_ACCOUNT", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@COSTCENTER", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ASSET_NUM", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@MENGE", DbType.Decimal, DBNull.Value);
                //DB.AddInParameter(Command, "@MEINS", DbType.String, DBNull.Value);
                //DB.AddInParameter(Command, "@PREIS", DbType.Decimal, DBNull.Value);
                //DB.AddInParameter(Command, "@SERVICIO", DbType.String, DBNull.Value);
                //DB.AddInParameter(Command, "@TEXTO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_I_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_I_BE> ZEBAN_I_GET(String consulta)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = new SqlCommand(consulta);

                return ZEBAN_I_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override int ZEBAN_I_UPDATE(List<ZEBAN_I_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_I_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_I);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@SERIAL", DbType.Int32, z.SERIAL);
                        DB.AddInParameter(Command, "@LINE", DbType.Int32, z.LINE);
                        DB.AddInParameter(Command, "@GL_ACCOUNT", DbType.String, z.GL_ACCOUNT);
                        DB.AddInParameter(Command, "@COSTCENTER", DbType.String, z.COSTCENTER);
                        DB.AddInParameter(Command, "@ASSET_NUM", DbType.String, z.ASSET_NUM);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        //DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        //DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        //DB.AddInParameter(Command, "@SERVICIO", DbType.String, z.SERVICIO);
                        //DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.UPDATE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override int ZEBAN_I_DELETE(List<ZEBAN_I_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_I_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_I);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@SERIAL", DbType.Int32, z.SERIAL);
                        DB.AddInParameter(Command, "@LINE", DbType.Int32, z.LINE);
                        DB.AddInParameter(Command, "@GL_ACCOUNT", DbType.String, z.GL_ACCOUNT);
                        DB.AddInParameter(Command, "@COSTCENTER", DbType.String, z.COSTCENTER);
                        DB.AddInParameter(Command, "@ASSET_NUM", DbType.String, z.ASSET_NUM);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        //DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        //DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        //DB.AddInParameter(Command, "@SERVICIO", DbType.String, z.SERVICIO);
                        //DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.DELETE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        #endregion
        #region ZEBAN_S
        public override int ZEBAN_S_INSERT(List<ZEBAN_S_BE> requisiciones)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_S_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_S);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@SERIAL", DbType.Int32, z.SERIAL);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        DB.AddInParameter(Command, "@WAERS", DbType.String, z.WAERS);
                        DB.AddInParameter(Command, "@SERVICIO", DbType.String, z.SERVICIO);
                        DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override List<ZEBAN_S_BE> ZEBAN_S_GET()
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_S);

                DB.AddInParameter(Command, "@BANFN", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@BNFPO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SERIAL", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@MENGE", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@MEINS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@PREIS", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@WAERS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SERVICIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TEXTO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET);

                return ZEBAN_S_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_S_BE> ZEBAN_S_GET(int banfn, string bnfpo)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_S);

                DB.AddInParameter(Command, "@BANFN", DbType.Int32, banfn);
                DB.AddInParameter(Command, "@BNFPO", DbType.String, bnfpo);
                DB.AddInParameter(Command, "@SERIAL", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@MENGE", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@MEINS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@PREIS", DbType.Decimal, DBNull.Value);
                DB.AddInParameter(Command, "@WAERS", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@SERVICIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TEXTO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_S_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_S_BE> ZEBAN_S_GET(String consulta)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = new SqlCommand(consulta);

                return ZEBAN_S_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override int ZEBAN_S_UPDATE(List<ZEBAN_S_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_S_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_S);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@SERIAL", DbType.Int32, z.SERIAL);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        DB.AddInParameter(Command, "@WAERS", DbType.String, z.WAERS);
                        DB.AddInParameter(Command, "@SERVICIO", DbType.String, z.SERVICIO);
                        DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.UPDATE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override int ZEBAN_S_DELETE(List<ZEBAN_S_BE> requisiciones)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {


                    foreach (ZEBAN_S_BE z in requisiciones)
                    {
                        Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_S);

                        DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                        DB.AddInParameter(Command, "@BNFPO", DbType.String, z.BNFPO);
                        DB.AddInParameter(Command, "@SERIAL", DbType.Int32, z.SERIAL);
                        DB.AddInParameter(Command, "@MENGE", DbType.Decimal, z.MENGE);
                        DB.AddInParameter(Command, "@MEINS", DbType.String, z.MEINS);
                        DB.AddInParameter(Command, "@PREIS", DbType.Decimal, z.PREIS);
                        DB.AddInParameter(Command, "@WAERS", DbType.String, z.WAERS);
                        DB.AddInParameter(Command, "@SERVICIO", DbType.String, z.SERVICIO);
                        DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                        DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.DELETE);

                        returnValue = DB.ExecuteNonQuery(Command);
                    }
                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        #endregion
        #region ZEBAN_C
        public override int ZEBAN_C_INSERT(ZEBAN_C_BE z)
        {
            DbCommand Command = null;
            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {



                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_C);

                    DB.AddInParameter(Command, "@ID", DbType.Int32, z.ID);
                    DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                    DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, z.FK_USUARIO);
                    DB.AddInParameter(Command, "@FECHA", DbType.DateTime, z.FECHA);
                    DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.ADD);

                    returnValue = DB.ExecuteNonQuery(Command);

                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        public override List<ZEBAN_C_BE> ZEBANFN_C_GET(int banfn)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_C);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@ID", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FECHA", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@TEXTO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return ZEBAN_C_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override List<ZEBAN_C_BE> ZEBANFN_C_GET(int banfn, int id)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_C);

                DB.AddInParameter(Command, "@BANFN", DbType.String, banfn);
                DB.AddInParameter(Command, "@ID", DbType.Int32, id);
                DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@FECHA", DbType.DateTime, DBNull.Value);
                DB.AddInParameter(Command, "@TEXTO", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, 24);

                return ZEBAN_C_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
        public override int ZEBAN_C_DELETE(ZEBAN_C_BE z)
        {
            DbCommand Command = null;

            int returnValue = 0;

            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                using (TransactionScope MyTranasaccion = new TransactionScope())
                {

                    Command = DB.GetStoredProcCommand(Constants.CSP_ZEBAN_C);

                    DB.AddInParameter(Command, "@ID", DbType.Int32, z.ID);
                    DB.AddInParameter(Command, "@BANFN", DbType.Int32, z.BANFN);
                    DB.AddInParameter(Command, "@FK_USUARIO", DbType.String, z.FK_USUARIO);
                    DB.AddInParameter(Command, "@FECHA", DbType.DateTime, z.FECHA);
                    DB.AddInParameter(Command, "@TEXTO", DbType.String, z.TEXTO);
                    DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.DELETE);

                    returnValue = DB.ExecuteNonQuery(Command);

                    MyTranasaccion.Complete();
                    return 1;
                }
            }
            catch (Exception ex)
            {
                string strError = ex.Message.ToString();
                return 0;
            }
            finally
            {
                Command.Dispose();
            }

        }
        #endregion
        #region usuario
        public override List<USUARIO_BE> USUARIO_GET(string nipLogin)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_USUARIO);

                DB.AddInParameter(Command, "@NIPLOGIN", DbType.String, nipLogin);
                DB.AddInParameter(Command, "@EMAIL", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@USUA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@CONTA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@GEREN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@DIREC", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@AREA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TIPO", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_B);

                return USUARIO_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }

        public override List<USUARIO_BE> USUARIO_GET()
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_USUARIO);

                DB.AddInParameter(Command, "@NIPLOGIN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@EMAIL", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@USUA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@CONTA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@GEREN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@DIREC", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@AREA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TIPO", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET);

                return USUARIO_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }

        public override List<USUARIO_BE> USUARIO_GET(int tipo)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_USUARIO);

                DB.AddInParameter(Command, "@NIPLOGIN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@EMAIL", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@USUA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@CONTA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@GEREN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@DIREC", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@AREA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@TIPO", DbType.Int32, tipo);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, Constants.AccionesTabla.GET_C);

                return USUARIO_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }

        public override List<USUARIO_BE> USUARIO_GET(string t, string area)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_USUARIO);

                DB.AddInParameter(Command, "@NIPLOGIN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@EMAIL", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@USUA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@CONTA", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@GEREN", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@DIREC", DbType.String, DBNull.Value);
                DB.AddInParameter(Command, "@AREA", DbType.String, area);
                DB.AddInParameter(Command, "@TIPO", DbType.Int32, DBNull.Value);
                DB.AddInParameter(Command, "@ACCION", DbType.Int32, 25);

                return USUARIO_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }



        #endregion
        #region ZACCMATG
        public override List<ZACCXMATG_BE> ZACCMATGR_GET(string GL_ACCOUNT)
        {
            DbCommand Command = null;
            try
            {
                Database DB = DatabaseFactory.CreateDatabase();
                Command = DB.GetStoredProcCommand(Constants.CSP_ZACCXMATG);

                DB.AddInParameter(Command, "@GL_ACCOUNT", DbType.String, GL_ACCOUNT);

                return ZACCMATG_GET(DB.ExecuteReader(Command));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Command.Dispose();
            }
        }
            #endregion ZACCMATG
    }

}
