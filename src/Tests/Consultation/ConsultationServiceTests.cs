using MedicalSystem.Services.Consultation.Dals;
using MedicalSystem.Services.Consultation.DomainModels;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ConsultationServiceTests
    {
        private ConsultationService? _consultationService;
        private Mock<IConsultationDal>? _consultationDalMock;

        [SetUp]
        public void Setup()
        {
            _consultationDalMock = new Mock<IConsultationDal>();
            _consultationService = new ConsultationService(_consultationDalMock.Object);
        }

        [Test]
        public void GetAll_GivenValidDomainModels_ReturnsValidViewModels()
        {
            var consultationDomainModels = new List<ConsultationDomainModel>()
            {
                new ConsultationDomainModel(1, DateTime.Now, "India", "Maharashtra", "Mumbai", "123456",
                    "Preg", "Med1", 1, 1)
                {
                    Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                    Patent = new PatentDomainModel(1, "pat1first", "pat1last")
                },
                new ConsultationDomainModel(2, DateTime.Now, "UK", "Bihar", "Pune", "987654",
                    "Preg1", "Med2", 2, 2)
                {
                    Doctor = new DoctorDomainModel(2, "doc2first", "doc2last"),
                    Patent = new PatentDomainModel(2, "pat2first", "pat2last")
                }
            };
            _consultationDalMock!.Setup(dal => dal.GetAll()).Returns(consultationDomainModels);

            var consultationViewModels = _consultationService!.GetAll().ToList();

            Assert.AreEqual(2, consultationViewModels.Count);

            Assert.AreEqual(1, consultationViewModels[0].Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModels[0].Date.Date);
            Assert.AreEqual("India", consultationViewModels[0].Country);
            Assert.AreEqual("Maharashtra", consultationViewModels[0].State);
            Assert.AreEqual("Mumbai", consultationViewModels[0].City);
            Assert.AreEqual("123456", consultationViewModels[0].PinCode);
            Assert.AreEqual("Preg", consultationViewModels[0].Problem);
            Assert.AreEqual("Med1", consultationViewModels[0].Medicine);
            Assert.AreEqual(1, consultationViewModels[0].DoctorId);
            Assert.AreEqual(1, consultationViewModels[0].Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModels[0].Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationViewModels[0].Doctor!.LastName);
            Assert.AreEqual(1, consultationViewModels[0].PatentId);
            Assert.AreEqual(1, consultationViewModels[0].Patent!.Id);
            Assert.AreEqual("pat1first", consultationViewModels[0].Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModels[0].Patent!.LastName);

            Assert.AreEqual(2, consultationViewModels[1].Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModels[1].Date.Date);
            Assert.AreEqual("UK", consultationViewModels[1].Country);
            Assert.AreEqual("Bihar", consultationViewModels[1].State);
            Assert.AreEqual("Pune", consultationViewModels[1].City);
            Assert.AreEqual("987654", consultationViewModels[1].PinCode);
            Assert.AreEqual("Preg1", consultationViewModels[1].Problem);
            Assert.AreEqual("Med2", consultationViewModels[1].Medicine);
            Assert.AreEqual(2, consultationViewModels[1].DoctorId);
            Assert.AreEqual(2, consultationViewModels[1].Doctor!.Id);
            Assert.AreEqual("doc2first", consultationViewModels[1].Doctor!.FirstName);
            Assert.AreEqual("doc2last", consultationViewModels[1].Doctor!.LastName);
            Assert.AreEqual(2, consultationViewModels[1].PatentId);
            Assert.AreEqual(2, consultationViewModels[1].Patent!.Id);
            Assert.AreEqual("pat2first", consultationViewModels[1].Patent!.FirstName);
            Assert.AreEqual("pat2last", consultationViewModels[1].Patent!.LastName);
        }

        [Test]
        public void GetAll_GivenEmptyDomainModels_ReturnsEmptyViewModels()
        {
            var consultationDomainModels = new List<ConsultationDomainModel>();
            _consultationDalMock!.Setup(dal => dal.GetAll()).Returns(consultationDomainModels);
            var consultationViewModels = _consultationService!.GetAll().ToList();
            Assert.AreEqual(0, consultationViewModels.Count);
        }

        [Test]
        public void GetAll_GivenNullDomainModels_ExpectException()
        {
            _consultationDalMock!.Setup(dal => dal.GetAll()).Returns<IEnumerable<ConsultationDomainModel>>(null);
            Assert.Throws<NullReferenceException>(() => _consultationService!.GetAll());
        }

        [Test]
        public void GetById_GivenValidDomainModel_ReturnsValidViewModel()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patent = new PatentDomainModel(1, "pat1first", "pat1last")
            };

            _consultationDalMock!.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(consultationDomainModel);

            var consultationViewModel = _consultationService!.GetById(It.IsAny<int>());

            Assert.NotNull(consultationViewModel);
            Assert.IsInstanceOf<ConsultationViewModel>(consultationViewModel);

            Assert.AreEqual(1, consultationViewModel!.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModel!.Date.Date);
            Assert.AreEqual("India", consultationViewModel!.Country);
            Assert.AreEqual("Maharashtra", consultationViewModel!.State);
            Assert.AreEqual("Mumbai", consultationViewModel!.City);
            Assert.AreEqual("123456", consultationViewModel!.PinCode);
            Assert.AreEqual("Preg", consultationViewModel!.Problem);
            Assert.AreEqual("Med1", consultationViewModel!.Medicine);
            Assert.AreEqual(1, consultationViewModel!.DoctorId);
            Assert.AreEqual(1, consultationViewModel!.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModel!.Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationViewModel!.Doctor!.LastName);
            Assert.AreEqual(1, consultationViewModel!.PatentId);
            Assert.AreEqual(1, consultationViewModel!.Patent!.Id);
            Assert.AreEqual("pat1first", consultationViewModel!.Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModel!.Patent!.LastName);
        }

        [Test]
        public void GetById_GivenNullDomainModel_ReturnsNull()
        {
            _consultationDalMock!.Setup(dal => dal.GetById(It.IsAny<int>())).Returns<ConsultationDomainModel>(null);
            var consultationViewModel = _consultationService!.GetById(It.IsAny<int>());
            Assert.Null(consultationViewModel);
        }

        [Test]
        public void Add_CanCallDalAdd()
        {
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = 1,
                Date = DateTime.Now,
                Country = "India",
                State = "WB",
                City = "Kol",
                PinCode = "1234",
                Problem = "P1",
                Medicine = "M1",
                DoctorId = 1,
                Doctor = new DoctorViewModel()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last",
                },
                PatentId = 1,
                Patent = new PatentViewModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            };

            _consultationDalMock!.Setup(dal => dal.Add(It.IsAny<ConsultationDomainModel>()));
            _consultationService!.Add(consultationViewModel);
            _consultationDalMock.Verify();
        }

        [Test]
        public void Update_CanCallDalUpdate()
        {
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = 1,
                Date = DateTime.Now,
                Country = "India",
                State = "WB",
                City = "Kol",
                PinCode = "1234",
                Problem = "P1",
                Medicine = "M1",
                DoctorId = 1,
                Doctor = new DoctorViewModel()
                {
                    Id = 1,
                    FirstName = "doc1first",
                    LastName = "doc1last",
                },
                PatentId = 1,
                Patent = new PatentViewModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            };
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patent = new PatentDomainModel(1, "pat1first", "pat1last")
            };
            _consultationDalMock!.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(consultationDomainModel);
            _consultationDalMock!.Setup(dal => dal.Update(consultationDomainModel));
            _consultationService!.Update(It.IsAny<int>(), consultationViewModel);
            _consultationDalMock.Verify();
        }

        [Test]
        public void Delete_CanCallDalDelete()
        {
            _consultationDalMock!.Setup(dal => dal.Delete(It.IsAny<ConsultationDomainModel>()));
            _consultationService!.Delete(It.IsAny<int>());
            _consultationDalMock.Verify();
        }
    }
}