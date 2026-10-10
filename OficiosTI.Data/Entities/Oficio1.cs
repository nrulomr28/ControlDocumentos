using OficiosTI.Data.Entities;
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
        [Key] public int? OficioId { get; set; } /// ID DE LA TABLA 
        public int? OficioNoControl { get; set; }   /// USO DESCONOCIDO 
        public short? OficioAnio { get; set; }     /// AÑO DE CAPTURA 
        public string? OficioRespuesta { get; set; }    /// PODRIA SER EL ID DE LA TABLA DE RESPUESTAS ?????
        public string? OficioStatusSeguimiento  { get; set; }  // ESTADO DEL OFICIO 
        public int? UsuarioId  { get; set; }   /// ID DEL USUARIO
        public int? Cat_TipoDocumentoId {  get; set; }  // catalogo de documentos : oficios, tarjetas 
        public string? OficioNoOficio { get; set; }   /// CAMPO DE TEXTO DONDE VA EL OFICIO SSP /OM/DTI/1691/2026
        public DateTime? OficioFechaDocumento { get; set; }  /// fecha y hora         
        public int? Cat_TipoRecepcionId { get; set; }   // LLEVAN UNA O ????
        public int? RemitenteId { get; set; }   // ID DEL REMITENTE 
        public int? DependenciaId { get; set; }  // ID DE LA DEPENDENCIA 
        public int ?DestinatarioId {  get; set; }  /// ID DEL DESTINATARIO 
        public int? Cat_AsuntosId { get; set; } // ID DEL CATALOGO DE ASUNTOS 
        public string? OficioPrioridad { get; set; } /// NORMAL O URGENTE 
        public string? OficioAsunto { get; set; } /// TEXTO DEL ASUNTO 
        public int? Oficio_Archivo { get; set; } ///1 o 0 ????
        public int? OficioCentroTrabajo { get; set; }   /// ID DEL CATALOGO DE CENTROS DE TRABAJO 
        public DateTime? OficioFechaRecepcion { get; set; }
        public string? OficioSeguimiento { get; set; }  /// CASI SIEMPRE ESTA NULL 
        public string? OficioInstruccion { get; set; }  /// SE CAPTURA UN TEXTO ??? NO SE MUESTRA EN PANTALLA 
        public DateTime? OficioInstruccionDateTime {  get; set; } /// FECHA DE LA INSTRUCCION 
        public DateTime? OficioDate	{ get; set;  } // fecha de captura 
        public int? OficioTur {  get; set; } /// LLEVAN UN 0 ????????        
        public int? OficioUSub { get; set; } /// LLEVAN UN 0 ????????   
        public int? Cat_EjercicioId { get; set; } /// ID DEL CATALOGO DEL AÑO EJERCICIO 

     /* CAMPOS QUE NO SE OCUPAN 
        NESubPeticionId
        Cat_NEPeticionId
        TercerNivelId
      */
    }
}


