﻿using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly DoctorOptions _doctorOptions;

        public DoctorController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<DoctorOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _doctorOptions = optionsAccessor.CurrentValue;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorApiResponseMessage = await httpClient.GetAsync(_doctorOptions.DoctorGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);
                if (doctorModels == null || doctorModels.Count() == 0)
                {
                    return NotFound();
                }
                return View(doctorModels);
            }
            else
            {
                return NotFound();
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                if (doctorModel == null)
                {
                    return NotFound();
                }
                return View(doctorModel);
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), System.Text.Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PostAsync(_doctorOptions.DoctorGatewayUrl, doctorContent);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorGetByIdGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.GetAsync(doctorGetByIdGatewayUrl);
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                var doctorModel = await JsonSerializer.DeserializeAsync<DoctorModel>(doctorApiResponseStream);
                if (doctorModel == null)
                {
                    return NotFound();
                }
                return View(doctorModel);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, DoctorModel doctor)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorUpdateGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorContent = new StringContent(JsonSerializer.Serialize(doctor), System.Text.Encoding.UTF8, "application/json");
            var doctorApiResponseMessage = await httpClient.PutAsync(doctorUpdateGatewayUrl, doctorContent);
            return RedirectToAction(nameof(Details), new { id });
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var doctorDeleteGatewayUrl = $"{_doctorOptions.DoctorGatewayUrl}/{id}";
            var doctorApiResponseMessage = await httpClient.DeleteAsync(doctorDeleteGatewayUrl);
            return RedirectToAction(nameof(Index));
        }
    }
}