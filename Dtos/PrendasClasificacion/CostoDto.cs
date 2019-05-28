namespace Dtos.PrendasClasificacion
{
    public class CostoDto
    {
        public int Id { get; set; }
        public PrendaClasificacionDto PrendaClasificacion { get; set; }
        public double LavadoSeco { get; set; }
        public double LavadoPlanchado { get; set; }
        public double Planchado { get; set; }
        public double Doblado { get; set; }
    }
}
