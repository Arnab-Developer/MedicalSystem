using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class DoctorController
    {
        private readonly IDoctorService _doctorService;

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorControllerConstructor/*'/>
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getAll/*'/>
        public IEnumerable<DoctorViewModel> GetAll()
        {
            IEnumerable<DoctorViewModel> doctorViewModels = _doctorService.GetAll();
            return doctorViewModels;
        }
    }
}
