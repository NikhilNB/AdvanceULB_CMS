namespace AdvanceULB_CMS.Models
{
    public class ULB
    {
        public int code { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
        public List<STDModel>? Data { get; set; }
    }

    public class ULBData
    {
        public string? server { get; set; }
        public int appId { get; set; }
        public string? appName { get; set; }
    }
}
