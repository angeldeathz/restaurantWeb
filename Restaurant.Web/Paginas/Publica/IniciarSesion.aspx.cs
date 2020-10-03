using System;
using System.Web.UI;
using Restaurant.Services.Servicios;

namespace Restaurant.Web.Paginas.Publica
{
    public partial class IniciarSesion : Page
    {
        private UsuarioService _usuarioService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] != null & Session["token"] != null)
            {
                Response.Redirect("../Administrador/Inicio.aspx");
            }
        }

        protected void btnIniciarSesion_OnClick(object sender, EventArgs e)
        {
            _usuarioService = new UsuarioService(string.Empty);
            var token = _usuarioService.Autenticar(txtRut.Text, txtContrasena.Text);
            if (token != null)
            {
                _usuarioService = new UsuarioService(token.access_token);
                Session["token"] = token;
                var usuario = _usuarioService.ObtenerPorRut(txtRut.Text);

                if (usuario != null)
                {
                    Session["usuario"] = usuario;
                    Response.Redirect("../Administrador/inicio");
                }
            }
        }
    }
}