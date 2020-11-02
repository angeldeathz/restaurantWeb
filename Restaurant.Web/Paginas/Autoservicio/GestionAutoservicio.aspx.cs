﻿using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Autoservicio
{
    public partial class GestionAutoservicio : System.Web.UI.Page
    {
        private PedidoService _pedidoService;
        private ArticuloPedidoService _articuloPedidoService;
        private ArticuloService _articuloService;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                validarIngreso();
                Token token = (Token)Session["token"];
                Reserva reserva = (Reserva)Session["reservaCliente"];
                Pedido pedido = (Pedido)Session["pedidoCliente"];
                if (Session["articulosPedido"] == null)
                {
                    Session["articulosPedido"] = new List<ArticuloPedido>();
                }
                _pedidoService = new PedidoService(token.access_token);

                if (pedido == null)
                {
                    List<Pedido> pedidos = _pedidoService.Obtener();
                    if (pedidos != null && pedidos.Count > 0)
                    {
                        Pedido pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido == EstadoPedido.enCurso
                                                                      && x.IdMesa == reserva.IdMesa
                                                                      && x.FechaHoraInicio.Date == DateTime.Now.Date);
                        if (pedidoCliente != null)
                        {
                            btnPagarCuenta.Visible = true;

                            cargarPedido(token, pedidoCliente);
                            //listaArticulosPedido.FindControl("btnEliminarArticulo").Visible = false;
                        }
                    }
                }

                _articuloService = new ArticuloService(token.access_token);
                List<Articulo> articulos = _articuloService.Obtener();
                List<Articulo> articulosDisponibles = articulos.Where(x => x.IdEstadoArticulo == EstadoArticulo.disponible).ToList();
                Session["articulosDisponibles"] = articulosDisponibles;

                List<Articulo> entradas = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.entradas).ToList();
                listaEntradas.DataSource = entradas;
                listaEntradas.DataBind();
                actualizarRepeater(listaEntradas, entradas, listaEntradasVacia);

                List<Articulo> platosFondo = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.platosFondo).ToList();
                listaPlatosFondo.DataSource = platosFondo;
                listaPlatosFondo.DataBind();
                actualizarRepeater(listaPlatosFondo, platosFondo, listaPlatosFondoVacia);

                List<Articulo> ensaladas = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.ensaladas).ToList();
                listaEnsaladas.DataSource = ensaladas;
                listaEnsaladas.DataBind();
                actualizarRepeater(listaEnsaladas, ensaladas, listaEnsaladasVacia);

                List<Articulo> postres = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.postres).ToList();
                listaPostres.DataSource = postres;
                listaPostres.DataBind();
                actualizarRepeater(listaPostres, postres, listaPostresVacia);

                List<Articulo> bebestibles = articulosDisponibles.Where(x => x.IdTipoConsumo == TipoConsumo.bebestibles).ToList();
                listaBebestibles.DataSource = bebestibles;
                listaBebestibles.DataBind();
                actualizarRepeater(listaBebestibles, bebestibles, listaBebestiblesVacia);
            }
            else
            {
                Pedido pedido = (Pedido)Session["pedidoCliente"];
                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedido"];

                if (articulosPedido != null && articulosPedido.Count > 0)
                {
                    if (pedido != null)
                    { 
                        btnPagarCuenta.Visible = true;
                    }
                    else
                    {
                        btnHacerPedido.Visible = true;
                    }
                }
            }
            upArticulosPedido.Update();
        }
        protected void validarIngreso()
        {
            if (Session["reservaCliente"] == null || Session["token"] == null)
            {
                Response.Redirect("/Paginas/Publica/Autoservicio.aspx");
            }
        }
        protected void cargarPedido(Token token, Pedido pedido)
        {
            Session["pedidoCliente"] = pedido;
            _articuloPedidoService = new ArticuloPedidoService(token.access_token);
            List<ArticuloPedido> articulos = _articuloPedidoService.Obtener();
            if(articulos == null || articulos.Count == 0)
            {
                return;
            }
            List<ArticuloPedido> articulosPedido = articulos.Where(x => x.IdPedido == pedido.Id).ToList();
            Session["articulosPedido"] = articulosPedido;

            actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);

            var totalPedido = articulosPedido.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();
        }
        protected void btnEliminarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                Session["articulosPedido"] = listaArticulos;
                actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
                var totalPedido = listaArticulos.Sum(x => x.Total);
                if (totalPedido == 0)
                {
                    lblTotalPedido.Text = "";
                }
                else
                {
                    lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
                }
                txtTotalPedido.Text = totalPedido.ToString();
                upArticulosPedido.Update();
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected void btnModalAgregarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            validarIngreso();
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<Articulo> articulosDisponibles = (List<Articulo>)Session["articulosDisponibles"];
                Articulo articulo = articulosDisponibles.FirstOrDefault(a => a.Id == idArticulo);
                txtIdArticulo.Text = idArticulo.ToString();
                txtCantidadArticulo.Text = "";
                txtComentarioArticulo.Text = "";
                lblTituloModalArticulo.Text = "Pedir " + articulo.Nombre;
                lblPrecioArticulo.Text = "$" + articulo.Precio.ToString();
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('show');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "alert('Error al agregar artículo');", true);
            }
            limpiarTabs();
            tabMenu.Attributes.Add("class", "nav-link active");
            divMenu.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnAgregarArticuloPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();
            int cantidad = Convert.ToInt32(txtCantidadArticulo.Text);
            string comentarios = txtComentarioArticulo.Text;
            int idArticulo = Convert.ToInt32(txtIdArticulo.Text);

            List <Articulo> articulosDisponibles = (List<Articulo>) Session["articulosDisponibles"];
            Articulo articulo = articulosDisponibles.FirstOrDefault(a => a.Id == idArticulo);
            if(articulo == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "erroArticulo", "alert('Error al agregar artículo');", true);
                return;
            }

            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedido"];
            ArticuloPedido articuloPedido = articulosPedido.FirstOrDefault(a => a.Id == idArticulo);

            if (articuloPedido != null)
            {
                articuloPedido.Cantidad = articuloPedido.Cantidad + cantidad;
                articuloPedido.Total = articulo.Precio * articuloPedido.Cantidad;
            }
            else
            {
                ArticuloPedido nuevoArticuloPedido = new ArticuloPedido();
                nuevoArticuloPedido.Articulo = articulo;
                nuevoArticuloPedido.IdArticulo = articulo.Id;
                nuevoArticuloPedido.Precio = articulo.Precio;
                nuevoArticuloPedido.Cantidad = cantidad;
                nuevoArticuloPedido.Total = nuevoArticuloPedido.Precio * cantidad;
                EstadoArticuloPedido estadoInicialArticuloPedido = new EstadoArticuloPedido();
                estadoInicialArticuloPedido.Id = EstadoArticuloPedido.recibido;
                estadoInicialArticuloPedido.Nombre = "Recibido";
                nuevoArticuloPedido.EstadosArticuloPedido = new List<EstadoArticuloPedido>();
                nuevoArticuloPedido.EstadosArticuloPedido.Add(estadoInicialArticuloPedido);
                articulosPedido.Add(nuevoArticuloPedido);
            }
            Session["articulosPedido"] = articulosPedido;
            actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);
            var totalPedido = articulosPedido.Sum(x => x.Total);
            lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
            txtTotalPedido.Text = totalPedido.ToString();
            upArticulosPedido.Update();

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "creacionArticulo", "alert('Artículo agregado al pedido');", true);

            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarArticuloPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            validarIngreso();
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedido"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                Session["articulosPedido"] = listaArticulos;
                actualizarRepeater(listaArticulosPedido, listaArticulos, listaArticulosPedidoVacia);
                var totalPedido = listaArticulos.Sum(x => x.Total);
                if (totalPedido == 0)
                {
                    lblTotalPedido.Text = "";
                }
                else
                {
                    lblTotalPedido.Text = "Total: $" + totalPedido.ToString() + "-";
                }
                txtTotalPedido.Text = totalPedido.ToString();
                upArticulosPedido.Update();
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected void btnHacerPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Reserva reserva = (Reserva)Session["reservaCliente"];

            Pedido pedido = new Pedido();
            pedido.FechaHoraInicio = DateTime.Now;
            pedido.FechaHoraFin = DateTime.Now;
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = EstadoPedido.enCurso;
            pedido.IdMesa = reserva.IdMesa;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            int idPedido = _pedidoService.Guardar(pedido);
            if (idPedido != 0)
            {
                Session["pedidoCliente"] = pedido;
                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedido"];
                foreach (ArticuloPedido articuloPedido in articulosPedido)
                {
                    articuloPedido.IdPedido = idPedido;
                    _articuloPedidoService = new ArticuloPedidoService(token.access_token);
                    int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedido);
                }

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "alert('Pedido enviado a la cocina');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "alert('Error al hacer pedido');", true);
            }
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected void btnPagarCuenta_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedido"];
            pedido.IdEstadoPedido = EstadoPedido.pagado;
            var totalPedido = articulosPedido.Sum(x => x.Total);
            pedido.Total = totalPedido;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            bool editar = _pedidoService.Modificar(pedido, pedido.Id);

            //PENDIENTE: GESTIÓN DEL PAGO, CREACIÓN DE BOLETA, FACTURA
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "alert('Pedido pagado exitosamente');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "alert('Error al realizar el pago');", true);
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
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
        public void limpiarTabs()
        {
            tabMenu.Attributes.Add("class", "nav-link");
            tabMiOrden.Attributes.Add("class", "nav-link");
            divMenu.Attributes.Add("class", "tab-pane fade");
            divMiOrden.Attributes.Add("class", "tab-pane fade");
        }
    }
}