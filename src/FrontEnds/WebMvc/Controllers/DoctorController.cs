using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<DoctorOptions> _optionsAccessor;

        public DoctorController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _optionsAccessor = optionsAccessor;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage doctorApiResponseMessage = await httpClient.GetAsync(_optionsAccessor.CurrentValue.DoctorGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using Stream doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                IEnumerable<DoctorModel>? doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);
                return View(doctorModels);
            }
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using Stream doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                ErrorModel? errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(doctorApiResponseStream);
                ViewData["ErrorReason"] = errorModel != null ? errorModel!.Reason : string.Empty;
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
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdGatewayUrl = $"{_optionsAccessor.CurrentValue.DoctorGatewayUrl}/{id}";
            HttpResponseMessage doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdGatewayUrl);
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using Stream doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                DoctorModel? doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
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
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), Encoding.UTF8, "application/json");
            HttpResponseMessage doctorApiResponseMessage = await httpClient.PostAsync(_optionsAccessor.CurrentValue.DoctorGatewayUrl, doctorContent);
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
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdGatewayUrl = $"{_optionsAccessor.CurrentValue.DoctorGatewayUrl}/{id}";
            HttpResponseMessage doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdGatewayUrl);
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using Stream doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                DoctorModel? doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                return View(doctorModel);
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorModel doctor)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var doctorUpdateGatewayUrl = $"{_optionsAccessor.CurrentValue.DoctorGatewayUrl}/{id}";
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), Encoding.UTF8, "application/json");
            HttpResponseMessage doctorApiResponseMessage = await httpClient.PutAsync(doctorUpdateGatewayUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var doctorDeleteGatewayUrl = $"{_optionsAccessor.CurrentValue.DoctorGatewayUrl}/{id}";
            HttpResponseMessage doctorApiResponseMessage = await httpClient.DeleteAsync(doctorDeleteGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }
    }
}
