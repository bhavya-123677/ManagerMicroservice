using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ManagerMicroservice.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly ManagerContext context;



        public ManagerController(ManagerContext ctx)
        {
            this.context = ctx;
        }
        

        [HttpGet("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Manager))]
        public async Task<IActionResult> GetExecutives(int id)
        {
            var manager = await context.Managers.FindAsync(id);
            if (manager == null)
                return NotFound();

            return Ok(manager);
        }
        [HttpGet("GetLocality/{Locality}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Manager))]
        public async Task<IActionResult> GetByLocality(string Locality)
        {
            try
            {
                var prop = context.Managers.Where(p => p.Locality.Contains(Locality)).ToList();
                return Ok(prop);
            }
            catch
            {
                return BadRequest();
            }

        }

        [HttpPost]
        [ProducesResponseType(400)]
        [ProducesResponseType(201)]
        public async Task<IActionResult> createExecutive(Manager obj)
        {
            context.Managers.Add(obj);
            await context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpPut]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(200, Type = typeof(Manager))]
        public async Task<IActionResult> assignExecutive(Manager obj)
        {
            var manager = await context.Managers.FindAsync(obj.id);
            if (manager == null)
                return NotFound();

            manager.Name = obj.Name;
            manager.ContactNumber = obj.ContactNumber;
            manager.Locality = obj.Locality;
            manager.Emailid = obj.Emailid;
            await context.SaveChangesAsync();
            return Ok(manager);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public async Task<IActionResult> DeleteExecutive(int id)
        {
            var manager = await context.Managers.FindAsync(id);
            if (manager == null)
                return NotFound();
            context.Managers.Remove(manager);
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<Customer>))]
        [Route("/GetProperties")]
        public async Task<IActionResult> GetProperties()
        {
            List<Customer> customers = new List<Customer>();
            string apiurl = "http://localhost:30223/";
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiurl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/Property");
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    customers = JsonConvert.DeserializeObject<List<Customer>>(json);
                }
            }
            return Ok(customers);
        }        
    }
}