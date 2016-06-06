using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Journey.Web.DTO
{
    public class CommunityGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Leader Leader { get; set; }
    }

    public class Leader
    {
        public int Id { get; set; }   
        public string Email { get; set; }
        public List<CommunityGroup> CommunityGroups { get; set; } 
    }

    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMember { get; set; }
        public List<Meeting> Meetings { get; set; }
    }

    public class Meeting
    {
        public int Id { get; set; }   
        public DateTime Date { get; set; }
        public CommunityGroup CommunityGroup { get; set; }
        public List<Attendee> Attendees { get; set; } 
    }
}