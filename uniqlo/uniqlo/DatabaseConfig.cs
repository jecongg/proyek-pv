using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uniqlo
{
    public static class DatabaseConfig
    {
        public static string ConnectionString { get; } = "server=localhost;uid=root;pwd=;database=db_uniqlo";
    }
}
