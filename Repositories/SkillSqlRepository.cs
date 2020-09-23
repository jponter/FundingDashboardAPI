using FundingDashboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Repositories
{
    public class SkillSqlRepository : ISkillRepository
    {
        private readonly AppDbContext db = null;

        public SkillSqlRepository(AppDbContext db)
        {
            this.db = db;
        }


        public void Delete(int SkillId)
        {
            int count = db.Database.ExecuteSqlRaw("DELETE FROM Skills WHERE SkillID={0}", SkillId);
        }

        public void Insert(Skills skill)
        {
            int count = db.Database.ExecuteSqlRaw("INSERT INTO Skills(SkillCode, ProfessionID, SkillText, SkillLevel) VALUES({0},{1},{2},{3})", skill.SkillCode, skill.ProfessionId, skill.SkillText, skill.SkillLevel);
        }

        public List<Skills> SelectAll()
        {
            List<Skills> data = db.Skills.FromSqlRaw("SELECT SkillID, SkillCode, ProfessionID, SkillText, SkillLevel FROM Skills ORDER BY SkillCode ASC").ToList();
            return data;
        }

        public List<Skills> SelectByProfession(int ProfessionID)
        {
            List<Skills> data = db.Skills.FromSqlRaw("SELECT SkillID, SkillCode, ProfessionID, SkillText, SkillLevel FROM Skills WHERE ProfessionID={0}", ProfessionID).ToList();
            return data;

        }

        public Skills SelectByID(int SkillID)
        {
            Skills data = db.Skills.FromSqlRaw("SELECT SkillID, SkillCode, ProfessionID, SkillText, SkillLevel FROM Skills WHERE SkillID={0}", SkillID).FirstOrDefault();
            return data;
        }

        public void Update(Skills skill)
        {
            int count = db.Database.ExecuteSqlRaw("UPDATE Skills SET SkillCode={0}, ProfessionID={1}, SkillText={2}, SkillLevel={3} WHERE SkillID={4}", skill.SkillCode, skill.ProfessionId, skill.SkillText, skill.SkillLevel, skill.SkillId);
        }

        List<Skills> ISkillRepository.SelectByProfessionAndLevel(int ProfessionID, int Level)
        {
            List<Skills> data = db.Skills.FromSqlRaw("SELECT SkillID, SkillCode, ProfessionID, SkillText, SkillLevel FROM Skills WHERE ProfessionID={0} AND SkillLevel={1}", ProfessionID, Level).ToList();
            return data;
        }
    }
}
