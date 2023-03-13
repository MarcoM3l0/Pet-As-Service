using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pet_As_Service
{
    public partial class FrmPrincipal : Form
    {
        public FrmPrincipal()
        {
            InitializeComponent();
        }


        private void FrmPrincipal_Load(object sender, EventArgs e)
        {

        }

        private void buscarRacasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmbBuscaDeRacas frmbBuscaDeRacas = new FrmbBuscaDeRacas();
            frmbBuscaDeRacas.Show();
            this.Hide();

        }

        private void meusFavoritosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frmFavoritos = new FrmFavoritos();
            frmFavoritos.Show();
            this.Hide();
        }
    }//Fim
}
