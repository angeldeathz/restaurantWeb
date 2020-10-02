namespace Restaurant.Model.Clases
{
    public class Cliente
    {
        public int Id { get; set; }
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }
        public string NombreCliente { get { return string.Concat(Persona.Nombre, Persona.Apellido); } }
    }
}
