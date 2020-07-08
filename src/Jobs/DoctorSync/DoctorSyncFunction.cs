using MedicalSystem.Jobs.DoctorSync.Data;
using MedicalSystem.Jobs.DoctorSync.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Jobs.DoctorSync
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorSyncFunction"]/doctorSyncFunction/*'/>
    public class DoctorSyncFunction
    {
        private readonly IConfiguration _configuration;

        /// <include file='docs.xml' path='docs/members[@name="DoctorSyncFunction"]/doctorSyncFunctionConstructor/*'/>
        public DoctorSyncFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorSyncFunction"]/run/*'/>
        [FunctionName("DoctorSyncFunction")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            IEnumerable<DoctorModel> doctorModels = GetDoctorsFromDoctorDb();
            SaveDoctorsToConsultationDb(doctorModels);
        }

        private IEnumerable<DoctorModel> GetDoctorsFromDoctorDb()
        {
            string doctorDbConnectionString = _configuration.GetConnectionString("DoctorDbConnectionString");
            var options = new DbContextOptionsBuilder<DoctorContext>()
                .UseSqlServer(doctorDbConnectionString)
                .Options;
            var doctorContext = new DoctorContext(options);
            List<DoctorModel> doctorModels = doctorContext.Doctors.AsNoTracking().ToList();
            return doctorModels;
        }

        private void SaveDoctorsToConsultationDb(IEnumerable<DoctorModel> doctorModels)
        {
            string consultationDbConnectionString = _configuration.GetConnectionString("ConsultationDbConnectionString");
            var options = new DbContextOptionsBuilder<DoctorContext>()
                .UseSqlServer(consultationDbConnectionString)
                .Options;
            var doctorContext = new DoctorContext(options);
            using IDbContextTransaction transaction = doctorContext.Database.BeginTransaction();
            try
            {
                doctorContext.Doctors!.RemoveRange(doctorContext.Doctors);
                doctorContext.SaveChanges();
                doctorContext.Doctors.AddRange(doctorModels);
                doctorContext.SaveChanges();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
