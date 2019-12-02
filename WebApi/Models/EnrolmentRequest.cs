using System;
using System.ComponentModel.DataAnnotations;
using WebApi.Validation;

namespace WebApi.Models
{
    public class EnrolmentRequest
    {
        [Required]
        [MaxLength(50)]
        [WhitelistCharacter]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        [WhitelistCharacter]
        public string GivenNames { get; set; }

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [MaxLength(100)]
        [WhitelistCharacter]
        public string StreetName { get; set; }
    }
}
