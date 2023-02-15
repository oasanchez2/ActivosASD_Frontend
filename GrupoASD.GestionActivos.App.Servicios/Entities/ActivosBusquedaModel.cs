using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoASD.GestionActivos.App.Servicios.Entities
{
    public class ActivosBusquedaModel
    {
        public int IdTipoActivo { get; set; }
        public DateTime? FechaCompraInicio { get; set; }
        public DateTime? FechaCompraFin { get; set; }
        public string Serial { get; set; }
    }
}
