using MedicalSystem.Services.Patent.Data;
using MedicalSystem.Services.Patent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MedicalSystem.Services.Patent.Controllers
{
    /// <include file='docs.xml' path='docs/members[@name="PatentController"]/patentController/*'/>
    [ApiController]
    [Route("[controller]")]
    public class PatentController
    {
        private readonly PatentContext _patentContext;

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/patentControllerConstructor/*'/>
        public PatentController(PatentContext patentContext)
        {
            _patentContext = patentContext;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/getAll/*'/>
        [HttpGet]
        public IEnumerable<PatentModel> GetAll()
        {
            var patents = _patentContext.Patents.OrderBy(patent => patent.FirstName);
            return patents;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/getById/*'/>
        [HttpGet]
        [Route("{id:int}")]
        public PatentModel GetById(int id)
        {
            var patent = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);
            return patent;
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/add/*'/>
        [HttpPost]
        public void Add(PatentModel patent)
        {
            _patentContext.Patents!.Add(patent);
            _patentContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/update/*'/>
        [HttpPut]
        [Route("{id:int}")]
        public void Update(int id, PatentModel patent)
        {
            var d = _patentContext.Patents.FirstOrDefault(patent => patent.Id == id);

            d.FirstName = patent.FirstName;
            d.LastName = patent.LastName;

            _patentContext.SaveChanges();
        }

        /// <include file='docs.xml' path='docs/members[@name="PatentController"]/delete/*'/>
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
