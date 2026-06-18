using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Oficio1

    {
        [Key]
        public int? OficioId { get; set; }

        public string? OficioNoControl { get; set; }

        public string? OficioAnio { get; set; }
    }
}



    