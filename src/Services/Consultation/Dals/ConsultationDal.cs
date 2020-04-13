using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    internal class ConsultationDal : IConsultationDal
    {
        private readonly ConsultationContext _consultationContext;

        public ConsultationDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        public IEnumerable<ConsultationDomainModel> GetAll()
        {
            var consultationDomainModels = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .OrderByDescending(consultation => consultation.Date);

            return consultationDomainModels;
        }

        public ConsultationDomainModel? GetById(int id)
        {
            var consultationDomainModel = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .FirstOrDefault(consultation => consultation.Id == id);

            return consultationDomainModel;
        }

        public void Add(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.Consultations!.Add(consultationDomainModel);
            _consultationContext.SaveChanges();
        }

        public void Update(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.SaveChanges();
        }

        public void Delete(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.Consultations!.Remove(consultationDomainModel);
            _consultationContext.SaveChanges();
        }
    }
}
