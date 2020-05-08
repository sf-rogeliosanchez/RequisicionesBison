using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZEBAN_P_BLL
    {
        public static int INSERT(ZEBAN_P_BE requisicion)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_P_INSERT(requisicion);
        }
        public static List<ZEBAN_P_BE> GET()
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_P_GET();
        }
        public static List<ZEBAN_P_BE> GET(string banfn)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_P_GET(banfn);
        }
        public static List<ZEBAN_P_BE> GET(string banfn, string aprobo)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_P_GET(banfn,aprobo);
        }
        public static List<ZEBAN_P_BE> GET(int status)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_P_GET(status);
        }
        public static List<ZEBAN_P_BE> GET(int iss, string comprador)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_P_GET(iss, comprador);
        }
        public static int UPDATE(ZEBAN_P_BE requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_P_UPDATE(requisiciones);
        }
        public static int DELETE(List<ZEBAN_P_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_P_DELETE(requisiciones);
        }
        public static List<ZEBAN_P_BE> GET(USUARIO_BE usuario)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_P_GET(usuario);
        }
    }
}
