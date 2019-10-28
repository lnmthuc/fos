using FOS.Model.Domain;
using FOS.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Services.DelegateHostService
{
    public class DelegateHostService : IDelegateHostService
    {
        private readonly IDelegateHostRepository _repository;

        public DelegateHostService(IDelegateHostRepository repository)
        {
            _repository = repository;
        }

        public Model.Domain.DelegateHost Read(User userInfo)
        {
            try
            {
                Model.Domain.DelegateHost domain = _repository.Read(userInfo);
                return domain;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Create(DelegateHost delegateHost)
        {
            try
            {
                _repository.Create(delegateHost);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public void Update(DelegateHost delegateHost)
        {
            try
            {
                _repository.Update(delegateHost);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
