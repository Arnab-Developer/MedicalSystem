using MedicalSystem.Services.Consultation.Data;
using MedicalSystem.Services.Consultation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController
    {
        private readonly ConsultationContext _consultationContext;

        public ConsultationController(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        [HttpGet]
        public IEnumerable<ConsultationModel> GetAll()
        {
            var consultations = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .OrderByDescending(consultation => consultation.Date);
            return consultations.ToList();
        }

        [HttpGet]
        [Route("{id:int}")]
        public ConsultationModel GetById(int id)
        {
            var consultation = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .FirstOrDefault(consultation => consultation.Id == id);
            return consultation;
        }

        [HttpPost]
        public void Add(ConsultationModel consultation)
        {
            var c = new ConsultationModel()
            {
                Id = consultation.Id,
                Date = consultation.Date,
                Place = consultation.Place,
                Problem = consultation.Problem,
                Medicine = consultation.Medicine,
                DoctorId = consultation.DoctorId,
                PatentId = consultation.PatentId
            };
            _consultationContext.Consultations.Add(c);
            _consultationContext.SaveChanges();
        }

        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, ConsultationModel consultation)
        {
            var c = _consultationContext.Consultations
                .FirstOrDefault(consultation => consultation.Id == id);

            c.Date = consultation.Date;
            c.Place = consultation.Place;
            c.Problem = consultation.Problem;
            c.Medicine = consultation.Medicine;
            c.DoctorId = consultation.DoctorId;
            c.PatentId = consultation.PatentId;

            _consultationContext.SaveChanges();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var consultation = _consultationContext.Consultations
                .FirstOrDefault(consultation => consultation.Id == id);
            _consultationContext.Consultations.Remove(consultation);
            _consultationContext.SaveChanges();
        }
    }
}
