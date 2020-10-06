using Restaurant.Services.Servicios;
using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Restaurant.Model.Dto;

namespace Restaurant.Web.Paginas.Administrador
{
    public partial class GestionInventario : System.Web.UI.Page
    {
        private ProveedorService _proveedorService;
        private InsumoService _insumoService;
        private UnidadDeMedidaService _unidadDeMedidaService;
        private DetalleOrdenProveedorService _detalleOrdenProveedorService;
        private OrdenProveedorService _ordenProveedorService;
        private EstadoOrdenService _estadoOrdenService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                
                Token token = (Token) Session["token"];
                _proveedorService = new ProveedorService(token.access_token);
                _insumoService = new InsumoService(token.access_token);
                _unidadDeMedidaService = new UnidadDeMedidaService(token.access_token);
                _ordenProveedorService = new OrdenProveedorService(token.access_token);
                _estadoOrdenService = new EstadoOrdenService(token.access_token);

                List<Insumo> insumos = _insumoService.Obtener();
                if (insumos != null && insumos.Count > 0)
                {
                    actualizarRepeater(listaInsumos, insumos, listaInsumosVacia);

                    ddlInsumoOrden.DataSource = insumos;
                    ddlInsumoOrden.DataTextField = "Nombre";
                    ddlInsumoOrden.DataValueField = "Id";
                    ddlInsumoOrden.DataBind();
                    ddlInsumoOrden.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlInsumoOrden.SelectedIndex = 0;
                }

                List<OrdenProveedor> ordenesProveedor = _ordenProveedorService.Obtener();
                if (ordenesProveedor != null && ordenesProveedor.Count > 0)
                {
                    actualizarRepeater(listaOrdenes, ordenesProveedor, listaOrdenesVacia);
                }

                List<Proveedor> proveedores = _proveedorService.Obtener();
                if (proveedores != null && proveedores.Count > 0)
                {
                    actualizarRepeater(listaProveedores, proveedores, listaProveedoresVacia);

                    ddlProveedorInsumo.DataSource = proveedores;
                    ddlProveedorInsumo.DataTextField = "NombreProveedor";
                    ddlProveedorInsumo.DataValueField = "Id";
                    ddlProveedorInsumo.DataBind();
                    ddlProveedorInsumo.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlProveedorInsumo.SelectedIndex = 0;

                    ddlProveedorOrden.DataSource = proveedores;
                    ddlProveedorOrden.DataTextField = "NombreProveedor";
                    ddlProveedorOrden.DataValueField = "Id";
                    ddlProveedorOrden.DataBind();
                    ddlProveedorOrden.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlProveedorOrden.SelectedIndex = 0;
                }

                List<EstadoOrden> estadosOrden = _estadoOrdenService.Obtener();
                if (estadosOrden != null && estadosOrden.Count > 0)
                {
                    ddlEstadoOrden.DataSource = estadosOrden;
                    ddlEstadoOrden.DataTextField = "Nombre";
                    ddlEstadoOrden.DataValueField = "Id";
                    ddlEstadoOrden.DataBind();
                    ddlEstadoOrden.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlEstadoOrden.SelectedIndex = 0;                    
                }

                List<UnidadMedida> unidades = _unidadDeMedidaService.Obtener();
                if (unidades != null && unidades.Count > 0)
                { 
                    ddlUnidadMedida.DataSource = unidades;
                    ddlUnidadMedida.DataTextField = "Nombre";
                    ddlUnidadMedida.DataValueField = "Id";
                    ddlUnidadMedida.DataBind();
                    ddlUnidadMedida.Items.Insert(0, new ListItem("Seleccionar", ""));
                    ddlUnidadMedida.SelectedIndex = 0;                    
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
            tabInsumos.Attributes.Add("class", "nav-link");
            tabProveedores.Attributes.Add("class", "nav-link");
            tabOrdenes.Attributes.Add("class", "nav-link");
            divInventario.Attributes.Add("class", "tab-pane fade");
            divInsumos.Attributes.Add("class", "tab-pane fade");
            divProveedores.Attributes.Add("class", "tab-pane fade");
            divOrdenes.Attributes.Add("class", "tab-pane fade");
        }
        protected void btnModalCrearInsumo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalInsumo.Text = "Crear Insumo";
            btnCrearInsumo.Visible = true;
            btnEditarInsumo.Visible = false;
            txtNombreInsumo.Text = "";
            txtStockActual.Text = "";
            txtStockCritico.Text = "";
            txtStockOptimo.Text = "";
            ddlProveedorInsumo.SelectedValue = "";
            ddlUnidadMedida.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('show');", true);
            upModalInsumo.Update();

            limpiarTabs();
            tabInsumos.Attributes.Add("class", "nav-link active");
            divInsumos.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnModalEditarInsumo_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idInsumo;
            if (int.TryParse((string)e.CommandArgument, out idInsumo))
            {
                Token token = (Token)Session["token"];
                _insumoService = new InsumoService(token.access_token);
                Insumo insumo = _insumoService.Obtener(idInsumo);
                if (insumo != null)
                {
                    tituloModalInsumo.Text = "Editar Insumo";
                    btnCrearInsumo.Visible = false;
                    btnEditarInsumo.Visible = true;
                    txtIdInsumo.Text = insumo.Id.ToString();
                    txtNombreInsumo.Text = insumo.Nombre;
                    txtStockActual.Text = insumo.StockActual.ToString();
                    txtStockCritico.Text = insumo.StockCritico.ToString();
                    txtStockOptimo.Text = insumo.StockOptimo.ToString();
                    ddlProveedorInsumo.SelectedValue = insumo.IdProveedor.ToString();
                    ddlUnidadMedida.SelectedValue = insumo.IdUnidadDeMedida.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('show');", true);
                    upModalInsumo.Update();
                }
            }
            limpiarTabs();
            tabInsumos.Attributes.Add("class", "nav-link active");
            divInsumos.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearInsumo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Insumo insumo = new Insumo();
            insumo.Nombre = txtNombreInsumo.Text;
            insumo.StockActual = int.Parse(txtStockActual.Text);
            insumo.StockCritico = int.Parse(txtStockCritico.Text);
            insumo.StockOptimo = int.Parse(txtStockOptimo.Text);
            insumo.IdProveedor = int.Parse(ddlProveedorInsumo.SelectedValue);
            insumo.IdUnidadDeMedida = int.Parse(ddlUnidadMedida.SelectedValue);

            Token token = (Token)Session["token"];
            _insumoService = new InsumoService(token.access_token);
            int idInsumo = _insumoService.Guardar(insumo);
            if (idInsumo != 0)
            {
                List<Insumo> insumos = _insumoService.Obtener();
                if (insumos != null && insumos.Count > 0)
                {
                    actualizarRepeater(listaInsumos, insumos, listaInsumosVacia);
                    upListaInsumos.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearInsumo", "alert('Insumo creado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Error al crear insumo');", true);
            }
        }
        protected void btnEditarInsumo_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Insumo insumo = new Insumo();
            insumo.Id = int.Parse(txtIdInsumo.Text);
            insumo.Nombre = txtNombreInsumo.Text;
            insumo.StockActual = int.Parse(txtStockActual.Text);
            insumo.StockCritico = int.Parse(txtStockCritico.Text);
            insumo.StockOptimo = int.Parse(txtStockOptimo.Text);
            insumo.IdProveedor = int.Parse(ddlProveedorInsumo.SelectedValue);
            insumo.IdUnidadDeMedida = int.Parse(ddlUnidadMedida.SelectedValue);

            Token token = (Token)Session["token"];
            _insumoService = new InsumoService(token.access_token);
            bool editar = _insumoService.Modificar(insumo, insumo.Id);
            if (editar)
            {
                List<Insumo> insumos = _insumoService.Obtener();
                if (insumos != null && insumos.Count > 0)
                {
                    actualizarRepeater(listaInsumos, insumos, listaInsumosVacia);
                    upListaInsumos.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarInsumo", "alert('Insumo editado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('hide');", true);  
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Error al editar insumo');", true);
            }
        }

        protected void btnModalCrearProveedor_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalProveedor.Text = "Crear Proveedor";
            btnCrearProveedor.Visible = true;
            btnEditarProveedor.Visible = false;
            txtIdProveedor.Text = "";
            txtNombreProveedor.Text = "";
            txtApellidoProveedor.Text = "";
            txtRutProveedor.Text = "";
            txtDigitoVerificadorProveedor.Text = "";
            txtEmailProveedor.Text = "";
            txtTelefonoProveedor.Text = "";
            txtDireccionProveedor.Text = "";
            chkEsPersonaJuridica.Checked = false;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal('show');", true);
            upModalProveedor.Update();

            limpiarTabs();
            tabProveedores.Attributes.Add("class", "nav-link active");
            divProveedores.Attributes.Add("class", "tab-pane fade active show");
        }
        protected void btnModalEditarProveedor_Click(object source, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idProveedor;
            if (int.TryParse((string)e.CommandArgument, out idProveedor))
            {
                Token token = (Token)Session["token"];
                _proveedorService = new ProveedorService(token.access_token);
                Proveedor proveedor = _proveedorService.Obtener(idProveedor);
                if (proveedor != null)
                {
                    tituloModalProveedor.Text = "Editar Proveedor";
                    btnCrearProveedor.Visible = false;
                    btnEditarProveedor.Visible = true;
                    txtIdProveedor.Text = proveedor.Id.ToString();
                    txtNombreProveedor.Text = proveedor.Persona.Nombre;
                    txtApellidoProveedor.Text = proveedor.Persona.Apellido;
                    txtRutProveedor.Text = proveedor.Persona.Rut.ToString();
                    txtDigitoVerificadorProveedor.Text = proveedor.Persona.DigitoVerificador.ToString();
                    txtEmailProveedor.Text = proveedor.Persona.Email;
                    txtTelefonoProveedor.Text = proveedor.Persona.Telefono.ToString();
                    txtDireccionProveedor.Text = proveedor.Direccion;
                    chkEsPersonaJuridica.Checked = true;
                    if (proveedor.Persona.EsPersonaNatural == '0')
                    {
                        chkEsPersonaJuridica.Checked = false;
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal('show');", true);
                    upModalProveedor.Update();
                }
            }
            limpiarTabs();
            tabProveedores.Attributes.Add("class", "nav-link active");
            divProveedores.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnCrearProveedor_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            Proveedor proveedor = new Proveedor();
            proveedor.Persona = new Persona();
            proveedor.Persona.Nombre = txtNombreProveedor.Text;
            proveedor.Persona.Apellido = txtApellidoProveedor.Text;
            proveedor.Persona.Rut = int.Parse(txtRutProveedor.Text);
            proveedor.Persona.DigitoVerificador = txtDigitoVerificadorProveedor.Text;
            proveedor.Persona.Email = txtEmailProveedor.Text;
            proveedor.Persona.Telefono = int.Parse(txtTelefonoProveedor.Text);
            proveedor.Persona.EsPersonaNatural = chkEsPersonaJuridica.Checked ? '0' : '1';
            proveedor.Direccion = txtDireccionProveedor.Text;

            Token token = (Token)Session["token"];
            _proveedorService = new ProveedorService(token.access_token);            
            int idProveedor = _proveedorService.Guardar(proveedor);

            if (idProveedor != 0)
            {
                List<Proveedor> proveedores = _proveedorService.Obtener();
                if (proveedores != null && proveedores.Count > 0)
                {
                    actualizarRepeater(listaProveedores, proveedores, listaProveedoresVacia);
                    upListaProveedores.Update();

                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearProveedor", "alert('Proveedor creado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "alert('Error al crear proveedor');", true);
            }
        }

        protected void btnEditarProveedor_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Proveedor proveedor = new Proveedor();
            proveedor.Persona = new Persona();
            proveedor.Id = int.Parse(txtIdProveedor.Text);
            proveedor.Persona.Nombre = txtNombreProveedor.Text;
            proveedor.Persona.Apellido = txtApellidoProveedor.Text;
            proveedor.Persona.Rut = int.Parse(txtRutProveedor.Text);
            proveedor.Persona.DigitoVerificador = txtDigitoVerificadorProveedor.Text;
            proveedor.Persona.Email = txtEmailProveedor.Text;
            proveedor.Persona.Telefono = int.Parse(txtTelefonoProveedor.Text);
            proveedor.Direccion = txtDireccionProveedor.Text;
            proveedor.Persona.EsPersonaNatural = chkEsPersonaJuridica.Checked ? '0' : '1';

            Token token = (Token)Session["token"];
            _proveedorService = new ProveedorService(token.access_token);
            bool editar = _proveedorService.Modificar(proveedor, proveedor.Id);
            if (editar)
            {
                List<Proveedor> proveedores = _proveedorService.Obtener();
                if (proveedores != null && proveedores.Count > 0)
                {
                    actualizarRepeater(listaProveedores, proveedores, listaProveedoresVacia);
                    upListaProveedores.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarProveedor", "alert('Proveedor editado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "alert('Error al editar proveedor');", true);
            }
        }
        protected void btnModalCrearOrden_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalOrden.Text = "Crear Orden Proveedor";
            btnCrearOrden.Visible = true;
            btnEditarOrden.Visible = false;
            txtFechaHoraOrden.Text = "";
            txtTotalOrden.Text = "";
            ddlEstadoOrden.SelectedValue = "";
            ddlProveedorOrden.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalOrden", "$('#modalOrden').modal('show');", true);
            upModalOrden.Update();
            Session["detalleOrdenProveedor"] = new List<DetalleOrdenProveedor>();

            limpiarTabs();
            tabOrdenes.Attributes.Add("class", "nav-link active");
            divOrdenes.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnModalEditarOrden_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idOrdenProveedor;
            if (int.TryParse((string)e.CommandArgument, out idOrdenProveedor))
            {
                Token token = (Token)Session["token"];
                _ordenProveedorService = new OrdenProveedorService(token.access_token);
                OrdenProveedor ordenProveedor = _ordenProveedorService.Obtener(idOrdenProveedor);
                if (ordenProveedor != null)
                {
                    tituloModalOrden.Text = "Editar OrdenProveedor";
                    btnCrearOrden.Visible = false;
                    btnEditarOrden.Visible = true;
                    txtIdOrden.Text = ordenProveedor.Id.ToString();
                    txtFechaHoraOrden.Text = ordenProveedor.FechaHora.ToShortTimeString();
                    txtTotalOrden.Text = ordenProveedor.Total.ToString();
                    ddlEstadoOrden.SelectedValue = ordenProveedor.IdEstadoOrden.ToString();
                    ddlProveedorOrden.SelectedValue = ordenProveedor.IdProveedor.ToString();

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalOrden", "$('#modalOrden').modal('show');", true);
                    upModalOrden.Update();
                }
            }
            Session["detalleOrdenProveedor"] = new List<DetalleOrdenProveedor>();

            limpiarTabs();
            tabOrdenes.Attributes.Add("class", "nav-link active");
            divOrdenes.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnCrearOrden_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            var usuario = (Usuario)Session["usuario"];

            OrdenProveedor ordenProveedor = new OrdenProveedor();
            ordenProveedor.FechaHora = Convert.ToDateTime(txtFechaHoraOrden.Text);
            ordenProveedor.Total = int.Parse(txtTotalOrden.Text);
            ordenProveedor.IdEstadoOrden = int.Parse(ddlEstadoOrden.SelectedValue);
            ordenProveedor.IdProveedor = int.Parse(ddlProveedorOrden.SelectedValue);
            ordenProveedor.IdUsuario = usuario.Id;

            Token token = (Token)Session["token"];
            _ordenProveedorService = new OrdenProveedorService(token.access_token);
            int idOrdenProveedor = _ordenProveedorService.Guardar(ordenProveedor);
            if (idOrdenProveedor != 0)
            {
                List<DetalleOrdenProveedor> listaInsumos = (List<DetalleOrdenProveedor>)Session["detalleOrdenProveedor"];
                foreach (DetalleOrdenProveedor detalleOrdenProveedor in listaInsumos)
                {
                    detalleOrdenProveedor.IdOrdenProveedor = idOrdenProveedor;
                    _detalleOrdenProveedorService = new DetalleOrdenProveedorService(token.access_token);
                    int idDetalleOrdenProveedor = _detalleOrdenProveedorService.Guardar(detalleOrdenProveedor);
                }

                List<OrdenProveedor> ordenesProveedor = _ordenProveedorService.Obtener();
                if (ordenesProveedor != null && ordenesProveedor.Count > 0)
                {
                    actualizarRepeater(listaOrdenes, ordenesProveedor, listaOrdenesVacia);
                    upListaOrdenes.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearOrden", "alert('OrdenProveedor creado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalOrden", "$('#modalOrden').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalOrden", "alert('Error al crear OrdenProveedor');", true);
            }
        }

        protected void btnEditarOrden_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            OrdenProveedor ordenProveedor = new OrdenProveedor();
            ordenProveedor.Id = int.Parse(txtIdOrden.Text);
            ordenProveedor.FechaHora = Convert.ToDateTime(txtFechaHoraOrden.Text);
            ordenProveedor.Total = int.Parse(txtTotalOrden.Text);
            ordenProveedor.IdEstadoOrden = int.Parse(ddlEstadoOrden.SelectedValue);
            ordenProveedor.IdProveedor = int.Parse(ddlProveedorOrden.SelectedValue);

            Token token = (Token)Session["token"];
            _ordenProveedorService = new OrdenProveedorService(token.access_token);
            bool editar = _ordenProveedorService.Modificar(ordenProveedor, ordenProveedor.Id);
            if (editar)
            {
                List<DetalleOrdenProveedor> listaInsumos = (List<DetalleOrdenProveedor>)Session["detalleOrdenProveedor"];
                //SE DEBERÍAN ELIMINAR LOS insumosOrdenProveedor que ya existen, asociados?
                foreach (DetalleOrdenProveedor detalleOrdenProveedor in listaInsumos)
                {
                    detalleOrdenProveedor.IdOrdenProveedor = ordenProveedor.Id;
                    _detalleOrdenProveedorService = new DetalleOrdenProveedorService(token.access_token);
                    int idDetalleOrdenProveedor = _detalleOrdenProveedorService.Guardar(detalleOrdenProveedor);
                }
                List<OrdenProveedor> ordenesProveedor = _ordenProveedorService.Obtener();
                if (ordenesProveedor != null && ordenesProveedor.Count > 0)
                {
                    actualizarRepeater(listaOrdenes, ordenesProveedor, listaOrdenesVacia);
                    upListaOrdenes.Update();
                }
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarOrden", "alert('OrdenProveedor editado');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalOrden", "$('#modalOrden').modal('hide');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalOrden", "alert('Error al editar OrdenProveedor');", true);
            }
        }
        protected void btnAgregarInsumoOrden_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            DetalleOrdenProveedor detalleOrdenProveedor = new DetalleOrdenProveedor();
            detalleOrdenProveedor.Insumo = new Insumo();

            detalleOrdenProveedor.Insumo.Nombre = ddlInsumoOrden.SelectedItem.Text;
            detalleOrdenProveedor.IdInsumo = int.Parse(ddlInsumoOrden.SelectedValue);
            detalleOrdenProveedor.Precio = int.Parse(txtPrecioInsumoOrden.Text);
            detalleOrdenProveedor.Cantidad = int.Parse(txtCantidadInsumoOrden.Text);
            detalleOrdenProveedor.Total = detalleOrdenProveedor.Precio * detalleOrdenProveedor.Cantidad;

            List<DetalleOrdenProveedor> listaInsumos = (List<DetalleOrdenProveedor>)Session["detalleOrdenProveedor"];
            var insumoExiste = listaInsumos.FirstOrDefault(a => a.IdInsumo == detalleOrdenProveedor.IdInsumo);
            if (insumoExiste != null)
            {
                insumoExiste.Cantidad = insumoExiste.Cantidad + detalleOrdenProveedor.Cantidad;
                insumoExiste.Total = insumoExiste.Precio * insumoExiste.Cantidad;
            }
            else
            {
                listaInsumos.Add(detalleOrdenProveedor);
            }
            Session["detalleOrdenProveedor"] = listaInsumos;
            actualizarRepeater(listaInsumosOrden, listaInsumos, listaInsumosOrdenVacia);
            var totalOrdenProveedor = listaInsumos.Sum(x => x.Total);
            lblTotalOrden.Text = "Total: $" + totalOrdenProveedor.ToString() + "-";
            txtTotalOrden.Text = totalOrdenProveedor.ToString();
            upInsumosOrden.Update();

            limpiarTabs();
            tabOrdenes.Attributes.Add("class", "nav-link active");
            divOrdenes.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarInsumoOrden_Click(object sender, RepeaterCommandEventArgs e)
        {
            ValidarSesion();
            int idInsumo;
            if (int.TryParse((string)e.CommandArgument, out idInsumo))
            {
                List<DetalleOrdenProveedor> listaInsumos = (List<DetalleOrdenProveedor>)Session["detalleOrdenProveedor"];
                var insumoEliminar = listaInsumos.FirstOrDefault(a => a.IdInsumo == idInsumo);
                if (insumoEliminar != null)
                {
                    listaInsumos.Remove(insumoEliminar);
                }
                Session["detalleOrdenProveedor"] = listaInsumos;
                actualizarRepeater(listaInsumosOrden, listaInsumos, listaInsumosOrdenVacia);
                var totalOrdenProveedor = listaInsumos.Sum(x => x.Total);
                if (totalOrdenProveedor == 0)
                {
                    lblTotalOrden.Text = "";
                }
                else
                {
                    lblTotalOrden.Text = "Total: $" + totalOrdenProveedor.ToString() + "-";
                }
                txtTotalOrden.Text = totalOrdenProveedor.ToString();
                upInsumosOrden.Update();
            }
            limpiarTabs();
            tabOrdenes.Attributes.Add("class", "nav-link active");
            divOrdenes.Attributes.Add("class", "tab-pane active show");
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
    }
}