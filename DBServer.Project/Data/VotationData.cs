using DBServer.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Data
{
    public class VotationData : IVotationData
    {
        private static List<VoteModel> _votes = new List<VoteModel>();

        public List<VoteModel> GetVotes()
        {
            return _votes;
        }
        
        public List<VoteModel> GetVotesByDate(DateTime date)
        {
            return _votes.FindAll(vote => vote.DateVote.ToString("dd/MM/yyyy") == date.ToString("dd/MM/yyyy"));
        }

        public void SubmitVote(VoteModel vote)
        {
            _votes.Add(vote);
        }
    }
}
