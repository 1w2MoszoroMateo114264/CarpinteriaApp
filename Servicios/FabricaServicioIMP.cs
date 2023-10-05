using CarpinteriaApp.Servicios.Interfaz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.Servicios
{
    public class FabricaServicioIMP : FabricaServicios
    {
        public override IServicio CrearServicio()
        {
            return new Servicio();
        }
    }
}
