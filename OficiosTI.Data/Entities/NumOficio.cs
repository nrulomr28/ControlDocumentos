using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class NumOficio
    {
        [Key]
        public int OficioId { get; set; }

        public string NumeroConsecutivo { get; set; }

        public string Bis { get; set; }

        public int? Oficinas_Id { get; set; }

        public int? Tipo { get; set; }
        public int? Anio { get; set; }

        public DateTime FechaCaptura { get; set; }
        public int? TicketId { get; set; }
        public string? UsuarioId { get; set; }

     //   public int ReferenciaOficioId { get; set; }    
    }
}


