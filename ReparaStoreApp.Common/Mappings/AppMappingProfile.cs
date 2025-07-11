using ReparaStoreApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Cliente;

namespace ReparaStoreApp.Common.Mappings
{
    public class AppMappingProfile : Profile
    {
        public AppMappingProfile()
        {
            // Mapeo de User (entidad de BD) a UserItem (DTO para WPF)
            CreateMap<User, UserItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => string.Empty));

            // Mapeos inversos si son necesarios
            CreateMap<UserItem, User>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber));

            CreateMap<Clientes, ClientesItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<ClientesItem, Clientes>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name));

        }
    }
}
