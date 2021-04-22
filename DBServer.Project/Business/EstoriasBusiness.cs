using DBServer.Project.Data;
using DBServer.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Business
{
    public class EstoriasBusiness : IEstoriasBusiness
    {
        private readonly IUserData _userDate;
        private readonly IVotationData _votationData;
        private readonly IRestaurantData _restaurantData;

        public EstoriasBusiness(IUserData userDate, IVotationData votationData, IRestaurantData restaurantData)
        {
            _userDate = userDate;
            _votationData = votationData;
            _restaurantData = restaurantData;
        }

        public bool CheckUser(int idUser, DateTime dateVote)
        {
            List<VoteModel> teste = _votationData.GetVotesByDate(dateVote);
            bool hasAlreadyVoted = teste.Any(row => row.IdUser.Equals(idUser));

            if (hasAlreadyVoted) return false;

            return true;
        }

        public bool CheckRestaurant(int idRestaurant, DateTime dateVote)
        {
            if (!_restaurantData.Exists(idRestaurant)) return false;

            List<int> idWinningRestaurants = new List<int>();

            var startDate = dateVote.Date.AddDays((int)DayOfWeek.Sunday - (int)dateVote.DayOfWeek);
            var endDate = startDate.AddDays(6);

            var votesOfWeek = _votationData.GetVotes()
                .Where(x => x.DateVote.Date >= startDate && x.DateVote.Date <= endDate);

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
