﻿namespace HumanCapitalManagment.Data.Models
{
    using System.ComponentModel.DataAnnotations;
    using System;
    using static DataConstants.Candidate;

    public class Candidate
    {
        public int Id { get; init; }

        [Required(ErrorMessage = "The name is required")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = "The name must be between {2} and {1} characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(PhoneNumberMaxLength, MinimumLength = PhoneNumberMinLength)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "The nationality is required")]
        [StringLength(NationalityMaxLength, MinimumLength = NationalityMinLength, ErrorMessage = "The nationality must be between {2} and {1} characters")]
        public string Nationality { get; set; }

        [Required(ErrorMessage = "You must provide a date of birth")]
        [DataType(DataType.Date, ErrorMessage = "Invalid date")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "You must check one of the two options")]
        public string Gender { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; init; }
    }
}