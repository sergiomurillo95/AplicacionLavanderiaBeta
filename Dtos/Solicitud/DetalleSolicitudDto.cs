using System.ComponentModel.DataAnnotations;

namespace Dtos.Solicitud
{
    public class DetalleSolicitudDto
    {   [Key]
        public int Id { get; set; }
        public int SolicitudesId { get; set; }
        public int PrendasClasificacionId { get; set; }

        public string Prenda { get; set; }
        public string Clasificacion { get; set; }

        public bool LavadoSeco { get; set; }
        public bool LavadoPlanchado { get; set; }
        public bool Planchado { get; set; }
        public bool Doblado { get; set; }
        public int CantidadPrendas { get; set; }
        public string Estado { get; set; }
    }
}
