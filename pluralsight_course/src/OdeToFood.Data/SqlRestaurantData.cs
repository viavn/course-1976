using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Restaurant> ListByName(string name)
        {
            var query = from r in _context.Restaurants
                        where string.IsNullOrEmpty(name) || r.Name.ToUpper().StartsWith(name.ToUpper())
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant GetById(int id)
        {
            return _context.Restaurants.Find(id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var entity = _context.Restaurants.Attach(restaurant);
            entity.State = EntityState.Modified;
            return restaurant;
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if (restaurant != null)
            {
                _context.Restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }
    }
}