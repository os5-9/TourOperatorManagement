using System.Data.Entity;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    public class MyContextInitializer : CreateDatabaseIfNotExists<Agency>
    {
        protected override void Seed(Agency model)
        {
            Staff staff = new Staff
            {
                FullName = "defaul admin",
                Login = "admin",
                Password = "admin",
                IsAdmin = 1,
                IsExists = 1
            };
            Operators operators = new Operators
            {
                FullName = "default operator",
                Login = "default",
                Password = "default",
                Comment = "default operator",
                IsDenied = 1
            };
            TourStates state = new TourStates
            {
                Code = 1,
                Name = "Обычная"
            };
            TourStates state2 = new TourStates
            {
                Code = 2,
                Name = "Горящая"
            };
            TourType type = new TourType
            {
                Code = 1,
                Name = "Прямой путь"
            };
            TourType type2 = new TourType
            {
                Code = 2,
                Name = "С пересадками"
            };

            model.Operators.Add(operators);
            model.Staff.Add(staff);
            model.TourType.Add(type);
            model.TourType.Add(type2);
            model.TourStates.Add(state);
            model.TourStates.Add(state2);
            model.SaveChanges();
        }
    }
}
