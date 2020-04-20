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
    public class ConsultationController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ConsultationOptions _consultationOptions;

        public ConsultationController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<ConsultationOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _consultationOptions = optionsAccessor.CurrentValue;
        }

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
                    return NotFound();
                }
                return Ok(consultationModels);
            }
            return NotFound();
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ConsultationModel>> GetById(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var httpClient = _httpClientFactory.CreateClient();
            var consultationGetByIdUrl = $"{_consultationOptions.ConsultationApiUrl}/{id}";
            var consultationApiResponseMessage = await httpClient.GetAsync(consultationGetByIdUrl);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                using var consultationApiResponseStream = await consultationApiResponseMessage.Content.ReadAsStreamAsync();
                var consultationModel = await JsonSerializer.DeserializeAsync<ConsultationModel>(consultationApiResponseStream);
                if (consultationModel == null)
                {
                    return NotFound();
                }
                return Ok(consultationModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add(ConsultationModel consultation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), System.Text.Encoding.UTF8, "application/json");
            var consultationApiResponseMessage = await httpClient.PostAsync(_consultationOptions.ConsultationApiUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, ConsultationModel consultation)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var consultationUpdateUrl = $"{_consultationOptions.ConsultationApiUrl}/{id}";
            var consultationContent = new StringContent(JsonSerializer.Serialize(consultation), System.Text.Encoding.UTF8, "application/json");
            var consultationApiResponseMessage = await httpClient.PutAsync(consultationUpdateUrl, consultationContent);
            if (consultationApiResponseMessage.IsSuccessStatusCode)
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