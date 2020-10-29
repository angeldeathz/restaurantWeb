namespace Restaurant.Model.Clases
{
    public class HorarioReserva
    {
        public HorarioReserva()
        {
            Id = 0;
            DiaSemana = 0;
            HoraInicio = string.Empty;
            HoraCierre = string.Empty;
            MaximoComensales = 0;
        }
        public int Id { get; set; }
        public int DiaSemana { get; set; }
        public string HoraInicio { get; set; }
        public string HoraCierre { get; set; }
        public int MaximoComensales { get; set; }
    }
}
