using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatentController"]/patentController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class PatentController
    {
        private readonly IPatentService _patentService;

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/patentControllerConstructor/*'/>
        public PatentController(IPatentService patentService)
        {
            _patentService = patentService;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/getAll/*'/>
        public IEnumerable<PatentViewModel> GetAll()
        {
            var patentViewModels = _patentService.GetAll();
            return patentViewModels;
        }
    }
}
