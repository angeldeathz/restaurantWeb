using Restaurant.Model.Clases;
using Restaurant.Model.Dto;
using Restaurant.Services.Servicios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Restaurant.Web.Paginas.Mantenedores
{
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private UsuarioService _usuarioService;
        private TipoUsuarioService _tipoUsuarioService;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ValidarSesion();

                    Token token = (Token)Session["token"];
                    _usuarioService = new UsuarioService(token.access_token);
                    _tipoUsuarioService = new TipoUsuarioService(token.access_token);

                    List<Usuario> usuarios = _usuarioService.Obtener();
                    if (usuarios != null && usuarios.Count > 0)
                    {
                        actualizarRepeater(listaUsuarios, usuarios, listaUsuariosVacia);
                    }

                    List<TipoUsuario> tipoUsuarios = _tipoUsuarioService.Obtener();
                    if (tipoUsuarios != null && tipoUsuarios.Count > 0)
                    {
                        ddlTipoUsuario.DataSource = tipoUsuarios;
                        ddlTipoUsuario.DataTextField = "Nombre";
                        ddlTipoUsuario.DataValueField = "Id";
                        ddlTipoUsuario.DataBind();
                        ddlTipoUsuario.Items.Insert(0, new ListItem("Seleccionar", ""));
                        ddlTipoUsuario.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', 'No se pudo cargar la página. Intente nuevamente', 'error');", true);
                return;
            }
        }

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
            }
            Usuario usuario = (Usuario)Session["usuario"];
            if (usuario.IdTipoUsuario != TipoUsuario.administrador)
            {
                Response.Redirect("../Mantenedores/Inicio.aspx");
            }
        }
        public void limpiarTabs()
        {
            tabUsuarios.Attributes.Add("class", "nav-link");
            divUsuarios.Attributes.Add("class", "tab-pane fade");
        }

        protected void btnModalCrearUsuario_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            tituloModalUsuario.Text = "Crear Usuario";
            btnCrearUsuario.Visible = true;
            btnEditarUsuario.Visible = false;
            txtIdUsuario.Text = "";
            txtNombreUsuario.Text = "";
            txtApellidoUsuario.Text = "";
            txtRutUsuario.Text = "";
            txtDigitoVerificadorUsuario.Text = "";
            txtEmailUsuario.Text = "";
            txtTelefonoUsuario.Text = "";
            ddlTipoUsuario.SelectedValue = "";
            txtContrasena.Text = "";
            txtContrasenaRepetir.Text = "";

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "$('#modalUsuario').modal('show');", true);
            upModalUsuario.Update();

            limpiarTabs();
            tabUsuarios.Attributes.Add("class", "nav-link active");
            divUsuarios.Attributes.Add("class", "tab-pane fade active show");
        }
        protected void btnModalEditarUsuario_Click(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                ValidarSesion();
                int idUsuario;
                if (int.TryParse((string)e.CommandArgument, out idUsuario))
                {
                    Token token = (Token)Session["token"];
                    _usuarioService = new UsuarioService(token.access_token);
                    Usuario usuario = _usuarioService.Obtener(idUsuario);
                    if (usuario != null)
                    {
                        tituloModalUsuario.Text = "Editar Usuario";
                        btnCrearUsuario.Visible = false;
                        btnEditarUsuario.Visible = true;
                        txtIdUsuario.Text = usuario.Id.ToString();
                        txtNombreUsuario.Text = usuario.Persona.Nombre;
                        txtApellidoUsuario.Text = usuario.Persona.Apellido;
                        txtRutUsuario.Text = usuario.Persona.Rut.ToString();
                        txtDigitoVerificadorUsuario.Text = usuario.Persona.DigitoVerificador.ToString();
                        txtEmailUsuario.Text = usuario.Persona.Email;
                        txtTelefonoUsuario.Text = usuario.Persona.Telefono.ToString();
                        ddlTipoUsuario.SelectedValue = usuario.IdTipoUsuario.ToString();
                        txtContrasena.Text = "";
                        txtContrasenaRepetir.Text = "";

                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "$('#modalUsuario').modal('show');", true);
                        upModalUsuario.Update();
                    }
                }
                limpiarTabs();
                tabUsuarios.Attributes.Add("class", "nav-link active");
                divUsuarios.Attributes.Add("class", "tab-pane fade active show");
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("Validacion");
            if (!Page.IsValid)
            {
                upModalUsuario.Update();
                return;
            }
            string contrasena = txtContrasena.Text;
            string contrasenaRepetir = txtContrasenaRepetir.Text;

            if (contrasena != contrasenaRepetir)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "Swal.fire('Las contraseñas no coinciden', '', 'error');", true);
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.Persona = new Persona();

                usuario.Persona.Nombre = txtNombreUsuario.Text;
                usuario.Persona.Apellido = txtApellidoUsuario.Text;
                usuario.Persona.Rut = int.Parse(txtRutUsuario.Text);
                usuario.Persona.DigitoVerificador = txtDigitoVerificadorUsuario.Text;
                usuario.Persona.Email = txtEmailUsuario.Text;
                usuario.Persona.Telefono = int.Parse(txtTelefonoUsuario.Text);
                usuario.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                usuario.Contrasena = txtContrasena.Text;

                Token token = (Token)Session["token"];
                _usuarioService = new UsuarioService(token.access_token);
                int idUsuario = _usuarioService.Guardar(usuario);

                if (idUsuario != 0)
                {
                    List<Usuario> usuarios = _usuarioService.Obtener();
                    if (usuarios != null && usuarios.Count > 0)
                    {
                        actualizarRepeater(listaUsuarios, usuarios, listaUsuariosVacia);
                        upListaUsuarios.Update();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "crearUsuario", "Swal.fire('Usuario creado', '', 'success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "$('#modalUsuario').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "Swal.fire('Error al crear usuario', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            Page.Validate("Validacion");
            if (!Page.IsValid)
            {
                upModalUsuario.Update();
                return;
            }
            string contrasena = txtContrasena.Text;
            string contrasenaRepetir = txtContrasenaRepetir.Text;

            if (contrasena != contrasenaRepetir)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "Swal.fire('Las contraseñas no coinciden', '', 'error');", true);
                return;
            }

            try
            {
                Usuario usuario = new Usuario();
                usuario.Persona = new Persona();
                usuario.Id = int.Parse(txtIdUsuario.Text);
                usuario.Persona.Nombre = txtNombreUsuario.Text;
                usuario.Persona.Apellido = txtApellidoUsuario.Text;
                usuario.Persona.Rut = int.Parse(txtRutUsuario.Text);
                usuario.Persona.DigitoVerificador = txtDigitoVerificadorUsuario.Text;
                usuario.Persona.Email = txtEmailUsuario.Text;
                usuario.Persona.Telefono = int.Parse(txtTelefonoUsuario.Text);
                usuario.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                usuario.Contrasena = txtContrasena.Text;

                Token token = (Token)Session["token"];
                _usuarioService = new UsuarioService(token.access_token);
                bool editar = _usuarioService.Modificar(usuario, usuario.Id);
                if (editar)
                {
                    List<Usuario> usuarios = _usuarioService.Obtener();
                    if (usuarios != null && usuarios.Count > 0)
                    {
                        actualizarRepeater(listaUsuarios, usuarios, listaUsuariosVacia);
                        upListaUsuarios.Update();
                    }
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "editarUsuario", "Swal.fire('Usuario editado', '', 'success');", true);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "$('#modalUsuario').modal('hide');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "Swal.fire('Error al editar usuario', '', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                string mensaje = ex != null && ex.Message != null ? ex.Message : "Ocurrió un error inesperado. Intente nuevamente";
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "error", "Swal.fire('Error', '" + mensaje + "', 'error');", true);
                return;
            }
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