using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class PositionInCompany : Base<PositionInCompany>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Employee> Employees
        {
            get
            {
                return from items in Employee_PositionInCompany.Items where items.PositionInCompany.Id == Id select items.Employee;
            }
        }
    }
}
