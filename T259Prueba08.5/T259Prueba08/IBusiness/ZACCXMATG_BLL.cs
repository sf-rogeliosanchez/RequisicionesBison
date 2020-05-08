using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IEntities;
using IPersistence;
using System.Configuration;

namespace IBusiness
{
    public class ZACCXMATG_BLL
    {
      public static List<ZACCXMATG_BE> GET(string GL_ACCOUNT)
        {
            return AbstractDataAccesLayerHelper.GetDataAccessLayer().ZACCMATGR_GET(GL_ACCOUNT);
        }
    }
}
