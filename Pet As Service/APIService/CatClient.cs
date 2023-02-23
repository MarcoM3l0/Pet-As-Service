using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pet_As_Service.APIService
{
    class CatClient
    {
        public CatModel GetCat(string id)
        {
            var cliente = new RestClient("https://api.thecatapi.com/v1/breeds/search?");
            cliente.AddDefaultParameter("q", id);
            var request = new RestRequest(Method.GET);
            IRestResponse response = cliente.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                List<CatModel> resultados = JsonConvert.DeserializeObject<List<CatModel>>(response.Content.ToString());
                return resultados[0];
            }
            else
                return null;
        }

        public List<string> GetCatBreeds()
        {
            var client = new RestClient("https://api.thecatapi.com/v1");
            var request = new RestRequest("breeds/", Method.GET);
            request.AddHeader("x-api-key", "use sua api key"); // Substitua isso pela sua chave da API

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
                Console.WriteLine("Erro ao obter raças de gatos da API TheCatAPI.com");
                return null;
            }
        }
    }
}
