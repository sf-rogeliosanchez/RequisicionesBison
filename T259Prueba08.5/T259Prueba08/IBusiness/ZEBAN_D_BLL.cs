using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZEBAN_D_BLL
    {
        public static int INSERT(ZEBAN_D_BE documento)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_D_INSERT(documento);
        }
        public static List<ZEBAN_D_BE> GET(int banfn)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_D_GET(banfn);
        }
        public static List<ZEBAN_D_BE> GET(int banfn, int id)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_D_GET(banfn,id);
        }
        public static int DELETE(ZEBAN_D_BE documento)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_D_DELETE(documento);
        }
    }
}
