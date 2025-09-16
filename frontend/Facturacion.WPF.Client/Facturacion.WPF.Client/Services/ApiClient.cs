using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Facturacion.WPF.Client.Services
{
    public class ApiClient
    {
        private readonly HttpClient client;

        public ApiClient(string baseAddress = "https://localhost:7202", bool ignoreSslErrorsForLocalhost = false)
        {
            if (baseAddress == null) throw new ArgumentNullException(nameof(baseAddress));

            if (ignoreSslErrorsForLocalhost && baseAddress.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                var handler = new HttpClientHandler();
#if NET7_0_OR_GREATER
                handler.ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
#else
                handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
#endif
                client = new HttpClient(handler) { BaseAddress = new Uri(baseAddress) };
            }
            else
            {
                client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            }
        }

        public async Task<HttpResponseMessage> CrearPersonaAsync(object persona)
        {
            var json = JsonSerializer.Serialize(persona);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            // Endpoint: crea una persona en el backend
            return await client.PostAsync("/api/DirectorioRestService", content);
        }

        /// <summary>
        /// Obtiene todas las personas desde el servicio de directorio.
        /// </summary>
        public async Task<PersonaDto[]> GetPersonasAsync()
        {
            var resp = await client.GetAsync("/api/DirectorioRestService");
            resp.EnsureSuccessStatusCode();
            var stream = await resp.Content.ReadAsStreamAsync();
            return await JsonSerializer.DeserializeAsync<PersonaDto[]>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }

    public class PersonaDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Identificacion { get; set; }
    }
}
