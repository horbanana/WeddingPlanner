using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;


namespace WeddingPlanner.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}
        [Required(ErrorMessage="First Name is required")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Last Name is required")]
        public string LastName {get;set;}

        [Required(ErrorMessage="Email is required")]
        [EmailAddress]
        public string Email {get;set;}

        [Required (ErrorMessage="Password is required")]
        [DataType(DataType.Password)]//add Validation
        public string Password { get; set; }

        [NotMapped]
        [Required (ErrorMessage="Confirm Password")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        
        public List<Occasion> Occasions { get; set; }
        public  List<Plan> CreatetedPlans { get; set; }
       
        
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}