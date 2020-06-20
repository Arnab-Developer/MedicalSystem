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
    /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
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

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getAll/*'/>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientApiResponseMessage = await httpClient.GetAsync(_patientOptions.PatientApiUrl);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                var patientModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatientModel>>(patientApiResponseStream);
                if (patientModels == null || patientModels.Count() == 0)
                {
                    var error = new ErrorModel("No doctor record found.");
                    return NotFound(error);
                }
                return Ok(patientModels);
            }
            return NotFound();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PatientModel>> GetById(int? id)
        {
            if (id == null)
            {
                var error = new ErrorModel("Id is null");
                return NotFound(error);
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patientGetByIdUrl = $"{_patientOptions.PatientApiUrl}/{id}";
            var patientApiResponseMessage = await httpClient.GetAsync(patientGetByIdUrl);
            if (patientApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var patientApiResponseStream = await patientApiResponseMessage.Content.ReadAsStreamAsync();
                var patientModel = await JsonSerializer.DeserializeAsync<PatientModel>(patientApiResponseStream);
                return Ok(patientModel);
            }
            else
            {
                return StatusCode((int)patientApiResponseMessage.StatusCode);
            }
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/add/*'/>
        [HttpPost]
        public async Task<IActionResult> Add(PatientModel patient)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");
            var patientApiResponseMessage = await httpClient.PostAsync(_patientOptions.PatientApiUrl, patientContent);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, PatientModel patient)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patientUpdateUrl = $"{_patientOptions.PatientApiUrl}/{id}";
            var patientContent = new StringContent(JsonSerializer.Serialize(patient), Encoding.UTF8, "application/json");
            var patientApiResponseMessage = await httpClient.PutAsync(patientUpdateUrl, patientContent);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patientDeleteUrl = $"{_patientOptions.PatientApiUrl}/{id}";
            var patientApiResponseMessage = await httpClient.DeleteAsync(patientDeleteUrl);
            if (patientApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
