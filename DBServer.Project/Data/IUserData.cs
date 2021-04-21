using DBServer.Project.Models;
using System.Collections.Generic;

namespace DBServer.Project.Data
{
    public interface IUserData
    {
        public IEnumerable<UserModel> GetUsers();
        public UserModel GetUserByName(string userName);
    }
}
