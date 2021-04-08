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
    public class PatientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<PatientOptions> _optionsAccessor;

        public PatientController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<PatientOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _optionsAccessor = optionsAccessor;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage patientApiResponseMessage = await httpClient.GetAsync(_optionsAccessor.CurrentValue.PatientGatewayUrl);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                using Stream patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                IEnumerable<PatientModel>? patientModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatientModel>>(patientApiResponseStream);
                return View(patientModels);
            }
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using Stream patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                ErrorModel? errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(patientApiResponseStream);
                ViewData["ErrorReason"] = errorModel != null ? errorModel!.Reason : string.Empty;
                return View();
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var patientGetByIdGatewayUrl = $"{_optionsAccessor.CurrentValue.PatientGatewayUrl}/{id}";
            HttpResponseMessage patientApiResponseMessage = await httpClient.GetAsync(patientGetByIdGatewayUrl);
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using Stream patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                PatientModel? patientModel = await JsonSerializer.DeserializeAsync<PatientModel>(patientApiResponseStream);
                return View(patientModel);
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientModel patient)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var patientContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");
            HttpResponseMessage patientApiResponseMessage = await httpClient.PostAsync(_optionsAccessor.CurrentValue.PatientGatewayUrl, patientContent);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var patientGetByIdGatewayUrl = $"{_optionsAccessor.CurrentValue.PatientGatewayUrl}/{id}";
            HttpResponseMessage patientApiResponseMessage = await httpClient.GetAsync(patientGetByIdGatewayUrl);
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using Stream patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                PatientModel? patientModel = await JsonSerializer.DeserializeAsync<PatientModel>(patientApiResponseStream);
                return View(patientModel);
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientModel patient)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var patientUpdateGatewayUrl = $"{_optionsAccessor.CurrentValue.PatientGatewayUrl}/{id}";
            var patientContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");
            HttpResponseMessage patientApiResponseMessage = await httpClient.PutAsync(patientUpdateGatewayUrl, patientContent);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var patientDeleteGatewayUrl = $"{_optionsAccessor.CurrentValue.PatientGatewayUrl}/{id}";
            HttpResponseMessage patientApiResponseMessage = await httpClient.DeleteAsync(patientDeleteGatewayUrl);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }
    }
}
