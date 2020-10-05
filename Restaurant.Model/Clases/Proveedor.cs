namespace Restaurant.Model.Clases
{
    public class Proveedor
    {
        public Proveedor()
        {
            Id = 0;
            Direccion = string.Empty;
            IdPersona = 0;
        }
        public int Id { get; set; }
        public string Direccion { get; set; }
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }
        public string NombreProveedor { get { return string.Concat(Persona.Nombre, Persona.Apellido); } }
    }
}
