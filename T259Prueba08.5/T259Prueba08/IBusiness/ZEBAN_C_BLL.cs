using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZEBAN_C_BLL
    {
        public static int INSERT(ZEBAN_C_BE comentario)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_C_INSERT(comentario);
        }
        public static List<ZEBAN_C_BE> GET(int banfn)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_C_GET(banfn);
        }
        public static List<ZEBAN_C_BE> GET(int banfn, int id)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_C_GET(banfn,id);
        }
        public static int DELETE(ZEBAN_C_BE comentario)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_C_DELETE(comentario);
        }
    }
}
