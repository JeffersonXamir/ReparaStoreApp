using ReparaStoreApp.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using AutoMapper;
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
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
                //.ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            //// Mapeos inversos si son necesarios
            //CreateMap<UserItem, User>()
            //    .ForMember(dest => dest.EmployeeCode, opt => opt.MapFrom(src => src.Code))
            //    // Otros mapeos...
            //    ;

            // Agregar más mapeos según necesites
        }
    }
}
