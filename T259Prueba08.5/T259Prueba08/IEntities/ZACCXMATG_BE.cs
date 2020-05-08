using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class ZACCXMATG_BE
    {
        private string _GL_ACCOUNT = string.Empty;
        private string _MAT_GR = string.Empty;


        public string GL_ACCOUNT
        {
            get { return _GL_ACCOUNT; }
            set { _GL_ACCOUNT = value; }
        }
        public string MAT_GR
        {
            get { return _MAT_GR; }
            set { _MAT_GR = value; }
        }
       
    }
}
