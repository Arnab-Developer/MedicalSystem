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
    /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/consultationServiceTests/*'/>
    internal class ConsultationServiceTests
    {
        private IConsultationService? _consultationService;
        private Mock<IConsultationDal>? _consultationDalMock;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/setup/*'/>
        [SetUp]
        public void Setup()
        {
            _consultationDalMock = new Mock<IConsultationDal>();
            _consultationService = new ConsultationService(_consultationDalMock.Object);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/getAll_GivenValidDomainModels_ReturnsValidViewModels/*'/>
        [Test]
        public void GetAll_GivenValidDomainModels_ReturnsValidViewModels()
        {
            var consultationDomainModels = new List<ConsultationDomainModel>()
            {
                new ConsultationDomainModel(1, DateTime.Now, "India", "Maharashtra", "Mumbai", "123456",
                    "Preg", "Med1", 1, 1)
                {
                    Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                    Patient = new PatientDomainModel(1, "pat1first", "pat1last")
                },
                new ConsultationDomainModel(2, DateTime.Now, "UK", "Bihar", "Pune", "987654",
                    "Preg1", "Med2", 2, 2)
                {
                    Doctor = new DoctorDomainModel(2, "doc2first", "doc2last"),
                    Patient = new PatientDomainModel(2, "pat2first", "pat2last")
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
            Assert.AreEqual(1, consultationViewModels[0].PatientId);
            Assert.AreEqual(1, consultationViewModels[0].Patient!.Id);
            Assert.AreEqual("pat1first", consultationViewModels[0].Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModels[0].Patient!.LastName);

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
            Assert.AreEqual(2, consultationViewModels[1].PatientId);
            Assert.AreEqual(2, consultationViewModels[1].Patient!.Id);
            Assert.AreEqual("pat2first", consultationViewModels[1].Patient!.FirstName);
            Assert.AreEqual("pat2last", consultationViewModels[1].Patient!.LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/getAll_GivenEmptyDomainModels_ReturnsEmptyViewModels/*'/>
        [Test]
        public void GetAll_GivenEmptyDomainModels_ReturnsEmptyViewModels()
        {
            var consultationDomainModels = new List<ConsultationDomainModel>();
            _consultationDalMock!.Setup(dal => dal.GetAll()).Returns(consultationDomainModels);
            var consultationViewModels = _consultationService!.GetAll().ToList();
            Assert.AreEqual(0, consultationViewModels.Count);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/getAll_GivenNullDomainModels_ExpectException/*'/>
        [Test]
        public void GetAll_GivenNullDomainModels_ExpectException()
        {
            _consultationDalMock!.Setup(dal => dal.GetAll()).Returns<IEnumerable<ConsultationDomainModel>>(null);
            Assert.Throws<NullReferenceException>(() => _consultationService!.GetAll());
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/getById_GivenValidDomainModel_ReturnsValidViewModel/*'/>
        [Test]
        public void GetById_GivenValidDomainModel_ReturnsValidViewModel()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
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
            Assert.AreEqual(1, consultationViewModel!.PatientId);
            Assert.AreEqual(1, consultationViewModel!.Patient!.Id);
            Assert.AreEqual("pat1first", consultationViewModel!.Patient!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModel!.Patient!.LastName);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/getById_GivenNullDomainModel_ReturnsNull/*'/>
        [Test]
        public void GetById_GivenNullDomainModel_ReturnsNull()
        {
            _consultationDalMock!.Setup(dal => dal.GetById(It.IsAny<int>())).Returns<ConsultationDomainModel>(null);
            var consultationViewModel = _consultationService!.GetById(It.IsAny<int>());
            Assert.Null(consultationViewModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/add_CanCallDalAdd/*'/>
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
                PatientId = 1,
                Patient = new PatientViewModel()
                {
                    Id = 1,
                    FirstName = "pat1first",
                    LastName = "pat1last"
                }
            };

            _consultationDalMock!.Setup(dal => dal.Add(It.IsAny<ConsultationDomainModel>())).Verifiable();
            _consultationService!.Add(consultationViewModel);
            _consultationDalMock.Verify();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/update_CanCallDalUpdate/*'/>
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
                PatientId = 1,
                Patient = new PatientViewModel()
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
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };
            _consultationDalMock!.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(consultationDomainModel).Verifiable();
            _consultationDalMock!.Setup(dal => dal.Update(consultationDomainModel)).Verifiable();
            _consultationService!.Update(It.IsAny<int>(), consultationViewModel);
            _consultationDalMock.Verify();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationServiceTests"]/delete_CanCallDalDelete/*'/>
        [Test]
        public void Delete_CanCallDalDelete()
        {
            var consultationDomainModel = new ConsultationDomainModel(1, DateTime.Now, "India",
                "Maharashtra", "Mumbai", "123456", "Preg", "Med1", 1, 1)
            {
                Doctor = new DoctorDomainModel(1, "doc1first", "doc1last"),
                Patient = new PatientDomainModel(1, "pat1first", "pat1last")
            };
            _consultationDalMock!.Setup(dal => dal.GetById(It.IsAny<int>())).Returns(consultationDomainModel).Verifiable();
            _consultationDalMock!.Setup(dal => dal.Delete(consultationDomainModel)).Verifiable();
            _consultationService!.Delete(It.IsAny<int>());
            _consultationDalMock.Verify();
        }
    }
}