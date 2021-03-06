﻿using FundingDashboardAPI.Models;
using FundingDashboardAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FundingDashboardAPI.Controllers
{
    [Authorize]
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

        [HttpGet("byCSP")]
        public async Task<List<Funding>> GetByCSP(string CSP)
        {
            var funding = await fundingRepository.SelectByCSP(CSP);
            return funding;
        }

        [HttpGet("bySL")]
        public async Task<List<Funding>> GetBySL(string SL)
        {
            var funding = await fundingRepository.SelectByServiceLine(SL);
            return funding;
        }

        [HttpGet("byRegion")]
        public async Task<List<Funding>> GetByRegion(string region)
        {
            var funding = await fundingRepository.SelectByRegion(region);
            return funding;
        }

    }
}
