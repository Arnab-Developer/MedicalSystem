using Dapper;
using MedicalSystem.Services.Consultation.Options;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Queries
{
    public class DoctorQueries : IDoctorQueries
    {
        private readonly DatabaseOptions _databaseOptions;

        public DoctorQueries(IOptionsMonitor<DatabaseOptions> optionsAccessor)
        {
            _databaseOptions = optionsAccessor.CurrentValue;
        }

        IEnumerable<DoctorViewModel> IDoctorQueries.GetAll()
        {
            dynamic dbResultModels;
            using (var con = new SqlConnection(_databaseOptions.ConsultationDbConnectionString))
            {
                dbResultModels = con.Query<dynamic>(
                    @"SELECT d.Id DoctorId, d.FirstName DoctorFirstName
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
