using System.Collections;
using System.Collections.Generic;

namespace Journey.Web.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class JourneyModel : DbContext
    {
        // Your context has been configured to use a 'JourneyModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'Journey.Web.Models.JourneyModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'JourneyModel' 
        // connection string in the application configuration file.
        public JourneyModel()
            : base("name=JourneyModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

         public virtual DbSet<Attendee> Attendees { get; set; }
         public virtual DbSet<Meeting> Meetings { get; set; }
         public virtual DbSet<CommunityGroup> CommunityGroups { get; set; }
    }

    public class Attendee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Meeting> Meetings { get; set; }
    }

    public class Meeting
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }

        public virtual CommunityGroup CommunityGroup { get; set; }
        public virtual ICollection<Attendee> Attendees { get; set; }
    }

    public class CommunityGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public virtual ICollection<Meeting> Meetings { get; set; } 
    }

}