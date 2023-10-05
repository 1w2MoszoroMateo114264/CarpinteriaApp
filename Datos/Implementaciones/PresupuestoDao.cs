using CarpinteriaApp.Datos.Interfaz;
using CarpinteriaApp.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.Datos.Implementaciones
{
    public class PresupuestoDao : IPresupuestoDao
    {
        public int ObtenerProximoNro()
        {
            return HelperDao.ObtenerInstancia().ConsultarEscalar("SP_PROXIMO_ID", "@next");
        }

        public List<Producto> ObtenerProductos()
        {
            List<Producto> Lproductos = new List<Producto>();
            //mapeo de productos
            DataTable Tabla = HelperDao.ObtenerInstancia().Consultar("SP_CONSULTAR_PRODUCTOS");
            foreach (DataRow fila in Tabla.Rows) 
            {
                int nro = int.Parse(fila[0].ToString());
                string nom = fila[1].ToString();
                double pre= double.Parse(fila[2].ToString());

                Producto p = new Producto(nro,nom,pre);
                Lproductos.Add(p);
            }
            return Lproductos;
        }

        public bool Crear(Presupuesto oPresupuesto)
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
                comando.CommandText = "SP_INSERTAR_MAESTRO";
                comando.Parameters.AddWithValue("@cliente", oPresupuesto.Cliente);
                comando.Parameters.AddWithValue("@dto", oPresupuesto.Descuento);
                comando.Parameters.AddWithValue("@total", oPresupuesto.CalcularTotal());

                SqlParameter parametro = new SqlParameter();
                parametro.ParameterName = "@presupuesto_nro";
                parametro.SqlDbType = SqlDbType.Int;
                parametro.Direction = ParameterDirection.Output;
                comando.Parameters.Add(parametro);

                comando.ExecuteNonQuery();

                int presupuestoNro = (int)parametro.Value;
                int detalleNro = 1;
                SqlCommand cmdDetalle;

                foreach (DetallePresupuesto dp in oPresupuesto.Detalles)
                {
                    cmdDetalle = new SqlCommand("SP_INSERTAR_DETALLE", conexion, t);
                    cmdDetalle.CommandType = CommandType.StoredProcedure;
                    cmdDetalle.Parameters.AddWithValue("@presupuesto_nro", presupuestoNro);
                    cmdDetalle.Parameters.AddWithValue("@detalle", detalleNro);
                    cmdDetalle.Parameters.AddWithValue("@id_producto", dp.Producto.ProductoNro);
                    cmdDetalle.Parameters.AddWithValue("@cantidad", dp.Cantidad);
                    cmdDetalle.ExecuteNonQuery();
                    detalleNro++;
                }
                t.Commit();
            }
            catch
            {
                if (t != null)
                    t.Rollback();
                resultado = false;
            }
            finally
            {
                if (conexion != null && conexion.State == ConnectionState.Open)
                    conexion.Close();
            }

            return resultado;
        }

        public bool Actualizar(Presupuesto oPresupuesto)
        {
            throw new NotImplementedException();
        }

        public List<Presupuesto> ObtenerPresupuestosPorFiltros(DateTime desde, DateTime hasta, string cliente)
        {
            List<Presupuesto> lPresupuestos = new List<Presupuesto>();

            List<Parametro> lst = new List<Parametro>();
            lst.Add(new Parametro("@fecha_desde", desde));
            lst.Add(new Parametro("@fecha_hasta", hasta));
            lst.Add(new Parametro("@cliente", cliente));

            DataTable tabla = HelperDao.ObtenerInstancia().Consultar("SP_CONSULTAR_PRESUPUESTOS", lst);
            foreach (DataRow fila in tabla.Rows)
            {
                Presupuesto presupuesto = new Presupuesto();
                presupuesto.PresupuestoNro = int.Parse(fila["presupuesto_nro"].ToString());
                presupuesto.Fecha = DateTime.Parse(fila["fecha"].ToString());
                presupuesto.Cliente = fila["cliente"].ToString();
                presupuesto.Descuento = double.Parse(fila["descuento"].ToString());
                
                lPresupuestos.Add(presupuesto);
            }

            return lPresupuestos;
        }
    }
}
