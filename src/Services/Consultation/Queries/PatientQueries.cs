﻿using Dapper;
using MedicalSystem.Services.Consultation.Options;
using MedicalSystem.Services.Consultation.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Queries
{
    public class PatientQueries : IPatientQueries
    {
        private readonly DatabaseOptions _databaseOptions;

        public PatientQueries(IOptionsMonitor<DatabaseOptions> optionsAccessor)
        {
            _databaseOptions = optionsAccessor.CurrentValue;
        }

        IEnumerable<PatientViewModel> IPatientQueries.GetAll()
        {
            dynamic dbResultModels;
            using (var con = new SqlConnection(_databaseOptions.ConsultationDbConnectionString))
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
