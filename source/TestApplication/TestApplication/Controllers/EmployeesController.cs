using System;
using System.Web.Mvc;
using Ext.Direct.Mvc;
using TestApplication.Models;
using System.Linq.Dynamic;

namespace TestApplication.Controllers {

    public class EmployeesController : Controller {

        readonly SampleDataContext _db = new SampleDataContext();

        public ActionResult Get(int start, int limit, string sort, string dir) {
            var employeeList = _db.Employees
                .OrderBy(sort + " " + dir)
                .Skip(start)
                .Take(limit);

            var result = new {
                total = _db.Employees.Count(),
                data = employeeList
            };

            return this.Direct(result);
        }
    }
}
