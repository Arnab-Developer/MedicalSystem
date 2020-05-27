using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DoctorOptions _doctorOptions;

        public DoctorController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _doctorOptions = optionsAccessor.CurrentValue;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorApiResponseMessage = await httpClient.GetAsync(_doctorOptions.DoctorGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);
                return View(doctorModels);
            }
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(doctorApiResponseStream);
                ViewData["ErrorReason"] = errorModel.Reason;
                return View();
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdGatewayUrl);
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                return View(doctorModel);
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), System.Text.Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PostAsync(_doctorOptions.DoctorGatewayUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdGatewayUrl);
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                return View(doctorModel);
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorUpdateGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), System.Text.Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PutAsync(doctorUpdateGatewayUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorDeleteGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.DeleteAsync(doctorDeleteGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }
    }
}
