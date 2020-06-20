using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientController/*'/>
    public class PatientController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PatientOptions _patientOptions;

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientControllerConstructor/*'/>
        public PatientController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<PatientOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _patientOptions = optionsAccessor.CurrentValue;
        }


        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/index/*'/>
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientApiResponseMessage = await httpClient.GetAsync(_patientOptions.PatientGatewayUrl);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                var patientModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatientModel>>(patientApiResponseStream);
                return View(patientModels);
            }
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                var errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(patientApiResponseStream);
                ViewData["ErrorReason"] = errorModel.Reason;
                return View();
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/details/*'/>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patientGetByIdGatewayUrl = $"{_patientOptions.PatientGatewayUrl}/{id}";
            var patientApiResponseMessage = await httpClient.GetAsync(patientGetByIdGatewayUrl);
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                var patientModel = await JsonSerializer.DeserializeAsync<PatientModel>(patientApiResponseStream);
                return View(patientModel);
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/createGet/*'/>
        public IActionResult Create()
        {
            return View();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/createPost/*'/>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientModel patient)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");
            var patientApiResponseMessage = await httpClient.PostAsync(_patientOptions.PatientGatewayUrl, patientContent);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/editGet/*'/>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patientGetByIdGatewayUrl = $"{_patientOptions.PatientGatewayUrl}/{id}";
            var patientApiResponseMessage = await httpClient.GetAsync(patientGetByIdGatewayUrl);
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                var patientModel = await JsonSerializer.DeserializeAsync<PatientModel>(patientApiResponseStream);
                return View(patientModel);
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/editPost/*'/>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientModel patient)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientUpdateGatewayUrl = $"{_patientOptions.PatientGatewayUrl}/{id}";
            var patientContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");
            var patientApiResponseMessage = await httpClient.PutAsync(patientUpdateGatewayUrl, patientContent);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/delete/*'/>
        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientDeleteGatewayUrl = $"{_patientOptions.PatientGatewayUrl}/{id}";
            var patientApiResponseMessage = await httpClient.DeleteAsync(patientDeleteGatewayUrl);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)patientApiResponseMessage.StatusCode);
        }
    }
}
