using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadSemana4.Entidades
{
    public class OrdenRetiro
    {
        public string Responsable { get; set; }
        public List<DetalleOrden> Detalles { get; set; }
        public OrdenRetiro()
        {
            Detalles=new List<DetalleOrden>();
        }
        public void AgregarDetalle(DetalleOrden detalle)
        {
            Detalles.Add(detalle);
        }
        public void QuitarDetalle(int posicion)
        {
            Detalles.RemoveAt(posicion);
        }
        
    }
}
