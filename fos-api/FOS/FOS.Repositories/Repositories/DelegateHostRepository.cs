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
        void Create(Model.Domain.DelegateHost delegateHost);
        void Update(Model.Domain.DelegateHost delegateHost);
    }
    public class DelegateHostRepository : IDelegateHostRepository
    {
        private readonly FosContext _context;
        public DelegateHostRepository(FosContext context)
        {
            _context = context;
        }
        public Model.Domain.DelegateHost Read(Model.Domain.User userInfo)
        {
            DelegateHost delegateHost = _context.DelegateHost.Where(d => d.Mail == userInfo.Mail).FirstOrDefault();
            if (delegateHost == null)
            {
                return null;
            }
            else
            {
                Mapping.DelegateHostMapper mapper = new Mapping.DelegateHostMapper();
                Model.Domain.DelegateHost domain = mapper.MapToDomain(delegateHost);
                return domain;
            }
        }
        public void Create(Model.Domain.DelegateHost delegateHost)
        {
            Mapping.DelegateHostMapper mapper = new Mapping.DelegateHostMapper();
            DataModel.DelegateHost dataModel = new DataModel.DelegateHost();
            mapper.MapToEfObject(dataModel, delegateHost);
            _context.DelegateHost.Add(dataModel);
            _context.SaveChanges();
        }
        public void Update(Model.Domain.DelegateHost delegateHost)
        {
            Mapping.DelegateHostMapper mapper = new Mapping.DelegateHostMapper();
            DelegateHost efObject = new DelegateHost();
            mapper.MapToEfObject(efObject,delegateHost);

            DelegateHost updateDelegate = (from d in _context.DelegateHost
                                           where d.Mail == efObject.Mail
                                           select d).SingleOrDefault();
            updateDelegate.DelegateUser = efObject.DelegateUser;
            _context.SaveChanges();
        }
    }
}
