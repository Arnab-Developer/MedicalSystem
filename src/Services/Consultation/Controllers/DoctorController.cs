using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public IEnumerable<DoctorViewModel> GetAll()
        {
            var doctorViewModels = _doctorService.GetAll();
            return doctorViewModels;
        }
    }
}
