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
    }
}
