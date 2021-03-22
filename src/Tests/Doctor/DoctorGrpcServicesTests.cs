using Grpc.Core;
using MedicalSystem.Services.Doctor.Api.Data;
using MedicalSystem.Services.Doctor.Api.GrpcServices;
using MedicalSystem.Services.Doctor.Api.Models;
using MedicalSystem.Services.Doctor.Api.Protos;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Doctor
{
    internal class DoctorGrpcServicesTests
    {
        private DoctorContext? _doctorContext;
        private DoctorService? _doctorController;
        private Mock<ServerCallContext>? _serverCallContextMock;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<DoctorContext>()
                .UseInMemoryDatabase("DoctorTestDb")
                .Options;
            _doctorContext = new DoctorContext(options);
            _doctorController = new DoctorService(_doctorContext);
            _serverCallContextMock = new Mock<ServerCallContext>();
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidModels()
        {
            AddDoctors();
            DoctorModelsMessage doctorModelsMessage = _doctorController!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            var doctorModels = new List<DoctorModel>();
            foreach (DoctorModelMessage doctorModelMessage in doctorModelsMessage.Doctors)
            {
                var doctorModel = new DoctorModel
                {
                    Id = doctorModelMessage.Id,
                    FirstName = doctorModelMessage.FirstName,
                    LastName = doctorModelMessage.LastName
                };
                doctorModels.Add(doctorModel);
            }

            Assert.AreEqual(2, doctorModels.Count);

            Assert.AreEqual(1, doctorModels[0].Id);
            Assert.AreEqual("doc1first", doctorModels[0].FirstName);
            Assert.AreEqual("doc1last", doctorModels[0].LastName);

            Assert.AreEqual(2, doctorModels[1].Id);
            Assert.AreEqual("doc2first", doctorModels[1].FirstName);
            Assert.AreEqual("doc2last", doctorModels[1].LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyModels()
        {
            DoctorModelsMessage doctorModelsMessage = _doctorController!.GetAll(new EmptyMessage(), _serverCallContextMock!.Object).Result;
            Assert.Zero(doctorModelsMessage.Doctors.Count);
        }

        [Test]
        public void GetById_GivenValidDbData_ReturnsValidModel()
        {
            AddDoctors();
            DoctorModelMessage doctorModelMessage = _doctorController!.GetById(new IdMessage { Id = 2 }, _serverCallContextMock!.Object).Result;
            var doctorModel = new DoctorModel
            {
                Id = doctorModelMessage.Id,
                FirstName = doctorModelMessage.FirstName,
                LastName = doctorModelMessage.LastName
            };

            Assert.AreEqual(2, doctorModel.Id);
            Assert.AreEqual("doc2first", doctorModel.FirstName);
            Assert.AreEqual("doc2last", doctorModel.LastName);
        }

        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            DoctorModelMessage doctorModelMessage = _doctorController!.GetById(new IdMessage { Id = 2 }, _serverCallContextMock!.Object).Result;
            Assert.Zero(doctorModelMessage.Id);
            Assert.AreEqual(string.Empty, doctorModelMessage.FirstName);
            Assert.AreEqual(string.Empty, doctorModelMessage.LastName);
        }

        [Test]
        public void Add_CanInsertInDb()
        {
            var doctor = new DoctorModel()
            {
                Id = 1,
                FirstName = "doc1first",
                LastName = "doc1last"
            };
            _doctorController!.Add(new DoctorModelMessage { Id = doctor.Id, FirstName = doctor.FirstName, LastName = doctor.LastName },
                _serverCallContextMock!.Object);

            DoctorModel? doctorModel = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == 1);

            Assert.AreEqual(1, doctorModel!.Id);
            Assert.AreEqual("doc1first", doctorModel.FirstName);
            Assert.AreEqual("doc1last", doctorModel.LastName);
        }

        [Test]
        public void Update_CanUpdateInDb()
        {
            AddDoctors();

            DoctorModel? doctorModel = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == 2);
            doctorModel!.FirstName = "update";
            _doctorController!.Update(
                new UpdateMessage
                {
                    Id = 2,
                    Doctor = new DoctorModelMessage
                    {
                        Id = doctorModel.Id,
                        FirstName = doctorModel.FirstName,
                        LastName = doctorModel.LastName
                    }
                },
                _serverCallContextMock!.Object);

            DoctorModel? doctorModelNew = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == 2);

            Assert.AreEqual(2, doctorModelNew!.Id);
            Assert.AreEqual("update", doctorModelNew.FirstName);
            Assert.AreEqual("doc2last", doctorModelNew.LastName);
        }

        [Test]
        public void Delete_CanDeleteInDb()
        {
            AddDoctors();
            DoctorModel? doctorModel = _doctorContext!.Doctors!.FirstOrDefault(doctor => doctor.Id == 2);
            _doctorController!.Delete(new IdMessage { Id = 2 }, _serverCallContextMock!.Object);
            Assert.AreEqual(1, _doctorContext!.Doctors!.Count());
        }

        [TearDown]
        public void Cleanup()
        {
            _doctorContext!.Doctors!.RemoveRange(_doctorContext.Doctors);
            _doctorContext.SaveChanges();
        }

        private void AddDoctors()
        {
            var doctors = new List<DoctorModel>()
            {
                new DoctorModel()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last"
                },
                new DoctorModel()
                {
                    Id = 2,
                    FirstName = "doc2first",
                    LastName = "doc2last"
                }
            };
            _doctorContext!.Doctors!.AddRange(doctors);
            _doctorContext.SaveChanges();
        }
    }
}