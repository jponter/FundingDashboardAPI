using FundingDashboardAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Repositories
{
    public interface ISkillRepository
    {

        List<Skills> SelectAll();
        List<Skills> SelectByProfession(int ProfessionID);

        List<Skills> SelectByProfessionAndLevel(int ProfessionID, int Level);
        Skills SelectByID(int skillID);
        void Insert(Skills skill);
        void Update(Skills skill);
        void Delete(int id);
    }
}
