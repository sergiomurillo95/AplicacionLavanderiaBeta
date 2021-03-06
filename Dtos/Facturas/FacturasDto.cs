﻿namespace Dtos.Facturas
{
    public class FacturasDto
    {
        public int Id { get; set; }
        public int SolicitudesId { get; set; }
        public int ClientesId { get; set; }
        public double TotalParcial { get; set; }
        public double Doblado { get; set; }
        public double Suplemento { get; set; }
        public double TotalGlobal { get; set; }
        public string Estado { get; set; }
    }
}
