using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourOperatorManagement.Models;

namespace TourOperatorManagement
{
    public class MyContextInitializer : CreateDatabaseIfNotExists<AgencyModel>
    {
        protected override void Seed(AgencyModel model)
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
