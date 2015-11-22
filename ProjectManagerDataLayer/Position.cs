﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagerDataLayer
{
    public class Position : Base<Position>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Employee> Employees
        {
            get
            {
                return (from items in Employee.Items where items.Position.Id == Id select items);
            }
        }
    }
}
