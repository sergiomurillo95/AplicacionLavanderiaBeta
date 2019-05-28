using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Persistencia.Entidades
{
    public class Solicitudes
    {
        public int Id { get; set; }

        [ForeignKey("Clientes")]
        public int ClienteId { get; set; }
        public Clientes Clientes { get; set; }

        public DateTime Fecha { get; set; }
        public bool SuplementoEntrega { get; set; }
        public string Estado { get; set; }
    }
}
