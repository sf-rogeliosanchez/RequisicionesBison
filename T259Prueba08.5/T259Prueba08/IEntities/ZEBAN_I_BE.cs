using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZEBAN_I_BE
    {
        private int _BANFN = 0;
        private string _BNFPO = string.Empty;
        private int _SERIAL = 0;
        private int _LINE = 0;
        private string _GL_ACCOUNT = string.Empty;
        private string _COSTCENTER = string.Empty;
        private string _ASSET_NUM = string.Empty;
        private decimal _MENGE = 0; 
        //private string _MEINS = string.Empty;
        //private decimal _PREIS = 0;
        //private string _SERVICIO = string.Empty;
        //private string _TEXTO = string.Empty;


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
        public int SERIAL
        {
            get { return _SERIAL; }
            set { _SERIAL = value; }
        }
        public int LINE
        {
            get { return _LINE; }
            set { _LINE = value; }
        }
        public string GL_ACCOUNT
        {
            get { return _GL_ACCOUNT; }
            set { _GL_ACCOUNT = value; }
        }
        public string COSTCENTER
        {
            get { return _COSTCENTER; }
            set { _COSTCENTER = value; }
        }
        public string ASSET_NUM
        {
            get { return _ASSET_NUM; }
            set { _ASSET_NUM = value; }
        }
        public decimal MENGE
        {
            get { return _MENGE; }
            set { _MENGE = value; }
        }
        //public string MEINS
        //{
        //    get { return _MEINS; }
        //    set { _MEINS = value; }
        //}
        //public decimal PREIS
        //{
        //    get { return _PREIS; }
        //    set { _PREIS = value; }
        //}

        //public string SERVICIO
        //{
        //    get { return _SERVICIO; }
        //    set { _SERVICIO = value; }
        //}
        //public string TEXTO
        //{
        //    get { return _TEXTO; }
        //    set { _TEXTO = value; }
        //}
    }
}
