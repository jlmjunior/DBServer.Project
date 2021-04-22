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
        private readonly IEstoriasBusiness _estoriasBusiness;

        public RestaurantBusiness(IRestaurantData restaurantData, IEstoriasBusiness estoriasBusiness)
        {
            _restaurantData = restaurantData;
            _estoriasBusiness = estoriasBusiness;
        }

        public List<RestaurantModel> GetRestaurants(DateTime date)
        {
            var restaurants = _restaurantData.GetAll();

            restaurants.ForEach(x => x.IsAvailable = _estoriasBusiness.CheckRestaurant(x.Id, date));

            return restaurants;
        }
    }
}
