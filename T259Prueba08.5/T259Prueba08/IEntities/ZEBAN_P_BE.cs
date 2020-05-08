using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZEBAN_P_BE
    {
        private int _BANFN = 0;
        private string _FK_USUARIO = string.Empty;
        private string _STATUS = string.Empty;
        private string _APROBO = string.Empty;
        private string _SAP_BANFN = string.Empty;
        private string _SAP_EBELN = string.Empty;
        private DateTime _F_APROBACION = DateTime.Now;
        private string _REFERENCIA = string.Empty;
        private string _COMPRADOR = string.Empty;

        public int BANFN
        {
            get { return _BANFN; }
            set { _BANFN = value; }
        }
        public string FK_USUARIO
        {
            get { return _FK_USUARIO; }
            set { _FK_USUARIO = value; }
        }
        public string STATUS
        {
            get{ return _STATUS;}
            set{_STATUS = value;}
        }
        public string APROBO
        {
            get { return _APROBO; }
            set { _APROBO = value; }
        }
        public string SAP_BANFN
        {
            get { return _SAP_BANFN; }
            set { _SAP_BANFN = value; }
        }
        public DateTime F_APROBACION
        {
            get { return _F_APROBACION; }
            set { _F_APROBACION = value; }
        }
        public string SAP_EBELN
        {
            get { return _SAP_EBELN; }
            set { _SAP_EBELN = value; }
        }
        public string REFERENCIA
        {
            get { return _REFERENCIA; }
            set { _REFERENCIA = value; }
        }
        public string COMPRADOR
        {
            get { return _COMPRADOR; }
            set { _COMPRADOR = value; }
        }
    }
}
