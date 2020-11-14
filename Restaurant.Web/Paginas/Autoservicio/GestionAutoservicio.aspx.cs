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
            validarIngreso();
            Token token = (Token)Session["token"];
            Reserva reserva = (Reserva)Session["reservaCliente"];
            _pedidoService = new PedidoService(token.access_token);
            List<Pedido> pedidos = _pedidoService.Obtener();

            Pedido pedidoCliente = null;
            if (pedidos != null && pedidos.Count > 0)
            {
                pedidoCliente = pedidos.FirstOrDefault(x => x.IdEstadoPedido != EstadoPedido.cancelado
                                                         && x.IdEstadoPedido != EstadoPedido.pagado
                                                         && x.Reserva.Id == reserva.Id
                                                         && x.FechaHoraInicio.Date == DateTime.Now.Date);

                if (pedidoCliente != null)
                {                  
                    cargarPedido(token, pedidoCliente);
                }
            }

            List<ArticuloPedido> articulosPedido = new List<ArticuloPedido>();
            if (Session["articulosPedidoCliente"] == null)
            {
                Session["articulosPedidoCliente"] = articulosPedido;
            }
            else
            {
                articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
            }

            if (articulosPedido != null && articulosPedido.Count > 0)
            {
                recargarArticulosPedido(articulosPedido);
                if (pedidoCliente != null)
                {
                    btnHacerPedido.Visible = false;
                    if (pedidoCliente.IdEstadoPedido == EstadoPedido.enCurso)
                    {
                        btnCerrarCuenta.Visible = true;
                    }
                    //listaArticulosPedido.FindControl("btnEliminarArticulo").Visible = false;
                }
                else
                {
                    btnHacerPedido.Visible = true;
                    btnCerrarCuenta.Visible = false;
                }
            }
            upArticulosPedido.Update();

            if (!IsPostBack)
            {
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
            recargarArticulosPedido(articulosPedido);
        }

        protected void recargarArticulosPedido(List<ArticuloPedido> articulosPedido)
        {
            Session["articulosPedidoCliente"] = articulosPedido;
            actualizarRepeater(listaArticulosPedido, articulosPedido, listaArticulosPedidoVacia);

            var totalPedido = articulosPedido.Sum(x => x.Total);
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
        protected void btnEliminarArticulo_Click(object sender, RepeaterCommandEventArgs e)
        {
            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                Session["articulosPedidoCliente"] = listaArticulos;
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
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "Swal.fire('Error al agregar artículo', '', 'error');", true);
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

            // Info de artículos disponibles para pedir
            List <Articulo> articulosDisponibles = (List<Articulo>) Session["articulosDisponibles"];
            Articulo articulo = articulosDisponibles.FirstOrDefault(a => a.Id == idArticulo);
            if(articulo == null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "erroArticulo", "Swal.fire('Error al agregar artículo', '', 'error');", true);
                return;
            }
            // Info de artículos en el pedido
            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
            ArticuloPedido articuloPedido = articulosPedido.FirstOrDefault(a => a.IdArticulo == idArticulo);
            Pedido pedidoCliente = (Pedido) Session["pedidoCliente"];

            if (articuloPedido != null)
            {
                articuloPedido.Cantidad = articuloPedido.Cantidad + cantidad;
                articuloPedido.Total = articulo.Precio * articuloPedido.Cantidad;
                if (pedidoCliente != null)
                {
                    editarArticuloPedido(articuloPedido);
                }
                else
                {
                    btnHacerPedido.Visible = true;
                }
            }
            else
            {
                ArticuloPedido nuevoArticuloPedido = crearArticuloPedido(articulo, cantidad, comentarios);
                articulosPedido.Add(nuevoArticuloPedido);
                if (pedidoCliente != null)
                {
                    guardarArticuloPedido(nuevoArticuloPedido, pedidoCliente.Id);
                }
                else
                {
                    btnHacerPedido.Visible = true;
                }
            }

            recargarArticulosPedido(articulosPedido);

            if (pedidoCliente != null)
            {
                pedidoCliente.Total = articulosPedido.Sum(x => x.Total); 
                Token token = (Token)Session["token"];
                _pedidoService = new PedidoService(token.access_token);
                bool editar = _pedidoService.Modificar(pedidoCliente, pedidoCliente.Id);                
            }

            Session["pedidoCliente"] = pedidoCliente;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalArticulo", "$('#modalArticulo').modal('hide');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "creacionArticulo", "Swal.fire('Artículo agregado al pedido', '', 'success');", true);

            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnEliminarArticuloPedido_Click(object sender, RepeaterCommandEventArgs e)
        {
            validarIngreso();
            Pedido pedidoCliente = (Pedido)Session["pedidoCliente"];
            if(pedidoCliente != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "eliminarArticulo", "Swal.fire('No se pueden eliminar artículos enviados a la cocina', '', 'warning');", true);
                return;
            }

            int idArticulo;
            if (int.TryParse((string)e.CommandArgument, out idArticulo))
            {
                List<ArticuloPedido> listaArticulos = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                var articuloEliminar = listaArticulos.FirstOrDefault(a => a.IdArticulo == idArticulo);
                if (articuloEliminar != null)
                {
                    listaArticulos.Remove(articuloEliminar);
                }
                recargarArticulosPedido(listaArticulos);
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected void btnHacerPedido_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedidoCliente = (Pedido)Session["pedidoCliente"];
            if (pedidoCliente != null)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "Swal.fire('Ya se creó un pedido', '', 'warning');", true);
                return;
            }

            int idPedido = guardarPedido();
            if (idPedido != 0)
            {
                List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
                foreach (ArticuloPedido articuloPedido in articulosPedido)
                {
                    guardarArticuloPedido(articuloPedido, idPedido);
                }

                btnHacerPedido.Visible = false;
                btnCerrarCuenta.Visible = true;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearPedido", "Swal.fire('Pedido enviado a la cocina', '', 'success');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPedido", "Swal.fire('Error al hacer pedido', '', 'error');", true);
            }
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }
        protected ArticuloPedido crearArticuloPedido(Articulo articulo, int cantidad, string comentarios)
        {
            ArticuloPedido nuevoArticuloPedido = new ArticuloPedido();
            nuevoArticuloPedido.Articulo = articulo;
            nuevoArticuloPedido.IdArticulo = articulo.Id;
            nuevoArticuloPedido.Precio = articulo.Precio;
            nuevoArticuloPedido.Cantidad = cantidad;
            nuevoArticuloPedido.Total = nuevoArticuloPedido.Precio * cantidad;
            nuevoArticuloPedido.Comentarios = comentarios;

            EstadoArticuloPedido estadoInicialArticuloPedido = new EstadoArticuloPedido();
            estadoInicialArticuloPedido.Id = EstadoArticuloPedido.recibido;
            estadoInicialArticuloPedido.Nombre = "Recibido";
            nuevoArticuloPedido.EstadosArticuloPedido = new List<EstadoArticuloPedido>();
            nuevoArticuloPedido.EstadosArticuloPedido.Add(estadoInicialArticuloPedido);
            nuevoArticuloPedido.IdEstadoArticuloPedido = estadoInicialArticuloPedido.Id;
            nuevoArticuloPedido.EstadoArticuloPedido = nuevoArticuloPedido.EstadosArticuloPedido.LastOrDefault();

            return nuevoArticuloPedido;
        }

        protected bool guardarArticuloPedido(ArticuloPedido articuloPedido, int idPedido)
        {
            articuloPedido.IdPedido = idPedido;

            Articulo articuloAux = articuloPedido.Articulo;
            EstadoArticuloPedido estadoArticuloAux = articuloPedido.EstadoArticuloPedido;
            List<EstadoArticuloPedido> estadosAux = articuloPedido.EstadosArticuloPedido;
            articuloPedido.Articulo = null;
            articuloPedido.EstadoArticuloPedido = null;
            articuloPedido.EstadosArticuloPedido = null;

            Token token = (Token)Session["token"];
            _articuloPedidoService = new ArticuloPedidoService(token.access_token);
            int idArticuloPedido = _articuloPedidoService.Guardar(articuloPedido);
            if (idArticuloPedido == 0)
            {
                return false;
            }

            articuloPedido.Id = idArticuloPedido;
            articuloPedido.Articulo = articuloAux;
            articuloPedido.EstadosArticuloPedido = estadosAux;
            articuloPedido.EstadoArticuloPedido = estadoArticuloAux;
            return true;
        }
        protected bool editarArticuloPedido(ArticuloPedido articuloPedido)
        {
            Articulo articuloAux = articuloPedido.Articulo;
            EstadoArticuloPedido estadoArticuloAux = articuloPedido.EstadoArticuloPedido;
            List<EstadoArticuloPedido> estadosAux = articuloPedido.EstadosArticuloPedido;
            articuloPedido.Articulo = null;
            articuloPedido.EstadoArticuloPedido = null;
            articuloPedido.EstadosArticuloPedido = null;

            Token token = (Token)Session["token"];
            _articuloPedidoService = new ArticuloPedidoService(token.access_token);
            bool editar = _articuloPedidoService.Modificar(articuloPedido, articuloPedido.Id);

            articuloPedido.Articulo = articuloAux;
            articuloPedido.EstadosArticuloPedido = estadosAux;
            articuloPedido.EstadoArticuloPedido = estadoArticuloAux;
            return editar;
        }
        protected int guardarPedido()
        {
            Reserva reserva = (Reserva)Session["reservaCliente"];

            Pedido pedido = new Pedido();
            pedido.FechaHoraInicio = DateTime.Now;
            pedido.FechaHoraFin = DateTime.Now;
            pedido.Total = int.Parse(txtTotalPedido.Text);
            pedido.IdEstadoPedido = EstadoPedido.enCurso;
            pedido.IdReserva = reserva.Id;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            int idPedido = _pedidoService.Guardar(pedido);
            if (idPedido != 0)
            {
                pedido.Id = idPedido;
                Session["pedidoCliente"] = pedido;
            }
            return idPedido;
        }
        protected void btnCerrarCuenta_Click(object sender, EventArgs e)
        {
            validarIngreso();
            Pedido pedido = (Pedido)Session["pedidoCliente"];
            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
            var totalPedido = articulosPedido.Sum(x => x.Total);
            lblTotalPagar.Text = totalPedido.ToString();
            upModalCerrarCuenta.Update();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCerrarCuenta", "$('#modalCerrarCuenta').modal('show');", true);
            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnPagarEfectivo_Click(object sender, EventArgs e)
        {
            int estadoPedido = EstadoPedido.cerradoConEfectivo;
            generarPago(estadoPedido);
        }

        protected void btnPagarTarjeta_Click(object sender, EventArgs e)
        {
            int estadoPedido = EstadoPedido.cerradoConTarjeta;
            generarPago(estadoPedido);
        }

        protected void btnPagarMixto_Click(object sender, EventArgs e)
        {
            int estadoPedido = EstadoPedido.cerradoMixto;
            generarPago(estadoPedido);
        }

        protected void generarPago(int estadoPedido)
        {
            validarIngreso();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCerrarCuenta", "$('#modalCerrarCuenta').modal('hide');", true);

            Pedido pedido = (Pedido)Session["pedidoCliente"];
            List<ArticuloPedido> articulosPedido = (List<ArticuloPedido>)Session["articulosPedidoCliente"];
            if(estadoPedido != EstadoPedido.cerradoMixto)
            {
                pedido.IdEstadoPedido = estadoPedido;
            }
            var totalPedido = articulosPedido.Sum(x => x.Total);
            pedido.Total = totalPedido;
            pedido.Reserva = null;
            pedido.EstadoPedido = null;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            bool editar = _pedidoService.Modificar(pedido, pedido.Id);

            if (!editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarCuenta", "Swal.fire('Error al cerrar la cuenta', '', 'error');", true);
            }
            Session["pedidoCliente"] = pedido;
            btnHacerPedido.Visible = false;
            btnCerrarCuenta.Visible = false;

            switch (estadoPedido)
            {
                case EstadoPedido.cerradoConEfectivo:
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('Por favor espere. Un garzón acudirá para realizar el pago', '', 'warning');", true);
                    break;
                case EstadoPedido.cerradoConTarjeta:
                    Response.Redirect("/Paginas/Autoservicio/PagoTarjeta.aspx");
                    break;
                case EstadoPedido.cerradoMixto:
                    lblTotalPagarCuenta.Text = pedido.Total.ToString();
                    upModalPagoMixto.Update();
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalPagoMixto", "$('#modalPagoMixto').modal('show');", true);
                    break;
            }

            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('Pedido pagado exitosamente', '', 'success');", true);

            limpiarTabs();
            tabMiOrden.Attributes.Add("class", "nav-link active");
            divMiOrden.Attributes.Add("class", "tab-pane active show");
        }

        protected void btnMontosPagoMixto_Click(object sender, EventArgs e)
        {
            validarIngreso();
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalCerrarCuenta", "$('#modalPagoMixto').modal('hide');", true);

            Pedido pedido = (Pedido)Session["pedidoCliente"];
            int montoEfectivo = Convert.ToInt32(txtMontoEfectivo.Text);
            int montoTarjeta = Convert.ToInt32(txtMontoTarjeta.Text);
            if ((montoEfectivo + montoTarjeta) != pedido.Total)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('La suma de los montos ingresados debe ser igual al total a pagar', '', 'error').then(function(){$('#modalPagoMixto').modal('show');});", true);
                return;
            }

            pedido.IdEstadoPedido = EstadoPedido.cerradoMixto;
            pedido.Reserva = null;
            pedido.EstadoPedido = null;

            Token token = (Token)Session["token"];
            _pedidoService = new PedidoService(token.access_token);
            bool editar = _pedidoService.Modificar(pedido, pedido.Id);

            if (!editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "cerrarCuenta", "Swal.fire('Error al cerrar la cuenta', '', 'error');", true);
            }
            Session["pedidoCliente"] = pedido;
            btnHacerPedido.Visible = false;
            btnCerrarCuenta.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "pagarPedido", "Swal.fire('Realice el pago con tarjeta mientras un garzón acude a su mesa para hacer el pago en efectivo', '', 'warning').then(function(){location.replace('/Paginas/Autoservicio/PagoTarjeta.aspx');});", true);
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