using OficiosTI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace OficiosTI.Data.Entities
{
    public class CapturaOficios
    {
        [Key] public int CapturaOficioId { get; set; }
        public string? TipoCaptura { get; set; }
    //    public int? OficioNoControl { get; set; }
        public int? Anio {  get; set; }
        public DateTime? FechaRecepcion { get; set; }
        public string? TipoDocumento { get; set; }
        public string? NumeroOficio { get; set; }
        public DateTime? FechaDocumento { get; set; }
        public string? TipoSeRecibe { get; set; }
        public int? Remitente { get; set; }
        public int? CargoRemitente { get; set; }
        public int? Destinatario { get; set; }
        public int? CargoDestinatario { get; set; }
        public string? RequiereTicket { get; set; }
        public string? Prioridad { get; set; }
        public string? Peticion { get; set; }
        public string? Asunto { get; set; }
        public string? FolioInicio { get; set; }
        public string? FolioFin {  get; set; }
        public string? Clasificacion { get; set; }
        public string? UbicacionFisica { get; set; }
        public int? OficioRespuestaId { get; set; }
        public string? Estatus { get; set; }
        public string? UsuarioCaptura {  get; set; }
        public int? IdUsuario {  get; set; }
        public DateTime? FechaCaptura { get; set; }
        public int? Activo { get; set; }

    }
}



