using DBServer.Project.Data;
using DBServer.Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Business
{
    public class VotationBusiness : IVotationBusiness
    {
        private readonly IUserData _userDate;
        private readonly IVotationData _votationData;
        private readonly IRestaurantData _restaurantData;

        public VotationBusiness(IUserData userDate, IVotationData votationData, IRestaurantData restaurantData)
        {
            _userDate = userDate;
            _votationData = votationData;
            _restaurantData = restaurantData;
        }

        public List<ReturnVotesModel> GetVotes(DateTime date)
        {
            List<ReturnVotesModel> resultVotes = new List<ReturnVotesModel>();

            var restaurants = _votationData.GetVotesByDate(date)
                .GroupBy(x => x.IdRestaurant);

            var votesDate = _votationData.GetVotesByDate(date);

            foreach (var restaurant in restaurants)
            {
                resultVotes.Add(new ReturnVotesModel()
                {
                    RestaurantName = _restaurantData.GetById(restaurant.Key).Name,
                    Votes = votesDate.FindAll(x => x.IdRestaurant == restaurant.Key).Count()
                }); ;
            }

            return resultVotes;
        }

        public ReturnModel SubmitVote(VoteModel vote)
        {
            DateTime limitTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 11, 45, 0);

            if (vote.DateVote.TimeOfDay > limitTime.TimeOfDay)
            {
                return new ReturnModel()
                {
                    Success = false,
                    Message = "Fora do horário de votação"
                };
            }

            bool userValid = CheckUser(vote);
            bool restaurantValid = CheckRestaurant(vote);

            if (userValid && restaurantValid)
            {
                _votationData.SubmitVote(vote);

                return new ReturnModel()
                {
                    Success = true,
                    Message = "Voto realizado com sucesso"
                };
            }

            return new ReturnModel()
            {
                Success = false,
                Message = userValid ? "Restaurante já selecionado está semana" : "Você já votou"
            };
        }

        private bool CheckUser(VoteModel vote)
        {
            if (!_userDate.Exists(vote.IdUser)) return false;

            List<VoteModel> teste = _votationData.GetVotesByDate(vote.DateVote);
            bool hasAlreadyVoted = teste.Any(row => row.IdUser.Equals(vote.IdUser));

            if (hasAlreadyVoted) return false;

            return true;
        }

        private bool CheckRestaurant(VoteModel vote)
        {
            if (!_restaurantData.Exists(vote.IdRestaurant)) return false;

            List<int> idWinningRestaurants = new List<int>();

            var startDate = vote.DateVote.Date.AddDays((int)DayOfWeek.Sunday - (int)vote.DateVote.DayOfWeek);
            var endDate = startDate.AddDays(6);

            var votesOfWeek = _votationData.GetVotes()
                .Where(x => x.DateVote.Date >= startDate && x.DateVote.Date <= endDate);

            var datesVoted = votesOfWeek.GroupBy(x => x.DateVote);

            foreach (var date in datesVoted)
            {
                if (vote.DateVote.Date == date.Key.Date) continue;

                var winningRestaurant = votesOfWeek
                    .Where(x => x.DateVote.Date == date.Key.Date)
                    .GroupBy(x => x.IdRestaurant)
                    .OrderByDescending(x => x.Count())
                    .First();

                idWinningRestaurants.Add(winningRestaurant.Key);
            }

            return !idWinningRestaurants.Contains(vote.IdRestaurant);
        }
    }
}
