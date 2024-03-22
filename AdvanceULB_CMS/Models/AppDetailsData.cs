namespace AdvanceULB_CMS.Models
{
    public class AppDetails
    {
        public int code { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
        public List<AppDetailsData>? Data { get; set; }
    }

    public class AppDetailsData
    {
        public int appId { get; set; }
        public string? appName { get; set; }
        public int apiHit { get; set; }
        public int ulbProperty { get; set; }
        public int addUlbProperty { get; set; }
    }
}
