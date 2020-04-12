using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ConsultationDalTests
    {
        private ConsultationDal? _consultationDal;
        private ConsultationContext? _consultationContext;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ConsultationContext>()
                .UseInMemoryDatabase("ConsultationTestDb")
                .Options;
            _consultationContext = new ConsultationContext(options);
            _consultationDal = new ConsultationDal(_consultationContext);
        }

        [Test]
        public void GetAll_GivenValidDbData_ReturnsValidDomainModels()
        {
            AddDoctors();
            AddPatents();
            AddConsultations();

            var consultationDomainModels = _consultationDal!.GetAll().ToList();

            Assert.AreEqual(2, consultationDomainModels.Count);

            Assert.AreEqual(1, consultationDomainModels[0].Id);
            Assert.AreEqual(DateTime.Now.Date, consultationDomainModels[0].Date.Date);
            Assert.AreEqual("India", consultationDomainModels[0].Place!.Country);
            Assert.AreEqual("Maharashtra", consultationDomainModels[0].Place!.State);
            Assert.AreEqual("Mumbai", consultationDomainModels[0].Place!.City);
            Assert.AreEqual("123456", consultationDomainModels[0].Place!.PinCode);
            Assert.AreEqual("Preg", consultationDomainModels[0].Problem);
            Assert.AreEqual("Med1", consultationDomainModels[0].Medicine);
            Assert.AreEqual(1, consultationDomainModels[0].DoctorId);
            Assert.AreEqual(1, consultationDomainModels[0].Doctor!.Id);
            Assert.AreEqual("doc1first", consultationDomainModels[0].Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationDomainModels[0].Doctor!.LastName);
            Assert.AreEqual(1, consultationDomainModels[0].PatentId);
            Assert.AreEqual(1, consultationDomainModels[0].Patent!.Id);
            Assert.AreEqual("pat1first", consultationDomainModels[0].Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationDomainModels[0].Patent!.LastName);

            Assert.AreEqual(2, consultationDomainModels[1].Id);
            Assert.AreEqual(DateTime.Now.AddDays(-1).Date, consultationDomainModels[1].Date.Date);
            Assert.AreEqual("UK", consultationDomainModels[1].Place!.Country);
            Assert.AreEqual("Bihar", consultationDomainModels[1].Place!.State);
            Assert.AreEqual("Pune", consultationDomainModels[1].Place!.City);
            Assert.AreEqual("987654", consultationDomainModels[1].Place!.PinCode);
            Assert.AreEqual("Preg1", consultationDomainModels[1].Problem);
            Assert.AreEqual("Med2", consultationDomainModels[1].Medicine);
            Assert.AreEqual(2, consultationDomainModels[1].DoctorId);
            Assert.AreEqual(2, consultationDomainModels[1].Doctor!.Id);
            Assert.AreEqual("doc2first", consultationDomainModels[1].Doctor!.FirstName);
            Assert.AreEqual("doc2last", consultationDomainModels[1].Doctor!.LastName);
            Assert.AreEqual(2, consultationDomainModels[1].PatentId);
            Assert.AreEqual(2, consultationDomainModels[1].Patent!.Id);
            Assert.AreEqual("pat2first", consultationDomainModels[1].Patent!.FirstName);
            Assert.AreEqual("pat2last", consultationDomainModels[1].Patent!.LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDbData_ReturnsEmptyDomainModels()
        {
            AddDoctors();
            AddPatents();

            var consultationDomainModels = _consultationDal!.GetAll().ToList();

            Assert.AreEqual(0, consultationDomainModels.Count);
        }

        [Test]
        public void GetById_GivenValidDbData_ReturnsValidViewModel()
        {
            AddDoctors();
            AddPatents();
            AddConsultation();

            var consultationDomainModel = _consultationDal!.GetById(1);

            Assert.NotNull(consultationDomainModel);

            Assert.AreEqual(1, consultationDomainModel!.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationDomainModel.Date.Date);
            Assert.AreEqual("India", consultationDomainModel.Place!.Country);
            Assert.AreEqual("Maharashtra", consultationDomainModel.Place!.State);
            Assert.AreEqual("Mumbai", consultationDomainModel.Place!.City);
            Assert.AreEqual("123456", consultationDomainModel.Place!.PinCode);
            Assert.AreEqual("Preg", consultationDomainModel.Problem);
            Assert.AreEqual("Med1", consultationDomainModel.Medicine);
            Assert.AreEqual(1, consultationDomainModel.DoctorId);
            Assert.AreEqual(1, consultationDomainModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationDomainModel.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationDomainModel.Doctor!.LastName);
            Assert.AreEqual(1, consultationDomainModel.PatentId);
            Assert.AreEqual(1, consultationDomainModel.Patent!.Id);
            Assert.AreEqual("pat1first", consultationDomainModel.Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationDomainModel.Patent!.LastName);
        }

        [Test]
        public void GetById_GivenEmptyDbData_ReturnsNull()
        {
            AddDoctors();
            AddPatents();

            var consultationDomainModel = _consultationDal!.GetById(1);

            Assert.Null(consultationDomainModel);
        }

        [Test]
        public void Add_CanInsertInDb()
        {
            AddDoctors();
            AddPatents();

            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1);
            _consultationDal!.Add(consultationDomainModel);

            var consultationDomainModelFromDb = _consultationContext!.Consultations.SingleOrDefault();

            Assert.NotNull(consultationDomainModelFromDb);

            Assert.AreEqual(1, consultationDomainModelFromDb!.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationDomainModelFromDb.Date.Date);
            Assert.AreEqual("India", consultationDomainModelFromDb.Place!.Country);
            Assert.AreEqual("Maharashtra", consultationDomainModelFromDb.Place!.State);
            Assert.AreEqual("Mumbai", consultationDomainModelFromDb.Place!.City);
            Assert.AreEqual("123456", consultationDomainModelFromDb.Place!.PinCode);
            Assert.AreEqual("Preg", consultationDomainModelFromDb.Problem);
            Assert.AreEqual("Med1", consultationDomainModelFromDb.Medicine);
            Assert.AreEqual(1, consultationDomainModelFromDb.DoctorId);
            Assert.AreEqual(1, consultationDomainModelFromDb.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationDomainModelFromDb.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationDomainModelFromDb.Doctor!.LastName);
            Assert.AreEqual(1, consultationDomainModelFromDb.PatentId);
            Assert.AreEqual(1, consultationDomainModelFromDb.Patent!.Id);
            Assert.AreEqual("pat1first", consultationDomainModelFromDb.Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationDomainModelFromDb.Patent!.LastName);
        }

        [Test]
        public void Update_CanUpdateInDb()
        {
            AddDoctors();
            AddPatents();
            AddConsultations();

            var consultationDomainModelFromDb = _consultationContext!.Consultations
                .FirstOrDefault(consultationDomainModel => consultationDomainModel.Id == 1);
            consultationDomainModelFromDb.Medicine = "updated1";
            _consultationDal!.Update(consultationDomainModelFromDb);
            var consultationDomainModelFromDbNew = _consultationContext!.Consultations
                .FirstOrDefault(consultationDomainModel => consultationDomainModel.Id == 1);

            Assert.NotNull(consultationDomainModelFromDbNew);

            Assert.AreEqual(1, consultationDomainModelFromDb!.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationDomainModelFromDb.Date.Date);
            Assert.AreEqual("India", consultationDomainModelFromDb.Place!.Country);
            Assert.AreEqual("Maharashtra", consultationDomainModelFromDb.Place!.State);
            Assert.AreEqual("Mumbai", consultationDomainModelFromDb.Place!.City);
            Assert.AreEqual("123456", consultationDomainModelFromDb.Place!.PinCode);
            Assert.AreEqual("Preg", consultationDomainModelFromDb.Problem);
            Assert.AreEqual("updated1", consultationDomainModelFromDb.Medicine);
            Assert.AreEqual(1, consultationDomainModelFromDb.DoctorId);
            Assert.AreEqual(1, consultationDomainModelFromDb.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationDomainModelFromDb.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationDomainModelFromDb.Doctor!.LastName);
            Assert.AreEqual(1, consultationDomainModelFromDb.PatentId);
            Assert.AreEqual(1, consultationDomainModelFromDb.Patent!.Id);
            Assert.AreEqual("pat1first", consultationDomainModelFromDb.Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationDomainModelFromDb.Patent!.LastName);
        }

        [Test]
        public void Delete_CanDeleteInDb()
        {
            AddDoctors();
            AddPatents();
            AddConsultations();

            var consultationDomainModelFromDb = _consultationContext!.Consultations
                .FirstOrDefault(consultationDomainModel => consultationDomainModel.Id == 1);
            _consultationDal!.Delete(consultationDomainModelFromDb);

            var consultationDomainModelFromDbNew = _consultationContext!.Consultations
                .FirstOrDefault(consultationDomainModel => consultationDomainModel.Id == 1);
            Assert.Null(consultationDomainModelFromDbNew);
            Assert.AreEqual(2, _consultationContext.Doctors.Count());
            Assert.AreEqual(2, _consultationContext.Patents.Count());
            Assert.AreEqual(1, _consultationContext.Consultations.Count());
        }

        [TearDown]
        public void Cleanup()
        {
            _consultationContext!.Consultations!.RemoveRange(_consultationContext.Consultations);
            _consultationContext.Doctors!.RemoveRange(_consultationContext.Doctors);
            _consultationContext.Patents!.RemoveRange(_consultationContext.Patents);

            _consultationContext.SaveChanges();
        }

        private void AddDoctors()
        {
            var doctors = new List<DoctorDomainModel>()
            {
                new DoctorDomainModel(1, "doc1first", "doc1last"),
                new DoctorDomainModel(2, "doc2first", "doc2last")
            };
            _consultationContext!.Doctors!.AddRange(doctors);
            _consultationContext.SaveChanges();
        }

        private void AddPatents()
        {
            var patents = new List<PatentDomainModel>()
            {
                new PatentDomainModel(1, "pat1first", "pat1last"),
                new PatentDomainModel(2, "pat2first", "pat2last"),
            };
            _consultationContext!.Patents!.AddRange(patents);
            _consultationContext.SaveChanges();
        }

        private void AddConsultations()
        {
            var consultationDomainModels = new List<ConsultationDomainModel>()
            {
                new ConsultationDomainModel(1, DateTime.Now, "India", "Maharashtra", "Mumbai", "123456",
                    "Preg", "Med1", 1, 1),
                new ConsultationDomainModel(2, DateTime.Now.AddDays(-1), "UK", "Bihar", "Pune", "987654",
                    "Preg1", "Med2", 2, 2)
            };
            _consultationContext!.Consultations!.AddRange(consultationDomainModels);
            _consultationContext.SaveChanges();
        }

        private void AddConsultation()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1);
            _consultationContext!.Consultations!.Add(consultationDomainModel);
            _consultationContext.SaveChanges();
        }
    }
}
