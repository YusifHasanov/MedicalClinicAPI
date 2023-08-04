﻿using Entities.Dto;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IService<T,U,C> 
        where T : BaseEntity, new()  
        where U : BaseDto, new()
        where C : BaseDto, new()

    { 
        public IQueryable<T> GetAll(); 
        public T GetById(int id); 
        public Task<T> DeleteAsync(int id);
        public T IsExist(int id);
        public Task SaveChangesAsync();
        public Task<T> UpdateAsync(int id, U entity);
        public Task<T> AddAsync(C entity);
    }
}