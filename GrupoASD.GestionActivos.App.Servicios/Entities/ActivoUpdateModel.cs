using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoASD.GestionActivos.App.Servicios.Entities
{
    public class ActivoUpdateModel
    {
        public int IdActivo { get; set; }

        public string Serial { get; set; }

        public DateTime? FechaBaja { get; set; }
    }
}
