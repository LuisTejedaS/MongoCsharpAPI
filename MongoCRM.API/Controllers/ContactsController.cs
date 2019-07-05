using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoCRM.Core;
using MongoCRM.Schema;
using MongoDB.Driver;

namespace MongoCRM.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {

        private readonly MongoManager<Contact> _manager;

        public ContactsController(MongoManager<Contact> mgr)
        {
            _manager = mgr;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Contact>>> Get()
        {
            ContactManager mgr = new ContactManager();
           return await mgr.GetAsync();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Contact>> Get(Guid id)
        {
            ContactManager mgr = new ContactManager();
            return await mgr.GetByIDAsync(id);
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] List<Contact> contacts)
        {
            ContactManager mgr = new ContactManager();
            await mgr.CreateAsync(contacts);
        }

        // PUT api/values/5
        [HttpPut]
        public async Task Put([FromBody] List<Contact> contacts)
        {
            ContactManager mgr = new ContactManager();
            await mgr.UpdateAsync(contacts);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<DeleteResult> Delete(Guid id)
        {
            ContactManager mgr = new ContactManager();
            return await mgr.DeleteByIDsAsync(new List<Guid>() { id });
        }
    }
}
