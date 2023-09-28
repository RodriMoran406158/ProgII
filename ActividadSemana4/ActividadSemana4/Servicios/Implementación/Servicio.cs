using ActividadSemana4.Datos.Implementación;
using ActividadSemana4.Datos.Interfaces;
using ActividadSemana4.Entidades;
using ActividadSemana4.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadSemana4.Servicios.Implementación
{
    public class Servicio : IServicio
    {
        private IOrdenRetiroDao dao;
        public Servicio()
        {
            dao = new OrdenRetiroDao();
        }
        public bool GrabarOrden(OrdenRetiro oRetiro)
        {
            return dao.Crear(oRetiro);
        }

        public List<Material> TraerMateriales()
        {
            return dao.ObtenerMateriales();
        }

        public int TraerProxNro()
        {
            return dao.ObtenerProxNro();
        }
    }
}
