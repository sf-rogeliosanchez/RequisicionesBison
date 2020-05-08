using IBusiness;
using IEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace T259Prueba08
{
    public partial class Login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnInicio_Click(object sender, EventArgs e)
        {
            String csname1 = "PopupScript";
            Type cstype = this.GetType();

            ClientScriptManager cs = Page.ClientScript;

            if (USUARIO_BLL.GET(txtUser.Text.Trim()).Count > 0)
            {
                USUARIO_BE userS = USUARIO_BLL.GET(txtUser.Text.ToUpper())[0];
                string pass = txtPass.Text;
                if (pass.Equals(userS.UserKey))
                {
                    HttpCookie cookie = new HttpCookie("UserName", txtUser.Text.ToUpper());
                    Session["UserName"] = txtUser.Text.ToUpper();

                    cookie.Expires = DateTime.Now.AddMinutes(600);
                    Response.Cookies.Add(cookie);

                    string user = Request.Cookies.Get("UserName").Value;


                    FormsAuthentication.RedirectFromLoginPage(txtUser.Text.ToUpper(), false);

                    //string urlDestino = "Historial.aspx";
                    //Response.Redirect(urlDestino, false);
                }
                else
                {
                    if (!cs.IsStartupScriptRegistered(cstype, csname1))
                    {
                        String cstext1 = "alert('Usuario y/o contraseña incorrecta.');";
                        cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                    }
                }
            }
            else
            {
                if (!cs.IsStartupScriptRegistered(cstype, csname1))
                {
                    String cstext1 = "alert('Usuario y/o contraseña incorrecta.');";
                    cs.RegisterStartupScript(cstype, csname1, cstext1, true);
                }
            }
        }
    }
}