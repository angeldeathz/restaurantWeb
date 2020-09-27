using System;
using System.Web.UI;

namespace Restaurant.Web.Master
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null)
                {
                    btnIniciarSesion.Visible = false;
                    btnCerrarSesion.Visible = true;
                }
                else
                {
                    btnIniciarSesion.Visible = true;
                    btnCerrarSesion.Visible = false;
                }
            }
        }

        protected void btnCerrarSesion_OnServerClick(object sender, EventArgs e)
        {
            btnIniciarSesion.Visible = true;
            btnCerrarSesion.Visible = false;
            Session["usuario"] = null;
            Session["token"] = null;
            Response.Redirect("/Paginas/Inicio.aspx");
        }
    }
}