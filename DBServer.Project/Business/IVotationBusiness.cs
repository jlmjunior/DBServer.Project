using DBServer.Project.Models;
using System;
using System.Collections.Generic;

namespace DBServer.Project.Business
{
    public interface IVotationBusiness
    {
        public List<ReturnVotesModel> GetVotes(DateTime date);
        public ReturnModel SubmitVote(VoteModel vote);
    }
}
