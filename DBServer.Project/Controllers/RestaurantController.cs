using DBServer.Project.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBServer.Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantController : ControllerBase
    {
        private readonly IRestaurantBusiness _restaurantBusiness;

        public RestaurantController(IRestaurantBusiness restaurantBusiness)
        {
            _restaurantBusiness = restaurantBusiness;
        }

        [HttpGet]
        public IActionResult GetRestaurants(DateTime date)
        {
            DateTime confirmDate = DateTime.Now;

            if (date != null) confirmDate = date;

            try
            {
                var restaurants = _restaurantBusiness.GetRestaurants(confirmDate);

                return Ok(restaurants);
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
