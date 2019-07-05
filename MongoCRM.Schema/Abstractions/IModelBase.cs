using System;
using System.Collections.Generic;
using System.Text;

namespace MongoCRM.Schema
{
    public interface IModelBase
    {
        string MongoID { get; set; }
        Guid ID { get; set; }
        string CollectionName { get; }


    }
}
