using MedicalSystem.Services.Doctor.Data;
using MedicalSystem.Services.Doctor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Doctor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DoctorController
    {
        private readonly DoctorContext _doctorContext;

        public DoctorController(DoctorContext doctorContext)
        {
            _doctorContext = doctorContext;
        }

        [HttpGet]
        public IEnumerable<DoctorModel> GetAllDoctors()
        {
            var doctors = _doctorContext.Doctors.OrderBy(doctor => doctor.FirstName);
            return doctors;
        }

        [HttpGet]
        [Route("{id:int}")]
        public DoctorModel GetById(int id)
        {
            var doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            return doctor;
        }

        [HttpPost]
        public void Add(DoctorModel doctor)
        {
            _doctorContext.Doctors.Add(doctor);
            _doctorContext.SaveChanges();
        }

        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, DoctorModel doctor)
        {
            var d = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);

            d.FirstName = doctor.FirstName;
            d.LastName = doctor.LastName;

            _doctorContext.SaveChanges();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            _doctorContext.Doctors.Remove(doctor);
            _doctorContext.SaveChanges();
        }
    }
}
