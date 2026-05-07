using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class TicketArchivo
    {
        public int TicketArchivoId { get; set; }

        public int TicketId { get; set; }

        public string NombreArchivo { get; set; }

        public string TipoMime { get; set; }

        public string Extension { get; set; }

        public byte[] Archivo { get; set; }

        public DateTime FechaCarga { get; set; }
    }
}
