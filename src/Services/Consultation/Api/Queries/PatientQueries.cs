using Dapper;
using MedicalSystem.Services.Consultation.Api.Options;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Api.Queries
{
    public class PatientQueries : IPatientQueries
    {
        private readonly IOptionsMonitor<DatabaseOptions> _optionsAccessor;

        public PatientQueries(IOptionsMonitor<DatabaseOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        IEnumerable<PatientViewModel> IPatientQueries.GetAll()
        {
            dynamic dbResultModels;
            using (var con = new SqlConnection(_optionsAccessor.CurrentValue.ConsultationDbConnectionString))
            {
                dbResultModels = con.Query<dynamic>(
                    @"SELECT p.Id, p.FirstName, p.LastName
                    FROM Patients p
                    ORDER BY p.FirstName DESC");
            }
            if (dbResultModels == null || dbResultModels!.Count == 0)
            {
                return new List<PatientViewModel>();
            }
            var patientViewModels = new List<PatientViewModel>();
            foreach (dynamic? dbResultModel in dbResultModels!)
            {
                var patientViewModel = new PatientViewModel()
                {
                    Id = dbResultModel!.Id,
                    FirstName = dbResultModel.FirstName,
                    LastName = dbResultModel.LastName
                };
                patientViewModels.Add(patientViewModel);
            }
            return patientViewModels;
        }
    }
}
