using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TahirMvc123.Models;

namespace TahirMvc123.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CasttController : ControllerBase
    {
        private readonly MvcDBContext _con;
        public CasttController(MvcDBContext _db)
        {
            _con = _db;
        }
        // GET: api/Castt
        [HttpGet]
        public IEnumerable<Cast> Get()
        {
            var Casts = _con.Cast
        .Include(c => c.Vlilage).AsNoTracking().ToList();
            
            return Casts;
        }

        // GET: api/Castt/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Castt
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Castt/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
