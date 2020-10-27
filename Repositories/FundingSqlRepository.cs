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

        public async Task<bool> Delete(int id)
        {
            Funding funding = new Funding();
            funding = await db.Funding.FirstOrDefaultAsync(f => f.Id == id);

            db.Remove(funding);
            int count = await db.SaveChangesAsync();

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

        public async Task<bool> Insert(Funding funding)
        {
            if (funding != null)
            {
                db.Add(funding);
                int count = await db.SaveChangesAsync();

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

        public async Task<List<Funding>> SelectAll()
        {
            List<Funding> funding = new List<Funding>();
            funding = await db.Funding.AsNoTracking().Where(f => f.Archived.Equals(false))
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
                int count = await db.SaveChangesAsync();

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

        public async Task<List<Funding>> SelectByCSP(string CSP)
        {
            List<Funding> funding = new List<Funding>();
            funding = await db.Funding.AsNoTracking().Where(f => f.CSP.Equals(CSP))
                .ToListAsync();
            return funding;
        }

        public async Task<List<Funding>> SelectByServiceLine(string SL)
        {
            List<Funding> funding = new List<Funding>();
            funding = await db.Funding.AsNoTracking().Where(f => f.ServiceLine.Contains(SL))
                .ToListAsync();
            return funding;
        }

        public async Task<List<Funding>> SelectByRegion(string Region)
        {
            switch (Region)
            {
                case "uk":
                    {
                        List<Funding> funding = new List<Funding>();
                        funding = await db.Funding.AsNoTracking().Where(f => f.UK.Equals(true))
                            .ToListAsync();
                        return funding;
                    }
                case "usa":
                    {
                        List<Funding> funding = new List<Funding>();
                        funding = await db.Funding.AsNoTracking().Where(f => f.USA.Equals(true))
                            .ToListAsync();
                        return funding;
                    }
                case "eur":
                    {
                        List<Funding> funding = new List<Funding>();
                        funding = await db.Funding.AsNoTracking().Where(f => f.EUR.Equals(true))
                            .ToListAsync();
                        return funding;
                    }
                case "asia":
                    {
                        List<Funding> funding = new List<Funding>();
                        funding = await db.Funding.AsNoTracking().Where(f => f.ASIA.Equals(true))
                            .ToListAsync();
                        return funding;
                    }
                default:
                    {
                        List<Funding> funding = new List<Funding>();
                        return funding;
                    }
            }




        }



    }
}
