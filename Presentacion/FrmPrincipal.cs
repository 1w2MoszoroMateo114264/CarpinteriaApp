using CarpinteriaApp.Presentacion;
using CarpinteriaApp.Servicios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarpinteriaApp
{
    public partial class FrmPrincipal : Form
    {
        FabricaServicios Fabrica = null;
        public FrmPrincipal(FabricaServicios Fabrica)
        {
            InitializeComponent();
            this.Fabrica = Fabrica;
        }

        private void nuevoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNuevoPresupuesto nuevo = new FrmNuevoPresupuesto(Fabrica);
            nuevo.ShowDialog();
        }

        private void consultarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmConsultarPresupuestos consulta=new FrmConsultarPresupuestos(Fabrica);
            consulta.ShowDialog();
        }

        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }
    }
}
