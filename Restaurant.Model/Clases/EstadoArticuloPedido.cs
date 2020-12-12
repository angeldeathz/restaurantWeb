namespace Restaurant.Model.Clases
{
    public class EstadoArticuloPedido
    {
        public const int pendiente = 1;
        public const int enPreparacion = 2;
        public const int listo = 3;
        public const int entregado = 4;
        public const int rechazado = 5;
        public EstadoArticuloPedido()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
