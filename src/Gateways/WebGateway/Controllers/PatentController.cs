using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatentController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PatentOptions _patentOptions;

        public PatentController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<PatentOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _patentOptions = optionsAccessor.CurrentValue;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatentModel>>> GetAll()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentApiResponseMessage = await httpClient.GetAsync(_patentOptions.PatentApiUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatentModel>>(patentApiResponseStream);
                if (patentModels == null || patentModels.Count() == 0)
                {
                    return NotFound();
                }
                return Ok(patentModels);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PatentModel>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patentGetByIdUrl = $"{_patentOptions.PatentApiUrl}/{id}";
            var patentApiResponseMessage = await httpClient.GetAsync(patentGetByIdUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModel = await JsonSerializer.DeserializeAsync<PatentModel>(patentApiResponseStream);
                if (patentModel == null)
                {
                    return NotFound();
                }
                return Ok(patentModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(PatentModel patent)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentContent = new StringContent(JsonSerializer.Serialize(patent), System.Text.Encoding.UTF8, "application/json");
            var patentApiResponseMessage = await httpClient.PostAsync(_patentOptions.PatentApiUrl, patentContent);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, PatentModel patent)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentUpdateUrl = $"{_patentOptions.PatentApiUrl}/{id}";
            var patentContent = new StringContent(JsonSerializer.Serialize(patent), System.Text.Encoding.UTF8, "application/json");
            var patentApiResponseMessage = await httpClient.PutAsync(patentUpdateUrl, patentContent);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patentDeleteUrl = $"{_patentOptions.PatentApiUrl}/{id}";
            var patentApiResponseMessage = await httpClient.DeleteAsync(patentDeleteUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }
    }
}
