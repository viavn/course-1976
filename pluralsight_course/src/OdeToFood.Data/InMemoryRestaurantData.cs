using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        private static List<Restaurant> restaurants;

        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>
            {
                new Restaurant { Id = 1, Name = "Vinicius's Pizza", Location = "Pq Novo Mundo", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Scott's Pizza", Location = "Maryland", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 3, Name = "Cinnamon Club", Location = "London", Cuisine = CuisineType.Indian },
                new Restaurant { Id = 4, Name = "La Costa", Location = "California", Cuisine = CuisineType.Mexican }
            };
        }

        public IEnumerable<Restaurant> ListByName(string name = null)
        {
            return from r in restaurants
                   where string.IsNullOrEmpty(name) || r.Name.ToUpper().StartsWith(name.ToUpper())
                   orderby r.Name
                   select r;
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurants.Add(restaurant);
            restaurant.Id = restaurants.Max(x => x.Id) + 1;
            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var updateRestaurant = restaurants.SingleOrDefault(x => x.Id == restaurant.Id);
            if (updateRestaurant != null)
            {
                updateRestaurant.Name = restaurant.Name;
                updateRestaurant.Location = restaurant.Location;
                updateRestaurant.Cuisine = restaurant.Cuisine;
            }
            return updateRestaurant;
        }

        public int Commit() => 0;
    }
}
