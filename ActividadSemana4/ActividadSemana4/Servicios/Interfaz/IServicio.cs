using ActividadSemana4.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadSemana4.Servicios.Interfaz
{
    public interface IServicio
    {
        int TraerProxNro();
        List<Material> TraerMateriales();
        bool GrabarOrden(OrdenRetiro oRetiro);
    }
}
