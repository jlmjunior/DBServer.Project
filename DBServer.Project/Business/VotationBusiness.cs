using DBServer.Project.Data;
using DBServer.Project.Models;
using Microsoft.Extensions.Configuration;
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
        private readonly IStoryBusiness _storyBusiness;
        private readonly IConfiguration _configuration;

        public VotationBusiness(IUserData userDate, IVotationData votationData, IRestaurantData restaurantData, 
            IStoryBusiness storyBusiness, IConfiguration configuration)
        {
            _userDate = userDate;
            _votationData = votationData;
            _restaurantData = restaurantData;
            _storyBusiness = storyBusiness;
            _configuration = configuration;
        }

        public List<ReturnVotesModel> GetVotes(DateTime date)
        {
            List<ReturnVotesModel> resultVotes = new List<ReturnVotesModel>();

            var votesDate = _votationData.GetVotesByDate(date);

            var restaurants = votesDate.GroupBy(x => x.IdRestaurant);

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
            bool activeLimitTimeVote = Convert.ToBoolean(_configuration["LimitTime:Active"]);

            if (activeLimitTimeVote)
            {
                bool timesUp = LimitTimeVote(vote);

                if (timesUp)
                {
                    return new ReturnModel()
                    {
                        Success = false,
                        Message = "Fora do horário de votação"
                    };
                }
            }

            if (!_userDate.Exists(vote.IdUser) || !_restaurantData.Exists(vote.IdRestaurant))
            {
                return new ReturnModel()
                {
                    Success = false,
                    Message = "Usuário ou restaurante não encontrado"
                };
            }

            bool userValid = _storyBusiness.CheckUser(vote.IdUser, vote.DateVote);
            bool restaurantValid = _storyBusiness.CheckRestaurant(vote.IdRestaurant, vote.DateVote);

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

        private bool LimitTimeVote(VoteModel vote)
        {
            int hourLimit = Convert.ToInt32(_configuration["LimitTime:Hour"]);
            int minuteLimit = Convert.ToInt32(_configuration["LimitTime:Minute"]);

            DateTime limitTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month,
                DateTime.Now.Day, hourLimit, minuteLimit, 0);

            if (vote.DateVote.TimeOfDay > limitTime.TimeOfDay)
            {
                return true;
            }

            return false;
        }
    }
}
