using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
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
            IEnumerable<DoctorModel> doctorModels;
            if (doctorApiResponseMessage.IsSuccessStatusCode)
            {
                using var doctorApiResponseStream = await doctorApiResponseMessage.Content.ReadAsStreamAsync();
                doctorModels = await JsonSerializer.DeserializeAsync<IEnumerable<DoctorModel>>(doctorApiResponseStream);
            }
            else
            {
                doctorModels = new List<DoctorModel>();
            }
            return View(doctorModels);
        }
    }
}
