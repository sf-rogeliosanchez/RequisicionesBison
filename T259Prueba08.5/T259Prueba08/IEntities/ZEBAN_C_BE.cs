using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZEBAN_C_BE
    {
        private int _ID = 0;
        private int _BANFN = 0;
        private string _FK_USUARIO = string.Empty;
        private DateTime _FECHA = DateTime.Now;
        private string _TEXTO;
        
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
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
        public DateTime FECHA
        {
            get{ return _FECHA;}
            set{_FECHA = value;}
        }
        public string TEXTO
        {
            get { return _TEXTO; }
            set { _TEXTO = value; }
        }
    }
}
