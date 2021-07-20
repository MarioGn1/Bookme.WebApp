﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Bookme.Data.DbConstants.User;

namespace Bookme.Data.Models
{
    public class Business
    {
        public int Id { get; init; }
        [Required]
        [MaxLength(50)]
        public string CompanyName { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }
        [Required]
        [MaxLength(50)]
        public string SupportedLocationArea { get; set; }
        [MaxLength(50)]
        public string Address { get; set; }
        public string ImageUrl { get; set; } = DEFAULT_BUSINESS_INFO_IMAGE;
        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int BookingConfigurationId { get; set; }
        public BookingConfiguration BookingConfiguration { get; set; }

        public ICollection<Comment> Comments { get; set; }
    }
}