using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Security
{
    public class AppConfiguration
    {
        public string MariaDBConnection { get; set; }
        public JwtSettings JwtSettings { get; set; }
    }

}
