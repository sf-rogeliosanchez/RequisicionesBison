using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace T259Prueba08.Models
{
    [Serializable]
    public class HISTORIAL
    {
        private int _BANFN = 0;
        private bool _MODIF = false;
        private string _F_CREACION = string.Empty;
        private string _SOLICITANTE = string.Empty;
        private ESTATUS _ESTATUS = new ESTATUS();
        private string _REFERENCIA = string.Empty;
        private string _SOLICITUD = string.Empty;
        private string _ORDEN = string.Empty;
        private string _F_MODIFICA = string.Empty;
        private string _MODIFICA = string.Empty;
        private string _COMENTARIO = string.Empty;
    
        public int BANFN
        {
            get { return _BANFN; }
            set { _BANFN = value; }
        }
        public bool MODIF
        {
            get{ return _MODIF;}
            set{ _MODIF = value;}
        }
        public string F_CREACION
        {
            get { return _F_CREACION; }
            set { _F_CREACION = value; }
        }
        public string SOLICITANTE
        {
            get { return _SOLICITANTE; }
            set { _SOLICITANTE = value; }
        }
        public string SOLICITUD
        {
            get { return _SOLICITUD; }
            set { _SOLICITUD = value; }
        }
        public string ORDEN
        {
            get { return _ORDEN; }
            set { _ORDEN = value; }
        }
        public string F_MODIFICA
        {
            get { return _F_MODIFICA; }
            set { _F_MODIFICA = value; }
        }

        public string MODIFICA
        {
            get { return _MODIFICA; }
            set { _MODIFICA = value; }
        }
        public string COMENTARIO
        {
            get { return _COMENTARIO; }
            set { _COMENTARIO = value; }
        }
        public ESTATUS ESTATUS
        {
            get { return _ESTATUS; }
            set { _ESTATUS = value; }
        }

        public string REFERENCIA
        {
            get { return _REFERENCIA; }
            set { _REFERENCIA = value; }
        }
    }
}
