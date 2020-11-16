namespace Restaurant.Model.Clases
{
    public class MedioPago
    {
        public const int debito = 1;
        public const int credito = 2;
        public const int efectivo = 3;

        public MedioPago()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
