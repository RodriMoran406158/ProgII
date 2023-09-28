using ActividadSemana4.Datos.Interfaces;
using ActividadSemana4.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ActividadSemana4.Datos.Implementación
{
    public class OrdenRetiroDao : IOrdenRetiroDao
    {
        public bool Crear(OrdenRetiro oOrdenRetiro)
        {
            bool resultado = true;
            SqlConnection conexion = HelperDao.ObtenerInstancia().ObtenerConexion();
            SqlTransaction t = null;
            try
            {
                conexion.Open();
                t = conexion.BeginTransaction();
                SqlCommand comando = new SqlCommand();
                comando.Connection = conexion;
                comando.Transaction = t;
                comando.CommandType = CommandType.StoredProcedure;
                comando.CommandText = "SP_INSERTAR_ORDEN";
                comando.Parameters.AddWithValue("@responsable", oOrdenRetiro.Responsable);

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@nro";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);
                comando.ExecuteNonQuery();

                int ordenNro = (int)parametro.Value;
                int detalleNro = 1;
                SqlCommand cmdDetalle;

                foreach (DetalleOrden d in oOrdenRetiro.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLES", conexion, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@nro_orden", ordenNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@codigo", d.Material.Codigo);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", d.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                    detalleNro++;
                }
                t.Commit();
            }
            catch
            {
                if(t!=null)
                {
                    t.Rollback();
                    resultado=false;
                }
            }
            finally
            {
                if(conexion!=null &&conexion.State==ConnectionState.Open)
                {
                    conexion.Close();
                }
            }
            return resultado;
        }

        public List<Material> ObtenerMateriales()
        {
            List<Material>lMateriales=new List<Material>();
            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_CONSULTAR_MATERIALES");
            foreach (DataRow fila in tabla.Rows)
            {
                int cod = Convert.ToInt32(fila[0].ToString());
                string nom = fila[1].ToString();
                int stock = Convert.ToInt32(fila[2]);
                Material m = new Material(cod,nom,stock);
                lMateriales.Add(m);
            }
            return lMateriales;
        }
        public int ObtenerProxNro()
        {
            return HelperDao.ObtenerInstancia().ConsultarEscalar("SP_PROXIMO_NRO", "@next");
        }
    }
}
