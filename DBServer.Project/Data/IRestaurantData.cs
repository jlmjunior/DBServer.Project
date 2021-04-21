using System.Collections.Generic;

namespace DBServer.Project.Data
{
    public interface IRestaurantData
    {
        public IEnumerable<RestaurantModel> GetRestaurants();
    }
}
