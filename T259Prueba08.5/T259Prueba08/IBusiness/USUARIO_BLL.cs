using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class USUARIO_BLL
    {
        public static List<USUARIO_BE> GET()
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().USUARIO_GET();
        }
        public static List<USUARIO_BE> GET(string user)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().USUARIO_GET(user);
        }
        public static List<USUARIO_BE> GET(int tipo)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().USUARIO_GET(tipo);
        }
        public static List<USUARIO_BE> GET(string t, string area)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().USUARIO_GET("", area);
        }
       
    }
}
