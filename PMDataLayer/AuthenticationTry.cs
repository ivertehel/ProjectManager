using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMDataLayer
{
    public class AuthenticationTry : Base<AuthenticationTry>
    {
        public string Login { get; set; }
        public int Number { get; set; }
    }
}
