using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmployeeService.Controllers
{
    [Authorize]
    public class EmployeeController : ApiController
    {
        
        public class Employee
        {
           
            public int ID { get; set;}
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Gender { get; set; }
            public int Salary { get; set; }

            public List<Employee> empList;

            public void CreateList()
            {
                empList = new List<Employee>();
                empList.Add(new Employee { ID = 1, FirstName = "Mark", LastName = "Hastings", Gender = "Male", Salary = 50000 });
                empList.Add(new Employee { ID = 2, FirstName = "Steve", LastName = "Pound", Gender = "Male", Salary = 45000 });
                empList.Add(new Employee { ID = 3, FirstName = "Ben", LastName = "Hastings", Gender = "Male", Salary = 70000 });
                empList.Add(new Employee { ID = 4, FirstName = "Mary", LastName = "Lambeth", Gender = "Female", Salary = 30000 });
                empList.Add(new Employee { ID = 5, FirstName = "Mary2", LastName = "Lambeth", Gender = "Female", Salary = 88000 });
                empList.Add(new Employee { ID = 6, FirstName = "Philip", LastName = "Hastings", Gender = "Male", Salary = 45000 });
                empList.Add(new Employee { ID = 7, FirstName = "Valarie", LastName = "Vikings", Gender = "Female", Salary = 35000 });
                empList.Add(new Employee { ID = 8, FirstName = "John", LastName = "Stanmore", Gender = "Male", Salary = 80000 });
            }
        }
        public IEnumerable<Employee> Get()
        {
            Employee entities = new Employee();
            entities.CreateList();
            return entities.empList.ToList();
        }
    }
}
