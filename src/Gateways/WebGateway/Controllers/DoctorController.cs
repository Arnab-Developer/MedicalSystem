using MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorGrpcClient _doctorGrpcClient;

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/doctorControllerConstructor/*'/>
        public DoctorController(IDoctorGrpcClient doctorGrpcClient)
        {
            _doctorGrpcClient = doctorGrpcClient;
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getAll/*'/>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorModel>>> GetAll()
        {
            DoctorModelsMessage doctorModelsMessage;
            try
            {
                doctorModelsMessage = await _doctorGrpcClient.GetAllAsync(new EmptyMessage());
            }
            catch
            {
                return StatusCode(500);
            }
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
            return Ok(doctorModels);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<DoctorModel>> GetById(int? id)
        {
            if (id == null)
            {
                var error = new ErrorModel("Id is null");
                return NotFound(error);
            }
            DoctorModelMessage doctorModelMessage;
            try
            {
                var idMessage = new IdMessage { Id = id.Value };
                doctorModelMessage = await _doctorGrpcClient.GetByIdAsync(idMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            if (doctorModelMessage == null ||
                (doctorModelMessage.Id == 0 &&
                string.IsNullOrEmpty(doctorModelMessage.FirstName) &&
                string.IsNullOrEmpty(doctorModelMessage.LastName)))
            {
                var error = new ErrorModel("No doctor record found.");
                return NotFound(error);
            }
            var doctorModel = new DoctorModel
            {
                Id = doctorModelMessage.Id,
                FirstName = doctorModelMessage.FirstName,
                LastName = doctorModelMessage.LastName
            };
            return Ok(doctorModel);
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/add/*'/>
        [HttpPost]
        public async Task<IActionResult> Add(DoctorModel doctor)
        {
            var doctorModelMessage = new DoctorModelMessage
            {
                Id = doctor.Id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName
            };
            try
            {
                await _doctorGrpcClient.AddAsync(doctorModelMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(int id, DoctorModel doctor)
        {
            var updateMessage = new UpdateMessage
            {
                Id = id,
                Doctor = new DoctorModelMessage
                {
                    Id = doctor.Id,
                    FirstName = doctor.FirstName,
                    LastName = doctor.LastName
                }
            };
            try
            {
                await _doctorGrpcClient.UpdateAsync(updateMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }

        /// <include file='docs.xml' path='docs/members[@name="DoctorController"]/delete/*'/>
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
                await _doctorGrpcClient.DeleteAsync(idMessage);
            }
            catch
            {
                return StatusCode(500);
            }
            return Ok();
        }
    }
}
