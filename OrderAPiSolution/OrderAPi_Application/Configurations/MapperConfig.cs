using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrderAPi_Application.DTOs;
using OrderAPi_Domain.Entites;

namespace OrderAPi_Application.Configurations
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
        CreateMap<Order , OrderDTO>().ReverseMap();
        
        
        
        
        }
    }
}
