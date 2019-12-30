using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace WeddingPlanner.Models
{
    public class Plan
    {
        [Key]
        public int PlanId {get;set;}
        public User Creator {get;set;}
        public int UserId {get;set;}
        [Required(ErrorMessage="Names of Bride and Groom are required")]        
        public string WedderOne {get;set;}
        [Required(ErrorMessage="Names of Bride and Groom are required")] 
        public string WedderTwo {get;set;}

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Date { get; set; }

        [Required]
        public string WeddingAddress { get; set; }  

        public List<Occasion> People{ get; set; }
        
        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}