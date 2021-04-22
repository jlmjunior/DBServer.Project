using System;

namespace DBServer.Project.Business
{
    public interface IEstoriasBusiness
    {
        public bool CheckUser(int idUser, DateTime dateVote);
        public bool CheckRestaurant(int idRestaurant, DateTime dateVote);
    }
}
