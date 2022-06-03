using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees.Models
{
    public class SingInResponse
    {
        public User User { get; set; }
        public AccessToken AccessToken { get; set; }
    }
}
