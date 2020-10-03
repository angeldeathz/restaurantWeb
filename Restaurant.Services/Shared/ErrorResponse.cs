using System.Collections.Generic;

namespace Restaurant.Services.Shared
{
    public class ErrorResponse
    {
        public int CodigoError { get; set; }
        public List<string> Errores { get; set; }
        public string Error { get; set; }
        public string error_description { get; set; }
    }
}
