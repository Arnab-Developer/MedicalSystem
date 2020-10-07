using MedicalSystem.Gateways.WebGateway.GrpcClients.Doctors;
using MedicalSystem.Gateways.WebGateway.Models;
using MedicalSystem.Gateways.WebGateway.Protos.Doctors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalSystem.Gateways.WebGateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorGrpcClient _doctorGrpcClient;

        public DoctorController(IDoctorGrpcClient doctorGrpcClient)
        {
            _doctorGrpcClient = doctorGrpcClient;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpGet]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpPut]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        [HttpDelete]
        [Route("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
