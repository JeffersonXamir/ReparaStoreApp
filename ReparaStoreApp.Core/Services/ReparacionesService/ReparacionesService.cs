using AutoMapper;
using ReparaStoreApp.Common;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Data.Repositories.DispositivosRepository;
using ReparaStoreApp.Data.Repositories.InventarioRepository;
using ReparaStoreApp.Data.Repositories.KardexRepository;
using ReparaStoreApp.Data.Repositories.ProductosRepository;
using ReparaStoreApp.Data.Repositories.ReparacionesRepository;
using ReparaStoreApp.Entities.Models.Inventario;
using ReparaStoreApp.Entities.Models.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.Core.Services.ReparacionesService
{
    public class ReparacionesService : IReparacionesService
    {
        private readonly IReparacionesRepository _reparacionesRepository;
        private readonly IKardexRepository _kardexRepository;
        private readonly IInventarioRepository _inventarioRepository;
        private readonly IProductosRepository _itemsRepository;
        private readonly IDispositivosRepository _dispositivosRepository;
        private readonly IMapper _mapper;

        public ReparacionesService(
            IReparacionesRepository reparacionesRepository,
            IKardexRepository kardexRepository,
            IInventarioRepository inventarioRepository,
            IProductosRepository itemsRepository,
            IDispositivosRepository dispositivosRepository,
            IMapper mapper)
        {
            _reparacionesRepository = reparacionesRepository;
            _kardexRepository = kardexRepository;
            _inventarioRepository = inventarioRepository;
            _itemsRepository = itemsRepository;
            _dispositivosRepository = dispositivosRepository;
            _mapper = mapper;
        }

        public async Task<DocumentResponse> CreateAsync(ReparacionItem reparacionItem)
        {
            try
            {
                var reparacion = _mapper.Map<Reparacion>(reparacionItem);
                reparacion.Numero = GenerarNumeroReparacion();
                reparacion.Estado = EstadoReparacion.Ingresado;
                reparacion.NotasReparado = "";
                reparacion.Activo = true;
                reparacion.FechaCreacion = DateTime.Now;
                reparacion.UsuarioReparadoId = 1;
                reparacion.UsuarioAprobacionId = 1;

                foreach (var detalle in reparacion.Detalles)
                {
                    // Solo asignamos el ItemId, y eliminamos el objeto Item duplicado
                    detalle.Item = null;
                }

                await _reparacionesRepository.SaveAsync(reparacion);

                return new DocumentResponse
                {
                    Success = true,
                    DocumentId = reparacion.Id
                };
            }
            catch (Exception ex)
            {
                return new DocumentResponse
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DocumentResponse> UpdateAsync(ReparacionItem reparacionItem)
        {
            try
            {
                var reparacionExistente = await _reparacionesRepository.GetByIdAsync(reparacionItem.Id);
                if (reparacionExistente == null)
                    return new DocumentResponse { Success = false, Error = "Reparación no encontrada" };

                // Validar transición de estado
                if (reparacionItem.Estado != reparacionExistente.Estado &&
                    !ValidarTransicionEstado(reparacionExistente.Estado, reparacionItem.Estado))
                    return new DocumentResponse { Success = false, Error = "Transición de estado no válida" };

                // Mapear cambios
                _mapper.Map(reparacionItem, reparacionExistente);
                reparacionExistente.FechaEdicion = DateTime.Now;

                // Manejar cambio de estado
                if (reparacionItem.Estado == EstadoReparacion.Aprobado)
                {
                    reparacionExistente.FechaAprobacion = DateTime.Now;
                    reparacionExistente.UsuarioAprobacionId = reparacionItem.UsuarioEdicionId ?? 1;
                }
                else if (reparacionItem.Estado == EstadoReparacion.Completado)
                {
                    reparacionExistente.FechaReparado = DateTime.Now;
                    reparacionExistente.UsuarioReparadoId = reparacionItem.UsuarioEdicionId ?? 1;
                    await ProcesarKardexReparacion(reparacionExistente.Id, reparacionItem.UsuarioEdicionId ?? 1);
                }

                await _reparacionesRepository.SaveAsync(reparacionExistente);

                return new DocumentResponse
                {
                    Success = true,
                    DocumentId = reparacionExistente.Id
                };
            }
            catch (Exception ex)
            {
                return new DocumentResponse
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DocumentResponse> DeleteAsync(int id, int usuarioId)
        {
            try
            {
                var reparacion = await _reparacionesRepository.GetByIdAsync(id);
                if (reparacion == null)
                    return new DocumentResponse { Success = false, Error = "Reparación no encontrada" };

                await _reparacionesRepository.DeleteAsync(reparacion);

                return new DocumentResponse
                {
                    Success = true,
                    DocumentId = id
                };
            }
            catch (Exception ex)
            {
                return new DocumentResponse
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<ReparacionItem> GetByIdAsync(int id)
        {
            var reparacion = await _reparacionesRepository.GetByIdAsync(id);
            return _mapper.Map<ReparacionItem>(reparacion);
        }

        public async Task<IEnumerable<ReparacionItem>> SearchAsync(string searchText, int page, int pageSize)
        {
            var reparaciones = await _reparacionesRepository.SearchAsync(searchText, page, pageSize);
            return _mapper.Map<IEnumerable<ReparacionItem>>(reparaciones);
        }

        public async Task<int> GetCountAsync(string searchText)
        {
            return await _reparacionesRepository.GetCountAsync(searchText);
        }

        public async Task<DocumentResponse> AprobarReparacionAsync(int reparacionId, int usuarioId, bool aprobado, string nota)
        {
            try
            {
                var reparacion = await _reparacionesRepository.GetByIdAsync(reparacionId);
                if (reparacion == null)
                    return new DocumentResponse { Success = false, Error = "Reparación no encontrada" };

                if (reparacion.Estado != EstadoReparacion.Pendiente)
                    return new DocumentResponse { Success = false, Error = "Solo se pueden aprobar reparaciones en estado Pendiente" };

                reparacion.Estado = aprobado ? EstadoReparacion.Aprobado : EstadoReparacion.Rechazado;
                reparacion.NotasReparado = nota;
                reparacion.FechaAprobacion = DateTime.Now;
                reparacion.UsuarioAprobacionId = usuarioId;
                reparacion.CajeroId = usuarioId;

                await _reparacionesRepository.SaveAsync(reparacion);

                return new DocumentResponse
                {
                    Success = true,
                    DocumentId = reparacion.Id
                };
            }
            catch (Exception ex)
            {
                return new DocumentResponse
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        public async Task<DocumentResponse> FinalizarReparacionAsync(int reparacionId, int usuarioId)
        {
            try
            {
                var reparacion = await _reparacionesRepository.GetByIdAsync(reparacionId);
                if (reparacion == null)
                    return new DocumentResponse { Success = false, Error = "Reparación no encontrada" };

                if (reparacion.Estado != EstadoReparacion.Aprobado && reparacion.Estado != EstadoReparacion.EnProceso)
                    return new DocumentResponse { Success = false, Error = "Solo se pueden finalizar reparaciones en estado Aprobado o EnProceso" };

                reparacion.Estado = EstadoReparacion.Completado;
                reparacion.FechaReparado = DateTime.Now;
                reparacion.UsuarioReparadoId = usuarioId;

                // Procesar kardex para los productos utilizados
                await ProcesarKardexReparacion(reparacionId, usuarioId);

                await _reparacionesRepository.SaveAsync(reparacion);

                return new DocumentResponse
                {
                    Success = true,
                    DocumentId = reparacion.Id
                };
            }
            catch (Exception ex)
            {
                return new DocumentResponse
                {
                    Success = false,
                    Error = ex.Message
                };
            }
        }

        private async Task ProcesarKardexReparacion(int reparacionId, int usuarioId)
        {
            var reparacion = await _reparacionesRepository.GetByIdAsync(reparacionId);
            if (reparacion == null) return;

            var detalles = await _reparacionesRepository.GetDetallesByReparacionIdAsync(reparacionId);
            var productos = detalles.Where(d => d.Item.Tipo == TipoItem.Producto);

            foreach (var detalle in productos)
            {
                // Registrar movimiento en kardex
                await _kardexRepository.RegistrarMovimientoAsync(
                    detalle.ItemId,
                    TipoMovimientoKardex.Egreso,
                    detalle.Cantidad,
                    detalle.PrecioUnitario,
                    $"Salida por reparación #{reparacion.Numero}",
                    usuarioId
                );

                // Actualizar inventario
                await _inventarioRepository.ActualizarStockAsync(detalle.ItemId, -detalle.Cantidad);
            }
        }

        private string GenerarNumeroReparacion()
        {
            var year = DateTime.Now.ToString("yy");
            var month = DateTime.Now.ToString("MM");
            var day = DateTime.Now.ToString("dd");
            //var random = new Random().Next(1000, 9999).ToString();
            var random = "";

            return $"{year}{month}{day}{random}";
        }

        private bool ValidarTransicionEstado(EstadoReparacion estadoActual, EstadoReparacion nuevoEstado)
        {
            switch (estadoActual)
            {
                case EstadoReparacion.Pendiente:
                    return nuevoEstado == EstadoReparacion.Aprobado || nuevoEstado == EstadoReparacion.Rechazado;
                case EstadoReparacion.Aprobado:
                    return nuevoEstado == EstadoReparacion.EnProceso || nuevoEstado == EstadoReparacion.Completado;
                case EstadoReparacion.EnProceso:
                    return nuevoEstado == EstadoReparacion.Completado;
                case EstadoReparacion.Completado:
                case EstadoReparacion.Rechazado:
                    return false;
                default:
                    return false;
            }
        }
    }
}