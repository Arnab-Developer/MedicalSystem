using MedicalSystem.Services.Doctor.Data;
using MedicalSystem.Services.Doctor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Doctor.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class DoctorController
    {
        private readonly DoctorContext _doctorContext;

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorControllerConstructor/*'/>
        public DoctorController(DoctorContext doctorContext)
        {
            _doctorContext = doctorContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getAll/*'/>
        [HttpGet]
        public IEnumerable<DoctorModel> GetAll()
        {
            IOrderedQueryable<DoctorModel> doctors = _doctorContext.Doctors.OrderBy(doctor => doctor.FirstName);
            return doctors;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public DoctorModel GetById(int id)
        {
            DoctorModel doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            return doctor;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/add/*'/>
        [HttpPost]
        public void Add(DoctorModel doctor)
        {
            _doctorContext.Doctors!.Add(doctor);
            _doctorContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, DoctorModel doctor)
        {
            DoctorModel d = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);

            d.FirstName = doctor.FirstName;
            d.LastName = doctor.LastName;

            _doctorContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            DoctorModel doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            _doctorContext.Doctors!.Remove(doctor);
            _doctorContext.SaveChanges();
        }
    }
}
