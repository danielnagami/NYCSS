using Geolocation;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NYCSS.Infra.MongoDB.Interfaces;
using NYCSS.SubwayApi.Models;
using NYCSS.Utils.Controllers;

namespace NYCSS.SubwayApi.Controllers
{
    [Authorize]
    [Route("api/subway")]
    public class SubwayController : MainController
    {
        private readonly IMongoService _mongoService;

        public SubwayController(IMongoService mongoService)
        {
            _mongoService = mongoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subways = await _mongoService.GetSubwaysAsync();

            if (subways != null)
                return Ok(subways);

            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var subway = await _mongoService.GetSubwayAsync(id);

            if (subway != null)
                return Ok(subway);

            return NotFound();
        }

        [HttpPost("distance")]
        public async Task<IActionResult> Distance(DistanceRequest request)
        {
            var ss1 = await _mongoService.GetSubwayAsync(request.SubwayStation1);
            var ss2 = await _mongoService.GetSubwayAsync(request.SubwayStation2);

            if (ss1 == null || ss2 == null)
                return NotFound();

            var ss1Location = new Coordinate(ss1.Location.Latitude, ss1.Location.Longitude);
            var ss2Location = new Coordinate(ss2.Location.Latitude, ss2.Location.Longitude);

            double distance = GeoCalculator.GetDistance(ss1Location, ss2Location, 1);

            return Ok(new DistanceResponse(distance));
        }
    }
}