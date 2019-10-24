using FOS.API.App_Start;
using FOS.Model.Domain;
using FOS.Model.Mapping;
using FOS.Model.Util;
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
                return ApiUtil.CreateSuccessfulResult();
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
