using System.Collections.Generic;
using System.Linq;

namespace DBServer.Project.Data
{
    public class RestaurantData : IRestaurantData
    {
        public List<RestaurantModel> GetAll()
        {
            return new List<RestaurantModel>
            {
                new RestaurantModel()
                {
                    Id = 1,
                    Name = "Chefe do bairro",
                    ImageLink = null,
                    IsAvailable = false
                },
                new RestaurantModel()
                {
                    Id = 2,
                    Name = "Teste 2",
                    ImageLink = null,
                    IsAvailable = true
                },
                new RestaurantModel()
                {
                    Id = 3,
                    Name = "Teste 3",
                    ImageLink = null,
                    IsAvailable = true
                },
                new RestaurantModel()
                {
                    Id = 4,
                    Name = "Teste 4",
                    ImageLink = null,
                    IsAvailable = true
                }
            };
        }

        public RestaurantModel GetById(int id)
        {
            return GetAll().Find(restaurant => restaurant.Id == id);
        }

        public bool Exists(int id)
        {
            return GetAll().Any(restaurant => restaurant.Id.Equals(id));
        }
    }
}
