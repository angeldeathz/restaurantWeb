namespace Restaurant.Model.Clases
{
    public class Cliente
    {
        public Cliente()
        {
            Id = 0;
            IdPersona = 0;
        }

        public int Id { get; set; }
        public int IdPersona { get; set; }
        public Persona Persona { get; set; }
        public string NombreCliente { get { return Persona.Nombre + " " + Persona.Apellido; } }
    }
}
