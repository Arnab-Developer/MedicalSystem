using Dapper;
using MedicalSystem.Services.Consultation.Api.Options;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Api.Queries
{
    public class DoctorQueries : IDoctorQueries
    {
        private readonly IOptionsMonitor<DatabaseOptions> _optionsAccessor;

        public DoctorQueries(IOptionsMonitor<DatabaseOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        IEnumerable<DoctorViewModel> IDoctorQueries.GetAll()
        {
            dynamic dbResultModels;
            using (var con = new SqlConnection(_optionsAccessor.CurrentValue.ConsultationDbConnectionString))
            {
                dbResultModels = con.Query<dynamic>(
                    @"SELECT d.Id, d.FirstName, d.LastName
                    FROM Doctors d
                    ORDER BY d.FirstName DESC");
            }
            if (dbResultModels == null || dbResultModels!.Count == 0)
            {
                return new List<DoctorViewModel>();
            }
            var doctorViewModels = new List<DoctorViewModel>();
            foreach (dynamic? dbResultModel in dbResultModels!)
            {
                var doctorViewModel = new DoctorViewModel()
                {
                    Id = dbResultModel!.Id,
                    FirstName = dbResultModel.FirstName,
                    LastName = dbResultModel.LastName
                };
                doctorViewModels.Add(doctorViewModel);
            }
            return doctorViewModels;
        }
    }
}
