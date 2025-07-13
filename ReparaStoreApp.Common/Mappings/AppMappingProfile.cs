using AutoMapper;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Entities.Models.Cliente;
using ReparaStoreApp.Entities.Models.Dispositivo;
using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Security;
using ReparaStoreApp.Entities.Models.Store;

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

            CreateMap<ItemEntity, ProductosItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<ProductosItem, ItemEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name));

            CreateMap<ItemEntity, ServiciosItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Nombre));

            CreateMap<ServiciosItem, ItemEntity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Codigo, opt => opt.MapFrom(src => src.Code))
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Name));

            CreateMap<Reparacion, ReparacionItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Detalle, opt => opt.MapFrom(src => src.Descripcion))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.NotasIngreso, opt => opt.MapFrom(src => src.NotasIngreso))
                .ForMember(dest => dest.NotasReparado, opt => opt.MapFrom(src => src.NotasReparado))
                .ForMember(dest => dest.CostoEstimado, opt => opt.MapFrom(src => src.CostoEstimado))
                .ForMember(dest => dest.CostoFinal, opt => opt.MapFrom(src => src.CostoFinal))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.UsuarioCreadorId, opt => opt.MapFrom(src => src.UsuarioCreadorId))
                .ForMember(dest => dest.FechaAprobacion, opt => opt.MapFrom(src => src.FechaAprobacion))
                .ForMember(dest => dest.UsuarioAprobacionId, opt => opt.MapFrom(src => src.UsuarioAprobacionId))
                .ForMember(dest => dest.FechaReparado, opt => opt.MapFrom(src => src.FechaReparado))
                .ForMember(dest => dest.UsuarioReparadoId, opt => opt.MapFrom(src => src.UsuarioReparadoId))
                .ForMember(dest => dest.DispositivoId, opt => opt.MapFrom(src => src.DispositivoId))
                .ForMember(dest => dest.TecnicoId, opt => opt.MapFrom(src => src.TecnicoId))
                .ForMember(dest => dest.CajeroId, opt => opt.MapFrom(src => src.CajeroId))
                .ForMember(dest => dest.FacturaId, opt => opt.MapFrom(src => src.FacturaId))
                .ForMember(dest => dest.FechaEdicion, opt => opt.MapFrom(src => src.FechaEdicion))
                .ForMember(dest => dest.UsuarioEdicionId, opt => opt.MapFrom(src => src.UsuarioEdicionId));

            CreateMap<ReparacionItem, Reparacion>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Numero, opt => opt.MapFrom(src => src.Numero))
                .ForMember(dest => dest.Descripcion, opt => opt.MapFrom(src => src.Detalle))
                .ForMember(dest => dest.Fecha, opt => opt.MapFrom(src => src.Fecha))
                .ForMember(dest => dest.NotasIngreso, opt => opt.MapFrom(src => src.NotasIngreso))
                .ForMember(dest => dest.NotasReparado, opt => opt.MapFrom(src => src.NotasReparado))
                .ForMember(dest => dest.CostoEstimado, opt => opt.MapFrom(src => src.CostoEstimado))
                .ForMember(dest => dest.CostoFinal, opt => opt.MapFrom(src => src.CostoFinal))
                .ForMember(dest => dest.Estado, opt => opt.MapFrom(src => src.Estado))
                .ForMember(dest => dest.Activo, opt => opt.MapFrom(src => src.Activo))
                .ForMember(dest => dest.FechaCreacion, opt => opt.MapFrom(src => src.FechaCreacion))
                .ForMember(dest => dest.UsuarioCreadorId, opt => opt.MapFrom(src => src.UsuarioCreadorId))
                .ForMember(dest => dest.FechaAprobacion, opt => opt.MapFrom(src => src.FechaAprobacion))
                .ForMember(dest => dest.UsuarioAprobacionId, opt => opt.MapFrom(src => src.UsuarioAprobacionId))
                .ForMember(dest => dest.FechaReparado, opt => opt.MapFrom(src => src.FechaReparado))
                .ForMember(dest => dest.UsuarioReparadoId, opt => opt.MapFrom(src => src.UsuarioReparadoId))
                .ForMember(dest => dest.DispositivoId, opt => opt.MapFrom(src => src.DispositivoId))
                .ForMember(dest => dest.TecnicoId, opt => opt.MapFrom(src => src.TecnicoId))
                .ForMember(dest => dest.CajeroId, opt => opt.MapFrom(src => src.CajeroId))
                .ForMember(dest => dest.FacturaId, opt => opt.MapFrom(src => src.FacturaId))
                .ForMember(dest => dest.FechaEdicion, opt => opt.MapFrom(src => src.FechaEdicion))
                .ForMember(dest => dest.UsuarioEdicionId, opt => opt.MapFrom(src => src.UsuarioEdicionId))
                .ForMember(dest => dest.Detalles, opt => opt.Ignore()); // Los detalles se manejan separadamente

            // Mapeo para los detalles
            CreateMap<ReparacionDetalle, ReparacionDetalleItem>()
                .ReverseMap();

            CreateMap<Params, ParamsItem>();

            CreateMap<ParamsItem, Params>();

            CreateMap<Role, RolesItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => ""))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.IsChecked, opt => opt.Ignore());

            CreateMap<RolesItem, Role>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
