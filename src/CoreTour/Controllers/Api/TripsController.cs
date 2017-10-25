using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CoreTour.Models;
using AutoMapper;
using CoreTour.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreTour.Controllers.Api
{
    [Route("api/[controller]")]
    public class TripsController : Controller
    {
        private ILogger<TripsController> _logger;
        private ICoreTourRepository _repository;

        public TripsController(ICoreTourRepository repository, ILogger<TripsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetTripsByUserName(User.Identity.Name);
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get all trips: {ex}");
                return BadRequest("Error occured");
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody]TripViewModel tripModel)
        {
            if (ModelState.IsValid)
            {
                // save to the database
                var newTrip = Mapper.Map<Trip>(tripModel);
                newTrip.UserName = User.Identity.Name;
                _repository.AddTrip(newTrip);

                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{tripModel.Name}", Mapper.Map<TripViewModel>(newTrip));
                }
            }

            return BadRequest("Failed to save the trip");
        }
    }
}
