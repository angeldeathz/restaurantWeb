namespace Restaurant.Model.Clases
{
    public class EstadoPedido
    {
        public const int enCurso = 1;
        public const int cerradoConEfectivo = 2;
        public const int cerradoConTarjeta = 3;
        public const int cerradoMixto = 4;
        public const int pagado = 5;
        public const int cancelado = 6;
        public EstadoPedido()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
