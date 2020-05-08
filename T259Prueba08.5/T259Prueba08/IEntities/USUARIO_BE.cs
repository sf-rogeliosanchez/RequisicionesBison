using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IEntities
{
    [Serializable]
    public class USUARIO_BE
    {
        private string _NipLogin = string.Empty;
        private string _User_Type = string.Empty;
        private string _User_Key = string.Empty;
        private string _email = string.Empty;
        private string _IdEmp = string.Empty;
        private bool _USUA = false;
        private bool _CONTA = false;
        private bool _GEREN = false;
        private bool _DIREC = false;
        private string _AREA = string.Empty;

        public string NipLogin
        {
            get { return _NipLogin; }
            set { _NipLogin = value; }
        }
        public string UserType
        {
            get { return _User_Type; }
            set { _User_Type= value; }
        }
        public string UserKey
        {
            get { return _User_Key; }
            set { _User_Key= value; }
        }
        public string eMail
        {
            get { return _email; }
            set { _email = value; }
        }
        public string IdEmp
        {
            get { return _IdEmp; }
            set { _IdEmp = value; }
        }
        public bool USUA
        {
            get { return _USUA; }
            set { _USUA = value; }
        }
        public bool CONTA
        {
            get { return _CONTA; }
            set { _CONTA = value; }
        }
        public bool GEREN
        {
            get { return _GEREN; }
            set { _GEREN = value; }
        }
        public bool DIREC
        {
            get { return _DIREC; }
            set { _DIREC = value; }
        }
        public string AREA
        {
            get { return _AREA; }
            set { _AREA = value; }
        }
    }
}
