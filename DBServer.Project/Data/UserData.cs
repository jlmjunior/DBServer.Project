using DBServer.Project.Models;
using System.Collections.Generic;
using System.Linq;

namespace DBServer.Project.Data
{
    public class UserData : IUserData
    {
        public List<UserModel> GetAll()
        {
            return new List<UserModel>
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

        public UserModel GetById(int id)
        {
            return GetAll().Find(user => user.Id == id);
        }

        public bool Exists(int id)
        {
            return GetAll().Any(user => user.Id.Equals(id));
        }
    }
}
