namespace Restaurant.Model.Clases
{
    public class TipoConsumo
    {
        public const int entradas = 1;
        public const int platosFondo = 2;
        public const int ensaladas = 1;
        public const int postres = 3;
        public const int bebestibles = 4;

        public TipoConsumo()
        {
            Id = 0;
            Nombre = string.Empty;
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
    }
}
