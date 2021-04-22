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
                    Name = "Bom apetite",
                    ImageLink = null,
                    IsAvailable = true
                },
                new RestaurantModel()
                {
                    Id = 3,
                    Name = "Restaurante comida caseira",
                    ImageLink = null,
                    IsAvailable = true
                },
                new RestaurantModel()
                {
                    Id = 4,
                    Name = "King food",
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
