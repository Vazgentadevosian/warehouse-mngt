using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WRMNGT.Infrastructure.Models
{
    public class DatabaseSecret
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Host { get; set; }
        public string Database { get; set; }
    }
}
