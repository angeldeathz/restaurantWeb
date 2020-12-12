using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class Inicio : System.Web.UI.Page
    {
        private MesaService _mesaService;
        private ReservaService _reservaService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();
                Usuario usuario = (Usuario)Session["usuario"];
                if (usuario.IdTipoUsuario != TipoUsuario.administrador)
                {
                    linkUsuarios.Attributes.Add("class", "list-group-item d-none");
                    linkReporteria.Attributes.Add("class", "list-group-item d-none");

                    if (usuario.IdTipoUsuario != TipoUsuario.bodega)
                    {
                        linkBodega.Attributes.Add("class", "list-group-item d-none");
                    }

                    if (!new int[] { TipoUsuario.cocina, TipoUsuario.garzon }.Contains(usuario.IdTipoUsuario))
                    {
                        linkCocina.Attributes.Add("class", "list-group-item d-none");
                        linkRestaurante.Attributes.Add("class", "list-group-item d-none");
                    }

                    if (usuario.IdTipoUsuario != TipoUsuario.garzon)
                    {
                        linkRestaurante.Attributes.Add("class", "list-group-item d-none");
                    }
                }
                Token token = (Token)Session["token"];
                _mesaService = new MesaService(token.access_token);
                List<Mesa> mesas = _mesaService.Obtener();
                if (mesas != null && mesas.Count > 0)
                {
                    List<Mesa> mesasDisponibles = mesas.Where(x => x.EstadoMesa.Id == EstadoMesa.disponible).ToList();
                    List<Mesa> mesasOcupadas = mesas.Where(x => x.EstadoMesa.Id == EstadoMesa.ocupada).ToList();
                    lblMesasDisponibles.Text = mesasDisponibles.Count.ToString();
                    lblMesasOcupadas.Text = mesasOcupadas.Count.ToString();
                }
                _reservaService = new ReservaService(token.access_token);
                List<Reserva> reservas = _reservaService.Obtener();
                if (reservas != null && reservas.Count > 0)
                {
                    List<Reserva> reservasActivas = reservas.Where(x => x.EstadoReserva.Id == EstadoReserva.enCurso).ToList();
                    lblComensales.Text = reservasActivas.Count.ToString();
                    List<Reserva> listaProximaReserva = reservas.Where(x => x.EstadoReserva.Id != EstadoReserva.enCurso &&
                                                                            x.FechaReserva.Date == DateTime.Now.Date &&
                                                                            x.FechaReserva > DateTime.Now).ToList();
                    lblProximaReserva.Text = "Sin próximas reservas para hoy";
                    if(listaProximaReserva != null && listaProximaReserva.Count > 0)
                    {
                        Reserva proximaReserva = listaProximaReserva.OrderBy(x => x.FechaReserva).FirstOrDefault();
                        if(proximaReserva != null)
                        {
                            lblProximaReserva.Text = proximaReserva.FechaReserva.ToString("g") + " - " + proximaReserva.Mesa.Nombre;
                        }
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
            else
            {
                ObtenerDatosUsuario();
            }
        }

        private void ObtenerDatosUsuario()
        {
            var usuario = (Usuario)Session["usuario"];
            lblNombres.Text = $"{usuario.Persona.Nombre} {usuario.Persona.Apellido}";
            lblPerfil.Text = usuario.TipoUsuario.Nombre;
        }
    }
}