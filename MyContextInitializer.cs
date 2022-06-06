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

            model.Staff.Add(staff);
            model.SaveChanges();
        }
    }
}
