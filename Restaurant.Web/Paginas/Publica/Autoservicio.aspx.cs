using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
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
            //Ingresar al cliente
            string email = txtEmail.Text;
            _usuarioService = new UsuarioService(string.Empty);
            var token = _usuarioService.AutenticarCliente();
            if (token == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorAutenticar", "alert('Ocurrió un error inesperado. Solicite atención del personal.');", true);
                return;
            }

            //Setear y limpiar sesión
            Session["token"] = token;
            Session["pedidoCliente"] = null;
            Session["articulosPedidoCliente"] = null;

            //Buscar info de la reserva
            _reservaService = new ReservaService(token.access_token);
            List<Reserva> reservas = _reservaService.Obtener();
            if (reservas == null || reservas.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorPedido", "alert('Debe registrarse en la entrada');", true);
                return;
            }
            List<int> idsEstadosReservas = new List<int>() {EstadoReserva.cancelada};

            Reserva reservaCliente = reservas.FirstOrDefault(x => x.Cliente.Persona.Email == email
                                                             && x.FechaReserva.Date == DateTime.Now.Date
                                                             && x.EstadosReserva.Any(y => !idsEstadosReservas.Contains(y.Id))
                                                             && x.EstadosReserva.Any(y => y.Id == EstadoReserva.enCurso));
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