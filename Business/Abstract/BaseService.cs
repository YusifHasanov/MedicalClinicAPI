using AutoMapper;
using Entities.Entities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Dto;
using Core.Utils.Constants;

namespace Business.Abstract
{
    public abstract class BaseService<T, U, C, R> : IService<T, U, C, R>
    where T : BaseEntity, new()
        where U : BaseDto, new()
        where C : BaseDto, new()
        where R : BaseDto, new()
    {
        protected readonly IUnitOfWorkRepository _unitOfWorkRepository;
        protected readonly IMapper _mapper;
        protected readonly ILogService _logService;
        protected readonly Globals _globals;


        public BaseService(IUnitOfWorkRepository unitOfWorkRepository, IMapper mapper, ILogService logService, Globals globals)
        {
            _unitOfWorkRepository = unitOfWorkRepository;
            _mapper = mapper;
            _logService = logService;
            _globals = globals;
        }

        public abstract Task<T> AddAsync(C entity);
        public abstract Task<T> DeleteAsync(int id);
        public abstract IQueryable<R> GetAll();
        public abstract R GetById(int id);
        public abstract T IsExist(int id);
        public abstract Task SaveChangesAsync();
        public abstract Task<T> UpdateAsync(int id, U entity);
    }
}
