using System.ComponentModel.DataAnnotations;

namespace AdvanceULB_CMS.Models
{
    public class ULBCreation
    {
        [Required]
        public string? server {  get; set; }
        [Required]
        public string? appName { get; set; }
        [Required]
        public string? extName { get; set; }
        [Required]
        public string? marName { get; set; }
        [Required]
        public double property { get; set; }
        [Required]
        public string? lat { get; set; }
        [Required]
        public string? lng { get; set; }
    }
}
