using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ActividadSemana4.Datos
{
    public class HelperDao
    {
        private static HelperDao instancia;
        private SqlConnection conexion;
        private HelperDao()
        {
            conexion= new SqlConnection(Properties.Resources.ConexionDBOrdenes);
        }
        public static HelperDao ObtenerInstancia() 
        { 
            if(instancia == null)
            {
                instancia = new HelperDao();
            }
            return instancia;
        }
        public SqlConnection ObtenerConexion()
        {
            return conexion;
        }
        public DataTable Consultar(string nombreSP)
        {
            conexion.Open();
            SqlCommand comando = new SqlCommand();
            comando.Connection = conexion;
            comando.CommandType= CommandType.StoredProcedure;
            comando.CommandText=nombreSP;
            DataTable tabla=new DataTable();
            tabla.Load(comando.ExecuteReader());
            conexion.Close();
            return tabla;
        }
        public int ConsultarEscalar(string nombreSP,string paramSalida)
        {
            conexion.Open();
            SqlCommand comando=new SqlCommand();
            comando.Connection= conexion;
            comando.CommandType=CommandType.StoredProcedure;
            comando.CommandText=nombreSP;
            SqlParameter parametro = new SqlParameter();
            parametro.ParameterName = paramSalida;
            parametro.SqlDbType = SqlDbType.Int;
            parametro.Direction = ParameterDirection.Output;
            comando.Parameters.Add(parametro);
            comando.ExecuteNonQuery();
            conexion.Close();
            return (int)parametro.Value;
        }

    }
}
