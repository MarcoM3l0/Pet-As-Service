using System;
using Pet_As_Service.APIService;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace Pet_As_Service
{
    public partial class FrmbBuscaDeRacas : Form
    {
        private readonly CatClient CatClient;
        private string FavoritoNome;
        string FavoritoIdImage;

        public FrmbBuscaDeRacas()
        {
            InitializeComponent();
            CatClient = new CatClient();
            FillBreedsComboBox();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(cbxRacaGato.SelectedIndex > 0)
            {
                string idRaca = cbxRacaGato.Text;
                CarregarRaca(CatClient.GetCat(idRaca));
                btnFavoritar.Enabled = true;
            }
            else
            {
                MessageBox.Show("Selecione uma Raça");
                cbxRacaGato.Focus();
                Limpar();
            }
        }

        private void btnFavoritar_Click(object sender, EventArgs e)
        {
            CatClient.AddFavourite(FavoritoIdImage, FavoritoNome);
            cbxRacaGato.SelectedIndex = 0;
            Limpar();
            cbxRacaGato.Focus();
            btnFavoritar.Enabled = false;
        }


        private void FillBreedsComboBox()
        {
            List<string> breeds = CatClient.GetCatBreeds();
            if (breeds != null)
            {
                cbxRacaGato.Items.Add("Selecione uma raça");
                foreach (string breed in breeds)
                {
                    cbxRacaGato.Items.Add(breed);
                }
                cbxRacaGato.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show("Falha ao obter raças de gatos da API TheCatAPI.com.");
            }
        }

        private void CarregarRaca(CatModel resultado)
        {
            lblDescricao.Text = resultado.description;
            lblOrigem.Text = resultado.origin;
            lblTemperamento.Text = resultado.temperament;
            FavoritoNome = resultado.name;
            FavoritoIdImage = resultado.id;


        }

        private void Limpar()
        {
            lblDescricao.Text = "Resultado";
            lblOrigem.Text = "Resultado";
            lblTemperamento.Text = "Resultado";
        }

        private void frmbBuscaDeRacas_FormClosed(object sender, FormClosedEventArgs e)
        {
            Form frmPrincipal = new FrmPrincipal();
            frmPrincipal.Show();
        }

    }
}
