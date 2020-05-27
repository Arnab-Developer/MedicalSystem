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
    /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorController/*'/>
    public class DoctorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DoctorOptions _doctorOptions;

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorControllerConstructor/*'/>
        public DoctorController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _doctorOptions = optionsAccessor.CurrentValue;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/index/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/details/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/createGet/*'/>
        public IActionResult Create()
        {
            return View();
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/createPost/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/editGet/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/editPost/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/delete/*'/>
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
