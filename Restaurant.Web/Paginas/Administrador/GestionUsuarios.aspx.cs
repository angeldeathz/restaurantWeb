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
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        private UsuarioService _usuarioService;
        private TipoUsuarioService _tipoUsuarioService;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ValidarSesion();

                Token token = (Token)Session["token"];
                _usuarioService = new UsuarioService();
                _tipoUsuarioService = new TipoUsuarioService(token.access_token);

                List<Usuario> usuarios = _usuarioService.Obtener();
                if (usuarios != null && usuarios.Count > 0)
                {
                    listaUsuarios.DataSource = usuarios;
                    listaUsuarios.DataBind();
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

        private void ValidarSesion()
        {
            if (Session["usuario"] == null || Session["token"] == null)
            {
                Response.Redirect("../Publica/IniciarSesion.aspx");
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
            ValidarSesion();
            int idUsuario;
            if (int.TryParse((string)e.CommandArgument, out idUsuario))
            {
                Token token = (Token)Session["token"];
                _usuarioService = new UsuarioService();
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
                    txtTelefonoUsuario.Text = usuario.Persona.Telefono;
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

        protected void btnCrearUsuario_Click(object sender, EventArgs e)
        {
            ValidarSesion();

            string contrasena = txtContrasena.Text = "";
            string contrasenaRepetir = txtContrasenaRepetir.Text = "";

            if (contrasena != contrasenaRepetir)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "alert('Las contraseñas no coinciden');", true);
                return;
            }

            Usuario usuario = new Usuario();
            usuario.Id = int.Parse(txtIdUsuario.Text);
            usuario.Persona.Nombre = txtNombreUsuario.Text;
            usuario.Persona.Apellido = txtApellidoUsuario.Text;
            usuario.Persona.Rut = int.Parse(txtRutUsuario.Text);
            usuario.Persona.DigitoVerificador = txtDigitoVerificadorUsuario.Text;
            usuario.Persona.Email = txtEmailUsuario.Text;
            usuario.Persona.Telefono = txtTelefonoUsuario.Text;
            usuario.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
            usuario.Contrasena = txtContrasena.Text;

            Token token = (Token)Session["token"];
            _usuarioService = new UsuarioService();
            int idUsuario = _usuarioService.Guardar(usuario);

            if (idUsuario != 0)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "$('#modalUsuario').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "alert('Usuario creado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "alert('Error al crear usuario');", true);
            }
        }

        protected void btnEditarUsuario_Click(object sender, EventArgs e)
        {
            ValidarSesion();
            string contrasena = txtContrasena.Text = "";
            string contrasenaRepetir = txtContrasenaRepetir.Text = "";

            if (contrasena != contrasenaRepetir)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "alert('Las contraseñas no coinciden');", true);
                return;
            }

            Usuario usuario = new Usuario();
            usuario.Id = int.Parse(txtIdUsuario.Text);
            usuario.Persona.Nombre = txtNombreUsuario.Text;
            usuario.Persona.Apellido = txtApellidoUsuario.Text;
            usuario.Persona.Rut = int.Parse(txtRutUsuario.Text);
            usuario.Persona.DigitoVerificador = txtDigitoVerificadorUsuario.Text;
            usuario.Persona.Email = txtEmailUsuario.Text;
            usuario.Persona.Telefono = txtTelefonoUsuario.Text;
            usuario.IdTipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
            usuario.Contrasena = txtContrasena.Text;

            Token token = (Token)Session["token"];
            _usuarioService = new UsuarioService();
            bool editar = _usuarioService.Modificar(usuario);
            if (editar)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "$('#modalUsuario').modal('hide');", true);
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "alert('Usuario editado');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "modalUsuario", "alert('Error al editar usuario');", true);
            }
        }
    }
}