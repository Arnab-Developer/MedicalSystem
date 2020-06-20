using MedicalSystem.Services.Patient.Data;
using MedicalSystem.Services.Patient.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Patient.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class PatientController
    {
        private readonly PatientContext _patientContext;

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientControllerConstructor/*'/>
        public PatientController(PatientContext patientContext)
        {
            _patientContext = patientContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getAll/*'/>
        [HttpGet]
        public IEnumerable<PatientModel> GetAll()
        {
            var patients = _patientContext.Patients.OrderBy(patient => patient.FirstName);
            return patients;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public PatientModel GetById(int id)
        {
            var patient = _patientContext.Patients.FirstOrDefault(patient => patient.Id == id);
            return patient;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/add/*'/>
        [HttpPost]
        public void Add(PatientModel patient)
        {
            _patientContext.Patients!.Add(patient);
            _patientContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, PatientModel patient)
        {
            var d = _patientContext.Patients.FirstOrDefault(patient => patient.Id == id);

            d.FirstName = patient.FirstName;
            d.LastName = patient.LastName;

            _patientContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var patient = _patientContext.Patients.FirstOrDefault(patient => patient.Id == id);
            _patientContext.Patients!.Remove(patient);
            _patientContext.SaveChanges();
        }
    }
}
