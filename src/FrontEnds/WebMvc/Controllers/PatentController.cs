using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class PatentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PatentOptions _patentOptions;

        public PatentController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<PatentOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _patentOptions = optionsAccessor.CurrentValue;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentApiResponseMessage = await httpClient.GetAsync(_patentOptions.PatentGatewayUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatentModel>>(patentApiResponseStream);
                if (patentModels == null || patentModels.Count() == 0)
                {
                    return NotFound();
                }
                return View(patentModels);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentGetByIdGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentApiResponseMessage = await httpClient.GetAsync(patentGetByIdGatewayUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModel = await JsonSerializer.DeserializeAsync<PatentModel>(patentApiResponseStream);
                if (patentModel == null)
                {
                    return NotFound();
                }
                return View(patentModel);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatentModel patent)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentContent = new StringContent(JsonSerializer.Serialize(patent), System.Text.Encoding.UTF8, "application/json");
            var patentApiResponseMessage = await httpClient.PostAsync(_patentOptions.PatentGatewayUrl, patentContent);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentGetByIdGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentApiResponseMessage = await httpClient.GetAsync(patentGetByIdGatewayUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModel = await JsonSerializer.DeserializeAsync<PatentModel>(patentApiResponseStream);
                if (patentModel == null)
                {
                    return NotFound();
                }
                return View(patentModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, PatentModel patent)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentUpdateGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentContent = new StringContent(JsonSerializer.Serialize(patent), System.Text.Encoding.UTF8, "application/json");
            var patentApiResponseMessage = await httpClient.PutAsync(patentUpdateGatewayUrl, patentContent);
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentDeleteGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentApiResponseMessage = await httpClient.DeleteAsync(patentDeleteGatewayUrl);
            return RedirectToAction(nameof(Index));
        }
    }
}
