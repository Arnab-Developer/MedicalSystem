using MedicalSystem.Services.Consultation.ViewModels;
using NUnit.Framework;
using System;

namespace MedicalSystem.Tests.Services.Consultation
{
    /// <include file='docs.xml' path='docs/members[@name="ViewModelTests"]/viewModelTests/*'/>
    internal class ViewModelTests
    {
        /// <include file='docs.xml' path='docs/members[@name="ViewModelTests"]/consultationViewModel_GivenValid_ReturnsValid/*'/>
        [Test]
        public void ConsultationViewModel_GivenValid_ReturnsValid()
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
            Assert.AreEqual(1, consultationViewModel.Id);
            Assert.AreEqual(DateTime.Now.Date, consultationViewModel.Date.Date);
            Assert.AreEqual("India", consultationViewModel.Country);
            Assert.AreEqual("WB", consultationViewModel.State);
            Assert.AreEqual("Kol", consultationViewModel.City);
            Assert.AreEqual("1234", consultationViewModel.PinCode);
            Assert.AreEqual("P1", consultationViewModel.Problem);
            Assert.AreEqual("M1", consultationViewModel.Medicine);
            Assert.AreEqual(1, consultationViewModel.DoctorId);
            Assert.AreEqual(1, consultationViewModel.Doctor!.Id);
            Assert.AreEqual("doc1first", consultationViewModel.Doctor.FirstName);
            Assert.AreEqual("doc1last", consultationViewModel.Doctor.LastName);
            Assert.AreEqual(1, consultationViewModel.PatentId);
            Assert.AreEqual(1, consultationViewModel.Patent!.Id);
            Assert.AreEqual("pat1first", consultationViewModel.Patent.FirstName);
            Assert.AreEqual("pat1last", consultationViewModel.Patent.LastName);
        }
    }
}
