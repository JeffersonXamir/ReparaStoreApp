using AutoMapper;
using Caliburn.Micro;
using ReparaStoreApp.Common.Entities;
using ReparaStoreApp.Core.Services.ClientesService;
using ReparaStoreApp.WPF.Models;
using ReparaStoreApp.WPF.ViewModels.Controls.GenericList;
using ReparaStoreApp.WPF.ViewModels.Services.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReparaStoreApp.WPF.ViewModels.Reparaciones
{
    public class ReparacionesViewModel : BaseViewModel
    {
        private readonly IWindowManager _windowManager;
        private readonly IEventAggregator _eventAggregator;
        private readonly IClientesService _ClientesService;
        private readonly IDataService<ClientesItem> _ClientesDataService;
        private readonly IMapper _mapper;

        public bool IsInEditOrCreationMode => CreationMode || EditMode;

        private DateTime _fechaActual;

        public DateTime FechaActual
        {
            get { return _fechaActual; }
            set { _fechaActual = value; NotifyOfPropertyChange(() => FechaActual); }
        }

        public ReparacionesViewModel(
                            IWindowManager windowManager,
                            IEventAggregator eventAggregator,
                            IDataService<ClientesItem> ClientesDataService,
                            IClientesService ClientesService,
                            IMapper mapper) : base(eventAggregator)
        {
            _windowManager = windowManager;
            _eventAggregator = eventAggregator;
            _ClientesDataService = ClientesDataService;
            _ClientesService = ClientesService;
            _mapper = mapper;

        }

        public override Task<ValidateForm> ValidateForm()
        {
            throw new NotImplementedException();
        }
    }
}
