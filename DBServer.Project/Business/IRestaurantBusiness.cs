using DBServer.Project.Data;
using System;
using System.Collections.Generic;

namespace DBServer.Project.Business
{
    public interface IRestaurantBusiness
    {
        public List<RestaurantModel> GetRestaurants(DateTime date);
    }
}
