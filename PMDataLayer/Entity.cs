using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class Entity<T> where T : Entity<T>
    {
        static Entity()
        {
            Items = new List<T>();
        }

        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public static List<T> Items { get; set; }

        public Guid Id { get; set; }


    }
}
