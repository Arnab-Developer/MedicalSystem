using MedicalSystem.Services.Patent.Data;
using MedicalSystem.Services.Patent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Patent.Controllers
{
    /// <summary>
    /// Controller for patent.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class PatentController
    {
        private readonly PatentContext _patentContext;

        /// <summary>
        /// Create a new object of patent controller.
        /// </summary>
        /// <param name="patentContext"></param>
        public PatentController(PatentContext patentContext)
        {
            _patentContext = patentContext;
        }

        /// <summary>
        /// Get all patent data.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PatentModel> GetAll()
        {
            var patents = _patentContext.Patents.OrderBy(patent => patent.FirstName);
            return patents;
        }

        /// <summary>
        /// Get patent data by id.
        /// </summary>
        /// <param name="id">Id of patent.</param>
        /// <returns>Patent data.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public PatentModel GetById(int id)
        {
            var patent = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);
            return patent;
        }

        /// <summary>
        /// Create new patent data.
        /// </summary>
        /// <param name="patent">Patent object.</param>
        [HttpPost]
        public void Add(PatentModel patent)
        {
            _patentContext.Patents!.Add(patent);
            _patentContext.SaveChanges();
        }

        /// <summary>
        /// Update existing patent object.
        /// </summary>
        /// <param name="id">Id of patent.</param>
        /// <param name="patent">Patent object.</param>
        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, PatentModel patent)
        {
            var d = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);

            d.FirstName = patent.FirstName;
            d.LastName = patent.LastName;

            _patentContext.SaveChanges();
        }

        /// <summary>
        /// Delete patent object.
        /// </summary>
        /// <param name="id">Id of patent.</param>
        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var patent = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);
            _patentContext.Patents!.Remove(patent);
            _patentContext.SaveChanges();
        }
    }
}
