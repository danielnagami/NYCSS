namespace NYCSS.SubwayApi.Models
{
    public class DistanceRequest
    {
        public Guid SubwayStation1 { get; set; }
        public Guid SubwayStation2 { get; set; }
    }

    public class DistanceResponse
    {
        public double Distance { get; set; }

        public DistanceResponse(double distance)
        {
            Distance = distance;
        }
    }
}