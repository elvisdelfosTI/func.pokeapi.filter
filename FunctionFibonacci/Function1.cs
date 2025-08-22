using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FunctionFibonacci
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly HttpClient _httpClient;

        public Function1(ILogger<Function1> logger, HttpClient httpClient)
        {
            _logger = logger;
            _httpClient = httpClient;
        }

        [Function("Function1")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post",Route ="api/pokemon/{pokemonName}")] HttpRequest req, string pokemonName)
        {
            string baseUrl = Environment.GetEnvironmentVariable("POKE_API_BASE_URL");
            string url = String.Format($"{baseUrl}/{pokemonName}");

            _logger.LogInformation(url);
            var response = await _httpClient.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var pokemonData = JsonSerializer.Deserialize<PokemonApiResponse>(json, options);
            var result = new PokemonDTO
            {
                Name = pokemonData.Name,
                Height = pokemonData.Height,
                Weight = pokemonData.Weight,
                Img = pokemonData.sprites.Front_default
            };


            return new OkObjectResult(result);
        }
    }
}
