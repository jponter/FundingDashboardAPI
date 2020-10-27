using FundingDashboardAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Repositories
{
    public interface IFundingRepository
    {
        public Task<List<Funding>> SelectAll();

        public Task<List<Funding>> SelectAllNotArchived();

        public Task<List<Funding>> SelectByCSP(string CSP);
        // Azure, AWS, GCP are the only acceptable strings

        public Task<Funding> SelectById(int id);

        public Task<List<Funding>> SelectByRegion(string Region);
        public Task<List<Funding>> SelectByServiceLine(string ServiceLine);

        public Task<bool> Insert(Funding funding);
        public Task<bool> Update(Funding funding);
        public Task<bool> Delete(int id);

    }
}
