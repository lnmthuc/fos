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

        public DelegateHost Read(User userInfo)
        {
            DelegateHost domain = _repository.Read(userInfo);
            return domain;
        }
    }
}
