namespace Restaurant.Model.Clases
{
    public class Usuario
    {
        public Usuario()
        {
            Id = 0;
            Contrasena = string.Empty;
            IdPersona = 0;
            IdTipoUsuario = 0;
            Persona = new Persona();
            TipoUsuario = new TipoUsuario();
        }

        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoUsuario { get; set; }
        public Persona Persona { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string NombreProveedor { get { return string.Concat(Persona.Nombre, Persona.Apellido); } }
    }
}
