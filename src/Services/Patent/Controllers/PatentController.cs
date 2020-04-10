using MedicalSystem.Services.Patent.Data;
using MedicalSystem.Services.Patent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Patent.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PatentController
    {
        private readonly PatentContext _patentContext;

        public PatentController(PatentContext patentContext)
        {
            _patentContext = patentContext;
        }

        [HttpGet]
        public IEnumerable<PatentModel> GetAll()
        {
            var patents = _patentContext.Patents.OrderBy(patent => patent.FirstName);
            return patents;
        }

        [HttpGet]
        [Route("{id:int}")]
        public PatentModel GetById(int id)
        {
            var patent = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);
            return patent;
        }

        [HttpPost]
        public void Add(PatentModel patent)
        {
            _patentContext.Patents.Add(patent);
            _patentContext.SaveChanges();
        }

        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, PatentModel patent)
        {
            var d = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);

            d.FirstName = patent.FirstName;
            d.LastName = patent.LastName;

            _patentContext.SaveChanges();
        }

        [HttpDelete]
        [Route("{id:int}")]
        public void Delete(int id)
        {
            var patent = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);
            _patentContext.Patents.Remove(patent);
            _patentContext.SaveChanges();
        }
    }
}
