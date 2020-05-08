using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZEBAN_H_BE
    {
        private string _BANFN = string.Empty;
        private string _NIPLOGIN = string.Empty;
        private DateTime _FECHA	= DateTime.Now;

        public string BANFN
        {
            get { return _BANFN; }
            set { _BANFN = value; }
        }
        public string NIPLOGIN
        {
            get { return _NIPLOGIN; }
            set { _NIPLOGIN = value; }
        }
        public DateTime FECHA
        {
            get { return _FECHA; }
            set { _FECHA = value; }
        }
    }
}
