using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatentController
    {
        private readonly IPatentService _patentService;

        public PatentController(IPatentService patentService)
        {
            _patentService = patentService;
        }

        public IEnumerable<PatentViewModel> GetAll()
        {
            var patentViewModels = _patentService.GetAll();
            return patentViewModels;
        }
    }
}
