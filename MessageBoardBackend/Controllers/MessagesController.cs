using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MessageBoardBackend.Controllers
{
    [Produces("application/json")]
    [Route("api/Messages")]
    public class MessagesController : Controller
    {
        private readonly ApiContext context;
        
        public MessagesController(ApiContext context)
        {
            this.context = context;
        }

        public IEnumerable<Models.Message> Get()
        {
            return context.Messages;
        }

        [HttpGet("{name}")]
        public IEnumerable<Models.Message> Get(string name)
        {
            return context.Messages.Where(x => x.Owner == name);
        }

        [HttpPost]
        public Models.Message Post([FromBody] Models.Message message)
        {
            var newMessage = context.Messages.Add(message).Entity;
            context.SaveChanges();

            return newMessage;
        }
    }
}