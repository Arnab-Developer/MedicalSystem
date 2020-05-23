using MedicalSystem.Jobs.PatentSync.Data;
using MedicalSystem.Jobs.PatentSync.Models;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Jobs.PatentSync
{
    public class PatentSyncFunction
    {
        private readonly IConfiguration _configuration;

        public PatentSyncFunction(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [FunctionName("PatentSyncFunction")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var patentModels = GetPatentsFromPatentDb();
            SavePatentsToConsultationDb(patentModels);
        }

        private IEnumerable<PatentModel> GetPatentsFromPatentDb()
        {
            var patentDbConnectionString = _configuration.GetConnectionString("PatentDbConnectionString");
            var options = new DbContextOptionsBuilder<PatentContext>()
                .UseSqlServer(patentDbConnectionString)
                .Options;
            var patentContext = new PatentContext(options);
            var patentModels = patentContext.Patents.AsNoTracking().ToList();
            return patentModels;
        }

        private void SavePatentsToConsultationDb(IEnumerable<PatentModel> patentModels)
        {
            var consultationDbConnectionString = _configuration.GetConnectionString("ConsultationDbConnectionString");
            var options = new DbContextOptionsBuilder<PatentContext>()
                .UseSqlServer(consultationDbConnectionString)
                .Options;
            var patentContext = new PatentContext(options);
            using var transaction = patentContext.Database.BeginTransaction();
            try
            {
                patentContext.Patents!.RemoveRange(patentContext.Patents);
                patentContext.SaveChanges();
                patentContext.Patents.AddRange(patentModels);
                patentContext.SaveChanges();

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
