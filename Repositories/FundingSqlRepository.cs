using FundingDashboardAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Repositories
{
    public class FundingSqlRepository : IFundingRepository
    {
        private readonly AppDbContext db = null;

        public FundingSqlRepository(AppDbContext db)
        {
            this.db = db;
        }

        public Task<bool> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Funding funding)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Funding>> SelectAll()
        {
            List<Funding> funding = new List<Funding>();
            funding = await db.Funding.AsNoTracking()
                .ToListAsync();
            return funding;
        }

        public async Task<Funding> SelectById(int id)
        {
            Funding funding = new Funding();
            funding = await db.Funding.AsNoTracking()
                .FirstOrDefaultAsync(f => f.Id == id);
            return funding;
        }

        public async Task<bool> Update(Funding funding)
        {
            if (funding != null)
            {
                db.Update(funding);
                int count =  await db.SaveChangesAsync();

                if (count > 0)
                {
                    return true;
                    //update was successful
                }
                else
                {
                    return false;
                }

            }
            return false;
           
        }

        public Task<List<Funding>> SelectAllNotArchived()
        {
            throw new NotImplementedException();
        }

        public Task<List<Funding>> SelectByCSP(string CSP)
        {
            throw new NotImplementedException();
        }

        public Task<List<Funding>> SelectByRegion(string Region)
        {
            throw new NotImplementedException();
        }

        public Task<List<Funding>> SelectByServiceLine(string ServiceLine)
        {
            throw new NotImplementedException();
        }

    
    }
}
