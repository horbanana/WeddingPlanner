using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
    public class Occasion
    {
        [Key]
        public int OccasionId {get;set;}
        public int PlanId {get;set;}
        public int UserId {get;set;}
        public Plan Plan {get;set;}
        public User User {get;set;}
    }
}
