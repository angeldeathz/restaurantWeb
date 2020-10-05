namespace Restaurant.Model.Clases
{
    public class EstadoReserva
    {
        public EstadoReserva()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
