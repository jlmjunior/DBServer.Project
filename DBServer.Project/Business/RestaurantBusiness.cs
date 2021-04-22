using DBServer.Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Business
{
    public class RestaurantBusiness : IRestaurantBusiness
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IStoryBusiness _storyBusiness;

        public RestaurantBusiness(IRestaurantData restaurantData, IStoryBusiness storyBusiness)
        {
            _restaurantData = restaurantData;
            _storyBusiness = storyBusiness;
        }

        public List<RestaurantModel> GetRestaurants(DateTime date)
        {
            var restaurants = _restaurantData.GetAll();

            restaurants.ForEach(x => x.IsAvailable = _storyBusiness.CheckRestaurant(x.Id, date));

            return restaurants;
        }
    }
}
