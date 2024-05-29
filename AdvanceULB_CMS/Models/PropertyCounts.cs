namespace AdvanceULB_CMS.Models
{
    public class PropertyCounts
    {
        public string? server {  get; set; }
        public int appId { get; set; }
        public string? appName { get; set; }
        public int apiHit { get; set; }
        public int ulbProperty { get; set; }
        public int addUlbProperty { get; set; }
        public List<PropertyCounts>? Data { get; set; }
    }

    public class UpdateSTDModel
    {
        public string? server { get; set; }
        public int appId { get; set; }
        public string? State { get; set; }
        public string? Tehsil { get; set; }
        public string? District { get; set; }

    }

    public class STDModel
    {
        public string? server { get; set; }
        public int appId { get; set; }
        public string? appName { get; set; }
        public string? State { get; set; } 
        public string? Tehsil { get; set; } 
        public string? District { get; set; }
        public List<STDModel>? Data { get; set; }
    }


}
