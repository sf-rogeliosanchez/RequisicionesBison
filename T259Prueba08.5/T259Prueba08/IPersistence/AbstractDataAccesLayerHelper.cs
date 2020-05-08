using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPersistence
{
    public class AbstractDataAccesLayerHelper
    {
        public static AbstractDataAccessLayer GetDataAccessLayer()
        {
            Type trp = Type.GetType(Globales.DataLayerType, true);
            if (trp.BaseType != Type.GetType(Globales.DataLayerBaseType))
                throw new Exception("La capa de datos especificado no hereda de AbstractDataAccessLayer!!!");
            AbstractDataAccessLayer dc = (AbstractDataAccessLayer)Activator.CreateInstance(trp);
            return (dc);
        }
    }
}

