namespace Restaurant.Model.Clases
{
    public class EstadoArticuloPedido
    {
        public const int recibido = 1;
        public const int preparado = 2;
        public const int entregado = 3;
        public EstadoArticuloPedido()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
