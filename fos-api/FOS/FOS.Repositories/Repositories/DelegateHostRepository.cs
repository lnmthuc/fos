using FOS.Repositories.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Repositories.Repositories
{
    public interface IDelegateHostRepository
    {
        Model.Domain.DelegateHost Read(Model.Domain.User userInfo);
    }
    class DelegateHostRepository : IDelegateHostRepository
    {
        private readonly FosContext _context;
        public DelegateHostRepository(FosContext context)
        {
            _context = context;
        }
        public Model.Domain.DelegateHost Read(Model.Domain.User userInfo)
        {
            DelegateHost delegateHost = _context.DelegateHost.Where(d => d.Mail == userInfo.Mail).FirstOrDefault();
            Mapping.DelegateHostMapper mapper = new Mapping.DelegateHostMapper();

            Model.Domain.DelegateHost domain = mapper.MapToDomain(delegateHost);

            return domain;
        }
    }
}
