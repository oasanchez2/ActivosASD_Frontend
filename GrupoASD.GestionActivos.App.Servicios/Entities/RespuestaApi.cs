using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoASD.GestionActivos.App.Servicios.Entities
{
    public class RespuestaApi
    {
        public System.Net.HttpStatusCode HttpStatus { get; set; }
        public string JsonResultado { get; set; }
    }
}
