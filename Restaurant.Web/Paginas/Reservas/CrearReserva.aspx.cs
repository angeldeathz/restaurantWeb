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
    public partial class CrearReserva : System.Web.UI.Page
    {
        private ReservaService _reservaService;
        private HorarioReservaService _horarioReservaService;
        private ClienteService _clienteService;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCrearReserva_Click(object sender, EventArgs e)
        {
            int idCliente = crearCliente();
            if (idCliente == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorCliente", "alert('Error al crear cliente');", true);
            }

            Reserva reserva = new Reserva();
            reserva.FechaReserva = Convert.ToDateTime(txtFecha.Text);
            reserva.CantidadComensales = int.Parse(txtComensales.Text);
            reserva.IdEstadoReserva = EstadoReserva.creada;

            reserva.IdCliente = int.Parse(ddlClienteReserva.SelectedValue);
            reserva.IdMesa = int.Parse(ddlMesaReserva.SelectedValue);

            Token token = (Token)Session["token"];
            _reservaService = new ReservaService(token.access_token);
            int idReserva = _reservaService.Guardar(reserva);
            if (idReserva != 0)
            {
                List<Reserva> reservas = _reservaService.Obtener();
                if (reservas != null && reservas.Count > 0)
                {
                    actualizarRepeater(listaReservas, reservas, listaReservasVacia);
                    upListaReservas.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearReserva", "alert('Reserva creada');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "alert('Error al crear reserva');", true);
            }
        }

        public int crearCliente()
        {
            Cliente cliente = new Cliente();
            Persona persona = new Persona();

            persona.Nombre = txtNombre.Text;
            persona.Apellido = txtApellido.Text;
            //persona.Rut = int.Parse(txtRutCliente.Text);
            //persona.DigitoVerificador = txtDigitoVerificadorCliente.Text;
            persona.Email = txtEmail.Text;
            //persona.Telefono = int.Parse(txtTelefonoCliente.Text);
            persona.EsPersonaNatural = Convert.ToChar(1);
            cliente.Persona = persona;

            Token token = (Token)Session["token"];
            _clienteService = new ClienteService(token.access_token);
            int idCliente = _clienteService.Guardar(cliente);
            return idCliente;
        }
        protected void txtFecha_TextChanged(object sender, EventArgs e)
        {
            DateTime fecha = Convert.ToDateTime(txtFecha.Text).Date;
            List<int> horasDisponibilidad = getHorasDisponibles(fecha);
            if (horasDisponibilidad.Count == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "alert('No hay horarios disponibles para reservar');", true);
                return;
            }
            ddlHora.DataSource = horasDisponibilidad;
            ddlHora.DataBind();
        }

        public List<int> getHorasDisponibles(DateTime fecha)
        {
            int diaSemana = (int)fecha.DayOfWeek;
            _horarioReservaService = new HorarioReservaService(token.access_token);
            List<HorarioReserva> horarioReserva = _horarioReservaService.Obtener();
            if (horarioReserva == null || horarioReserva.Count == 0)
            {
                return new List<int>();
            }
            HorarioReserva horarioDia = horarioReserva.FirstOrDefault(x => x.DiaSemana == diaSemana);
            int maximoComensales = horarioDia.MaximoComensales;
            int horaInicio = Convert.ToInt32(horarioDia.HoraInicio.Substring(0, 2));
            int horaFin = Convert.ToInt32(horarioDia.HoraCierre.Substring(0, 2));
            List<int> horasDisponibles = new List<int>();
            for (int i = horaInicio; i < horaFin; i++)
            {
                horasDisponibles.Add(i);
            }
              
            List<Reserva> reservas = _reservaService.Obtener();
            if (reservas != null && reservas.Count > 0)
            {
                foreach (int hora in horasDisponibles)
                {
                    List<Reserva> reservasHora = (List<Reserva>)reservas.Where(x => x.FechaReserva.Date == fecha
                                                                              && x.FechaReserva.ToString("HH") == hora.ToString());
                    var comensalesHora = reservasHora.Sum(x => x.CantidadComensales);
                    if(comensalesHora >= maximoComensales)
                    {
                        horasDisponibles.Remove(hora);
                    }
                }
            }
            return horasDisponibles;
        }
    }
}