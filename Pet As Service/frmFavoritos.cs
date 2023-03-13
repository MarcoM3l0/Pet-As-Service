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
        private string nomeCat;
        private readonly CatClient CatClient;
        private List<CatStored> namesCats;
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
            // Obtém a lista de gatos favoritos do CatClient
            namesCats = CatClient.GetCatFavourites();

            if(namesCats != null)
            {
                // Itera sobre cada CatStored na lista e adiciona o nome de cada gato ao ListBox
                foreach (CatStored cat in namesCats)
                {
                    lbxFavorito.Items.Add(cat.name);
                }
            }
        }

        private void lbxFavorito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxFavorito.SelectedItem != null)
            {
                btnExcluir.Enabled = true;

                // Obtém o nome do gato selecionado a partir do item selecionado no ListBox
                nomeCat = (string)lbxFavorito.SelectedItem;
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Deseja remover o {nomeCat} dos favaritos?", "Realizar Ação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            namesCats = CatClient.GetCatFavourites();
            btnExcluir.Enabled = false;

            if (namesCats != null)
            {
                foreach (CatStored cat in namesCats)
                {
                    if (cat.name == nomeCat)
                    {
                        CatClient.DeleteFavourite(cat.id, cat.name);
                        lbxFavorito.Items.Clear();
                        FillFavouritesCatListBox();
                        break;
                    }
                }
            }
        }
    }// Fim
}
