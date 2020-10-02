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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                
                Token token = (Token) Session["token"];
                _proveedorService = new ProveedorService(token.access_token);
                _insumoService = new InsumoService(token.access_token);
                _unidadDeMedidaService = new UnidadDeMedidaService(token.access_token);

                List<Insumo> insumos = _insumoService.Obtener();
                if (insumos != null && insumos.Count > 0)
                {
                    listaInsumos.DataSource = insumos;
                    listaInsumos.DataBind();
                }

                List<Proveedor> proveedores = _proveedorService.Obtener();
                if (proveedores != null)
                {
                    if (proveedores.Count > 0 && proveedores.Count > 0)
                    {
                        listaProveedores.DataSource = proveedores;
                        listaProveedores.DataBind();

                        ddlProveedorInsumo.DataSource = proveedores;
                        ddlProveedorInsumo.DataTextField = "NombreProveedor";
                        ddlProveedorInsumo.DataValueField = "Id";
                        ddlProveedorInsumo.DataBind();
                        ddlProveedorInsumo.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlProveedorInsumo.SelectedIndex = 0;
                    }
                }

                List<UnidadMedida> unidades = _unidadDeMedidaService.Obtener();
                if (unidades != null)
                {
                    if (unidades.Count > 0)
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
        protected void btnModalCrearInsumos_Click(object sender, EventArgs e)
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

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal();", true);
            upModalInsumo.Update();

            limpiarTabs();
            tabInsumos.Attributes.Add("class", "nav-link active");
            divInsumos.Attributes.Add("class", "tab-pane fade active show");
        }

        protected void btnModalEditarInsumos_Click(object sender, RepeaterCommandEventArgs e)
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

                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal();", true);
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Insumo creado');", true);
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
            bool editar = _insumoService.Modificar(insumo);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Insumo editado');", true);        
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Error al editar insumo');", true);
            }
        }

        protected void btnModalCrearProveedor_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalProveedor.Text = "Crear Prvoeedor";
            btnCrearProveedor.Visible = true;
            btnEditarProveedor.Visible = false;
            txtIdProveedor.Text = "";
            txtNombreProveedor.Text = "";
            txtApellidoProveedor.Text = "";
            txtRutProveedor.Text = "";
            txtEmailProveedor.Text = "";
            txtTelefonoProveedor.Text = "";
            txtDireccionProveedor.Text = "";
           
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal();", true);
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
                    tituloModalProveedor.Text = "Editar Prvoeedor";
                    btnCrearProveedor.Visible = false;
                    btnEditarProveedor.Visible = true;
                    txtIdProveedor.Text = proveedor.Id.ToString();
                    txtNombreProveedor.Text = proveedor.Persona.Nombre;
                    txtApellidoProveedor.Text = proveedor.Persona.Apellido;
                    txtRutProveedor.Text = proveedor.Persona.Rut.ToString() + proveedor.Persona.DigitoVerificador.ToString();
                    txtEmailProveedor.Text = proveedor.Persona.Email;
                    txtTelefonoProveedor.Text = proveedor.Persona.Telefono;
                    txtDireccionProveedor.Text = proveedor.Direccion;
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal();", true);
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
            proveedor.Id = int.Parse(txtIdProveedor.Text);
            proveedor.Persona.Nombre = txtNombreProveedor.Text;
            proveedor.Persona.Apellido = txtApellidoProveedor.Text;
            proveedor.Persona.Rut = int.Parse(txtRutProveedor.Text);
            proveedor.Persona.DigitoVerificador = txtDigitoVerificadorProveedor.Text;
            proveedor.Persona.Email = txtEmailProveedor.Text;
            proveedor.Persona.Telefono = txtTelefonoProveedor.Text;
            proveedor.Direccion = txtDireccionProveedor.Text;

            Token token = (Token)Session["token"];
            _proveedorService = new ProveedorService(token.access_token);
            int idProveedor = _proveedorService.Guardar(proveedor);

            if (idProveedor != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "$('#modalProveedor').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "alert('Proveedor creado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalProveedor", "alert('Error al crear proveedor');", true);
            }
        }

        protected void btnEditarProveedor_Click(object sender, EventArgs e)
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
            bool editar = _insumoService.Modificar(insumo);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Insumo editado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "alert('Error al editar insumo');", true);
            }
        }

        protected void btnModalCrearOrden_Click(object sender, EventArgs e)
        {

        }
    }
}