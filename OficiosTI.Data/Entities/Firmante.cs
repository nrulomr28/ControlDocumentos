using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Firmante
    {
        public int FirmanteId { get; set; }

        public string? Nombre { get; set; }
        public string? Cargo { get; set; }

        public bool EsTitular { get; set; } 
        public bool Activo { get; set; }

        public string? TipoFirma { get; set; }

        public string NombreCompleto => $"{Nombre} - {Cargo}";
     
    }
}
