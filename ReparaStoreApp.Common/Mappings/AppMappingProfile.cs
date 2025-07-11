using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Security;

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

            CreateMap<Dispositivos, DispositivosItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<DispositivosItem, Dispositivos>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name));

        }
    }
}
