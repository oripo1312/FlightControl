﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FlightControlWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        
        private IFlightManager flightManager;
        private IMemoryCache memoryCache;
       
        // dependency injection
        public FlightPlanController(IFlightManager manager, IMemoryCache cache)
        {
            flightManager = manager;
            memoryCache = cache;
            
        }
        
  

        // GET: api/FlightPlan/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/FlightPlan
        [HttpPost]
        public void Post([FromBody] FlightPlan flightPlan)
        {
            //just an example
            int id = 13;
            memoryCache.Set(id, flightPlan);

        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
    }
}