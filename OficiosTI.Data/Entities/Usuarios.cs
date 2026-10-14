using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Usuarios
    {
        [Key]
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public string? Paterno { get; set; }
        public string? Materno { get; set; }
        public int Activo { get; set; }
        public int? IdOficina { get; set; }
        public int UsuarioAnterior { get; set; }
        public string? UsuarioDominio { get; set; }
        public string? Tipo { get; set; }
        public int? Jefe { get; set; }
        public int? SuplenteJefe { get; set; }

        [NotMapped]
        public string NombreCompleto => $"{Nombre} {Paterno} {Materno}".Trim();
    }
}
