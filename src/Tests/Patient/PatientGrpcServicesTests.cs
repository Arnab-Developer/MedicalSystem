using Grpc.Core;
using MedicalSystem.Services.Patient.Api.Data;
using MedicalSystem.Services.Patient.Api.GrpcServices;
using MedicalSystem.Services.Patient.Api.Models;
using MedicalSystem.Services.Patient.Api.Protos;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Patient
{
    internal class PatientGrpcServicesTests
    {
        private PatientContext? _patientContext;
        private PatientService? _patientController;
        private Mock<ServerCallContext>? _serverCallContextMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<PatientContext>()
                .UseInMemoryDatabase("PatientTestDb")
                .Options;
            _patientContext = new PatientContext(options);
            _patientController = new PatientService(_patientContext);
            _serverCallContextMock = new Mock<ServerCallContext>();
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidModels()
        {
            AddPatients();
            PatientModelsMessage patientModelsMessage = _patientController!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            var patientModels = new List<PatientModel>();
            foreach (PatientModelMessage patientModelMessage in patientModelsMessage.Patients)
            {
                var patientModel = new PatientModel
                {
                    Id = patientModelMessage.Id,
                    FirstName = patientModelMessage.FirstName,
                    LastName = patientModelMessage.LastName
                };
                patientModels.Add(patientModel);
            }

            Assert.AreEqual(2, patientModels.Count);

            Assert.AreEqual(1, patientModels[0].Id);
            Assert.AreEqual("pat1first", patientModels[0].FirstName);
            Assert.AreEqual("pat1last", patientModels[0].LastName);

            Assert.AreEqual(2, patientModels[1].Id);
            Assert.AreEqual("pat2first", patientModels[1].FirstName);
            Assert.AreEqual("pat2last", patientModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyModels()
        {
            PatientModelsMessage patientModelsMessage = _patientController!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            Assert.Zero(patientModelsMessage.Patients.Count);
        }

        [Test]
        public void GetById_GivenValidDbData_ReturnsValidModel()
        {
            AddPatients();
            PatientModelMessage patientModelMessage = _patientController!.GetById(new IdMessage { Id = 2 }, _serverCallContextMock!.Object).Result;
            var patientModel = new PatientModel
            {
                Id = patientModelMessage.Id,
                FirstName = patientModelMessage.FirstName,
                LastName = patientModelMessage.LastName
            };

            Assert.AreEqual(2, patientModel.Id);
            Assert.AreEqual("pat2first", patientModel.FirstName);
            Assert.AreEqual("pat2last", patientModel.LastName);
        }

        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            PatientModelMessage patientModelMessage = _patientController!.GetById(new IdMessage { Id = 2 }, _serverCallContextMock!.Object).Result;
            Assert.Zero(patientModelMessage.Id);
            Assert.AreEqual(string.Empty, patientModelMessage.FirstName);
            Assert.AreEqual(string.Empty, patientModelMessage.LastName);
        }

        [Test]
        public void Add_CanInsertInDb()
        {
            var patient = new PatientModel()
            {
                Id = 1,
                FirstName = "pat1first",
                LastName = "pat1last"
            };
            _patientController!.Add(new PatientModelMessage { Id = patient.Id, FirstName = patient.FirstName, LastName = patient.LastName },
                _serverCallContextMock!.Object);

            PatientModel? patientModel = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == 1);

            Assert.AreEqual(1, patientModel!.Id);
            Assert.AreEqual("pat1first", patientModel.FirstName);
            Assert.AreEqual("pat1last", patientModel.LastName);
        }

        [Test]
        public void Update_CanUpdateInDb()
        {
            AddPatients();

            PatientModel? patientModel = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == 2);
            patientModel!.FirstName = "update";
            _patientController!.Update(
                new UpdateMessage
                {
                    Id = 2,
                    Patient = new PatientModelMessage
                    {
                        Id = patientModel.Id,
                        FirstName = patientModel.FirstName,
                        LastName = patientModel.LastName
                    }
                },
                _serverCallContextMock!.Object);

            PatientModel? patientModelNew = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == 2);

            Assert.AreEqual(2, patientModelNew!.Id);
            Assert.AreEqual("update", patientModelNew.FirstName);
            Assert.AreEqual("pat2last", patientModelNew.LastName);
        }

        [Test]
        public void Delete_CanDeleteInDb()
        {
            AddPatients();
            PatientModel? patientModel = _patientContext!.Patients!.FirstOrDefault(patient => patient.Id == 2);
            _patientController!.Delete(new IdMessage { Id = 2 }, _serverCallContextMock!.Object);
            Assert.AreEqual(1, _patientContext.Patients!.Count());
        }

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