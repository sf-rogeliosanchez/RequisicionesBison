using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPersistence
{
   public static class Constants
   {
       #region Constantes Store Procedure

       public const string CSP_USUARIO = "CSP_USUARIO";
       public const string CSP_ZEBAN = "CSP_ZEBAN";
       public const string CSP_ZEBAN_P = "CSP_ZEBAN_P";
       public const string CSP_ZEBAN_H = "CSP_ZEBAN_H";
       public const string CSP_ZEBAN_D = "CSP_ZEBAN_D";
       public const string CSP_ZEBAN_I = "CSP_ZEBAN_I";
       public const string CSP_ZEBAN_S = "CSP_ZEBAN_S";
       public const string CSP_ZEBAN_C = "CSP_ZEBAN_C";
       public const string CSP_ZACCXMATG = "CSP_ZACCXMATG";
       
       #endregion

       #region Enumeracion de las Acciones en los Store Procedure
            

       public enum AccionesTabla
       { 
           NONE = 0,
           ADD = 1,
           GET = 2,
           GET_B = 22,
           GET_C = 23,
           UPDATE = 3,
           DELETE = 4
       }

       public enum AccionesUsuario
       { 
          NONE = 0,
          GET_ROL = 1
       }
       public enum AccionesZEBAN
       { 
          GET_USER_HISTORICAL = 21
       }
       public enum AccionesZVARIABLES
       { 
          GET_USER_VARIABLES = 21
       }
       #endregion
   }
}
