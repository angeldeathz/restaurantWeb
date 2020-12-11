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
            if(!IsPostBack)
            {
                _usuarioService = new UsuarioService(string.Empty);
                var token = _usuarioService.AutenticarCliente();
                if (token == null)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorAutenticar", "Swal.fire('Error', 'Ocurrió un error inesperado. Solicite atención del personal.', 'error');", true);
                    return;
                }
                Session["token"] = token;

                DateTime fecha = DateTime.Now;
                txtFecha.Text = fecha.ToString("yyyy-MM-dd");
                txtFecha.Attributes["min"] = fecha.ToString("yyyy-MM-dd");
                cargarHoras(fecha);
            }
        }

        protected void btnCrearReserva_Click(object sender, EventArgs e)
        {
            Page.Validate();
            if (!Page.IsValid)
            {
                return;
            }
            Token token = (Token)Session["token"];

            int idCliente = 0;
            try
            {
                 idCliente = crearCliente();
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }

            if (idCliente == 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorCliente", "Swal.fire('Error al crear cliente', 'Inténtelo nuevamente', 'error');", true);
                return;
            }

            int idReserva = 0;
            DateTime fechaReserva;
            try
            {
                fechaReserva = Convert.ToDateTime(txtFecha.Text);
                int hora = int.Parse(ddlHora.Text);
                int minuto = int.Parse(ddlMinuto.Text);
                TimeSpan tsHora = new TimeSpan(hora, minuto, 0);
                fechaReserva = fechaReserva.Date + tsHora;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', 'Debe seleccionar una fecha válida', 'error');", true);
                return;
            }
            int cantidadComensales = int.Parse(txtComensales.Text);
            try
            {
                _mesaService = new MesaService(token.access_token);
                List<Mesa> mesas = _mesaService.Obtener();

                int maximoComensales = mesas.Max(x => x.CantidadComensales);
                if (cantidadComensales > maximoComensales)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorComensales", "Swal.fire('El número máximo de comensales por reserva es " + maximoComensales + "', 'Para agregar más comensales puede crear otra reserva', 'warning');", true);
                    return;
                }

                int idMesa = buscarMesa(fechaReserva, cantidadComensales);
                if (idMesa == 0)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "errorMesa", "Swal.fire('No se encontró una mesa disponible. Seleccione otra fecha u hora', '', 'error');", true);
                    return;
                }
                idReserva = guardarReserva(idCliente, idMesa, fechaReserva);
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }

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
                if(reservas == null || reservas.Count == 0)
                {
                    idMesa = mesa.Id;
                    break;
                }
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

        protected int guardarReserva(int idCliente, int idMesa, DateTime fecha)
        {
            Reserva reserva = new Reserva();
            reserva.FechaReserva = fecha;
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
            DateTime fecha = Convert.ToDateTime(txtFecha.Text);
            cargarHoras(fecha);
        }

        protected void cargarHoras(DateTime fecha)
        {
            int diaSemana = (int)fecha.DayOfWeek;
            Token token = (Token)Session["token"];
            _horarioReservaService = new HorarioReservaService(token.access_token);
            List<HorarioReserva> listaHorarioReserva = _horarioReservaService.Obtener();
            HorarioReserva horarioReserva = listaHorarioReserva.FirstOrDefault(x => x.DiaSemana == diaSemana);

            if (horarioReserva == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "Swal.fire('No hay horarios disponibles para reservar en la fecha seleccionada', '', 'error');", true);
                return;
            }
            int horaInicioHora = int.Parse(horarioReserva.HoraInicioTime.Substring(0, 2));
            int horaFinHora = int.Parse(horarioReserva.HoraFinTime.Substring(0, 2));

            List<string> horas = new List<string>();
            DateTime hoy = DateTime.Now;
            for (int i = horaInicioHora; i <= horaFinHora; i++)
            {
                string x = i < 10 ? "0" + i.ToString() : i.ToString();
                if(fecha.Date == hoy.Date)
                {
                    if(int.Parse(x) <= int.Parse(hoy.ToString("HH")))
                    {
                        continue;
                    }
                }
                horas.Add(x);
            }
            ddlHora.DataSource = horas;
            ddlHora.DataBind();
            if (horas.Count == 0 && fecha.Date == hoy.Date) //Cargar el día siguiente por defecto
            {
                DateTime diaSiguiente = fecha.AddDays(1);
                txtFecha.Text = diaSiguiente.ToString("yyyy-MM-dd");
                txtFecha.Attributes["min"] = diaSiguiente.ToString("yyyy-MM-dd");
                cargarHoras(diaSiguiente);
            }
            int horaInicioMinuto = int.Parse(horarioReserva.HoraInicioTime.Substring(3, 2));
            int horaFinMinuto = int.Parse(horarioReserva.HoraFinTime.Substring(3, 2));
            List<string> minutos = new List<string>();
            for (int i = 0; i < 60; i++)
            {
                string x = i < 10 ? "0" + i.ToString() : i.ToString();
                minutos.Add(x);
            }
            ddlMinuto.DataSource = minutos;
            ddlMinuto.DataBind();
        }        
    }
}