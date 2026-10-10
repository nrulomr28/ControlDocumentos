using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Cat_Status
    {
        [Key] public int Cat_StatusId { get; set; }
        public string? Cat_StatusStatus { get; set; }
        public DateTime? Cat_StatusDateTime { get; set; }

    }
}