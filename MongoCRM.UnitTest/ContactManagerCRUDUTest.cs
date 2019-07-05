using MongoCRM.Core;
using MongoCRM.Schema;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MongoCRM.UnitTest
{
    public class ContactManagerCRUDUTest
    {
        [Fact]
        public async Task Test1Async()
        {
            var id = Guid.NewGuid();
            var id2 = Guid.NewGuid();
            Contact found = new Contact();

            Contact ct = new Contact()
            {
                ID = id,
                Email = "un imail",
                Name = "Luis",
                LastName = "Tejeda"
            };

            ct.Address = new Address()
            {
                Street = "Camino",
                ExternalNumber = "5150",
                InternalNumber = "803A",
                Location = ""
            };

            Contact ct2 = new Contact()
            {
                ID = id2,
                Email = "un imail",
                Name = "Alfredo",
                LastName = "Tejeda"
            };

            ct.Address = new Address()
            {
                Street = "Camino",
                ExternalNumber = "5150",
                InternalNumber = "803A",
                Location = ""
            };

            ContactManager mgr = new ContactManager();
            var ser = JsonConvert.SerializeObject(new List<Contact>() { ct, ct2 });
            await mgr.CreateAsync(new List<Contact>() { ct, ct2 }).ContinueWith(p =>
             {
                 var contactFoundLoc = mgr.GetByIDAsync(id).Result;
                 found = contactFoundLoc;
                 Assert.NotNull(contactFoundLoc);


                 var contactAgainRes = mgr.GetByMongoIDAsync(contactFoundLoc.MongoID).Result;
                 var contactAgain = contactAgainRes.FirstOrDefault();


                 Assert.NotNull(contactAgain);
             }).ContinueWith(task =>
             {

                 found.Name = "updated";
                 mgr.UpdateAsync(new List<Contact>() { found }).Wait();


                 var contactFound = mgr.GetByIDAsync(id).Result;

                 Assert.Equal("updated", contactFound.Name);


                 var contactFound2 = mgr.GetByIDAsync(id2).Result;

                 var res = mgr.DeleteByIDsAsync(new List<Guid>() { contactFound.ID, contactFound2.ID }).Result;

                 var contactFound3 = mgr.GetByIDAsync(id).Result;

                 var contactFound4 = mgr.GetByIDAsync(id2).Result;

                 

             });



        }
    }
}
