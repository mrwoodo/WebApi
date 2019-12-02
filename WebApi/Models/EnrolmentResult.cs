using System;

namespace WebApi.Models
{
    public class EnrolmentResult
    {
        public string StateElectoralDistrict { get; set; }
        public string EnrolledAddress { get; set; }
        public DateTime ValidAsAt { get; set; }
        public string LocalGovernmentArea { get; set; }
        public string LocalGovernmentAreaWard { get; set; }
    }
}