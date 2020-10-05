﻿namespace Restaurant.Model.Clases
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Contrasena { get; set; }
        public int IdPersona { get; set; }
        public int IdTipoUsuario { get; set; }
        public Persona Persona { get; set; }
        public TipoUsuario TipoUsuario { get; set; }
        public string NombreUsuario { get { return string.Concat(Persona.Nombre, Persona.Apellido); } }
    }
}
