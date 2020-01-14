using CopaFilmes.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CopaFilmes.Domain.Services
{
    public class MoviesService
    {

        private const string moviesUrl = "http://copafilmes.azurewebsites.net/api/filmes";
        private static HttpClient _httpClient;
        private static HttpClient HttpClient => _httpClient ?? (_httpClient = new HttpClient());


        public async Task<List<Movie>> GetMoviesAsync()
        {
            try
            {
                HttpResponseMessage response = await HttpClient.GetAsync(moviesUrl);
                if (response.IsSuccessStatusCode)
                {
                    string responseBodyAsText = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<Movie>>(responseBodyAsText);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return null;
        }
    }
}
