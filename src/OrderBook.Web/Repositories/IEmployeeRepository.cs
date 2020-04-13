using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderBook.Web.Models;

namespace OrderBook.Web.Repositories
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(int id);

        IEnumerable<Employee> GetAllEmployees();

        void Add(Employee employee);

        void Update(Employee employee);

        void Delete(int id);
    }
}