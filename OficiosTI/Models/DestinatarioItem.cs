using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Models
{
    public class DestinatarioItem
    {
        public string Nombre { get; set; }
        public string Cargo { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
