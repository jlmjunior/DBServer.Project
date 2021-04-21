using DBServer.Project.Models;

namespace DBServer.Project.Business
{
    public interface IVotationBusiness
    {
        public ReturnModel SubmitVote(VoteModel vote);
    }
}
