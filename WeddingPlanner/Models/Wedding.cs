using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId { get; set;}
        public string WedderOne { get; set;}
        public string WedderTwo { get; set;}
        // public DateTime Date { get; set; }
        public string Address { get; set; }
        public string Date { get; set; }
        //foreign keys
        public User User { get; set; }
        public int UserID{ get; set; }
        public string Guest{ get; set; }
        public List<Guest> Guests { get; set; }
        public Wedding()
        {
            Guests = new List<Guest>();
        }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}