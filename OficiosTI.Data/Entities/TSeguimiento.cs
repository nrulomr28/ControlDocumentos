using OficiosTI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class TSeguimiento
    {
        [Key] public int? TSeguimientoId { get; set; }      /// ID DE TABLA
        public string? TSeguimientoSeguimiento { get; set; }     /// TEXTO DEL SEGUIMIENTO
        public int ? OficioId { get; set; }  //// ID DEL OFICIO
        public DateTime? TSeguimientoFecha { get; set;  }   /// FECHA 
        public string?TSeguimientoNombreUsuario { get; set; }  /// NOMBRE DEL USUARIO
        public int? TSeguimientoUsuarioId { get; set; }     /// ID DEL USUARIO

    }
}

