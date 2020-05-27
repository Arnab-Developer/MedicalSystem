using MedicalSystem.FrontEnds.WebMvc.Models;
using MedicalSystem.FrontEnds.WebMvc.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MedicalSystem.FrontEnds.WebMvc.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatentController"]/patentController/*'/>
    public class PatentController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly PatentOptions _patentOptions;

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/patentControllerConstructor/*'/>
        public PatentController(IHttpClientFactory httpClientFactory,
            IOptionsMonitor<PatentOptions> optionsAccessor)
        {
            _httpClientFactory = httpClientFactory;
            _patentOptions = optionsAccessor.CurrentValue;
        }


        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/index/*'/>
        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentApiResponseMessage = await httpClient.GetAsync(_patentOptions.PatentGatewayUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModels = await JsonSerializer.DeserializeAsync<IEnumerable<PatentModel>>(patentApiResponseStream);
                return View(patentModels);
            }
            if (patentApiResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var errorModel = await JsonSerializer.DeserializeAsync<ErrorModel>(patentApiResponseStream);
                ViewData["ErrorReason"] = errorModel.Reason;
                return View();
            }
            return StatusCode((int)patentApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/details/*'/>
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patentGetByIdGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentApiResponseMessage = await httpClient.GetAsync(patentGetByIdGatewayUrl);
            if (patentApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModel = await JsonSerializer.DeserializeAsync<PatentModel>(patentApiResponseStream);
                return View(patentModel);
            }
            return StatusCode((int)patentApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/createGet/*'/>
        public IActionResult Create()
        {
            return View();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/createPost/*'/>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatentModel patent)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentContent = new StringContent(JsonSerializer.Serialize(patent), Encoding.UTF8, "application/json");
            var patentApiResponseMessage = await httpClient.PostAsync(_patentOptions.PatentGatewayUrl, patentContent);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)patentApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/editGet/*'/>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var httpClient = _httpClientFactory.CreateClient();
            var patentGetByIdGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentApiResponseMessage = await httpClient.GetAsync(patentGetByIdGatewayUrl);
            if (patentApiResponseMessage.StatusCode == HttpStatusCode.OK)
            {
                using var patentApiResponseStream = await patentApiResponseMessage.Content.ReadAsStreamAsync();
                var patentModel = await JsonSerializer.DeserializeAsync<PatentModel>(patentApiResponseStream);
                return View(patentModel);
            }
            return StatusCode((int)patentApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/editPost/*'/>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatentModel patent)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentUpdateGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentContent = new StringContent(JsonSerializer.Serialize(patent), Encoding.UTF8, "application/json");
            var patentApiResponseMessage = await httpClient.PutAsync(patentUpdateGatewayUrl, patentContent);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Details), new { id });
            }
            return StatusCode((int)patentApiResponseMessage.StatusCode);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/delete/*'/>
        public async Task<IActionResult> Delete(int? id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var patentDeleteGatewayUrl = $"{_patentOptions.PatentGatewayUrl}/{id}";
            var patentApiResponseMessage = await httpClient.DeleteAsync(patentDeleteGatewayUrl);
            if (patentApiResponseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            return StatusCode((int)patentApiResponseMessage.StatusCode);
        }
    }
}
