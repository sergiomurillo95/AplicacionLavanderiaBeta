using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class Costo
    {
        public int Id { get; set; }

        [ForeignKey("PrendasClasificacion")]
        public int PrendasClasificacionId { get; set; }
        public PrendasClasificacion PrendasClasificacion { get; set; }

        public double LavadoSeco { get; set; }
        public double LavadoPlanchado { get; set; }
        public double Planchado { get; set; }
        public double Doblado { get; set; }
    }
}
