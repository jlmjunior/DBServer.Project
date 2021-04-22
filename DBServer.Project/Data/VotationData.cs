using DBServer.Project.Models;
using System;
using System.Collections.Generic;

namespace DBServer.Project.Data
{
    public class VotationData : IVotationData
    {
        private List<VoteModel> _votes = new List<VoteModel>();

        public List<VoteModel> GetVotes()
        {
            return _votes;
        }
        
        public List<VoteModel> GetVotesByDate(DateTime date)
        {
            return _votes.FindAll(vote => vote.DateVote.Date == date.Date);
        }
        
        public void SubmitVote(VoteModel vote)
        {
            _votes.Add(vote);
        }
    }
}
