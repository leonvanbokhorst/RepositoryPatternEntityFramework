using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Remondo.Model;

namespace Remondo.Database.Repositories.Tests
{
    [TestClass]
    public class HotelRepositoryInitegrationTests
    {
        private const string Amsterdam = "Amsterdam";

        [TestMethod]
        public void QueryHotelsInAmsterdam()
        {
            using (var dataContext = new HotelContext())
            {
                var hotelRepository = new Repository<Hotel>(((IObjectContextAdapter) dataContext).ObjectContext);

                IEnumerable<Hotel> hotels = hotelRepository
                    .GetAll()
                    .Where(h => h.City.Name == Amsterdam);

                foreach (Hotel hotel in hotels)
                {
                    Assert.AreEqual(Amsterdam, hotel.City.Name);
                    Debug.WriteLine(hotel.Name);
                }
            }
        }

        [TestMethod]
        public void QueryById()
        {
            using (var dataContext = new HotelContext())
            {
                var hotelRepository = new Repository<Hotel>(((IObjectContextAdapter)dataContext).ObjectContext);

                Hotel hotel = hotelRepository
                    .GetById(44);

                Assert.IsNotNull(hotel);

            }
        }

        [TestMethod]
        public void SearchForHotelsInAmsterdam()
        {
            using (var dataContext = new HotelContext())
            {
                var hotelRepository = new Repository<Hotel>(((IObjectContextAdapter) dataContext).ObjectContext);

                IEnumerable<Hotel> hotels = hotelRepository
                    .SearchFor(h => h.City.Name == Amsterdam);

                foreach (Hotel hotel in hotels)
                {
                    Assert.AreEqual(Amsterdam, hotel.City.Name);
                    Debug.WriteLine(hotel.Name);
                }
            }
        }

        #region Init and Cleanup

        [TestInitialize]
        public void Init()
        {
            Cleanup();

            using (var dataContext = new HotelContext())
            {
                var amsterdam = new City {Name = Amsterdam};

                var h1 = new Hotel
                             {
                                 City = amsterdam,
                                 Name = "F1 Hotels"
                             };

                var h2 = new Hotel
                             {
                                 City = amsterdam,
                                 Name = "Dam Hotel"
                             };

                dataContext.Hotels.Add(h1);
                dataContext.Hotels.Add(h2);
                dataContext.SaveChanges();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            using (var dataContext = new HotelContext())
            {
                IQueryable<Hotel> hotels = dataContext.Hotels.Select(h => h);
                IQueryable<City> cities = dataContext.Cities.Select(c => c);

                foreach (City city in cities)
                {
                    dataContext.Cities.Remove(city);
                }

                foreach (Hotel hotel in hotels)
                {
                    dataContext.Hotels.Remove(hotel);
                }

                dataContext.SaveChanges();
            }
        }

        #endregion
    }
}