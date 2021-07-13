using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Bookme.Data.Models
{
    public class Booking
    {
        public int Id { get; set; }
        [Required]
        public string ClientId { get; set; }
        public ApplicationUser Client { get; set; }
        [Required]
        public string BusinessId { get; set; }
        public ApplicationUser Business { get; set; }
        public int ConfirmationId { get; set; }
        public ConfirmationType Confirmation { get; set; }
        public DateTime Date { get; set; }
        [Required]
        [MaxLength(20)]
        public string BookedService { get; set; }
        public string Notes { get; set; }

    }
}
