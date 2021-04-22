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
        private readonly IEstoriasBusiness _estoriasBusiness;

        public VotationBusiness(IUserData userDate, IVotationData votationData, IRestaurantData restaurantData, IEstoriasBusiness estoriasBusiness)
        {
            _userDate = userDate;
            _votationData = votationData;
            _restaurantData = restaurantData;
            _estoriasBusiness = estoriasBusiness;
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

            if (!_userDate.Exists(vote.IdUser) || !_restaurantData.Exists(vote.IdRestaurant))
            {
                return new ReturnModel()
                {
                    Success = false,
                    Message = "Erro"
                };
            }

            bool userValid = _estoriasBusiness.CheckUser(vote.IdUser, vote.DateVote);
            bool restaurantValid = _estoriasBusiness.CheckRestaurant(vote.IdRestaurant, vote.DateVote);

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
    }
}
