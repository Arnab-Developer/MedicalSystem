using Google.Protobuf.WellKnownTypes;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Consultations;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Protos.Consultations;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class ConsultationController : ControllerBase
    {
        private readonly IDoctorGrpcClient _doctorGrpcClient;
        private readonly IPatientGrpcClient _patientGrpcClient;
        private readonly IConsultationGrpcClient _consultationGrpcClient;

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/consultationControllerConstructor/*'/>
        public ConsultationController(
            IDoctorGrpcClient doctorGrpcClient,
            IPatientGrpcClient patientGrpcClient,
            IConsultationGrpcClient consultationGrpcClient)
        {
            _doctorGrpcClient = doctorGrpcClient;
            _patientGrpcClient = patientGrpcClient;
            _consultationGrpcClient = consultationGrpcClient;
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getAll/*'/>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConsultationModel>>> GetAll()
        {
            ConsultationModelsMessage consultationModelsMessage;
            try
            {
                consultationModelsMessage = await _consultationGrpcClient.GetAllAsync(new EmptyMessage());
            }
            catch
            {
                return StatusCode(500);
            }
            if (consultationModelsMessage == null ||
                consultationModelsMessage.Consultations == null ||
                consultationModelsMessage.Consultations.Count() == 0)
            {
                var error = new ErrorModel("No consultation record found.");
                return NotFound(error);
            }
            var consultationModels = new List<ConsultationModel>();
            foreach (ConsultationModelMessage consultationModelMessage in consultationModelsMessage.Consultations)
            {
                var consultationModel = new ConsultationModel
                {
                    Id = consultationModelMessage.Id,
                    Date = consultationModelMessage.Date.ToDateTime(),
                    Country = consultationModelMessage.Country,
                    State = consultationModelMessage.State,
                    City = consultationModelMessage.City,
                    PinCode = consultationModelMessage.PinCode,
                    Problem = consultationModelMessage.Problem,
                    Medicine = consultationModelMessage.Medicine,
                    DoctorId = consultationModelMessage.DoctorId,
                    Doctor = new DoctorModel
                    {
                        Id = consultationModelMessage.Doctor!.Id,
                        FirstName = consultationModelMessage.Doctor!.FirstName,
                        LastName = consultationModelMessage.Doctor!.LastName
                    },
                    PatientId = consultationModelMessage.PatientId,
                    Patient = new PatientModel
                    {
                        Id = consultationModelMessage.Patient!.Id,
                        FirstName = consultationModelMessage.Patient!.FirstName,
                        LastName = consultationModelMessage.Patient!.LastName
                    }
                };
                consultationModels.Add(consultationModel);
            }
            return Ok(consultationModels);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ConsultationModel>> GetById(int? id)
        {
            if (id == null)
            {
                var error = new ErrorModel("Id is null");
                return NotFound(error);
            }
            ConsultationModelMessage consultationModelMessage;
            try
            {
                var idMessage = new IdMessage { Id = id.Value };
                consultationModelMessage = await _consultationGrpcClient.GetByIdAsync(idMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            if (consultationModelMessage == null ||
                (consultationModelMessage.Id == 0 &&
                string.IsNullOrEmpty(consultationModelMessage.Country)))
            {
                var error = new ErrorModel("No consultation record found.");
                return NotFound(error);
            }
            var consultationModel = new ConsultationModel
            {
                Id = consultationModelMessage.Id,
                Date = consultationModelMessage.Date.ToDateTime(),
                Country = consultationModelMessage.Country,
                State = consultationModelMessage.State,
                City = consultationModelMessage.City,
                PinCode = consultationModelMessage.PinCode,
                Problem = consultationModelMessage.Problem,
                Medicine = consultationModelMessage.Medicine,
                DoctorId = consultationModelMessage.DoctorId,
                Doctor = new DoctorModel
                {
                    Id = consultationModelMessage.Doctor!.Id,
                    FirstName = consultationModelMessage.Doctor!.FirstName,
                    LastName = consultationModelMessage.Doctor!.LastName
                },
                PatientId = consultationModelMessage.PatientId,
                Patient = new PatientModel
                {
                    Id = consultationModelMessage.Patient!.Id,
                    FirstName = consultationModelMessage.Patient!.FirstName,
                    LastName = consultationModelMessage.Patient!.LastName
                }
            };
            return Ok(consultationModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/getAddEditInitData/*'/>
        [HttpGet]
        [Route("AddEditInitData")]
        public async Task<ActionResult<ConsultationModel>> GetAddEditInitData()
        {
            DoctorModelsMessage doctorModelsMessage = await _doctorGrpcClient.GetAllAsync(new EmptyMessage());
            if (doctorModelsMessage == null ||
                doctorModelsMessage.Doctors == null ||
                doctorModelsMessage.Doctors.Count() == 0)
            {
                var error = new ErrorModel("No doctor record found.");
                return NotFound(error);
            }
            var doctorModels = new List<DoctorModel>();
            foreach (DoctorModelMessage doctorModelMessage in doctorModelsMessage.Doctors)
            {
                var doctorModel = new DoctorModel
                {
                    Id = doctorModelMessage.Id,
                    FirstName = doctorModelMessage.FirstName,
                    LastName = doctorModelMessage.LastName
                };
                doctorModels.Add(doctorModel);
            }

            PatientModelsMessage patientModelsMessage = await _patientGrpcClient.GetAllAsync(new EmptyMessage());
            if (patientModelsMessage == null ||
                patientModelsMessage.Patients == null ||
                patientModelsMessage.Patients.Count() == 0)
            {
                var error = new ErrorModel("No patient record found.");
                return NotFound(error);
            }
            var patientModels = new List<PatientModel>();
            foreach (PatientModelMessage patientModelMessage in patientModelsMessage.Patients)
            {
                var PatientModel = new PatientModel
                {
                    Id = patientModelMessage.Id,
                    FirstName = patientModelMessage.FirstName,
                    LastName = patientModelMessage.LastName
                };
                patientModels.Add(PatientModel);
            }

            var consultationModel = new ConsultationModel()
            {
                Doctors = doctorModels,
                Patients = patientModels
            };
            return Ok(consultationModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/add/*'/>
        [HttpPost]
        public async Task<IActionResult> Add(ConsultationModel consultation)
        {
            var consultationModelMessage = new ConsultationModelMessage
            {
                Id = consultation.Id,
                Date = consultation.Date.ToUniversalTime().ToTimestamp(),
                Country = consultation.Country,
                State = consultation.State,
                City = consultation.City,
                PinCode = consultation.PinCode,
                Problem = consultation.Problem,
                Medicine = consultation.Medicine,
                DoctorId = consultation.DoctorId,
                PatientId = consultation.PatientId
            };
            try
            {
                await _consultationGrpcClient.AddAsync(consultationModelMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, ConsultationModel consultation)
        {
            var updateMessage = new UpdateMessage
            {
                Id = id,
                Consultation = new ConsultationModelMessage
                {
                    Id = consultation.Id,
                    Date = consultation.Date.ToUniversalTime().ToTimestamp(),
                    Country = consultation.Country,
                    State = consultation.State,
                    City = consultation.City,
                    PinCode = consultation.PinCode,
                    Problem = consultation.Problem,
                    Medicine = consultation.Medicine,
                    DoctorId = consultation.DoctorId,
                    PatientId = consultation.PatientId
                }
            };
            try
            {
                await _consultationGrpcClient.UpdateAsync(updateMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <include file='docs.xml' path='docs/members[@name="ConsultationController"]/delete/*'/>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                var idMessage = new IdMessage { Id = id.Value };
                await _consultationGrpcClient.DeleteAsync(idMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
