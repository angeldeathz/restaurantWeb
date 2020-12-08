using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class GestiónBodega : System.Web.UI.Page
    {
        private ClienteService _clienteService;
        private ReservaService _reservaService;
        private MesaService _mesaService;
        private EstadoReservaService _estadoReservaService;
        private EstadoMesaService _estadoMesaService;
        private HorarioReservaService _horarioReservaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();

                Token token = (Token)Session["token"];
                _clienteService = new ClienteService(token.access_token);
                _reservaService = new ReservaService(token.access_token);
                _mesaService = new MesaService(token.access_token);
                _estadoReservaService = new EstadoReservaService(token.access_token);
                _estadoMesaService = new EstadoMesaService(token.access_token);
                _horarioReservaService = new HorarioReservaService(token.access_token);
                List<Reserva> reservas = _reservaService.Obtener();
                if (reservas != null && reservas.Count > 0)
                {
                    var reservasOrdenadas = reservas.OrderByDescending(x => x.FechaReserva).ToList();
                    actualizarRepeater(listaReservas, reservasOrdenadas, listaReservasVacia);
                }

                List<Cliente> clientes = _clienteService.Obtener();
                if (clientes != null && clientes.Count > 0)
                {
                    var clientesOrdenados = clientes.OrderBy(x => x.Id).ToList();
                    actualizarRepeater(listaClientes, clientesOrdenados, listaClientesVacia);
                    actualizarDdlClientes(clientes);
                }

                List<Mesa> mesas = _mesaService.Obtener();
                if (mesas != null && mesas.Count > 0)
                {
                    var mesasOrdenadas = mesas.OrderBy(x => x.Id).ToList();
                    actualizarRepeater(listaMesas, mesasOrdenadas, listaMesasVacia);
                    actualizarDdlMesas(mesas);
                }

                List<EstadoReserva> estadosReserva = _estadoReservaService.Obtener();
                if (estadosReserva != null && estadosReserva.Count > 0)
                {
                    ddlEstadoReserva.DataSource = estadosReserva;
                    ddlEstadoReserva.DataTextField = "Nombre";
                    ddlEstadoReserva.DataValueField = "Id";
                    ddlEstadoReserva.DataBind();
                    ddlEstadoReserva.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlEstadoReserva.SelectedIndex = 0;
                }
                List<EstadoMesa> estadoMesa = _estadoMesaService.Obtener();
                if (estadoMesa != null && estadoMesa.Count > 0)
                {
                    ddlEstadoMesa.DataSource = estadoMesa;
                    ddlEstadoMesa.DataTextField = "Nombre";
                    ddlEstadoMesa.DataValueField = "Id";
                    ddlEstadoMesa.DataBind();
                    ddlEstadoMesa.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlEstadoMesa.SelectedIndex = 0;
                }
                List<HorarioReserva> horarioReserva = _horarioReservaService.Obtener();
                if (horarioReserva != null && horarioReserva.Count > 0)
                {
                    var horarioDomingo = horarioReserva.FirstOrDefault(x => x.DiaSemana == 0);
                    horarioReserva.RemoveAt(horarioReserva.FindIndex(x => x.DiaSemana == 0));
                    horarioReserva.Add(horarioDomingo);
                    listaHorariosReserva.DataSource = horarioReserva;
                    listaHorariosReserva.DataBind();
                }
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
            Usuario usuario = (Usuario)Session["usuario"];
            if (!new int[] { TipoUsuario.administrador, TipoUsuario.garzon }.Contains(usuario.IdTipoUsuario))
            {
                Response.Redirect("../Mantenedores/Inicio.aspx");
            }
        }
        public void limpiarTabs()
        {
            tabReservas.Attributes.Add("class", "nav-link");
            tabClientes.Attributes.Add("class", "nav-link");
            tabMesas.Attributes.Add("class", "nav-link");
            tabHorarioReservas.Attributes.Add("class", "nav-link");
            divReservas.Attributes.Add("class", "tab-pane fade");
            divClientes.Attributes.Add("class", "tab-pane fade");
            divMesas.Attributes.Add("class", "tab-pane fade");
            divHorarioReservas.Attributes.Add("class", "tab-pane fade");
        }
        protected void btnModalCrearReserva_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalReserva.Text = "Crear Reserva";
            btnCrearReserva.Visible = true;
            btnEditarReserva.Visible = false;
            txtFechaHoraReserva.Text = "";
            txtCantidadComensalesReserva.Text = "";
            ddlEstadoReserva.SelectedValue = "";
            ddlClienteReserva.SelectedValue = "";
            ddlMesaReserva.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('show');", true);
            upModalReserva.Update();

            limpiarTabs();
            tabReservas.Attributes.Add("class", "nav-link active");
            divReservas.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnModalEditarReserva_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idReserva;
            if (int.TryParse((string)e.CommandArgument, out idReserva))
            {
                Token token = (Token)Session["token"];
                _reservaService = new ReservaService(token.access_token);
                Reserva reserva = _reservaService.Obtener(idReserva);
                EstadoReserva estadoReserva = reserva.EstadosReserva.LastOrDefault();
                if (reserva != null)
                {
                    tituloModalReserva.Text = "Editar Reserva";
                    btnCrearReserva.Visible = false;
                    btnEditarReserva.Visible = true;
                    txtIdReserva.Text = reserva.Id.ToString();
                    txtFechaHoraReserva.Text = reserva.FechaReserva.ToString("yyyy-MM-ddTHH:mm");
                    txtCantidadComensalesReserva.Text = reserva.CantidadComensales.ToString();
                    ddlEstadoReserva.SelectedValue = estadoReserva.Id.ToString();
                    ddlClienteReserva.SelectedValue = reserva.IdCliente.ToString();
                    ddlMesaReserva.SelectedValue = reserva.IdMesa.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('show');", true);
                    upModalReserva.Update();
                }
            }
            limpiarTabs();
            tabReservas.Attributes.Add("class", "nav-link active");
            divReservas.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearReserva_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Reserva reserva = new Reserva();
            reserva.FechaReserva = Convert.ToDateTime(txtFechaHoraReserva.Text);
            reserva.CantidadComensales = int.Parse(txtCantidadComensalesReserva.Text);
            reserva.IdEstadoReserva = int.Parse(ddlEstadoReserva.SelectedValue);
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
                    var reservasOrdenadas = reservas.OrderByDescending(x => x.FechaReserva).ToList();
                    actualizarRepeater(listaReservas, reservasOrdenadas, listaReservasVacia);
                    upListaReservas.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearReserva", "Swal.fire('Reserva creada', '', 'success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "Swal.fire('Error al crear reserva', '', 'error');", true);
            }
        }

        protected void btnEditarReserva_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Reserva reserva = new Reserva();
            reserva.Id = int.Parse(txtIdReserva.Text);
            reserva.FechaReserva = Convert.ToDateTime(txtFechaHoraReserva.Text);
            reserva.CantidadComensales = int.Parse(txtCantidadComensalesReserva.Text);
            reserva.IdEstadoReserva = int.Parse(ddlEstadoReserva.SelectedValue);
            reserva.IdCliente = int.Parse(ddlClienteReserva.SelectedValue);
            reserva.IdMesa = int.Parse(ddlMesaReserva.SelectedValue);

            Token token = (Token)Session["token"];
            _reservaService = new ReservaService(token.access_token);
            bool editar = _reservaService.Modificar(reserva, reserva.Id);
            if (editar)
            {
                List<Reserva> reservas = _reservaService.Obtener();
                if (reservas != null && reservas.Count > 0)
                {
                    var reservasOrdenadas = reservas.OrderByDescending(x => x.FechaReserva).ToList();
                    actualizarRepeater(listaReservas, reservasOrdenadas, listaReservasVacia);
                    upListaReservas.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarReserva", "Swal.fire('Reserva editada', '', 'success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "Swal.fire('Error al editar reserva', '', 'error');", true);
            }
        }

        protected void btnModalCrearCliente_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalCliente.Text = "Crear Cliente";
            btnCrearCliente.Visible = true;
            btnEditarCliente.Visible = false;
            txtIdCliente.Text = "";
            txtNombreCliente.Text = "";
            txtApellidoCliente.Text = "";
            txtRutCliente.Text = "";
            txtDigitoVerificadorCliente.Text = "";
            txtEmailCliente.Text = "";
            txtTelefonoCliente.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal('show');", true);
            upModalCliente.Update();

            limpiarTabs();
            tabClientes.Attributes.Add("class", "nav-link active");
            divClientes.Attributes.Add("class", "tab-pane fade active show");
        }
        protected void btnModalEditarCliente_Click(object source, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idCliente;
            if (int.TryParse((string)e.CommandArgument, out idCliente))
            {
                Token token = (Token)Session["token"];
                _clienteService = new ClienteService(token.access_token);
                Cliente cliente = _clienteService.Obtener(idCliente);
                if (cliente != null)
                {
                    tituloModalCliente.Text = "Editar Cliente";
                    btnCrearCliente.Visible = false;
                    btnEditarCliente.Visible = true;
                    txtIdCliente.Text = cliente.Id.ToString();
                    txtNombreCliente.Text = cliente.Persona.Nombre;
                    txtApellidoCliente.Text = cliente.Persona.Apellido;
                    txtRutCliente.Text = cliente.Persona.Rut.ToString();
                    txtDigitoVerificadorCliente.Text = cliente.Persona.DigitoVerificador.ToString();
                    txtEmailCliente.Text = cliente.Persona.Email;
                    txtTelefonoCliente.Text = cliente.Persona.Telefono.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal('show');", true);
                    upModalCliente.Update();
                }
            }
            limpiarTabs();
            tabClientes.Attributes.Add("class", "nav-link active");
            divClientes.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearCliente_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Cliente cliente = new Cliente();
            Persona persona = new Persona();

            persona.Nombre = txtNombreCliente.Text;
            persona.Apellido = txtApellidoCliente.Text;
            persona.Rut = int.Parse(txtRutCliente.Text);
            persona.DigitoVerificador = txtDigitoVerificadorCliente.Text;
            persona.Email = txtEmailCliente.Text;
            persona.Telefono = int.Parse(txtTelefonoCliente.Text);
            persona.EsPersonaNatural = Convert.ToChar(chkEsPersonaJuridica.Checked ? 0 : 1);

            cliente.Persona = persona;
            Token token = (Token)Session["token"];
            _clienteService = new ClienteService(token.access_token);
            int idCliente = _clienteService.Guardar(cliente);

            if (idCliente != 0)
            {
                List<Cliente> clientes = _clienteService.Obtener();
                if (clientes != null && clientes.Count > 0)
                {
                    actualizarRepeater(listaClientes, clientes, listaClientesVacia);
                    upListaClientes.Update();
                    actualizarDdlClientes(clientes);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearCliente", "Swal.fire('Cliente creado', '', 'success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "Swal.fire('Error al crear cliente', '', 'error');", true);
            }
        }

        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Cliente cliente = new Cliente();
            Persona persona = new Persona();

            persona.Nombre = txtNombreCliente.Text;
            persona.Apellido = txtApellidoCliente.Text;
            persona.Rut = int.Parse(txtRutCliente.Text);
            persona.DigitoVerificador = txtDigitoVerificadorCliente.Text;
            persona.Email = txtEmailCliente.Text;
            persona.Telefono = int.Parse(txtTelefonoCliente.Text);
            persona.EsPersonaNatural = Convert.ToChar(chkEsPersonaJuridica.Checked ? 0 : 1);

            cliente.Id = int.Parse(txtIdCliente.Text);
            cliente.Persona = persona;


            Token token = (Token)Session["token"];
            _clienteService = new ClienteService(token.access_token);
            bool editar = _clienteService.Modificar(cliente, cliente.Id);
            if (editar)
            {
                List<Cliente> clientes = _clienteService.Obtener();
                if (clientes != null && clientes.Count > 0)
                {
                    actualizarRepeater(listaClientes, clientes, listaClientesVacia);
                    upListaClientes.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarCliente", "Swal.fire('Cliente editado', '', 'success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "Swal.fire('Error al editar cliente', '', 'error');", true);
            }
        }
        protected void btnModalCrearMesa_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalMesa.Text = "Crear Mesa";
            btnCrearMesa.Visible = true;
            btnEditarMesa.Visible = false;
            txtNombreMesa.Text = "";
            txtCantidadComensalesMesa.Text = "";
            ddlEstadoMesa.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMesa", "$('#modalMesa').modal('show');", true);
            upModalMesa.Update();

            limpiarTabs();
            tabMesas.Attributes.Add("class", "nav-link active");
            divMesas.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnModalEditarMesa_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idMesa;
            if (int.TryParse((string)e.CommandArgument, out idMesa))
            {
                Token token = (Token)Session["token"];
                _mesaService = new MesaService(token.access_token);
                Mesa mesa = _mesaService.Obtener(idMesa);
                if (mesa != null)
                {
                    tituloModalMesa.Text = "Editar Mesa";
                    btnCrearMesa.Visible = false;
                    btnEditarMesa.Visible = true;
                    txtIdMesa.Text = mesa.Id.ToString();
                    txtNombreMesa.Text = mesa.Nombre;
                    txtCantidadComensalesMesa.Text = mesa.CantidadComensales.ToString();
                    ddlEstadoMesa.SelectedValue = mesa.IdEstadoMesa.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMesa", "$('#modalMesa').modal('show');", true);
                    upModalMesa.Update();
                }
            }
            limpiarTabs();
            tabMesas.Attributes.Add("class", "nav-link active");
            divMesas.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearMesa_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Mesa mesa = new Mesa();
            mesa.Nombre = txtNombreMesa.Text;
            mesa.CantidadComensales = int.Parse(txtCantidadComensalesMesa.Text);
            mesa.IdEstadoMesa = int.Parse(ddlEstadoMesa.SelectedValue);

            Token token = (Token)Session["token"];
            _mesaService = new MesaService(token.access_token);
            int idMesa = _mesaService.Guardar(mesa);
            if (idMesa != 0)
            {
                List<Mesa> mesas = _mesaService.Obtener();
                if (mesas != null && mesas.Count > 0)
                {
                    actualizarRepeater(listaMesas, mesas, listaMesasVacia);
                    upListaMesas.Update();
                    actualizarDdlMesas(mesas);
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearMesa", "Swal.fire('Mesa creada', '', 'success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMesa", "$('#modalMesa').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMesa", "Swal.fire('Error al crear mesa', '', 'error');", true);
            }
        }

        protected void btnEditarMesa_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Mesa mesa = new Mesa();
            mesa.Id = int.Parse(txtIdMesa.Text);
            mesa.Nombre = txtNombreMesa.Text;
            mesa.Nombre = txtNombreMesa.Text;
            mesa.CantidadComensales = int.Parse(txtCantidadComensalesMesa.Text);
            mesa.IdEstadoMesa = int.Parse(ddlEstadoMesa.SelectedValue);

            Token token = (Token)Session["token"];
            _mesaService = new MesaService(token.access_token);
            bool editar = _mesaService.Modificar(mesa, mesa.Id);
            if (editar)
            {
                List<Mesa> mesas = _mesaService.Obtener();
                if (mesas != null && mesas.Count > 0)
                {
                    actualizarRepeater(listaMesas, mesas, listaMesasVacia);
                    upListaMesas.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarMesa", "Swal.fire('Mesa editada', '', 'success');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMesa", "$('#modalMesa').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalMesa", "Swal.fire('Error al editar mesa', '', 'error');", true);
            }
        }
        protected void btnEditarHorarioReserva_Click(object sender, RepeaterCommandEventArgs e)
        {      
            int idHorario;
            if (int.TryParse((string)e.CommandArgument, out idHorario))
            {
                TextBox txtDiaSemana = (TextBox)e.Item.FindControl("txtDiaSemana");
                TextBox txtHoraInicio = (TextBox)e.Item.FindControl("txtHoraInicioHorario");
                TextBox txtHoraFin = (TextBox)e.Item.FindControl("txtHoraFinHorario");
                DateTime horaInicio = DateTime.Now;
                DateTime horaFin = DateTime.Now;

                int horaInicioHora = int.Parse(txtHoraInicio.Text.Substring(0, 2));
                int horaInicioMinuto = int.Parse(txtHoraInicio.Text.Substring(3, 2));
                TimeSpan tsHoraInicio = new TimeSpan(horaInicioHora, horaInicioMinuto, 0);
                horaInicio = horaInicio.Date + tsHoraInicio;

                int horaFinHora = int.Parse(txtHoraFin.Text.Substring(0, 2));
                int horaFinMinuto = int.Parse(txtHoraFin.Text.Substring(3, 2));
                TimeSpan tsHoraFin = new TimeSpan(horaFinHora, horaFinMinuto, 0);
                horaFin = horaFin.Date + tsHoraFin;

                HorarioReserva horario = new HorarioReserva();
                horario.Id = idHorario;
                horario.DiaSemana = int.Parse(txtDiaSemana.Text);
                horario.HoraInicio = horaInicio;
                horario.HoraFin = horaFin;

                Token token = (Token)Session["token"];
                _horarioReservaService = new HorarioReservaService(token.access_token);
                bool editar = _horarioReservaService.Modificar(horario, idHorario);
                if (editar)
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarHorario", "Swal.fire('Horario guardado correctamente', '', 'success');", true);
                }
            }
            limpiarTabs();
            tabHorarioReservas.Attributes.Add("class", "nav-link active");
            divHorarioReservas.Attributes.Add("class", "tab-pane fade active show");           
        }
        public void actualizarRepeater<T>(Repeater repeater, List<T> listaData, Label mensajeListaVacia)
        {
            repeater.DataSource = listaData;
            repeater.DataBind();
            if (listaData.Count() == 0)
            {
                repeater.Visible = false;
                mensajeListaVacia.Visible = true;
            }
            else
            {
                repeater.Visible = true;
                mensajeListaVacia.Visible = false;
            }
        }

        public void actualizarDdlClientes(List<Cliente> clientes)
        {
            ddlClienteReserva.DataSource = clientes;
            ddlClienteReserva.DataTextField = "NombreCliente";
            ddlClienteReserva.DataValueField = "Id";
            ddlClienteReserva.DataBind();
            ddlClienteReserva.Items.Insert(0, new ListItem("Seleccionar", ""));
            ddlClienteReserva.SelectedIndex = 0;
        }
        public void actualizarDdlMesas(List<Mesa> mesas)
        {
            ddlMesaReserva.DataSource = mesas;
            ddlMesaReserva.DataTextField = "Nombre";
            ddlMesaReserva.DataValueField = "Id";
            ddlMesaReserva.DataBind();
            ddlMesaReserva.Items.Insert(0, new ListItem("Seleccionar", ""));
            ddlMesaReserva.SelectedIndex = 0;
        }
    }
}