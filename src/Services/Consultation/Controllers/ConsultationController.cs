using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController
    {
        private readonly IConsultationService _consultationService;

        public ConsultationController(IConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

        [HttpGet]
        public IEnumerable<ConsultationViewModel> GetAll()
        {
            var consultationViewModels = _consultationService.GetAll();
            return consultationViewModels;
        }

        [HttpGet]
        [Route("{id:int}")]
        public ConsultationViewModel? GetById(int id)
        {
            var consultationViewModel = _consultationService.GetById(id);
            return consultationViewModel;
        }

        [HttpPost]
        public void Add(ConsultationViewModel consultationViewModel)
        {
            _consultationService.Add(consultationViewModel);
        }

        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, ConsultationViewModel consultationViewModel)
        {
            _consultationService.Update(id, consultationViewModel);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _consultationService.Delete(id);
        }
    }
}
