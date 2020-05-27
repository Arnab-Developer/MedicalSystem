using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationController/*'/>
    public class ConsultationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConsultationOptions _consultationOptions;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationControllerConstructor/*'/>
        public ConsultationController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ConsultationOptions> consultationOptionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _consultationOptions = consultationOptionsAccessor.CurrentValue;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/index/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/details/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/createGet/*'/>
        public async Task<IActionResult> Create()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var consultationAddEditInitResponseMessage =
                await httpClient.GetAsync(_consultationOptions.ConsultationGatewayAddEditInitDataUrl);
            if (consultationAddEditInitResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)consultationAddEditInitResponseMessage.StatusCode);
            }

            using var consultationAddEditInitResponseStream =
                await consultationAddEditInitResponseMessage.Content.ReadAsStreamAsync();
            var consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationAddEditInitResponseStream);

            if (!(consultationModel.Doctors.Count() > 0 && consultationModel.Patents.Count() > 0))
            {
                return RedirectToAction(nameof(Index));
            }

            var doctorListItems = new List<SelectListItem>();
            foreach (var doctorModel in consultationModel.Doctors!)
            {
                var doctorListItem = new SelectListItem(
                    $"{doctorModel.FirstName} {doctorModel.LastName}",
                    doctorModel.Id.ToString());
                doctorListItems.Add(doctorListItem);
            }

            var patentListItems = new List<SelectListItem>();
            foreach (var patentModel in consultationModel.Patents!)
            {
                var patentListItem = new SelectListItem(
                    $"{patentModel.FirstName} {patentModel.LastName}",
                    patentModel.Id.ToString());
                patentListItems.Add(patentListItem);
            }

            consultationModel.DoctorSelectList = doctorListItems;
            consultationModel.PatentSelectList = patentListItems;
            return View(consultationModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/createPost/*'/>
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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/editGet/*'/>
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

                var consultationAddEditInitResponseMessage =
                    await httpClient.GetAsync(_consultationOptions.ConsultationGatewayAddEditInitDataUrl);
                if (consultationAddEditInitResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return StatusCode((int)consultationAddEditInitResponseMessage.StatusCode);
                }

                using var consultationAddEditInitResponseStream =
                    await consultationAddEditInitResponseMessage.Content.ReadAsStreamAsync();
                var consultationModelAddEditInit = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationAddEditInitResponseStream);

                if (!(consultationModelAddEditInit.Doctors.Count() > 0 && consultationModelAddEditInit.Patents.Count() > 0))
                {
                    return RedirectToAction(nameof(Index));
                }

                var doctorListItems = new List<SelectListItem>();
                foreach (var doctorModel in consultationModelAddEditInit.Doctors!)
                {
                    var doctorListItem = new SelectListItem(
                        $"{doctorModel.FirstName} {doctorModel.LastName}",
                        doctorModel.Id.ToString());
                    doctorListItems.Add(doctorListItem);
                }

                var patentListItems = new List<SelectListItem>();
                foreach (var patentModel in consultationModelAddEditInit.Patents!)
                {
                    var patentListItem = new SelectListItem(
                        $"{patentModel.FirstName} {patentModel.LastName}",
                        patentModel.Id.ToString());
                    patentListItems.Add(patentListItem);
                }

                consultationModel.DoctorSelectList = doctorListItems;
                consultationModel.PatentSelectList = patentListItems;
                return View(consultationModel);
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/editPost/*'/>
        [HttpPost]
        [ValidateAntiForgeryToken]
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

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/delete/*'/>
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
