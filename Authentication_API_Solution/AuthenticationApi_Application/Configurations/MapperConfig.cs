using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationApi_Application.DTOs;
using AuthenticationApi_Domain.Entites;
using AutoMapper;

namespace AuthenticationApi_Application.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        { 
            CreateMap<AppUserDTO, AppUser>().ReverseMap();
            CreateMap<GetUserDTO, AppUser>().ReverseMap();

        
        }
    }
}
