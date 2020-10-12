using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Master
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Usuario"] != null && Session["token"] != null)
                {
                    btnCerrarSesion.Visible = true;
                }
                else
                {
                    btnCerrarSesion.Visible = false;
                }
                Usuario usuario = (Usuario)Session["usuario"];
                if(usuario.IdTipoUsuario != TipoUsuario.administrador)
                {
                    linkUsuarios.Attributes.Add("class", "nav-link d-none");
                    linkReporteria.Attributes.Add("class", "nav-link d-none");

                    if (usuario.IdTipoUsuario != TipoUsuario.bodega)
                    {
                        linkBodega.Attributes.Add("class", "nav-link d-none");
                    }

                    if (![TipoUsuario.cocina, TipoUsuario.garzon].Contains(usuario.IdTipoUsuario))
                    {
                        linkCocina.Attributes.Add("class", "nav-link d-none");
                        linkRestaurante.Attributes.Add("class", "nav-link d-none");
                    }

                    if (usuario.IdTipoUsuario != TipoUsuario.garzon)
                    {
                        linkRestaurante.Attributes.Add("class", "nav-link d-none");
                    }
                }
            }
        }

        protected void btnCerrarSesion_OnServerClick(object sender, EventArgs e)
        {
            btnCerrarSesion.Visible = false;
            Session["usuario"] = null;
            Session["token"] = null;
            Response.Redirect("/Paginas/Inicio.aspx");
        }

        protected void navLinkInicio_OnServerClick(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null && Session["token"] != null)
            {
                Response.Redirect("/Paginas/Mantenedores/Inicio.aspx");
            }
            else
            {
                Response.Redirect("/Paginas/Inicio.aspx");
            }
        }
    }
}