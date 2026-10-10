using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class CatClasificacion
    {
        [Key] public int ClasificacionId { get; set; }
        public string ? Codigo { get; set; }
        public string ? Nombre { get; set; }
        public int ? Activo { get; set; }

    }
}
