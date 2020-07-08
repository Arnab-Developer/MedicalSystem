using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class PatientController
    {
        private readonly IPatientService _patientService;

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientControllerConstructor/*'/>
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getAll/*'/>
        public IEnumerable<PatientViewModel> GetAll()
        {
            IEnumerable<PatientViewModel> patientViewModels = _patientService.GetAll();
            return patientViewModels;
        }
    }
}
