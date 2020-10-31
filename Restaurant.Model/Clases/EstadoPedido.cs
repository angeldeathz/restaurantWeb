namespace Restaurant.Model.Clases
{
    public class EstadoPedido
    {
        public const int enCurso = 1;
        public const int pagado = 2;
        public const int cancelado = 3;
        public EstadoPedido()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
