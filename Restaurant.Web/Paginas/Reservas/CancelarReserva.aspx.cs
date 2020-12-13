using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Reservas
{
    public partial class CancelarReserva : System.Web.UI.Page
    {
        private ReservaService _reservaService;
        private ClienteService _clienteService;
        private UsuarioService _usuarioService;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["reservaCancelada"] = null;
        }

        protected void btnCancelarReserva_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }

            try
            {
                _usuarioService = new UsuarioService(string.Empty);
                var token = _usuarioService.AutenticarCliente();
                if (token == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorAutenticar", "Swal.fire('Error', 'Ocurrió un error inesperado. Solicite atención del personal', 'error');", true);
                    return;
                }
                Session["token"] = token;

                string email = txtEmail.Text;
                int idReserva = Convert.ToInt32(txtNumeroReserva.Text);
            
                _clienteService = new ClienteService(token.access_token);
                Cliente cliente = _clienteService.ObtenerPorMail(email);
                _reservaService = new ReservaService(token.access_token);
                List<Reserva> reservas = _reservaService.Obtener();
                if (cliente == null || reservas == null || reservas.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorCliente", "Swal.fire('No se encontró ninguna reserva asociada al e-mail ingresado', '', 'error');", true);
                    return;
                }
           
                Reserva reservaCliente = reservas.FirstOrDefault(x => x.IdCliente == cliente.Id
                                                                && x.Id == idReserva);
                if(reservaCliente == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorReserva", "Swal.fire('No se encontró la reserva con el Número ingresado', '', 'error');", true);
                    return;
                }
                EstadoReserva ultimoEstado = reservaCliente.EstadosReserva.OrderByDescending(x => x.Fecha).FirstOrDefault();
                if (ultimoEstado != null && ultimoEstado.Id == EstadoReserva.finalizada)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorCancelada", "Swal.fire('Esta reserva ya fue finalizada', '', 'warning');", true);
                    return;
                }
                if (ultimoEstado != null && ultimoEstado.Id == EstadoReserva.cancelada)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorCancelada", "Swal.fire('Esta reserva ya fue cancelada', '', 'warning');", true);
                    return;
                }
                ReservaCambioEstado reservaCambioEstado = new ReservaCambioEstado();
                reservaCambioEstado.IdReserva = reservaCliente.Id;
                reservaCambioEstado.IdEstadoReserva = EstadoReserva.cancelada;
                bool editar = _reservaService.ModificarEstado(reservaCambioEstado);
                if (!editar)
                {
                    Response.Redirect("/Paginas/Reservas/MensajeCancelarReservaError.aspx");
                    return;
                }

                Session["reservaCancelada"] = reservaCliente;
                Response.Redirect("/Paginas/Reservas/MensajeCancelarReservaExito.aspx");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }
    }
}