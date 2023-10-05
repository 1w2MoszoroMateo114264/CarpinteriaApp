using CarpinteriaApp.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.Datos.Interfaz
{
    public interface IPresupuestoDao
    {

        int ObtenerProximoNro();
        List<Producto> ObtenerProductos();
        bool Crear(Presupuesto oPresupuesto);
        bool Actualizar(Presupuesto oPresupuesto);

        // agregar metodos propios de un abm y consulta con filtros
        List<Presupuesto> ObtenerPresupuestosPorFiltros(DateTime desde, DateTime hasta, string cliente);

        
    }
}
