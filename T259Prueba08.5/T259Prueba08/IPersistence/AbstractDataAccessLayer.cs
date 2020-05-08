using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;
using System.Web.Profile;
using System.Data.Common;
using IEntities;
using System.Data.SqlClient;


namespace IPersistence
{
    public abstract partial class AbstractDataAccessLayer : BaseDataLayer
    {

        protected delegate List<object> GenerateListFromReader(IDataAdapter reader);

        #region ZEBAN_P
        public abstract int ZEBAN_P_INSERT(ZEBAN_P_BE requisiciones);
        public abstract List<ZEBAN_P_BE> ZEBAN_P_GET();
        public abstract List<ZEBAN_P_BE> ZEBAN_P_GET(USUARIO_BE usuario);
        public abstract List<ZEBAN_P_BE> ZEBANFN_P_GET(string banfn);
        public abstract List<ZEBAN_P_BE> ZEBANFN_P_GET(string banfn, string aprobo);
        public abstract List<ZEBAN_P_BE> ZEBANFN_P_GET(int status);
        public abstract List<ZEBAN_P_BE> ZEBANFN_P_GET(int iss, string comprador);
        public abstract List<ZEBAN_P_BE> ZEBAN_P_GET(String consulta);
        public abstract int ZEBAN_P_UPDATE(ZEBAN_P_BE requisiciones);
        public abstract int ZEBAN_P_DELETE(List<ZEBAN_P_BE> requisiciones);

        #endregion

        #region ZEBAN
        public abstract int ZEBAN_INSERT(List<ZEBAN_BE> requisiciones);
        public abstract List<ZEBAN_BE> ZEBAN_GET();
        public abstract List<ZEBAN_BE> ZEBAN_GET(int banfn);
        public abstract List<ZEBAN_BE> ZEBAN_GET(String consulta);
        public abstract int ZEBAN_UPDATE(List<ZEBAN_BE> requisiciones);
        public abstract int ZEBAN_DELETE(List<ZEBAN_BE> requisiciones);

        #endregion

        #region ZEBAN_H
        public abstract int ZEBAN_H_INSERT(ZEBAN_H_BE historial);
        public abstract ZEBAN_H_BE ZEBANFN_H_GET(string banfn);
        public abstract List<ZEBAN_H_BE> ZEBANFN_H_GET(int banfn);
        #endregion

        #region ZEBAN_D
        public abstract int ZEBAN_D_INSERT(ZEBAN_D_BE documento);
        public abstract List<ZEBAN_D_BE> ZEBANFN_D_GET(int banfn);
        public abstract List<ZEBAN_D_BE> ZEBANFN_D_GET(int banfn, int id);
        public abstract int ZEBAN_D_DELETE(ZEBAN_D_BE documentos);

        #endregion

        #region ZEBAN_I
        public abstract int ZEBAN_I_INSERT(List<ZEBAN_I_BE> requisiciones);
        public abstract List<ZEBAN_I_BE> ZEBAN_I_GET();
        public abstract List<ZEBAN_I_BE> ZEBAN_I_GET(int banfn, string bnfpo);
        public abstract List<ZEBAN_I_BE> ZEBAN_I_GET(String consulta);
        public abstract int ZEBAN_I_UPDATE(List<ZEBAN_I_BE> requisiciones);
        public abstract int ZEBAN_I_DELETE(List<ZEBAN_I_BE> requisiciones);

        #endregion


        #region ZEBAN_S
        public abstract int ZEBAN_S_INSERT(List<ZEBAN_S_BE> requisiciones);
        public abstract List<ZEBAN_S_BE> ZEBAN_S_GET();
        public abstract List<ZEBAN_S_BE> ZEBAN_S_GET(int banfn, string bnfpo);
        public abstract List<ZEBAN_S_BE> ZEBAN_S_GET(String consulta);
        public abstract int ZEBAN_S_UPDATE(List<ZEBAN_S_BE> requisiciones);
        public abstract int ZEBAN_S_DELETE(List<ZEBAN_S_BE> requisiciones);

        #endregion


        #region ZEBAN_C
        public abstract int ZEBAN_C_INSERT(ZEBAN_C_BE comentario);
        public abstract List<ZEBAN_C_BE> ZEBANFN_C_GET(int banfn);
        public abstract List<ZEBAN_C_BE> ZEBANFN_C_GET(int banfn, int id);
        public abstract int ZEBAN_C_DELETE(ZEBAN_C_BE comentario);

        #endregion

        #region USUARIO
        public abstract List<USUARIO_BE> USUARIO_GET();
        public abstract List<USUARIO_BE> USUARIO_GET(string nipLogin);
        public abstract List<USUARIO_BE> USUARIO_GET(int tipo);
        public abstract List<USUARIO_BE> USUARIO_GET(string t , string area);

        #endregion

        #region ZACCXMATG

        public abstract List<ZACCXMATG_BE> ZACCMATGR_GET(string GL_ACCOUNT);
        #endregion
    }
}
