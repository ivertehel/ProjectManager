using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Employee_PositionInCompany :Base<Employee_PositionInCompany>
    {
        private Guid _employeeId;
        public Employee Employee
        {
            get
            {
                return (from items in Employee.Items where items.Id == _employeeId select items).FirstOrDefault();
            }
            set
            {
                _employeeId = value.Id;
            }
        }

        private Guid _positionInCompanyId;
        public PositionInCompany PositionInCompany
        {
            get
            {
                return (from items in PositionInCompany.Items where items.Id == _positionInCompanyId select items).FirstOrDefault();
            }
            set
            {
                _positionInCompanyId = value.Id;
            }
        }
    }
}
