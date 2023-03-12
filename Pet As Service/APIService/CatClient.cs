using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Windows.Forms;


namespace Pet_As_Service.APIService
{
   
    class CatClient
    {
        public string json;
        private readonly HttpClient httpClient = new HttpClient();
        private string api_key = "live_hzw6gaV612d3bnWWqJOKmDTRIeVdp63E6QeP27kJ5xVsJOZyabbrCThj7BESGAeM";

        public CatModel GetCat(string id)
        {
            try
            {
                var cliente = new RestClient("https://api.thecatapi.com/v1/breeds/search?");
                cliente.AddDefaultParameter("q", id);
                var request = new RestRequest(Method.GET);
                IRestResponse response = cliente.Execute(request);
                json = response.Content.ToString();

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    List<CatModel> resultados = JsonConvert.DeserializeObject<List<CatModel>>(response.Content.ToString());
                    return resultados[0];
                }
                else
                    return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro durante a execução do método GetCat: { ex.Message}");
                return null;
            }
            
        }

        public List<string> GetCatBreeds()
        {
            try
            {
                RestClient client = new RestClient("https://api.thecatapi.com/v1");
                var request = new RestRequest("breeds/", Method.GET);
                request.AddHeader("x-api-key", api_key); // Substitua isso pela sua chave da API

                var response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var breedsJson = response.Content;
                    var breeds = JArray.Parse(breedsJson);

                    // Aqui você pode converter a lista de JObjects em uma lista de strings ou fazer qualquer outra coisa com ela.
                    var breedNames = breeds.Select(b => b["name"].ToString()).ToList();
                    return breedNames;
                }
                else
                {
                    MessageBox.Show("Erro ao obter raças de gatos da API TheCatAPI.com");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro durante a execução do método GetCatBreeds: {ex.Message}");
                return null;
            }

        }


        public void AdicionarFavorito(string imageId, string breeds)
        {
            try
            {
                var client = new RestClient("https://api.thecatapi.com/v1/favourites");
                var request = new RestRequest(Method.POST);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-api-Key", api_key);
                request.AddParameter("application/json", "{\n    \"image_id\":\"" + imageId + "\",\n\t\t\"sub_id\":\"user-123\"\n}", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show($"Raça {breeds} favoritada com sucesso!");
                }
                else
                {
                    MessageBox.Show($"Erro ao adicionar favorito. Código de status HTTP: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro durante a execução do método AdicionarFavorito: {ex.Message}");
            }
            
        }
    }
}
