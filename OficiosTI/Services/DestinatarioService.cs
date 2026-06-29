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

        /// <summary>
        /// SE OBTIENE DE LA TABLA DE OFICIOREMITENTE //////
        /// </summary>
        /// <returns></returns>
       
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


        /*
        public List<DestinatarioItem> ObtenerCatalogo()
        {
            return _context.OficioRespuesta
                // 1. Proyectar a un tipo anónimo. EF lo convierte en: SELECT DISTINCT Destinatario, Cargo...
                .Select(x => new
                {
                    x.Destinatario,
                    x.CargoDestinatario
                })
                .Distinct()
                // 2. Traer los resultados únicos a la memoria del programa
                .AsEnumerable()
                // 3. Ahora sí, mapearlos a tu objeto personalizado
                .Select(x => new DestinatarioItem
                {
                    Nombre = x.Destinatario,
                    Cargo = x.CargoDestinatario
                })
                .ToList();
        }
        */
    }
}
