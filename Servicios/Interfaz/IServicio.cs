using CarpinteriaApp.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.Servicios.Interfaz
{
    public interface IServicio
    {
        int TraerProximoNro();
        List<Producto> TraerProducto();
        bool CrearPresupuesto(Presupuesto oPresupuesto);
        bool ModificarPresupuesto(Presupuesto oPresupuesto);
        List<Presupuesto> TraerPresupuestosFiltrados(DateTime desde, DateTime hasta, string cliente);
    }
}
