using CarpinteriaApp.Datos.Implementaciones;
using CarpinteriaApp.Datos.Interfaz;
using CarpinteriaApp.Entidades;
using CarpinteriaApp.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.Servicios
{
    public class Servicio : IServicio
    {
        private IPresupuestoDao dao;
        public Servicio()
        {
            dao = new PresupuestoDao();
        }
        public bool CrearPresupuesto(Presupuesto oPresupuesto)
        {
            return dao.Crear(oPresupuesto);
        }

        public bool ModificarPresupuesto(Presupuesto oPresupuesto)
        {
            throw new NotImplementedException();
        }

        public List<Presupuesto> TraerPresupuestosFiltrados(DateTime desde, DateTime hasta, string cliente)
        {
            return dao.ObtenerPresupuestosPorFiltros(desde, hasta, cliente);
        }

        public List<Producto> TraerProducto()
        {
            return dao.ObtenerProductos();
        }

        public int TraerProximoNro()
        {
            return dao.ObtenerProximoNro();
        }
    }
}
