using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundingDashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessionsController : ControllerBase
    {
        private readonly IProfessionRepository professionRepository = null;

        public ProfessionsController(IProfessionRepository professionRepository)
        {
            this.professionRepository = professionRepository;
        }


        [HttpGet]
        public List<Professions> Get()
        {
            return professionRepository.SelectAll();
        }

        [HttpGet("{id}")]
        public Professions Get(int id)
        {
            return professionRepository.SelectByID(id);
        }

        //[HttpPost]
        //public void Post([FromBody]Professions profession)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        professionRepository.Insert(profession);
        //    }
        //}

        //[HttpPut("{id}")]
        //public void Update(int id, [FromBody]Professions profession)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        professionRepository.Update(profession);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    professionRepository.Delete(id);
        //}

    }
}
