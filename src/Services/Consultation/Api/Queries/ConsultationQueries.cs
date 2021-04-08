using Dapper;
using MedicalSystem.Services.Consultation.Api.Options;
using MedicalSystem.Services.Consultation.Api.ViewModels;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

namespace MedicalSystem.Services.Consultation.Api.Queries
{
    public class ConsultationQueries : IConsultationQueries
    {
        private readonly IOptionsMonitor<DatabaseOptions> _optionsAccessor;

        public ConsultationQueries(IOptionsMonitor<DatabaseOptions> optionsAccessor)
        {
            _optionsAccessor = optionsAccessor;
        }

        IEnumerable<ConsultationViewModel> IConsultationQueries.GetAll()
        {
            dynamic dbResultModels;
            using (var con = new SqlConnection(_optionsAccessor.CurrentValue.ConsultationDbConnectionString))
            {
                dbResultModels = con.Query<dynamic>(
                    @"SELECT c.Id, c.Date, c.Place_Country, c.Place_State, c.Place_City, c.Place_PinCode,
                    c.Problem, c.Medicine, c.DoctorId, d.Id DoctorId, d.FirstName DoctorFirstName, d.LastName DoctorLastName, c.PatientId, p.Id PatientId,
                    p.FirstName PatientFirstName, p.LastName PatientLastName
                    FROM Consultations c
                    INNER JOIN Doctors d ON d.Id = c.DoctorId
                    INNER JOIN Patients p ON p.Id = c.PatientId
                    ORDER BY c.Date DESC");
            }
            if (dbResultModels == null || dbResultModels!.Count == 0)
            {
                return new List<ConsultationViewModel>();
            }
            var consultationViewModels = new List<ConsultationViewModel>();
            foreach (dynamic? dbResultModel in dbResultModels!)
            {
                var consultationViewModel = new ConsultationViewModel()
                {
                    Id = dbResultModel!.Id,
                    Date = dbResultModel.Date,
                    Country = dbResultModel.Place_Country,
                    State = dbResultModel.Place_State,
                    City = dbResultModel.Place_City,
                    PinCode = dbResultModel.Place_PinCode,
                    Problem = dbResultModel.Problem,
                    Medicine = dbResultModel.Medicine,
                    DoctorId = dbResultModel.DoctorId,
                    Doctor = new DoctorViewModel()
                    {
                        Id = dbResultModel.DoctorId,
                        FirstName = dbResultModel.DoctorFirstName,
                        LastName = dbResultModel.DoctorLastName
                    },
                    PatientId = dbResultModel.PatientId,
                    Patient = new PatientViewModel()
                    {
                        Id = dbResultModel.PatientId,
                        FirstName = dbResultModel.PatientFirstName,
                        LastName = dbResultModel.PatientLastName
                    }
                };
                consultationViewModels.Add(consultationViewModel);
            }
            return consultationViewModels;
        }

        ConsultationViewModel? IConsultationQueries.GetById(int id)
        {
            dynamic dbResultModel;
            using (var con = new SqlConnection(_optionsAccessor.CurrentValue.ConsultationDbConnectionString))
            {
                dbResultModel = con.QuerySingle<dynamic>(
                    @"SELECT c.Id, c.Date, c.Place_Country, c.Place_State, c.Place_City, c.Place_PinCode,
                    c.Problem, c.Medicine, c.DoctorId, d.Id DoctorId, d.FirstName DoctorFirstName, d.LastName DoctorLastName, c.PatientId, p.Id PatientId,
                    p.FirstName PatientFirstName, p.LastName PatientLastName
                    FROM Consultations c
                    INNER JOIN Doctors d ON d.Id = c.DoctorId
                    INNER JOIN Patients p ON p.Id = c.PatientId
                    WHERE c.Id = @Id
                    ORDER BY c.Date DESC", new { Id = id });
            }
            if (dbResultModel == null)
            {
                return null;
            }
            var consultationViewModel = new ConsultationViewModel()
            {
                Id = dbResultModel!.Id,
                Date = dbResultModel.Date,
                Country = dbResultModel.Place_Country,
                State = dbResultModel.Place_State,
                City = dbResultModel.Place_City,
                PinCode = dbResultModel.Place_PinCode,
                Problem = dbResultModel.Problem,
                Medicine = dbResultModel.Medicine,
                DoctorId = dbResultModel.DoctorId,
                Doctor = new DoctorViewModel()
                {
                    Id = dbResultModel.DoctorId,
                    FirstName = dbResultModel.DoctorFirstName,
                    LastName = dbResultModel.DoctorLastName
                },
                PatientId = dbResultModel.PatientId,
                Patient = new PatientViewModel()
                {
                    Id = dbResultModel.PatientId,
                    FirstName = dbResultModel.PatientFirstName,
                    LastName = dbResultModel.PatientLastName
                }
            };
            return consultationViewModel;
        }
    }
}
