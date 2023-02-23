using System;
using Pet_As_Service.APIService;
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
    public partial class FrmbBuscaDeRacas : Form
    {
        private CatClient CatClient;
        public FrmbBuscaDeRacas()
        {
            InitializeComponent();
            CatClient = new CatClient();
            FillBreedsComboBox();
        }

        private void FrmbBuscaDeRacas_Load(object sender, EventArgs e)
        {
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            if(cbxRacaGato.SelectedIndex > 0)
            {
                string idRaca = cbxRacaGato.Text;
                CarregarRaca(CatClient.GetCat(idRaca));
            }
            else
            {
                MessageBox.Show("Selecione uma Raça");
                Limpar();
            }
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
            lblTemperamento.Text = resultado.temperameny;
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
