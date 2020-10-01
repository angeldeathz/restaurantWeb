using Restaurant.Services.Servicios;
using Restaurant.Model.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

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
                _proveedorService = new ProveedorService();
                _insumoService = new InsumoService();
                _unidadDeMedidaService = new UnidadDeMedidaService();

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
                        ddlProveedorInsumo.DataSource = unidades;
                        ddlProveedorInsumo.DataTextField = "Nombre";
                        ddlProveedorInsumo.DataValueField = "Id";
                        ddlProveedorInsumo.DataBind();
                        ddlProveedorInsumo.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlProveedorInsumo.SelectedIndex = 0;
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
        protected void btnModalCrearInsumos_Click(object sender, EventArgs e)
        {
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
            System.Web.UI.HtmlControls.HtmlControl tab = tabInsumos;
            limpiarTabs();
            tabInsumos.Attributes.Add("class", "nav-link active");
            divInsumos.Attributes.Add("class", "tab-pane fade active show");
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
        protected void btnModalEditarInsumos_Click(object sender, EventArgs e)
        {
            // buscar insumo
            tituloModalInsumo.Text = "Editar Insumo";
            btnCrearInsumo.Visible = false;
            btnEditarInsumo.Visible = true;
            txtNombreInsumo.Text = "";
            txtStockActual.Text = "";
            txtStockCritico.Text = "";
            txtStockOptimo.Text = "";
            ddlProveedorInsumo.SelectedValue = "";
            ddlUnidadMedida.SelectedValue = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalInsumo", "$('#modalInsumo').modal();", true);
            upModalInsumo.Update();
        }

        protected void btnCrearInsumo_Click(object sender, EventArgs e)
        {

        }

        protected void btnEditarInsumo_Click(object sender, EventArgs e)
        {

        }
    }
}