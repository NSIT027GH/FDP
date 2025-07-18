using System;
using System.Collections.Generic;
using static FDP.WebServer.Components.Service.RestaurantApp.RestaurantService;
namespace FDP.WebServer.Components.Service.RestaurantApp
{
    public class RestaurantService
    {
        public List<Restaurant> GetRestaurants() => new List<Restaurant>
        {
            new Restaurant
            {
                Id = 1,
                Name = "The Gourmet Kitchen",
                Location = "123 Main St, New York, NY",
                Rating = 4.5,
                Cuisine = "Italian",
                OpeningDate = new DateTime(2015, 6, 15),
                Latitude = 40.712776,
                Longitude = -74.005974,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Spaghetti Carbonara", Price = 12.99 },
                    new MenuItem { Name = "Margherita Pizza", Price = 10.50 },
                    new MenuItem { Name = "Tiramisu", Price = 6.75 }
                }
            },
            new Restaurant
            {
                Id = 2,
                Name = "Sushi Paradise",
                Location = "456 Ocean Blvd, Los Angeles, CA",
                Rating = 4.8,
                Cuisine = "Japanese",
                OpeningDate = new DateTime(2018, 3, 22),
                Latitude = 34.052235,
                Longitude = -118.243683,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "California Roll", Price = 8.99 },
                    new MenuItem { Name = "Salmon Nigiri", Price = 9.50 },
                    new MenuItem { Name = "Miso Soup", Price = 3.25 }
                }
            },
            new Restaurant
            {
                Id = 3,
                Name = "Spice Symphony",
                Location = "789 Curry Ave, Chicago, IL",
                Rating = 4.2,
                Cuisine = "Indian",
                OpeningDate = new DateTime(2020, 1, 10),
                Latitude = 41.878113,
                Longitude = -87.629799,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Butter Chicken", Price = 13.75 },
                    new MenuItem { Name = "Palak Paneer", Price = 11.25 },
                    new MenuItem { Name = "Garlic Naan", Price = 2.50 }
                }
            },
            new Restaurant
            {
                Id = 4,
                Name = "Taco Fiesta 4",
                Location = "Chicago, IL",
                Rating = 4.1,
                Cuisine = "French",
                OpeningDate = new DateTime(2012, 7, 11),
                Latitude = 38.78349,
                Longitude = -72.829308,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Hot Dog", Price = 6.83 },
                    new MenuItem { Name = "Gyro", Price = 5.76 },
                    new MenuItem { Name = "Sushi Roll", Price = 17.25 }
                }
            },
            new Restaurant
            {
                Id = 5,
                Name = "Burger Haven 5",
                Location = "Jacksonville, FL",
                Rating = 4.5,
                Cuisine = "French",
                OpeningDate = new DateTime(2018,10,02),
                Latitude = 38.717883,
                Longitude = -79.097263,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Hot Dog", Price = 12.58 },
                    new MenuItem { Name = "Tempura", Price = 19.17 },
                    new MenuItem { Name = "Burger", Price = 11.55 }
                }
            },
            new Restaurant
            {
                Id = 6,
                Name = "Pasta House 6",
                Location = "Austin, TX",
                Rating = 4.9,
                Cuisine = "Korean",
                OpeningDate = new DateTime(2019,07,08),
                Latitude = 36.14736,
                Longitude = -70.467493,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Wonton Soup", Price = 6.88 },
                    new MenuItem { Name = "Donut", Price = 10.48 },
                    new MenuItem { Name = "Crepe", Price = 6.78 }
                }
            },
            new Restaurant
            {
                Id = 7,
                Name = "Dragon Wok 7",
                Location = "Austin, TX",
                Rating = 4.0,
                Cuisine = "Indian",
                OpeningDate = new DateTime(2014,01,25),
                Latitude = 41.661079,
                Longitude = -71.955805,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Momo", Price = 18.88 },
                    new MenuItem { Name = "Grilled Fish", Price = 11.77 },
                    new MenuItem { Name = "Croissant", Price = 19.54 }
                }
            },
            new Restaurant
            {
                Id = 8,
                Name = "Le Petit Chef 8",
                Location = "Phoenix, AZ",
                Rating = 4.6,
                Cuisine = "Thai",
                OpeningDate = new DateTime(2016,08,22),
                Latitude = 42.793914,
                Longitude = -76.991006,
                Menus = new  List<MenuItem>
                {
                    new MenuItem { Name = "Vegan Bowl", Price = 6.64 },
                    new MenuItem { Name = "Donut", Price = 11.26 },
                    new MenuItem { Name = "Dosa", Price = 12.96 }
                }
            },
            new Restaurant
            {
                Id = 9,
                Name = "Biryani Express 9",
                Location = "Houston, TX",
                Rating = 4.2,
                Cuisine = "Korean",
                OpeningDate = new DateTime(2022,10,18),
                Latitude = 36.372398,
                Longitude = -76.264354,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Grilled Fish", Price = 19.59 },
                    new MenuItem { Name = "Saffron Lounge", Price = 12.03 },
                    new MenuItem { Name = "Wonton Soup", Price = 5.88 }
                }
            },
            new Restaurant
            {
                Id = 10,
                Name = "Pho Garden 10",
                Location = "Los Angeles, CA",
                Rating = 4.4,
                Cuisine = "Greek",
                OpeningDate = new DateTime(2019,06,14),
                Latitude = 37.289256,
                Longitude = -71.048471,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Baklava", Price = 18.45 },
                    new MenuItem { Name = "Pad Thai", Price = 11.7 },
                    new MenuItem { Name = "Momo" , Price = 6.88 }
                }
            },
            new Restaurant
            {
                Id = 11,
                Name = "Curry Junction 11",
                Location = "Phoenix, AZ",
                Rating = 4.0,
                Cuisine = "American",
                OpeningDate = new DateTime(2012,01,06),
                Latitude = 37.174197,
                Longitude = -77.236332,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Grilled Fish", Price = 18.4 },
                    new MenuItem { Name = "Wrap", Price = 14.67 },
                    new MenuItem { Name = "Ramen", Price = 10.33 }
                }
            },
            new Restaurant
            {
                Id = 12,
                Name = "Pizza Villa 12",
                Location = "Jacksonville, FL",
                Rating = 4.8,
                Cuisine = "Italian",
                OpeningDate = new DateTime(2021,01,08),
                Latitude = 38.37677,
                Longitude = -77.222705,
                Menus = new List<MenuItem>
                {
                    new MenuItem { Name = "Pizza", Price = 19.88 },
                    new MenuItem { Name = "Naan", Price = 10.12 },
                    new MenuItem { Name = "Falafel", Price = 17.94 }
                }
            },

        };

        public List<string> GetCuisines() => new List<string>
        {
            "Italian", "Japanese", "Indian", "Mexican", "Chinese", "French"
        };

        public List<DateTime> GetRestaurantOpeningDates()
        {
            var restaurants = GetRestaurants();
            return restaurants.ConvertAll(r => r.OpeningDate);
        }

        public List<Restaurants> GetRestaurantsWithCuisines()
        {
            return new List<Restaurants>
            {
                new Restaurants { Id = 1, Name = "Casa Bella", Location = "New York", Menus = new List<MenuItems>
                    {
                        new MenuItems { Name = "Margherita Pizza", Price = 12.99 },
                        new MenuItems { Name = "Pasta Carbonara", Price = 14.49 },
                        new MenuItems { Name = "Tiramisu", Price = 6.75 }
                    } },
                new Restaurants { Id = 2, Name = "Sakura Spot", Location = "San Francisco", Menus = new List<MenuItems>() },
                new Restaurants { Id = 3, Name = "El Ranchero", Location = "Houston", Menus = new List<MenuItems>() }
            };
        }
        public List<Cuisines> GetCuisinesWithFood()
        {
            return new List<Cuisines>
            {
                new Cuisines
                {
                    Name = "Italian",
                    Items = new List<MenuItems>
                    {
                        new MenuItems { Name = "Margherita Pizza", Price = 12.99 },
                        new MenuItems { Name = "Pasta Carbonara", Price = 14.49 },
                        new MenuItems { Name = "Tiramisu", Price = 6.75 }
                    }
                },
                new Cuisines
                {
                    Name = "Japanese",
                    Items = new List<MenuItems>
                    {
                        new MenuItems { Name = "Sushi Roll", Price = 13.99 },
                        new MenuItems { Name = "Ramen", Price = 11.5 },
                        new MenuItems { Name = "Tempura", Price = 9.99 }
                    }
                },
                new Cuisines
                {
                    Name = "Mexican",
                    Items = new List<MenuItems>
                    {
                        new MenuItems { Name = "Tacos", Price = 9.49 },
                        new MenuItems { Name = "Burrito", Price = 10.5 },
                        new MenuItems { Name = "Churros", Price = 5.99 }
                    }
                }
            };
        }

    }

    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public double Rating { get; set; }
        public string Cuisine { get; set; }
        public DateTime OpeningDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<MenuItem> Menus { get; set; }
    }

    public class MenuItem
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }


    public class Cuisines
    {
        //public int Id { get; set; }
        //public int RestaurantId { get; set; }

        public string Name { get; set; } = string.Empty;
        public List<MenuItems> Items { get; set; } = new();
    }

    public class MenuItems
    {
        public string Name { get; set; } = string.Empty;
        public double Price { get; set; }
    }

    public class Restaurants
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public List<MenuItems> Menus { get; set; } = new();
    }
}