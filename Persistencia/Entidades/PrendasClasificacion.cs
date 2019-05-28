using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class PrendasClasificacion
    {
        public int Id { get; set; }

        [ForeignKey("Prendas")]
        public int PrendasId { get; set; }
        public Prendas Prendas { get; set; }

        [ForeignKey("Clasificacion")]
        public int ClasificacionId { get; set; }
        public Clasificacion Clasificacion { get; set; }
    }
}
