using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class OficioRespuesta
    {
        public int OficioRespuestaId { get; set; }

        public int? TicketId { get; set; }

        public string NumeroOficio { get; set; }

        public string? OficioReferencia { get; set; }

        public string Asunto { get; set; }

        public string Destinatario { get; set; }

        public string? CargoDestinatario { get; set; }

        public string CuerpoRespuesta { get; set; }

        public string? Copias { get; set; }

        public int? UsuarioId { get; set; }

        public DateTime FechaOficio { get; set; }

        public DateTime FechaCaptura { get; set; }

        public int? Anio { get; set; }

        public Ticket Ticket { get; set; }
        [ForeignKey("FirmanteId")]
        public int? FirmanteId { get; set; }
        public Firmante? Firmante { get; set; }

        public int?OficioId { get; set; }   //// OFICIO DE DONDE SALIO EL TICKET

        public int? RespuestaId { get; set; }   /// TABLA DONDE SE ENCUENTRA EL CONSECUTIVO DEL OFICIO 

    }
}
