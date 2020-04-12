using MedicalSystem.Services.Consultation.Controllers;
using MedicalSystem.Services.Consultation.Services;
using MedicalSystem.Services.Consultation.ViewModels;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class ConsultationControllerTests
    {
        private Mock<IConsultationService>? _consultationServiceMock;
        private ConsultationController? _consultationController;

        [SetUp]
        public void Setup()
        {
            _consultationServiceMock = new Mock<IConsultationService>();
            _consultationController = new ConsultationController(_consultationServiceMock.Object);
        }

        [Test]
        public void GetAll_GivenValidViewModels_ReturnsValidViewModels()
        {
            var consultationViewModels = new List<ConsultationViewModel>()
            {
                new ConsultationViewModel()
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
                },
                new ConsultationViewModel()
                {
                    Id = 2,
                    Date = DateTime.Now,
                    Country = "UK",
                    State = "st1",
                    City = "c1",
                    PinCode = "4567",
                    Problem = "p2",
                    Medicine = "m2",
                    DoctorId = 2,
                    Doctor = new DoctorViewModel()
                    {
                        Id = 2,
                        FirstName = "doc2 fir",
                        LastName = "doc2 las",
                    },
                    PatentId = 2,
                    Patent = new PatentViewModel()
                    {
                        Id = 2,
                        FirstName = "pat2 fir",
                        LastName = "pat2 las"
                    }
                }
            };
            _consultationServiceMock!.Setup(service => service.GetAll()).Returns(consultationViewModels);

            var consultationViewModelsFromController = _consultationController!.GetAll().ToList();

            Assert.AreEqual(2, consultationViewModelsFromController.Count);

            Assert.AreEqual(1, consultationViewModelsFromController[0].Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModelsFromController[0].Date.Date);
            Assert.AreEqual("India", consultationViewModelsFromController[0].Country);
            Assert.AreEqual("WB", consultationViewModelsFromController[0].State);
            Assert.AreEqual("Kol", consultationViewModelsFromController[0].City);
            Assert.AreEqual("1234", consultationViewModelsFromController[0].PinCode);
            Assert.AreEqual("P1", consultationViewModelsFromController[0].Problem);
            Assert.AreEqual("M1", consultationViewModelsFromController[0].Medicine);
            Assert.AreEqual(1, consultationViewModelsFromController[0].DoctorId);
            Assert.AreEqual(1, consultationViewModelsFromController[0].Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModelsFromController[0].Doctor!.FirstName);
            Assert.AreEqual("doc1last", consultationViewModelsFromController[0].Doctor!.LastName);
            Assert.AreEqual(1, consultationViewModelsFromController[0].PatentId);
            Assert.AreEqual(1, consultationViewModelsFromController[0].Patent!.Id);
            Assert.AreEqual("pat1first", consultationViewModelsFromController[0].Patent!.FirstName);
            Assert.AreEqual("pat1last", consultationViewModelsFromController[0].Patent!.LastName);

            Assert.AreEqual(2, consultationViewModelsFromController[1].Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModelsFromController[1].Date.Date);
            Assert.AreEqual("UK", consultationViewModelsFromController[1].Country);
            Assert.AreEqual("st1", consultationViewModelsFromController[1].State);
            Assert.AreEqual("c1", consultationViewModelsFromController[1].City);
            Assert.AreEqual("4567", consultationViewModelsFromController[1].PinCode);
            Assert.AreEqual("p2", consultationViewModelsFromController[1].Problem);
            Assert.AreEqual("m2", consultationViewModelsFromController[1].Medicine);
            Assert.AreEqual(2, consultationViewModelsFromController[1].DoctorId);
            Assert.AreEqual(2, consultationViewModelsFromController[1].Doctor!.Id);
            Assert.AreEqual("doc2 fir", consultationViewModelsFromController[1].Doctor!.FirstName);
            Assert.AreEqual("doc2 las", consultationViewModelsFromController[1].Doctor!.LastName);
            Assert.AreEqual(2, consultationViewModelsFromController[1].PatentId);
            Assert.AreEqual(2, consultationViewModelsFromController[1].Patent!.Id);
            Assert.AreEqual("pat2 fir", consultationViewModelsFromController[1].Patent!.FirstName);
            Assert.AreEqual("pat2 las", consultationViewModelsFromController[1].Patent!.LastName);
        }

        [Test]
        public void GetAll_GivenEmptyViewModels_ReturnsEmptyViewModels()
        {
            var consultationViewModels = new List<ConsultationViewModel>();
            _consultationServiceMock!.Setup(service => service.GetAll()).Returns(consultationViewModels);

            var consultationViewModelsFromController = _consultationController!.GetAll().ToList();

            Assert.Zero(consultationViewModelsFromController.Count);
        }

        [Test]
        public void GetAll_GivenNullViewModels_ExpectException()
        {
            var consultationViewModels = new List<ConsultationViewModel>();
            _consultationServiceMock!.Setup(service => service.GetAll()).Throws<NullReferenceException>();

            Assert.Throws<NullReferenceException>(() => _consultationController!.GetAll());
        }

        [Test]
        public void GetById_GivenValidViewModel_ReturnsValidViewModel()
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
            _consultationServiceMock!.Setup(service => service.GetById(It.IsAny<int>())).Returns(consultationViewModel);

            var consultationViewModelFromController = _consultationController!.GetById(It.IsAny<int>());

            Assert.AreEqual(1, consultationViewModelFromController!.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModelFromController.Date.Date);
            Assert.AreEqual("India", consultationViewModelFromController.Country);
            Assert.AreEqual("WB", consultationViewModelFromController.State);
            Assert.AreEqual("Kol", consultationViewModelFromController.City);
            Assert.AreEqual("1234", consultationViewModelFromController.PinCode);
            Assert.AreEqual("P1", consultationViewModelFromController.Problem);
            Assert.AreEqual("M1", consultationViewModelFromController.Medicine);
            Assert.AreEqual(1, consultationViewModelFromController.DoctorId);
            Assert.AreEqual(1, consultationViewModelFromController.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModelFromController.Doctor.FirstName);
            Assert.AreEqual("doc1last", consultationViewModelFromController.Doctor.LastName);
            Assert.AreEqual(1, consultationViewModelFromController.PatentId);
            Assert.AreEqual(1, consultationViewModelFromController.Patent!.Id);
            Assert.AreEqual("pat1first", consultationViewModelFromController.Patent.FirstName);
            Assert.AreEqual("pat1last", consultationViewModelFromController.Patent.LastName);
        }

        [Test]
        public void GetById_GivenNullViewModel_ReturnsNull()
        {
            _consultationServiceMock!.Setup(service => service.GetById(It.IsAny<int>())).Returns<ConsultationViewModel>(null);
            var consultationViewModelFromController = _consultationController!.GetById(It.IsAny<int>());
            Assert.Null(consultationViewModelFromController);
        }

        [Test]
        public void Add_CanCallServiceAdd()
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

            _consultationServiceMock!.Setup(service => service.Add(consultationViewModel)).Verifiable();
            _consultationController!.Add(consultationViewModel);
            _consultationServiceMock.Verify();
        }

        [Test]
        public void Update_CanCallServiceUpdate()
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
            _consultationServiceMock!.Setup(service => service.Update(It.IsAny<int>(), consultationViewModel)).Verifiable();
            _consultationController!.Update(It.IsAny<int>(), consultationViewModel);
            _consultationServiceMock.Verify();
        }

        [Test]
        public void Delete_CanCallServiceDelete()
        {
            _consultationServiceMock!.Setup(service => service.Delete(It.IsAny<int>())).Verifiable();
            _consultationController!.Delete(It.IsAny<int>());
            _consultationServiceMock.Verify();
        }
    }
}
