using DBServer.Project.Business;
using DBServer.Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VotationController : ControllerBase
    {
        private readonly IVotationBusiness _votationBusiness;

        public VotationController(IVotationBusiness votationBusiness)
        {
            _votationBusiness = votationBusiness;
        }

        [HttpGet]
        public IActionResult GetVotes(DateTime dateParameter)
        {
            DateTime date = DateTime.Now;

            if (dateParameter != null)
            {
                date = dateParameter;
            }

            var votes = _votationBusiness.GetVotes(date);

            return Ok(votes);
        }

        [HttpPost("submitvote")]
        public IActionResult SubmitVote([FromBody] VoteModel vote)
        {
            if (vote == null) return BadRequest();

            //vote.DateVote = DateTime.Today;

            ReturnModel result = _votationBusiness.SubmitVote(vote);

            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
