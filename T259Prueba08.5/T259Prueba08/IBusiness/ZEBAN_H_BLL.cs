using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZEBAN_H_BLL
    {
        public static int INSERT(ZEBAN_H_BE requisicion)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBAN_H_INSERT(requisicion);
        }
        public static ZEBAN_H_BE GET(string banfn)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_H_GET(banfn);
        }
        public static List<ZEBAN_H_BE> GET(int banfn)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZEBANFN_H_GET(banfn);
        }
    }
}
