using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Consultation.Dals
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/consultationDal/*'/>
    internal class ConsultationDal : IConsultationDal
    {
        private readonly ConsultationContext _consultationContext;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/consultationDalConstructor/*'/>
        public ConsultationDal(ConsultationContext consultationContext)
        {
            _consultationContext = consultationContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/getAll/*'/>
        public IEnumerable<ConsultationDomainModel> GetAll()
        {
            var consultationDomainModels = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .OrderByDescending(consultation => consultation.Date);

            return consultationDomainModels;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/getById/*'/>
        public ConsultationDomainModel? GetById(int id)
        {
            var consultationDomainModel = _consultationContext.Consultations
                .Include(consultation => consultation.Doctor)
                .Include(consultation => consultation.Patent)
                .FirstOrDefault(consultation => consultation.Id == id);

            return consultationDomainModel;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/add/*'/>
        public void Add(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.Consultations!.Add(consultationDomainModel);
            _consultationContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/update/*'/>
        public void Update(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationDal"]/delete/*'/>
        public void Delete(ConsultationDomainModel consultationDomainModel)
        {
            _consultationContext.Consultations!.Remove(consultationDomainModel);
            _consultationContext.SaveChanges();
        }
    }
}
