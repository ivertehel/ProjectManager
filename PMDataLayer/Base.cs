using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Base<T> where T : Base<T>
    {
        static Base()
        {
            Items = new List<T>();
        }

        public Base()
        {
            Id = Guid.NewGuid();
        }

        public static List<T> Items { get; set; }

        public Guid Id { get; set; }
    }
}
