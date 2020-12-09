namespace Restaurant.Model.Clases
{
    public class EstadoMesa
    {
        public const int disponible = 1;
        public const int ocupada = 2;
        public const int noDisponible = 3;

        public EstadoMesa()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
