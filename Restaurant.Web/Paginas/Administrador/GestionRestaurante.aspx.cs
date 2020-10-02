using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Administrador
{
    public partial class GestiónBodega : System.Web.UI.Page
    {
        private ClienteService _clienteService;
        private ReservaService _reservaService;
        private MesaService _mesaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();

                Token token = (Token)Session["token"];
                _clienteService = new ClienteService(token.access_token);
                _reservaService = new ReservaService(token.access_token);
                _mesaService = new MesaService(token.access_token);

                List<Reserva> reservas = _reservaService.Obtener();
                if (reservas != null && reservas.Count > 0)
                {
                    listaReservas.DataSource = reservas;
                    listaReservas.DataBind();
                }

                List<Cliente> clientes = _clienteService.Obtener();
                if (clientes != null && clientes.Count > 0)
                {
                    listaClientes.DataSource = clientes;
                    listaClientes.DataBind();

                    ddlClienteReserva.DataSource = clientes;
                    ddlClienteReserva.DataTextField = "NombreCliente";
                    ddlClienteReserva.DataValueField = "Id";
                    ddlClienteReserva.DataBind();
                    ddlClienteReserva.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlClienteReserva.SelectedIndex = 0;
                }
                

                List<Mesa> mesas = _mesaService.Obtener();
                if (mesas != null && mesas.Count > 0)
                {
                    listaMesas.DataSource = clientes;
                    listaMesas.DataBind();
                }
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
        }
        public void limpiarTabs()
        {
            tabInventario.Attributes.Add("class", "nav-link");
            tabReservas.Attributes.Add("class", "nav-link");
            tabClientes.Attributes.Add("class", "nav-link");
            tabOrdenes.Attributes.Add("class", "nav-link");
            divInventario.Attributes.Add("class", "tab-pane fade");
            divReservas.Attributes.Add("class", "tab-pane fade");
            divClientes.Attributes.Add("class", "tab-pane fade");
            divOrdenes.Attributes.Add("class", "tab-pane fade");
        }
        protected void btnModalCrearReservas_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalReserva.Text = "Crear Reserva";
            btnCrearReserva.Visible = true;
            btnEditarReserva.Visible = false;
            txtNombreReserva.Text = "";
            txtStockActual.Text = "";
            txtStockCritico.Text = "";
            txtStockOptimo.Text = "";
            ddlClienteReserva.SelectedValue = "";
            ddlMesa.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal();", true);
            upModalReserva.Update();

            limpiarTabs();
            tabReservas.Attributes.Add("class", "nav-link active");
            divReservas.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnModalEditarReservas_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idReserva;
            if (int.TryParse((string)e.CommandArgument, out idReserva))
            {
                Token token = (Token)Session["token"];
                _reservaService = new ReservaService(token.access_token);
                Reserva reserva = _reservaService.Obtener(idReserva);
                if (reserva != null)
                {
                    tituloModalReserva.Text = "Editar Reserva";
                    btnCrearReserva.Visible = false;
                    btnEditarReserva.Visible = true;
                    txtIdReserva.Text = reserva.Id.ToString();
                    txtNombreReserva.Text = reserva.Nombre;
                    txtStockActual.Text = reserva.StockActual.ToString();
                    txtStockCritico.Text = reserva.StockCritico.ToString();
                    txtStockOptimo.Text = reserva.StockOptimo.ToString();
                    ddlClienteReserva.SelectedValue = reserva.IdCliente.ToString();
                    ddlMesa.SelectedValue = reserva.IdMesa.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal();", true);
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
            reserva.Nombre = txtNombreReserva.Text;
            reserva.StockActual = int.Parse(txtStockActual.Text);
            reserva.StockCritico = int.Parse(txtStockCritico.Text);
            reserva.StockOptimo = int.Parse(txtStockOptimo.Text);
            reserva.IdCliente = int.Parse(ddlClienteReserva.SelectedValue);
            reserva.IdMesa = int.Parse(ddlMesa.SelectedValue);

            Token token = (Token)Session["token"];
            _reservaService = new ReservaService(token.access_token);
            int idReserva = _reservaService.Guardar(reserva);
            if (idReserva != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "alert('Reserva creado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "alert('Error al crear reserva');", true);
            }
        }

        protected void btnEditarReserva_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Reserva reserva = new Reserva();
            reserva.Id = int.Parse(txtIdReserva.Text);
            reserva.Nombre = txtNombreReserva.Text;
            reserva.StockActual = int.Parse(txtStockActual.Text);
            reserva.StockCritico = int.Parse(txtStockCritico.Text);
            reserva.StockOptimo = int.Parse(txtStockOptimo.Text);
            reserva.IdCliente = int.Parse(ddlClienteReserva.SelectedValue);
            reserva.IdMesa = int.Parse(ddlMesa.SelectedValue);

            Token token = (Token)Session["token"];
            _reservaService = new ReservaService(token.access_token);
            bool editar = _reservaService.Modificar(reserva);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "$('#modalReserva').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "alert('Reserva editado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalReserva", "alert('Error al editar reserva');", true);
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
            txtEmailCliente.Text = "";
            txtTelefonoCliente.Text = "";
            txtDireccionCliente.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal();", true);
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
                    txtRutCliente.Text = cliente.Persona.Rut.ToString() + cliente.Persona.DigitoVerificador.ToString();
                    txtEmailCliente.Text = cliente.Persona.Email;
                    txtTelefonoCliente.Text = cliente.Persona.Telefono;
                    txtDireccionCliente.Text = cliente.Direccion;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal();", true);
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
            cliente.Id = int.Parse(txtIdCliente.Text);
            cliente.Persona.Nombre = txtNombreCliente.Text;
            cliente.Persona.Apellido = txtApellidoCliente.Text;
            cliente.Persona.Rut = int.Parse(txtRutCliente.Text);
            cliente.Persona.DigitoVerificador = txtDigitoVerificadorCliente.Text;
            cliente.Persona.Email = txtEmailCliente.Text;
            cliente.Persona.Telefono = txtTelefonoCliente.Text;
            cliente.Direccion = txtDireccionCliente.Text;

            Token token = (Token)Session["token"];
            _clienteService = new ClienteService(token.access_token);
            /*
            int idCliente = _clienteService.Guardar(cliente);

            if (idCliente != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "alert('Cliente creado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "alert('Error al crear cliente');", true);
            }
            */
        }

        protected void btnEditarCliente_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Cliente cliente = new Cliente();
            cliente.Id = int.Parse(txtIdCliente.Text);
            cliente.Persona.Nombre = txtNombreCliente.Text;
            cliente.Persona.Apellido = txtApellidoCliente.Text;
            cliente.Persona.Rut = int.Parse(txtRutCliente.Text);
            cliente.Persona.DigitoVerificador = txtDigitoVerificadorCliente.Text;
            cliente.Persona.Email = txtEmailCliente.Text;
            cliente.Persona.Telefono = txtTelefonoCliente.Text;
            cliente.Direccion = txtDireccionCliente.Text;

            Token token = (Token)Session["token"];
            _clienteService = new ClienteService(token.access_token);
            /*
            bool editar = _clienteService.Modificar(cliente);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "$('#modalCliente').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "alert('Cliente editado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCliente", "alert('Error al editar cliente');", true);
            }
            */
        }
        protected void btnModalCrearOrden_Click(object sender, EventArgs e)
        {

        }
    }
}