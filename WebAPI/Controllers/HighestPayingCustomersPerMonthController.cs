using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebAPI.Interfaces;

namespace WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class HighestPayingCustomersPerMonthController : Controller
    {
        private readonly IReadModelService _service;

        public HighestPayingCustomersPerMonthController(IReadModelService service)
        {
            _service = service;
        }

        // GET: api/HighestPayingCustomersPerMonth
        [HttpGet]
        public string Get()
        {
            // TODO: Decouple read model update from endpoint.
            // Maybe have ReadModel save result in file, served here,
            // while Bootstrap perpetually runs and updates every x length of time.
            return JsonConvert.SerializeObject(_service.Get().ToArray());
        }

        // GET: api/HighestPayingCustomersPerMonth/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/HighestPayingCustomersPerMonth
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/HighestPayingCustomersPerMonth/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
