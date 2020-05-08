using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace IPersistence
{
    class Globales
    {
        public static string DataLayerType
        {
            get
            {
                string str = ConfigurationManager.AppSettings["DataLayerType"];
                if (str == null || str == String.Empty)
                    throw (new ApplicationException("No se encuentra la llave DataLayerType en el archivo web.config. Debería contener  <appSettings><add key=\"DataLayerType\" value=\"data layer type\" /></appSettings> "));
                else
                    return (str);
            }
        }

        public static string DataLayerBaseType
        {
            get
            {
                string str = ConfigurationManager.AppSettings["DataLayerBaseType"];
                if (str == null || str == String.Empty)
                    throw (new ApplicationException("No se encuentra la llave DataLayerBaseType en el archivo web.config. Debería contener  <appSettings><add key=\"DataLayerType\" value=\"data layer type\" /></appSettings> "));
                else
                    return (str);
            }

        }
    }
}

