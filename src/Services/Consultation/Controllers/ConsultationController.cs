using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController
    {
        private readonly IConsultationService _consultationService;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationControllerConstructor/*'/>
        public ConsultationController(IConsultationService consultationService)
        {
            _consultationService = consultationService;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getAll/*'/>
        [HttpGet]
        public IEnumerable<ConsultationViewModel> GetAll()
        {
            var consultationViewModels = _consultationService.GetAll();
            return consultationViewModels;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public ConsultationViewModel? GetById(int id)
        {
            var consultationViewModel = _consultationService.GetById(id);
            return consultationViewModel;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/add/*'/>
        [HttpPost]
        public void Add(ConsultationViewModel consultationViewModel)
        {
            _consultationService.Add(consultationViewModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, ConsultationViewModel consultationViewModel)
        {
            _consultationService.Update(id, consultationViewModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            _consultationService.Delete(id);
        }
    }
}
