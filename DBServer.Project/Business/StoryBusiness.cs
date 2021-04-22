using DBServer.Project.Data;
using DBServer.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Business
{
    public class StoryBusiness : IStoryBusiness
    {
        private readonly IVotationData _votationData;

        public StoryBusiness(IVotationData votationData)
        {
            _votationData = votationData;
        }

        public bool CheckUser(int idUser, DateTime dateVote)
        {
            List<VoteModel> votes = _votationData.GetVotesByDate(dateVote);
            bool hasAlreadyVoted = votes.Any(row => row.IdUser.Equals(idUser));

            if (hasAlreadyVoted) return false;

            return true;
        }

        public bool CheckRestaurant(int idRestaurant, DateTime dateVote)
        {
            List<int> idWinningRestaurants = new List<int>();

            var startWeek = dateVote.Date.AddDays((int)DayOfWeek.Sunday - (int)dateVote.DayOfWeek);
            var endWeek = startWeek.AddDays(6);

            var votesOfWeek = _votationData.GetVotes()
                .Where(x => x.DateVote.Date >= startWeek && x.DateVote.Date <= endWeek);

            var datesVoted = votesOfWeek.GroupBy(x => x.DateVote);

            foreach (var date in datesVoted)
            {
                if (dateVote.Date == date.Key.Date) continue;

                var winningRestaurant = votesOfWeek
                    .Where(x => x.DateVote.Date == date.Key.Date)
                    .GroupBy(x => x.IdRestaurant)
                    .OrderByDescending(x => x.Count())
                    .First();

                idWinningRestaurants.Add(winningRestaurant.Key);
            }

            return !idWinningRestaurants.Contains(idRestaurant);
        }
    }
}
