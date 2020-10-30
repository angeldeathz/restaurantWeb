using Restaurant.Model.Clases;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Publica
{
    public partial class Autoservicio : System.Web.UI.Page
    {
        private ReservaService _reservaService;
        private UsuarioService _usuarioService;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAutoservicio_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;
            _usuarioService = new UsuarioService(string.Empty);
            var token = _usuarioService.AutenticarCliente();
            if (token == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorAutenticar", "alert('Ocurrió un error inesperado. Solicite atención del personal.');", true);
                return;
            }
            Session["token"] = token;

            _reservaService = new ReservaService(token.access_token);
            List<Reserva> reservas = _reservaService.Obtener();
            if (reservas == null || reservas.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorPedido", "alert('Debe registrarse en la entrada');", true);
                return;
            }

            Reserva reservaCliente = reservas.FirstOrDefault(x => x.IdEstadoReserva == EstadoReserva.enCurso
                                                             && x.Cliente.Persona.Email == email);
            if (reservaCliente == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorPedido", "alert('Debe registrarse en la entrada');", true);
                return;
            }
            Session["reservaCliente"] = reservaCliente;
            Response.Redirect("/Paginas/Autoservicio/GestionAutoservicio.aspx");
        }
    }
}