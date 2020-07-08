using MedicalSystem.Jobs.PatientSync.Data;
using MedicalSystem.Jobs.PatientSync.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Jobs.PatientSync
{
    /// <include file='docs.xml' path='docs/members[@name="PatientSyncFunction"]/patientSyncFunction/*'/>
    public class PatientSyncFunction
    {
        private readonly IConfiguration _configuration;

        /// <include file='docs.xml' path='docs/members[@name="PatientSyncFunction"]/patientSyncFunctionConstructor/*'/>
        public PatientSyncFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientSyncFunction"]/run/*'/>
        [FunctionName("PatientSyncFunction")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            IEnumerable<PatientModel> patientModels = GetPatientsFromPatientDb();
            SavePatientsToConsultationDb(patientModels);
        }

        private IEnumerable<PatientModel> GetPatientsFromPatientDb()
        {
            string patientDbConnectionString = _configuration.GetConnectionString("PatientDbConnectionString");
            var options = new DbContextOptionsBuilder<PatientContext>()
                .UseSqlServer(patientDbConnectionString)
                .Options;
            var patientContext = new PatientContext(options);
            List<PatientModel> patientModels = patientContext.Patients.AsNoTracking().ToList();
            return patientModels;
        }

        private void SavePatientsToConsultationDb(IEnumerable<PatientModel> patientModels)
        {
            string consultationDbConnectionString = _configuration.GetConnectionString("ConsultationDbConnectionString");
            var options = new DbContextOptionsBuilder<PatientContext>()
                .UseSqlServer(consultationDbConnectionString)
                .Options;
            var patientContext = new PatientContext(options);
            using IDbContextTransaction transaction = patientContext.Database.BeginTransaction();
            try
            {
                patientContext.Patients!.RemoveRange(patientContext.Patients);
                patientContext.SaveChanges();
                patientContext.Patients.AddRange(patientModels);
                patientContext.SaveChanges();

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
