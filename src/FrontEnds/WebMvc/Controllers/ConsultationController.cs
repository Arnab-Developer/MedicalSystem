using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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
        private readonly DoctorOptions _doctorOptions;
        private readonly PatentOptions _patentOptions;

        public ConsultationController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ConsultationOptions> consultationOptionsAccessor, 
            IOptionsMonitor<DoctorOptions> doctorOptionsAccessor,
            IOptionsMonitor<PatentOptions> patentOptionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _consultationOptions = consultationOptionsAccessor.CurrentValue;
            _doctorOptions = doctorOptionsAccessor.CurrentValue;
            _patentOptions = patentOptionsAccessor.CurrentValue;
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

        public async Task<IActionResult> Create()
        {
            var httpClient = _httpClientFactory.CreateClient();
            
            var doctorApiResponseMessage = await httpClient.GetAsync(_doctorOptions.DoctorGatewayUrl);
            IEnumerable<DoctorModel>? doctorModels = null;
            if (doctorApiResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)doctorApiResponseMessage.StatusCode);
            }

            var patentApiResponseMessage = await httpClient.GetAsync(_patentOptions.PatentGatewayUrl);
            if (patentApiResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)patentApiResponseMessage.StatusCode);
            }

            using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
            doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);

            using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
            var patentModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatentModel>>(patentApiResponseStream);

            var doctorListItems = new List<SelectListItem>();
            foreach (var doctorModel in doctorModels)
            {
                var doctorListItem = new SelectListItem(
                    $"{doctorModel.FirstName} {doctorModel.LastName}", 
                    doctorModel.Id.ToString());
                doctorListItems.Add(doctorListItem);
            }

            var patentListItems = new List<SelectListItem>();
            foreach (var patentModel in patentModels)
            {
                var patentListItem = new SelectListItem(
                    $"{patentModel.FirstName} {patentModel.LastName}",
                    patentModel.Id.ToString());
                patentListItems.Add(patentListItem);
            }

            var consultationModel = new ConsultationModel()
            {
                Doctors = doctorListItems,
                Patents = patentListItems
            };
            return View(consultationModel);
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
