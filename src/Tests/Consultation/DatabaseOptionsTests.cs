using MedicalSystem.Services.Consultation.Api.Options;
using NUnit.Framework;

namespace MedicalSystem.Tests.Services.Consultation
{
    internal class DatabaseOptionsTests
    {
        [Test]
        public void DatabaseOptions_GivenValid_ReturnsValid()
        {
            var databaseOptions = new DatabaseOptions
            {
                ConsultationDbConnectionString = "eXample con str"
            };
            Assert.AreEqual("eXample con str", databaseOptions.ConsultationDbConnectionString);
        }
    }
}
