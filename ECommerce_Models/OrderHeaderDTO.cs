﻿using System.ComponentModel.DataAnnotations;

namespace ECommerce_Models;

public class OrderHeaderDTO
{
    public int Id { get; set; }
    [Required]
    public int UserId { get; set; }

    [Required]
    [Display(Name = "Order Total")]
    public double OrderTotal { get; set; }

    [Required]
    [Display(Name = "Shipping Date")]
    public DateTime ShippingDate { get; set; }

    [Required]
    public string Status { get; set; }

    public string? SessionId { get; set; }
    public string? PaymentIntentId { get; set; }

    [Required]
    [Display(Name = "Name")]
    public string Name { get; set; }
    [Required]
    [Display(Name = "Phone Number")]
    public string PhoneNumber { get; set; }
    [Required]
    [Display(Name = "Street Address")]
    public string StreetAddress { get; set; }
    [Required]
    public string State { get; set; }
    [Required]
    public string City { get; set; }

    [Required]
    [Display(Name = "Postal Code")]
    public string PostalCode { get; set; }

    [Required]
    [Display(Name = "Email")]
    public string Email { get; set; }
}
