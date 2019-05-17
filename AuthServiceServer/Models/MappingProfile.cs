using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServiceServer.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Add as many of these lines as you need to map your objects
            CreateMap<User, RegisterModel>();
            CreateMap<RegisterModel, User>()
                .ForMember(y => y.UserName, x => x.MapFrom(user => user.Email));

            CreateMap<User, AboutUser>();
            CreateMap<AboutUser, User>()
                .ForMember(y => y.Login, x => x.Ignore())
                .ForMember(y => y.Email, x => x.Ignore());
        }
    }
}
