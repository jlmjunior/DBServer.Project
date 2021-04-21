using DBServer.Project.Models;
using System;
using System.Collections.Generic;

namespace DBServer.Project.Data
{
    public interface IVotationData
    {
        public List<VoteModel> GetVotes();
        public List<VoteModel> GetVotesByDate(DateTime date);
        public void SubmitVote(VoteModel vote);
    }
}
