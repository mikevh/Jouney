using System;
using System.Data.Entity;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Journey.Web.Models
{
    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public bool IsMember { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
    }

    public class Meeting
    {
        public int Id { get; set; }
        
        public DateTime Date { get; set; }
        
        public int CommunityGroupId { get; set; }

        public virtual CommunityGroup CommunityGroup { get; set; }
        public virtual ICollection<Attendee> Attendees { get; set; }
    }

    public class CommunityGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int LeaderId { get; set; }

        public virtual Leader Leader { get; set; }

        [NotMapped]
        public DateTime? LastMeeting { get; private set; }
    }

    public class Leader
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public virtual ICollection<CommunityGroup> CommunityGroups { get; set; } 
    }

}