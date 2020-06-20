using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConsultationOptions _consultationOptions;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationControllerConstructor/*'/>
        public ConsultationController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _consultationOptions = optionsAccessor.CurrentValue;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getAll/*'/>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationModel>>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationApiResponseMessage = await httpClient.GetAsync(_consultationOptions.ConsultationApiUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var consultationModels = await JsonSerializer.DeserializeAsync<IEnumerable<ConsultationModel>>(consultationApiResponseStream);
                if (consultationModels == null || consultationModels.Count() == 0)
                {
                    var error = new ErrorModel("No doctor record found.");
                    return NotFound(error);
                }
                return Ok(consultationModels);
            }
            return NotFound();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ConsultationModel>> GetById(int? id)
        {
            if (id == null)
            {
                var error = new ErrorModel("Id is null");
                return NotFound(error);
            }
            var httpClient = _httpClientFactory.CreateClient();
            var consultationGetByIdUrl = $"{_consultationOptions.ConsultationApiUrl}/{id}";
            var consultationApiResponseMessage = await httpClient.GetAsync(consultationGetByIdUrl);
            if (consultationApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationApiResponseStream);
                return Ok(consultationModel);
            }
            else
            {
                return StatusCode((int)consultationApiResponseMessage.StatusCode);
            }
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getAddEditInitData/*'/>
        [HttpGet]
        [Route("AddEditInitData")]
        public async Task<ActionResult<ConsultationModel>> GetAddEditInitData()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var doctorApiResponseMessage = await httpClient.GetAsync(_consultationOptions.ConsultationDoctorApiUrl);
            IEnumerable<DoctorModel>? doctorModels;
            if (doctorApiResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)doctorApiResponseMessage.StatusCode);
            }

            var patientApiResponseMessage = await httpClient.GetAsync(_consultationOptions.ConsultationPatientApiUrl);
            if (patientApiResponseMessage.StatusCode != HttpStatusCode.OK)
            {
                return StatusCode((int)patientApiResponseMessage.StatusCode);
            }

            using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
            doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);

            using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
            var patientModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatientModel>>(patientApiResponseStream);

            var consultationModel = new ConsultationModel()
            {
                Doctors = doctorModels,
                Patients = patientModels
            };
            return Ok(consultationModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/add/*'/>
        [HttpPost]
        public async Task<IActionResult> Add(ConsultationModel consultation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), Encoding.UTF8, "application/json");
            var consultationApiResponseMessage = await httpClient.PostAsync(_consultationOptions.ConsultationApiUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, ConsultationModel consultation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationUpdateUrl = $"{_consultationOptions.ConsultationApiUrl}/{id}";
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), Encoding.UTF8, "application/json");
            var consultationApiResponseMessage = await httpClient.PutAsync(consultationUpdateUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var consultationDeleteUrl = $"{_consultationOptions.ConsultationApiUrl}/{id}";
            var consultationApiResponseMessage = await httpClient.DeleteAsync(consultationDeleteUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
