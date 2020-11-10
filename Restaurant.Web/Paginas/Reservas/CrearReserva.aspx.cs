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
        private MesaService _mesaService;
        private UsuarioService _usuarioService;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["reservaCreada"] = null;
        }

        protected void btnCrearReserva_Click(object sender, EventArgs e)
        {
            _usuarioService = new UsuarioService(string.Empty);
            var token = _usuarioService.AutenticarCliente();
            if (token == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorAutenticar", "alert('Ocurrió un error inesperado. Solicite atención del personal.');", true);
                return;
            }
            Session["token"] = token;

            int idCliente = crearCliente();
            if (idCliente == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorCliente", "alert('Error al crear cliente');", true);
                return;
            }

            DateTime fechaReserva = Convert.ToDateTime(txtFecha.Text);
            int cantidadComensales = int.Parse(txtComensales.Text);
            int idMesa = buscarMesa(fechaReserva, cantidadComensales);
            if (idMesa == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorMesa", "alert('No se encontró una mesa disponible. Seleccione otra fecha u hora');", true);
                return;
            }

            int idReserva = guardarReserva(idCliente, idMesa);
            if (idReserva == 0)
            {
                Response.Redirect("/Paginas/Reservas/MensajeCrearReservaError.aspx");
                return;
            }
            Response.Redirect("/Paginas/Reservas/MensajeCrearReservaExito.aspx");
        }

        public int buscarMesa(DateTime fechaReserva, int cantidadComensales)
        {
            Token token = (Token)Session["token"];
            _mesaService = new MesaService(token.access_token);
            List<Mesa> mesas = _mesaService.Obtener();
            List<Mesa> mesasConCapcidad = mesas.Where(x=> x.CantidadComensales >= cantidadComensales).ToList();

            _reservaService = new ReservaService(token.access_token);
            List<Reserva> reservas = _reservaService.Obtener();
            int idMesa = 0;
            foreach(Mesa mesa in mesasConCapcidad)
            {
                DateTime horaDesde = fechaReserva.AddMinutes(-60);
                DateTime horahasta = fechaReserva.AddMinutes(60*2);

                List<Reserva> reservasMesa = reservas.Where(x => x.IdMesa == mesa.Id
                                                            && x.FechaReserva.Date == fechaReserva.Date
                                                            && x.FechaReserva >= horaDesde
                                                            && x.FechaReserva <= horahasta
                                                            ).ToList();
                if(reservasMesa.Count == 0)
                {
                    idMesa = mesa.Id;
                    break;
                }
            }
            return idMesa;
        }

        public int crearCliente()
        {
            Cliente cliente = new Cliente();
            Persona persona = new Persona();

            persona.Nombre = txtNombre.Text;
            persona.Apellido = txtApellido.Text;
            persona.Email = txtEmail.Text;
            cliente.Persona = persona;

            Token token = (Token)Session["token"];
            _clienteService = new ClienteService(token.access_token);
            Cliente clienteExiste = _clienteService.ObtenerPorMail(txtEmail.Text);
            if(clienteExiste != null)
            {
                return clienteExiste.Id;
            }
            int idCliente = _clienteService.GuardarBasico(persona);
            return idCliente;
        }

        protected int guardarReserva(int idCliente, int idMesa)
        {
            Reserva reserva = new Reserva();
            reserva.FechaReserva = Convert.ToDateTime(txtFecha.Text);
            reserva.CantidadComensales = int.Parse(txtComensales.Text);
            reserva.IdEstadoReserva = EstadoReserva.creada;
            reserva.IdCliente = idCliente;
            reserva.IdMesa = idMesa;

            Token token = (Token)Session["token"];
            _reservaService = new ReservaService(token.access_token);
            int idReserva = _reservaService.Guardar(reserva);
            reserva.Id = idReserva;
            Session["reservaCreada"] = reserva;
            return idReserva;
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
            Token token = (Token)Session["token"];
            _horarioReservaService = new HorarioReservaService(token.access_token);
            List<HorarioReserva> horarioReserva = _horarioReservaService.Obtener();
            if (horarioReserva == null || horarioReserva.Count == 0)
            {
                return new List<int>();
            }
            HorarioReserva horarioDia = horarioReserva.FirstOrDefault(x => x.DiaSemana == diaSemana);
            int maximoComensales = 10; //CONTAR COMENSALES TODAS LAS MESAS
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
                    List<Reserva> reservasHora = reservas.Where(x => x.FechaReserva.Date == fecha
                                                                && x.FechaReserva.ToString("HH") == hora.ToString()).ToList(); ;
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