using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Model.Mapping
{   
    public interface IDelegateHostDtoMapper
    {
        Model.Domain.DelegateHost ToDomain(Model.Dto.DelegateHost domain);
        Model.Dto.DelegateHost ToDto(Model.Domain.DelegateHost dto);
    }
    public class DelegateHostDtoMapper : IDelegateHostDtoMapper
    {
        UserDtoMapper mapper = new UserDtoMapper();
        public Model.Domain.DelegateHost ToDomain(Model.Dto.DelegateHost domain)
        {
            return new Domain.DelegateHost
            {
                ID = domain.ID,
                Mail = domain.Mail,
                DelegateUser = domain.DelegateUser.Select(u => mapper.ToDomain(u)).ToList()
            };
        }
        public Model.Dto.DelegateHost ToDto(Model.Domain.DelegateHost dto)
        {
            return new Dto.DelegateHost
            {
                ID = dto.ID,
                Mail = dto.Mail,
                DelegateUser = dto.DelegateUser.Select(u=>mapper.ToDto(u)).ToList()
            };
        }
    }
}
