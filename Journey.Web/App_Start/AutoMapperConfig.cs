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
                x.CreateMap<Models.Leader, DTO.Leader>().ReverseMap();
                x.CreateMap<Models.CommunityGroup, DTO.CommunityGroup>().ReverseMap();
                x.CreateMap<Models.Meeting, DTO.Meeting>().ReverseMap();
            });
        }
    }

    public static class Maps
    {
        public static DTO.Attendee ToDto(this Models.Attendee src) { return Mapper.Map<DTO.Attendee>(src); }
        public static Models.Attendee ToModel(this DTO.Attendee src) { return Mapper.Map<Models.Attendee>(src); }
        public static List<DTO.Attendee> ToDtos(this IEnumerable<Models.Attendee> src) { return src.Select(ToDto).ToList(); } 
        public static List<Models.Attendee> ToModels(this IEnumerable<DTO.Attendee> src) { return src.Select(ToModel).ToList(); } 

        public static DTO.Leader ToDto(this Models.Leader src) { return Mapper.Map<DTO.Leader>(src); }
        public static Models.Leader ToModel(this DTO.Leader src) { return Mapper.Map<Models.Leader>(src); }
        public static List<DTO.Leader> ToDtos(this IEnumerable<Models.Leader> src) { return src.Select(ToDto).ToList(); }
        public static List<Models.Leader> ToModels(this IEnumerable<DTO.Leader> src) { return src.Select(ToModel).ToList(); }

        public static DTO.CommunityGroup ToDto(this Models.CommunityGroup src) { return Mapper.Map<DTO.CommunityGroup>(src); }
        public static Models.CommunityGroup ToModel(this DTO.CommunityGroup src) { return Mapper.Map<Models.CommunityGroup>(src); }
        public static List<DTO.CommunityGroup> ToDtos(this IEnumerable<Models.CommunityGroup> src) { return src.Select(ToDto).ToList(); }
        public static List<Models.CommunityGroup> ToModels(this IEnumerable<DTO.CommunityGroup> src) { return src.Select(ToModel).ToList(); }

        public static DTO.Meeting ToDto(this Models.Meeting src) { return Mapper.Map<DTO.Meeting>(src); }
        public static Models.Meeting ToModel(this DTO.Meeting src) { return Mapper.Map<Models.Meeting>(src); }
        public static List<DTO.Meeting> ToDtos(this IEnumerable<Models.Meeting> src) { return src.Select(ToDto).ToList(); }
        public static List<Models.Meeting> ToModels(this IEnumerable<DTO.Meeting> src) { return src.Select(ToModel).ToList(); }
    }
}