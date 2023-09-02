using AutoMapper;
using Business.Abstract;
using Core.Utils.Constants;
using DataAccess.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UnitOfWorkService : IUnitOfWorkService
    {
        private readonly IUnitOfWorkRepository _unitOfWorkRepository;
        private readonly IMapper _mapper;
        private readonly Globals _globals;
        private readonly ILogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UnitOfWorkService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, Globals globals, IHttpContextAccessor httpContextAccessor,ILogService logService)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _globals = globals;
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
        }   
        public IPatientService PatientService => new PatientService(_unitOfWorkRepository, _mapper,_logService );

        public IImageService ImageService => new ImageService(_unitOfWorkRepository, _mapper );

        public IUserService UserService => new  UserService(_unitOfWorkRepository, _mapper, _globals);

        public IPaymentService PaymentService => new  PaymentService(_unitOfWorkRepository, _mapper,_logService, _httpContextAccessor);

        public IDoctorService DoctorService => new DoctorService(_unitOfWorkRepository, _mapper);

        public ITherapyService TherapyService => new TherapyService(_unitOfWorkRepository, _mapper, _logService );

        public INotificationService NotificationService =>  new NotificationService(_unitOfWorkRepository, _mapper );

        #region Old
        //public UnitOfWorkService(
        //    IImageService imageService,
        //    IPatientService patientService,
        //    IUserService userService,
        //    IPaymentService paymentService,
        //    IDoctorService doctorService,
        //    ITherapyService therapyService,
        //    INotificationService notificationService
        //    ) { 
        //    ImageService = imageService;
        //    PatientService = patientService;
        //    UserService = userService;
        //    PaymentService = paymentService;
        //    DoctorService = doctorService;
        //    TherapyService = therapyService;
        //    NotificationService = notificationService;
        //} 


        //public IImageService ImageService { get; }
        //public IPatientService PatientService { get; }

        //public IUserService UserService { get; }

        //public IPaymentService PaymentService { get; }

        //public IDoctorService DoctorService { get; }

        //public ITherapyService TherapyService { get; }

        //public INotificationService NotificationService { get; }
        #endregion
    }
    }
