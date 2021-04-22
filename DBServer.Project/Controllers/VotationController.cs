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
        public IActionResult GetVotes(DateTime date)
        {
            DateTime confirmDate = DateTime.Now;

            if (date != null) confirmDate = date;

            try
            {
                var votes = _votationBusiness.GetVotes(confirmDate);

                return Ok(votes);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("submitvote")]
        public IActionResult SubmitVote([FromBody] VoteModel vote)
        {
            if (vote == null) return BadRequest();

            try
            {
                ReturnModel result = _votationBusiness.SubmitVote(vote);

                if (result.Success)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
