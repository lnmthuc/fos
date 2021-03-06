﻿using FOS.Model.Dto;
using FOS.Model.Mapping;
using FOS.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Services
{
    public interface IFOSFoodServiceAPIsService
    {
        //string GetByIdAsync(int businessId);
        APIs GetById(int Id);
    }
    public class FOSFoodServiceAPIsService : IFOSFoodServiceAPIsService
    {
        IFOSFoodServiceAPIsRepository _iFOSFood;
        IAPIsDtoMapper _aPIsDto;
        public FOSFoodServiceAPIsService(IFOSFoodServiceAPIsRepository iFOSFood, IAPIsDtoMapper aPIsDto)
        {
            _iFOSFood = iFOSFood;
            _aPIsDto = aPIsDto;
        }
        public APIs GetById(int Id)
        {
            return _aPIsDto.ToDto(_iFOSFood.GetFOSCrawlLinksById(Id));
        }

    }
}
