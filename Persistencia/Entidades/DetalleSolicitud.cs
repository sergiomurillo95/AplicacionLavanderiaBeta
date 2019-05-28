using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class DetalleSolicitud
    {
        public int Id { get; set; }

        [ForeignKey("Solicitudes")]
        public int SolicitudesId { get; set; }
        public Solicitudes Solicitudes { get; set; }

        [ForeignKey("PrendasClasificacion")]
        public int PrendasClasificacionId { get; set; }
        public PrendasClasificacion PrendasClasificacion { get; set; }

        public bool LavadoSeco { get; set; }
        public bool LavadoPlanchado { get; set; }
        public bool Planchado { get; set; }
        public bool Doblado { get; set; }
        public string Estado { get; set; }
    }
}
