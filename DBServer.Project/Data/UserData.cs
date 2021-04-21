using DBServer.Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace DBServer.Project.Data
{
    public class UserData : IUserData
    {
        public IEnumerable<UserModel> GetUsers()
        {
            return new[]
            {
                new UserModel()
                {
                    Id = 1,
                    UserName = "joao.pedro"
                },
                new UserModel()
                {
                    Id = 2,
                    UserName = "jose.luis"
                },
                new UserModel()
                {
                    Id = 3,
                    UserName = "ana.maria"
                }
            };
        }

        public UserModel GetUserByName(string userName)
        {
            return GetUsers()
                .ToList()
                .Find(user => user.UserName == userName);
        }
    }
}
