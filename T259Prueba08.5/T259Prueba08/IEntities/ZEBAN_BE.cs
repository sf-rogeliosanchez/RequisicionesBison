using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZEBAN_BE
    {
        private int _BANFN = 0;
        private string _BNFPO = string.Empty;
        private string _MATNR = string.Empty;
        private string _DESCR = string.Empty;
        private string _WERKS = string.Empty;
        private decimal _MENGE = 0;
        private string _MEINS = string.Empty;
        private decimal _PREIS = 0;
        private string _WAERS = string.Empty;
        private string _IMP = string.Empty;
        private string _PUR_GR = string.Empty;
        private DateTime _F_ENTREGA = DateTime.Now;
        private string _TIPO = string.Empty;
        private string _MAT_GR = string.Empty;


        public int BANFN
        {
            get { return _BANFN; }
            set { _BANFN = value; }
        }
        public string BNFPO
        {
            get{ return _BNFPO;}
            set{ _BNFPO = value;}
        }
        public string MATNR
        {
            get { return _MATNR; }
            set { _MATNR = value; }
        }
        public string DESCR
        {
            get { return _DESCR; }
            set { _DESCR = value; }
        }
        public string WERKS
        {
            get { return _WERKS; }
            set { _WERKS = value; }
        }
        public decimal MENGE
        {
            get { return _MENGE; }
            set { _MENGE = value; }
        }
        public string MEINS
        {
            get { return _MEINS; }
            set { _MEINS = value; }
        }
        public decimal PREIS
        {
            get { return _PREIS; }
            set { _PREIS = value; }
        }
        public string WAERS
        {
            get { return _WAERS; }
            set { _WAERS = value; }
        }

        public string IMP
        {
            get { return _IMP; }
            set { _IMP = value; }
        }
        public string PUR_GR
        {
            get { return _PUR_GR; }
            set { _PUR_GR = value; }
        }
        public DateTime F_ENTREGA
        {
            get { return _F_ENTREGA; }
            set { _F_ENTREGA = value; }
        }
        public string TIPO
        {
            get { return _TIPO; }
            set { _TIPO = value; }
        }
        public string MAT_GR
        {
            get { return _MAT_GR; }
            set { _MAT_GR = value; }
        }
    }
}
