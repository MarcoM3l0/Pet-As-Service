using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Pet_As_Service.APIService;

namespace Pet_As_Service
{
    public partial class FrmFavoritos : Form
    {
        private readonly CatClient CatClient;
        public FrmFavoritos()
        {
            InitializeComponent();
            CatClient = new CatClient();
            FillFavouritesCatListBox();
        }

        private void frmFavoritos_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frmPrincipal = new FrmPrincipal();
            frmPrincipal.Show();
        }

        private void FillFavouritesCatListBox()
        {
            List<string> namesCats = CatClient.GetCatFavourites();

            if(namesCats != null)
            {
                lbxFavorito.Items.AddRange(namesCats.ToArray());
            }
        }
    }
}
