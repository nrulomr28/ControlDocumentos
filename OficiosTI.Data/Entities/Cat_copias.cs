using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Cat_copias
    {
        [Key] public int CcpId { get; set; }
        public string?  Nombre { get; set; }
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public string? Cargo { get; set; }
        public bool Activo { get; set; }
        public string NombreCompleto => $"{Nombre} {Paterno} {Materno} - {Cargo}";

    }

}
