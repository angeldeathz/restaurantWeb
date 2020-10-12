using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Restaurant.Model.Clases;

namespace Restaurant.Web.Paginas.Administrador
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
            else
            {
                ObtenerDatosUsuario();
            }
        }

        private void ObtenerDatosUsuario()
        {
            var usuario = (Usuario)Session["usuario"];
            lblNombres.Text = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";
            lblPerfil.Text = usuario.TipoUsuario.Nombre;
        }
    }
}