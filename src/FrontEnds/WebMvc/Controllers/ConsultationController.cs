using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConsultationOptions _consultationOptions;

        public ConsultationController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _consultationOptions = optionsAccessor.CurrentValue;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationApiResponseMessage = await httpClient.GetAsync(_consultationOptions.ConsultationGatewayUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var consultationModels = await JsonSerializer.DeserializeAsync<IEnumerable<ConsultationModel>>(consultationApiResponseStream);
                return View(consultationModels);
            }
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(consultationApiResponseStream);
                ViewData["ErrorReason"] = errorModel.Reason;
                return View();
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var consultationGetByIdGatewayUrl = $"{_consultationOptions.ConsultationGatewayUrl}/{id}";
            var consultationApiResponseMessage = await httpClient.GetAsync(consultationGetByIdGatewayUrl);
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationApiResponseStream);
                return View(consultationModel);
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationModel consultation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), System.Text.Encoding.UTF8, "application/json");
            var consultationApiResponseMessage = await httpClient.PostAsync(_consultationOptions.ConsultationGatewayUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var consultationGetByIdGatewayUrl = $"{_consultationOptions.ConsultationGatewayUrl}/{id}";
            var consultationApiResponseMessage = await httpClient.GetAsync(consultationGetByIdGatewayUrl);
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationApiResponseStream);
                return View(consultationModel);
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ConsultationModel consultation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationUpdateGatewayUrl = $"{_consultationOptions.ConsultationGatewayUrl}/{id}";
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), System.Text.Encoding.UTF8, "application/json");
            var consultationApiResponseMessage = await httpClient.PutAsync(consultationUpdateGatewayUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationDeleteGatewayUrl = $"{_consultationOptions.ConsultationGatewayUrl}/{id}";
            var consultationApiResponseMessage = await httpClient.DeleteAsync(consultationDeleteGatewayUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }
    }
}
