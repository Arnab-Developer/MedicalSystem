﻿using MedicalSystem.Gateways.WebGateway.Models;
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
    public class DoctorController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DoctorOptions _doctorOptions;

        public DoctorController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _doctorOptions = optionsAccessor.CurrentValue;
        }

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
                    return NotFound();
                }
                return Ok(doctorModels);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<DoctorModel>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdUrl = $"{_doctorOptions.DoctorApiUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                if (doctorModel == null)
                {
                    return NotFound();
                }
                return Ok(doctorModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), System.Text.Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PostAsync(_doctorOptions.DoctorApiUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorUpdateUrl = $"{_doctorOptions.DoctorApiUrl}/{id}";
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), System.Text.Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PutAsync(doctorUpdateUrl, doctorContent);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
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