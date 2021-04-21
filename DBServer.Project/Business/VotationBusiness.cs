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

        public VotationBusiness(IUserData userDate, IVotationData votationData)
        {
            _userDate = userDate;
            _votationData = votationData;
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

            CheckUser(vote);
           

            return null;
        }

        private bool CheckUser(VoteModel vote)
        {
            if (!ExistUser(vote.IdUser)) return false;

            List<VoteModel> votesList = _votationData.GetVotesByDate(vote.DateVote)
                .FindAll(row => row.IdUser == vote.IdUser);

            bool hasAlreadyVoted = votesList.Count > 0;

            if (hasAlreadyVoted) return false;

            return true;
        }

        private bool CheckRestaurant()
        {
            var startDate = DateTime.Today.AddDays((int)DayOfWeek.Sunday - (int)DateTime.Today.DayOfWeek);
            var endDate = startDate.AddDays(6);

            return false;
        }

        private bool ExistUser(int id)
        {
            UserModel user = _userDate.GetById(id);

            if (user == null) return false;

            return true;
        }

    }
}
