using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZEBAN_I_BLL
    {
        public static int INSERT(List<ZEBAN_I_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_I_INSERT(requisiciones);
        }
        public static List<ZEBAN_I_BE> GET()
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_I_GET();
        }
        public static List<ZEBAN_I_BE> GET(int banfn, string bnfpo)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_I_GET(banfn,bnfpo);
        }
        public static int UPDATE(List<ZEBAN_I_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_I_UPDATE(requisiciones);
        }
        public static int DELETE(List<ZEBAN_I_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_I_DELETE(requisiciones);
        }
    }
}
