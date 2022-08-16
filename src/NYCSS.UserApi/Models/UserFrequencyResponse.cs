using NYCSS.Domain.Entities;
using NYCSS.Infra.MongoDB.Models;

namespace NYCSS.UserApi.Models
{
    public class UserFrequencyResponse
    {
        public UserFrequencyResponse(User user, Subway subwayStation, int visitedTimes)
        {
            User = user;
            SubwayStation = subwayStation;
            VisitedTimes = visitedTimes;
        }

        public User User { get; set; }
        public Subway SubwayStation { get; set; }
        public int VisitedTimes { get; set; }
    }
}