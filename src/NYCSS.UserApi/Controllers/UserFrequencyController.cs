using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using NYCSS.Domain.Entities;
using NYCSS.Infra.MongoDB.Interfaces;
using NYCSS.Infra.SqlServer.Interfaces;
using NYCSS.UserApi.Models;
using NYCSS.Utils.Controllers;
using NYCSS.Utils.User;

namespace NYCSS.UserApi.Controllers
{
    [Authorize]
    [Route("api/userfrequency")]
    public class UserFrequencyController : MainController
    {
        private readonly IMongoService _mongoService;
        private readonly IUserRepository _userRepository;
        private readonly IAspNetUser _user;

        public UserFrequencyController(IMongoService mongoService, IUserRepository userRepository, IAspNetUser user)
        {
            _mongoService = mongoService;
            _userRepository = userRepository;
            _user = user;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersMostFrequentStation()
        {
            var user = await _userRepository.GetById(_user.GetUserId());

            if (user == null)
                return NotFound();

            var frequency = await _mongoService.GetUserFrequencyAsync(user.Username);

            if (frequency == null)
                return NotFound();

            var mostVisitedSubway = frequency.SubwayFrequency.OrderByDescending(x => x.Value).FirstOrDefault();

            var subway = await _mongoService.GetSubwayAsync(Guid.Parse(mostVisitedSubway.Key));

            return Ok(new UserFrequencyResponse(user, subway, mostVisitedSubway.Value));
        }

        [HttpPost("increase")]
        public async Task<IActionResult> Increase(UserFrequencyRequest request)
        {
            var username = _user.GetUsername();

            var subway = await _mongoService.GetSubwayAsync(request.SubwayId);

            if (subway == null)
                return NotFound();

            var frequency = await _mongoService.GetUserFrequencyAsync(username);

            if (frequency == null)
            {
                await _mongoService.InsertUserFrequency(new Infra.MongoDB.Models.UserFrequency(username, new Dictionary<string, int>() { { subway.ID.ToString(), 1 } }));
            }
            else
            {
                await _mongoService.IncreaseUserFrequency(subway, username);
            }

            return Ok();
        }
    }
}