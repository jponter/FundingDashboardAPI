using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FundingDashboardAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FundingController : ControllerBase
    {

        private readonly IFundingRepository fundingRepository = null;

        public FundingController(IFundingRepository fundingRepository)
        {
            this.fundingRepository = fundingRepository;
        }

        public async Task<List<Funding>> Get()
        {
            var funding = await fundingRepository.SelectAll();
            return funding;
        }

    }
}
