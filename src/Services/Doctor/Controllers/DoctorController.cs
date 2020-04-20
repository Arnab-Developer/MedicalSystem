using MedicalSystem.Services.Doctor.Data;
using MedicalSystem.Services.Doctor.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Doctor.Controllers
{
    /// <summary>
    /// Controller for doctor.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class DoctorController
    {
        private readonly DoctorContext _doctorContext;

        /// <summary>
        /// Creates new doctor controller object.
        /// </summary>
        /// <param name="doctorContext"></param>
        public DoctorController(DoctorContext doctorContext)
        {
            _doctorContext = doctorContext;
        }

        /// <summary>
        /// Get all doctor data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<DoctorModel> GetAll()
        {
            var doctors = _doctorContext.Doctors.OrderBy(doctor => doctor.FirstName);
            return doctors;
        }

        /// <summary>
        /// Get doctor data by id.
        /// </summary>
        /// <param name="id">Takes doctor id to search.</param>
        /// <returns>Return doctor data.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public DoctorModel GetById(int id)
        {
            var doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            return doctor;
        }

        /// <summary>
        /// Add new doctor object.
        /// </summary>
        /// <param name="doctor">Takes new doctor object to add.</param>
        [HttpPost]
        public void Add(DoctorModel doctor)
        {
            _doctorContext.Doctors!.Add(doctor);
            _doctorContext.SaveChanges();
        }

        /// <summary>
        /// Update existing doctor object.
        /// </summary>
        /// <param name="id">Takes doctor id to locate the existing doctor object.</param>
        /// <param name="doctor">Takes updated doctor object to update.</param>
        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, DoctorModel doctor)
        {
            var d = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);

            d.FirstName = doctor.FirstName;
            d.LastName = doctor.LastName;

            _doctorContext.SaveChanges();
        }

        /// <summary>
        /// Delete existing doctor object.
        /// </summary>
        /// <param name="id">Takes doctor id to locate and delete the existing doctor object.</param>
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var doctor = _doctorContext.Doctors.FirstOrDefault(doctor => doctor.Id == id);
            _doctorContext.Doctors!.Remove(doctor);
            _doctorContext.SaveChanges();
        }
    }
}
