using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundingDashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillsController : ControllerBase
    {

        private readonly ISkillRepository skillRepository = null;

        public SkillsController(ISkillRepository skillRepository)
        {
            this.skillRepository = skillRepository;
        }


        [HttpGet]
        public List<Skills> Get()
        {
            return skillRepository.SelectAll();
        }

        [HttpGet("{id}")]
        public Skills Get(int id)
        {
            return skillRepository.SelectByID(id);

        }

        [HttpGet("byAttribute")]
        public List<Skills> Get(int ProfessionID, int Level)
        {
            return skillRepository.SelectByProfessionAndLevel(ProfessionID, Level);
        }

        [HttpGet("byProfession")]
        public List<Skills> GetByProfession(int ProfessionID)
        {
            return skillRepository.SelectByProfession(ProfessionID);
        }

        //[HttpPost]
        //public void Post([FromBody]Skills skill)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        skillRepository.Insert(skill);
        //    }
        //}

        //[HttpPut("{SkillId}")]
        //public void Put(int SkillId, [FromBody]Skills skill)
        //{

        //    skill.SkillId = SkillId;

        //    if (ModelState.IsValid)
        //    {
        //        skillRepository.Update(skill);
        //    }
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //    skillRepository.Delete(id);
        //}

    }


}
