using IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T259Prueba08.Models
{
    [Serializable]
    public class ESTATUS
    {
        private string _TEXTO = string.Empty;
        List<USUARIO_BE> _USUARIOS = new List<USUARIO_BE>();


        public string TEXTO
        {
            get{ return _TEXTO;}
            set{ _TEXTO = value;}
        }
        public List<USUARIO_BE> USUARIOS
        {
            get { return _USUARIOS; }
            set { _USUARIOS = value; }
        }
    }
}
