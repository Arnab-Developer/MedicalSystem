using MedicalSystem.Services.Patient.Controllers;
using MedicalSystem.Services.Patient.Data;
using MedicalSystem.Services.Patient.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Patient
{
    /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/patientControllerTests/*'/>
    internal class PatientControllerTests
    {
        private PatientContext? _patientContext;
        private PatientController? _patientController;

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PatientContext>()
                .UseInMemoryDatabase("PatientTestDb")
                .Options;
            _patientContext = new PatientContext(options);
            _patientController = new PatientController(_patientContext);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenValidDbData_ReturnsValidModels/*'/>
        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidModels()
        {
            AddPatients();
            var patientModels = _patientController!.GetAll().ToList();

            Assert.AreEqual(2, patientModels.Count);

            Assert.AreEqual(1, patientModels[0].Id);
            Assert.AreEqual("pat1first", patientModels[0].FirstName);
            Assert.AreEqual("pat1last", patientModels[0].LastName);

            Assert.AreEqual(2, patientModels[1].Id);
            Assert.AreEqual("pat2first", patientModels[1].FirstName);
            Assert.AreEqual("pat2last", patientModels[1].LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getAll_GivenEmptyDbData_ReturnsEmptyModels/*'/>
        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyModels()
        {
            var patientModels = _patientController!.GetAll().ToList();
            Assert.Zero(patientModels.Count);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getById_GivenValidDbData_ReturnsValidModel/*'/>
        [Test]
        public void GetById_GivenValidDbData_ReturnsValidModel()
        {
            AddPatients();
            var patientModel = _patientController!.GetById(2);

            Assert.AreEqual(2, patientModel.Id);
            Assert.AreEqual("pat2first", patientModel.FirstName);
            Assert.AreEqual("pat2last", patientModel.LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/getById_GivenEmptyDbData_ReturnsNull/*'/>
        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            var patientModel = _patientController!.GetById(2);
            Assert.Null(patientModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/add_CanInsertInDb/*'/>
        [Test]
        public void Add_CanInsertInDb()
        {
            var patient = new PatientModel()
            {
                Id = 1,
                FirstName = "pat1first",
                LastName = "pat1last"
            };
            _patientController!.Add(patient);

            var patientModel = _patientContext!.Patients.FirstOrDefault(patient => patient.Id == 1);

            Assert.AreEqual(1, patientModel.Id);
            Assert.AreEqual("pat1first", patientModel.FirstName);
            Assert.AreEqual("pat1last", patientModel.LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/update_CanUpdateInDb/*'/>
        [Test]
        public void Update_CanUpdateInDb()
        {
            AddPatients();

            var patientModel = _patientContext!.Patients.FirstOrDefault(patient => patient.Id == 2);
            patientModel.FirstName = "update";
            _patientController!.Update(2, patientModel);

            var patientModelNew = _patientContext!.Patients.FirstOrDefault(patient => patient.Id == 2);

            Assert.AreEqual(2, patientModelNew.Id);
            Assert.AreEqual("update", patientModelNew.FirstName);
            Assert.AreEqual("pat2last", patientModelNew.LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/delete_CanDeleteInDb/*'/>
        [Test]
        public void Delete_CanDeleteInDb()
        {
            AddPatients();
            var patientModel = _patientContext!.Patients.FirstOrDefault(patient => patient.Id == 2);
            _patientController!.Delete(2);
            Assert.AreEqual(1, _patientContext.Patients.Count());
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientControllerTests"]/cleanup/*'/>
        [TearDown]
        public void Cleanup()
        {
            _patientContext!.Patients!.RemoveRange(_patientContext.Patients);
            _patientContext.SaveChanges();
        }

        private void AddPatients()
        {
            var patients = new List<PatientModel>()
            {
                new PatientModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                },
                new PatientModel()
                {
                    Id = 2,
                    FirstName = "pat2first",
                    LastName = "pat2last"
                }
            };
            _patientContext!.Patients!.AddRange(patients);
            _patientContext.SaveChanges();
        }
    }
}