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
    /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
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

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getAll/*'/>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorModel>>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorApiResponseMessage = await httpClient.GetAsync(_doctorOptions.DoctorApiUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);
                if (doctorModels == null || doctorModels.Count() == 0)
                {
                    var error = new ErrorModel("No doctor record found.");
                    return NotFound(error);
                }
                return Ok(doctorModels);
            }
            return StatusCode((int)doctorApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<DoctorModel>> GetById(int? id)
        {
            if (id == null)
            {
                var error = new ErrorModel("Id is null");
                return NotFound(error);
            }
            var httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdUrl = $"{_doctorOptions.DoctorApiUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdUrl);
            if (doctorApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                return Ok(doctorModel);
            }
            else
            {
                return StatusCode((int)doctorApiResponseMessage.StatusCode);
            }
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/add/*'/>
        [HttpPost]
        public async Task<IActionResult> Add(DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PostAsync(_doctorOptions.DoctorApiUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorUpdateUrl = $"{_doctorOptions.DoctorApiUrl}/{id}";
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PutAsync(doctorUpdateUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var doctorDeleteUrl = $"{_doctorOptions.DoctorApiUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.DeleteAsync(doctorDeleteUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
