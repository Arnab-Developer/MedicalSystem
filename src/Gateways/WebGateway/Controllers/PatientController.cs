using Grpc.Net.Client;
using MedicalSystem.Gateways.WebGateway.GrpcClients.Patients;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Options;
using MedicalSystem.Gateways.WebGateway.Protos.Patients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly IPatientGrpcClient _patientGrpcClient;

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/patientControllerConstructor/*'/>
        public PatientController(IPatientGrpcClient doctorGrpcClient)
        {
            _patientGrpcClient = doctorGrpcClient;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getAll/*'/>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PatientModel>>> GetAll()
        {
            PatientModelsMessage patientModelsMessage;
            try
            {
                patientModelsMessage = await _patientGrpcClient.GetAllAsync(new EmptyMessage());
            }
            catch
            {
                return StatusCode(500);
            }
            if (patientModelsMessage == null ||
                patientModelsMessage.Patients == null ||
                patientModelsMessage.Patients.Count() == 0)
            {
                var error = new ErrorModel("No Patient record found.");
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
            return Ok(patientModels);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<PatientModel>> GetById(int? id)
        {
            if (id == null)
            {
                var error = new ErrorModel("Id is null");
                return NotFound(error);
            }
            PatientModelMessage patientModelMessage;
            try
            {
                var idMessage = new IdMessage { Id = id.Value };
                patientModelMessage = await _patientGrpcClient.GetByIdAsync(idMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            if (patientModelMessage == null ||
                (patientModelMessage.Id == 0 &&
                string.IsNullOrEmpty(patientModelMessage.FirstName) &&
                string.IsNullOrEmpty(patientModelMessage.LastName)))
            {
                var error = new ErrorModel("No patient record found.");
                return NotFound(error);
            }
            var patientModel = new PatientModel
            {
                Id = patientModelMessage.Id,
                FirstName = patientModelMessage.FirstName,
                LastName = patientModelMessage.LastName
            };
            return Ok(patientModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/add/*'/>
        [HttpPost]
        public async Task<IActionResult> Add(PatientModel patient)
        {
            var patientModelMessage = new PatientModelMessage
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName
            };
            try
            {
                await _patientGrpcClient.AddAsync(patientModelMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, PatientModel patient)
        {
            var updateMessage = new UpdateMessage
            {
                Id = id,
                Patient = new PatientModelMessage
                {
                    Id = patient.Id,
                    FirstName = patient.FirstName,
                    LastName = patient.LastName
                }
            };
            try
            {
                await _patientGrpcClient.UpdateAsync(updateMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatientController"]/delete/*'/>
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
                await _patientGrpcClient.DeleteAsync(idMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
