using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class Base<T> where T : Base<T>
    {
        static public List<T> Items = new List<T>();
        public Guid Id;
        public string Name;
        public Base()
        {
            Id = Guid.NewGuid();
        }
    }
}
