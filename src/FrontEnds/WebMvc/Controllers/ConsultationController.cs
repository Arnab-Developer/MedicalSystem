using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class ConsultationController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IOptionsMonitor<ConsultationOptions> _consultationOptionsAccessor;

        public ConsultationController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ConsultationOptions> consultationOptionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _consultationOptionsAccessor = consultationOptionsAccessor;
        }

        public async Task<IActionResult> Index()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpResponseMessage consultationApiResponseMessage = await httpClient.GetAsync(_consultationOptionsAccessor.CurrentValue.ConsultationGatewayUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                using Stream consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                IEnumerable<ConsultationModel>? consultationModels = await JsonSerializer.DeserializeAsync<IEnumerable<ConsultationModel>>(consultationApiResponseStream);
                return View(consultationModels);
            }
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using Stream consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                ErrorModel? errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(consultationApiResponseStream);
                ViewData["ErrorReason"] = errorModel!.Reason;
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
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var consultationGetByIdGatewayUrl = $"{_consultationOptionsAccessor.CurrentValue.ConsultationGatewayUrl}/{id}";
            HttpResponseMessage consultationApiResponseMessage = await httpClient.GetAsync(consultationGetByIdGatewayUrl);
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using Stream consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                ConsultationModel? consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationApiResponseStream);
                return View(consultationModel);
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Create()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();

            HttpResponseMessage consultationAddEditInitResponseMessage =
                await httpClient.GetAsync(_consultationOptionsAccessor.CurrentValue.ConsultationGatewayAddEditInitDataUrl);
            if (consultationAddEditInitResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)consultationAddEditInitResponseMessage.StatusCode);
            }

            using Stream consultationAddEditInitResponseStream =
                await consultationAddEditInitResponseMessage.Content.ReadAsStreamAsync();
            ConsultationModel? consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationAddEditInitResponseStream);

            if (consultationModel == null ||
                consultationModel.Doctors == null ||
                consultationModel.Patients == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!(consultationModel!.Doctors!.Any() && consultationModel!.Patients!.Any()))
            {
                return RedirectToAction(nameof(Index));
            }

            var doctorListItems = new List<SelectListItem>();
            foreach (DoctorModel doctorModel in consultationModel.Doctors!)
            {
                var doctorListItem = new SelectListItem(
                    $"{doctorModel.FirstName} {doctorModel.LastName}",
                    doctorModel.Id.ToString());
                doctorListItems.Add(doctorListItem);
            }

            var patientListItems = new List<SelectListItem>();
            foreach (PatientModel patientModel in consultationModel.Patients!)
            {
                var patientListItem = new SelectListItem(
                    $"{patientModel.FirstName} {patientModel.LastName}",
                    patientModel.Id.ToString());
                patientListItems.Add(patientListItem);
            }

            consultationModel.DoctorSelectList = doctorListItems;
            consultationModel.PatientSelectList = patientListItems;
            return View(consultationModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConsultationModel consultation)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), Encoding.UTF8, "application/json");
            HttpResponseMessage consultationApiResponseMessage = await httpClient.PostAsync(_consultationOptionsAccessor.CurrentValue.ConsultationGatewayUrl, consultationContent);
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
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var consultationGetByIdGatewayUrl = $"{_consultationOptionsAccessor.CurrentValue.ConsultationGatewayUrl}/{id}";
            HttpResponseMessage consultationApiResponseMessage = await httpClient.GetAsync(consultationGetByIdGatewayUrl);
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using Stream consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                ConsultationModel? consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationApiResponseStream);

                HttpResponseMessage consultationAddEditInitResponseMessage =
                    await httpClient.GetAsync(_consultationOptionsAccessor.CurrentValue.ConsultationGatewayAddEditInitDataUrl);
                if (consultationAddEditInitResponseMessage.StatusCode != HttpStatusCode.OK)
                {
                    return StatusCode((int)consultationAddEditInitResponseMessage.StatusCode);
                }

                using Stream consultationAddEditInitResponseStream =
                    await consultationAddEditInitResponseMessage.Content.ReadAsStreamAsync();
                ConsultationModel? consultationModelAddEditInit = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationAddEditInitResponseStream);

                if (consultationModelAddEditInit == null ||
                    consultationModelAddEditInit.Doctors == null ||
                    consultationModelAddEditInit.Patients == null ||
                    consultationModel == null)
                {
                    return RedirectToAction(nameof(Index));
                }

                if (!(consultationModelAddEditInit.Doctors.Any() && consultationModelAddEditInit.Patients.Any()))
                {
                    return RedirectToAction(nameof(Index));
                }

                var doctorListItems = new List<SelectListItem>();
                foreach (DoctorModel doctorModel in consultationModelAddEditInit.Doctors!)
                {
                    var doctorListItem = new SelectListItem(
                        $"{doctorModel.FirstName} {doctorModel.LastName}",
                        doctorModel.Id.ToString());
                    doctorListItems.Add(doctorListItem);
                }

                var patientListItems = new List<SelectListItem>();
                foreach (PatientModel patientModel in consultationModelAddEditInit.Patients!)
                {
                    var patientListItem = new SelectListItem(
                        $"{patientModel.FirstName} {patientModel.LastName}",
                        patientModel.Id.ToString());
                    patientListItems.Add(patientListItem);
                }

                consultationModel!.DoctorSelectList = doctorListItems;
                consultationModel.PatientSelectList = patientListItems;
                return View(consultationModel);
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConsultationModel consultation)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var consultationUpdateGatewayUrl = $"{_consultationOptionsAccessor.CurrentValue.ConsultationGatewayUrl}/{id}";
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), Encoding.UTF8, "application/json");
            HttpResponseMessage consultationApiResponseMessage = await httpClient.PutAsync(consultationUpdateGatewayUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            var consultationDeleteGatewayUrl = $"{_consultationOptionsAccessor.CurrentValue.ConsultationGatewayUrl}/{id}";
            HttpResponseMessage consultationApiResponseMessage = await httpClient.DeleteAsync(consultationDeleteGatewayUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)consultationApiResponseMessage.StatusCode);
        }
    }
}
