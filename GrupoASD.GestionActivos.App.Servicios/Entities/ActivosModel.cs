using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoASD.GestionActivos.App.Servicios.Entities
{
    public class Activos
    {
        public int IdActivo { get; set; }        
        public string Nombre { get; set; }                
        public string Descripcion { get; set; }        
        public int IdTipoActivo { get; set; }
        public string Serial { get; set; }
        public string NumeroInternoInventario { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Alto { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Largo { get; set; }
        public decimal ValorCompra { get; set; }
        public DateTime FechaCompra { get; set; }
        public DateTime? FechaBaja { get; set; }
        public int IdEstadoActual { get; set; }
        public string Color { get; set; }
    }
}
