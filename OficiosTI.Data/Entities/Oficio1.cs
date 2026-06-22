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

     //   public int? OficioNoControl { get; set; }    

        public string? OficioNoOficio { get; set; }

        public short? OficioAnio { get; set; }     
    }
}



    