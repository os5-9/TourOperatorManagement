using System;
using System.Collections.Generic;
using System.Linq;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    public static class TourRepository
    {
        private static AgencyModel model = new AgencyModel();
        public static IEnumerable<Tours> GetAllTours()
        {
            var list = model.Tours.Where(x => x.IsExists == 1);
            return list;
        }

        public static IEnumerable<Tours> SearchTour(string status, string type, string city, string country, int price, DateTime arrivalS, DateTime arrivalF, DateTime departureS, DateTime departureF)
        {
            var list = model.Tours
                    .Where(x => x.IsExists == 1 && x.TourStates.Name.Contains(status)
                        && x.TourType.Name.Contains(type) && x.City.Contains(city)
                        && x.Country.Contains(country) && x.Price == price
                        && (x.Arrival >= arrivalS && x.Arrival <= arrivalF) && (x.Departure >= departureS && x.Departure <= departureF));
            return list;
        }
        public static IEnumerable<Tours> SearchTourStatus(IEnumerable<Tours> tours, string status)
        {
            var list = tours.Where(x => x.TourStates.Name.Contains(status));
            return list;
        }
        public static IEnumerable<Tours> SearchTourType(IEnumerable<Tours> tours, string type)
        {
            var list = tours.Where(x => x.TourType.Name.Contains(type));
            return list;
        }
        public static IEnumerable<Tours> SearchTourCity(IEnumerable<Tours> tours, string city)
        {
            var list = tours.Where(x => x.City.ToLower().Contains(city.ToLower()));
            return list;
        }
        public static IEnumerable<Tours> SearchTourCountry(IEnumerable<Tours> tours, string country)
        {
            var list = tours.Where(x => x.Country.ToLower().Contains(country.ToLower()));
            return list;
        }
        public static IEnumerable<Tours> SearchTourPrice(IEnumerable<Tours> tours, int price)
        {
            var list = tours.Where(x => x.Price <= price);
            return list;
        }
        public static IEnumerable<Tours> SearchTourarrivalal(IEnumerable<Tours> tours, DateTime arrivalS, DateTime arrivalF)
        {
            var list = tours.Where(x => (x.Arrival >= arrivalS && x.Arrival <= arrivalF));
            return list;
        }
        public static IEnumerable<Tours> SearchTourdepartureture(IEnumerable<Tours> tours, DateTime departureS, DateTime departureF)
        {
            var list = tours.Where(x => (x.Departure >= departureS && x.Departure <= departureF));
            return list;
        }
    }
}
