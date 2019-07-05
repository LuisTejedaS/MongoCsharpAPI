using System;
using System.Collections.Generic;
using System.Text;


namespace MongoCRM.Schema
{
    public class CRMDatabaseSettings : ICRMDatabaseSettings
    { 
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface ICRMDatabaseSettings
    { 
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}