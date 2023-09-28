using ActividadSemana4.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadSemana4.Datos.Interfaces
{
    public interface IOrdenRetiroDao
    {
        List<Material> ObtenerMateriales();
        bool Crear(OrdenRetiro oOrdenRetiro);
        int ObtenerProxNro();
    }
}
