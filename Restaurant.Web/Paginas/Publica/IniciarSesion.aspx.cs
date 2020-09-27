using System;
using System.Web.UI;
using Restaurant.Services.Servicios;

namespace Restaurant.Web.Paginas.Publica
{
    public partial class IniciarSesion : Page
    {
        private readonly UsuarioService _usuarioService;

        public IniciarSesion()
        {
            _usuarioService = new UsuarioService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIniciarSesion_OnClick(object sender, EventArgs e)
        {
            var token = _usuarioService.Autenticar(txtRut.Text, txtContrasena.Text);
            if (token != null)
            {
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