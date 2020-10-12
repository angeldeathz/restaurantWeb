using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Restaurant.Model.Clases;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class Inicio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario.IdTipoUsuario != TipoUsuario.administrador)
                {
                    linkUsuarios.Attributes.Add("class", "list-group-item d-none");
                    linkReporteria.Attributes.Add("class", "list-group-item d-none");

                    if (usuario.IdTipoUsuario != TipoUsuario.bodega)
                    {
                        linkBodega.Attributes.Add("class", "list-group-item d-none");
                    }

                    if (!new int[] { TipoUsuario.cocina, TipoUsuario.garzon }.Contains(usuario.IdTipoUsuario))
                    {
                        linkCocina.Attributes.Add("class", "list-group-item d-none");
                        linkRestaurante.Attributes.Add("class", "list-group-item d-none");
                    }

                    if (usuario.IdTipoUsuario != TipoUsuario.garzon)
                    {
                        linkRestaurante.Attributes.Add("class", "list-group-item d-none");
                    }
                }
                
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