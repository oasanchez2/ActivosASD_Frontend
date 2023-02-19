using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrupoASD.GestionActivos.App.Servicios.Entities
{
    public class ActivoUpdateModel
    {
        public int IdActivo { get; set; }

        [Required(ErrorMessage = "{0} es requerido!")]
        public string Serial { get; set; }

        public DateTime? FechaBaja { get; set; }
    }
}
