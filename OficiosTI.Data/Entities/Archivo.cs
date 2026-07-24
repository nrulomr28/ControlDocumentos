using OficiosTI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Archivo
    {
        [Key] public int ArchivoId { get; set; }
        public string? ArchivoNombre { get; set; }
        public byte[]? ArchivoArchivo { get; set; }
        public DateTime ArchivoFecha { get; set; }
        public int? OficioId { get; set; }

    }
}
