using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZEBAN_BLL
    {
        public static int INSERT(List<ZEBAN_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_INSERT(requisiciones);
        }
        public static List<ZEBAN_BE> GET()
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_GET();
        }
        public static List<ZEBAN_BE> GET(int banfn)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_GET(banfn);
        }
        public static int UPDATE(List<ZEBAN_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_UPDATE(requisiciones);
        }
        public static int DELETE(List<ZEBAN_BE> requisiciones)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_DELETE(requisiciones);
        }
    }
}
