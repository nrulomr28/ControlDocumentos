using OficiosTI.Data;
using OficiosTI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Services
{
    public class DestinatarioService
    {
        private readonly OficiosContext _context;

        public DestinatarioService(OficiosContext context)
        {
            _context = context;
        }

        public List<DestinatarioItem> ObtenerCatalogo()
        {
            return _context.OficioRespuesta
                .Select(x => new DestinatarioItem
                {
                    Nombre = x.Destinatario,
                    Cargo = x.CargoDestinatario
                })
                .Distinct()
                .ToList();
        }
    }
}
