using FOS.API.App_Start;
using FOS.Model.Domain;
using FOS.Model.Mapping;
using FOS.Model.Util;
using FOS.Repositories.Repositories;
using FOS.Services.DelegateHostService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FOS.API.Controllers
{
    [LogActionWebApiFilter]
    [RoutePrefix("api/DelegateHost")]
    public class DelegateHostController : ApiController
    {
        IUserDtoMapper _userDtoMapper;
        IDelegateHostService _delegateHostService;
        IDelegateHostDtoMapper _delegateHostDtoMapper;

        public DelegateHostController(IUserDtoMapper userDtoMapper, IDelegateHostService delegateHostService, IDelegateHostDtoMapper delegateHostDtoMapper)
        {
            _userDtoMapper = userDtoMapper;
            _delegateHostService = delegateHostService;
            _delegateHostDtoMapper = delegateHostDtoMapper;
        }
        [HttpPost]
        [Route("Create")]
        public ApiResponse Create([FromBody]Model.Dto.DelegateHost delegateInfo)
        {
            try
            {
                Model.Domain.User currentUser = new Model.Domain.User();
                currentUser.Mail = delegateInfo.Mail;
                Model.Domain.DelegateHost currentList = _delegateHostService.Read(currentUser);
                if(currentList == null)
                {
                    Guid id = Guid.NewGuid();
                    delegateInfo.ID = id;
                    Model.Domain.DelegateHost domain = _delegateHostDtoMapper.ToDomain(delegateInfo);
                    _delegateHostService.Create(domain);
                    return ApiUtil.CreateSuccessfulResult();
                }
                else
                {
                    Model.Domain.DelegateHost domain = _delegateHostDtoMapper.ToDomain(delegateInfo);
                    _delegateHostService.Update(domain);
                    return ApiUtil.CreateSuccessfulResult();
                }
            }
            catch (Exception e)
            {
                return ApiUtil.CreateFailResult(e.ToString());
            }
        }
        [HttpPost]
        [Route("Read")]
        public ApiResponse<Model.Dto.DelegateHost> Read([FromBody]Model.Dto.User userInfo)
        {
            try
            {
                Model.Domain.User domain = _userDtoMapper.ToDomain(userInfo);
                Model.Domain.DelegateHost delegateHostDomain = _delegateHostService.Read(domain);
                if(delegateHostDomain == null)
                {
                    return ApiUtil<Model.Dto.DelegateHost>.CreateFailResult(Common.Constants.DelegateHost.DelegateHostError);
                }
                Model.Dto.DelegateHost delegateHostDto = _delegateHostDtoMapper.ToDto(delegateHostDomain);
                return ApiUtil<Model.Dto.DelegateHost>.CreateSuccessfulResult(delegateHostDto);
            }
            catch (Exception e)
            {
                return ApiUtil<Model.Dto.DelegateHost>.CreateFailResult(e.ToString());
            }
        }
    }
}
