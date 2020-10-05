namespace Restaurant.Model.Clases
{
    public class Mesa
    {
        public Mesa()
        {
            Id = 0;
            Nombre = string.Empty;
            CantidadComensales = 0;
            IdEstadoMesa = 0;
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int CantidadComensales { get; set; }
        public int IdEstadoMesa { get; set; }
        public EstadoMesa EstadoMesa { get; set; }
    }
}
