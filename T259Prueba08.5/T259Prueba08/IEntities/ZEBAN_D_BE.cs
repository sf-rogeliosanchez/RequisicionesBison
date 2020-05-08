using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZEBAN_D_BE
    {
        private int _ID = 0;
        private int _BANFN = 0;
        private string _NOMBRE = string.Empty;
        private string _EXT = string.Empty;
        private byte[] _INFO;
        
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
        public string NOMBRE
        {
            get { return _NOMBRE; }
            set { _NOMBRE = value; }
        }
        public string EXT
        {
            get{ return _EXT;}
            set{_EXT = value;}
        }
        public byte[] INFO
        {
            get { return _INFO; }
            set { _INFO = value; }
        }
    }
}
