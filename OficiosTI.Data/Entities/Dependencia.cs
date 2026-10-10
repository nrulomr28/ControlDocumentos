using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Dependencia
    {
        [Key] public int DependenciaId { get; set; }
        public string? DependenciaNombre {  get; set; }
        public DateTime ?DependenciaDateTime { get; set; }   

    }
}
