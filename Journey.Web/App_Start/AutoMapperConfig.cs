using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using AutoMapper;
using Microsoft.Ajax.Utilities;

namespace Journey.Web.App_Start
{
    public class AutoMapperConfig
    {
        public static void Configure() {
            Mapper.Initialize(x => {
                x.CreateMap<Models.Attendee, DTO.Attendee>().ReverseMap();
                x.CreateMap<Models.Leader, DTO.Leader>();
                x.CreateMap<Models.CommunityGroup, DTO.CommunityGroup>().ReverseMap();
                x.CreateMap<Models.Meeting, DTO.Meeting>();
            });
        }
    }
}