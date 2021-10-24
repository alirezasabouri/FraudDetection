using System;
using System.Collections.Generic;
using System.Text;

namespace FraudDetection.Misc
{
    public class Settings
    {
        public DatabaseSettings Database { get; set; }
    }

    public class DatabaseSettings
    {
        public string ConnectionString { get; set; }
    }
}
