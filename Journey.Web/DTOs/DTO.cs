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
        public int LeaderId { get; set; }

        public Leader Leader { get; set; }
    }

    public class Leader
    {
        public int Id { get; set; }   
        public string Email { get; set; }
    }

    public class CreateLeader
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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
        public int CommunityGroupId { get; set; }
        public CommunityGroup CommunityGroup { get; set; }
        public List<Attendee> Attendees { get; set; } 
    }

    public class JourneyUser
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }  
    }
}