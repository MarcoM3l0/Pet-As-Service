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

        private string api_key = "use sua api key";

        //Esse método utiliza a API do TheCatAPI para buscar informações sobre uma raça de gato
        //especificada pelo parâmetro id, que é o nome da raça
        public CatModel GetCat(string id)
        {
            try
            {
                RestClient cliente = new RestClient("https://api.thecatapi.com/v1/breeds/search?");
                cliente.AddDefaultParameter("q", id);
                RestRequest request = new RestRequest(Method.GET);
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

        //Este é método usa a API TheCatAPI para obter uma lista de raças de gatos.
        public List<string> GetCatBreeds()
        {
            try
            {
                RestClient client = new RestClient("https://api.thecatapi.com/v1");
                RestRequest request = new RestRequest("breeds/", Method.GET);
                request.AddHeader("x-api-key", api_key); // Substitua isso pela sua chave da API

                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    var breedsJson = response.Content;
                    JArray breeds = JArray.Parse(breedsJson);

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

        //Esse método realiza uma requisição HTTP POST para adicionar um gato como favorito na API.
        public void AddFavourite(string imageId, string breeds)
        {
            try
            {
                RestClient client = new RestClient("https://api.thecatapi.com/v1/favourites");
                RestRequest request = new RestRequest(Method.POST);
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

        // O método é responsável por obter a lista de gatos favoritados pelo usuário e o nome das raças desses gatos.
        public List<CatStored> GetCatFavourites()
        {
            try
            {
                // Obetem os gatos favoritados
                RestClient clientFav = new RestClient("https://api.thecatapi.com/v1/favourites?sub_id=user-123");
                RestRequest requestFav = new RestRequest(Method.GET);
                requestFav.AddHeader("Content-Type", "application/json");
                requestFav.AddHeader("x-api-Key", api_key);
                IRestResponse responseFav = clientFav.Execute(requestFav);

                // Obetem as raças de gatos
                RestClient clientBre = new RestClient("https://api.thecatapi.com/v1");
                RestRequest requestBre = new RestRequest("breeds/", Method.GET);
                requestBre.AddHeader("x-api-key", api_key);
                IRestResponse responseBre = clientBre.Execute(requestBre);

                // Lista para armazenar os nomes e os ids dos gatos favoritados
                List<CatStored> names = new List<CatStored>();


                // Verifica se a resposta foi bem sucedida
                if (responseFav.IsSuccessful && responseBre.IsSuccessful)
                {
                    // Obtém os dados dos gatos favoritados e das raças de gatos
                    List<FavoriteCat> favoriteCats = JsonConvert.DeserializeObject<List<FavoriteCat>>(responseFav.Content);
                    List<CatModel> breedCats = JsonConvert.DeserializeObject<List<CatModel>>(responseBre.Content);

                    // Loop que percorre todos os gatos favoritados
                    foreach (FavoriteCat favoriteCat in favoriteCats)
                    {
                        // Busca o gato correspondente na lista de raças de gatos
                        CatModel breedCat = breedCats.Find(c => c.id == favoriteCat.image_id);

                        // Verifica se encontrou o gato correspondente
                        if (breedCat != null)
                        {
                            // Imprime o nome do gato correspondente
                            CatStored nome = new CatStored { id = favoriteCat.id, name = breedCat.name };
                            names.Add(nome);
                        }
                    }

                    return names;
                }
                else
                {
                    // Imprime a mensagem de erro
                    MessageBox.Show("Erro ao obter os gatos favoritados ou as raças de gatos.");
                    return null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro durante a execução do método GetCatFavourites: {ex.Message}");
                return null;
            }

        }

        // Método responsável por deletar uma raça dos favoritos
        // Recebe como parâmetros o id e o nome da raça a ser deletada
        public void DeleteFavourite(string id, string name)
        {
            try
            {
                RestClient client = new RestClient($"https://api.thecatapi.com/v1/favourites/{id}");
                RestRequest request = new RestRequest(Method.DELETE);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("x-api-Key", api_key);
                IRestResponse response = client.Execute(request);

                if (response.IsSuccessful)
                {
                    MessageBox.Show($"Raça {name} deletada dos favoritos com sucesso!");
                }
                else
                {
                    MessageBox.Show($"Erro ao adicionar favorito. Código de status HTTP: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocorreu um erro durante a execução do método DeleteFavourite: {ex.Message}");
            }
        }
            
    }//Fim
}
