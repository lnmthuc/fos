using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Model.Domain;

namespace FOS.Services.DelegateHostService
{
    public interface IDelegateHostService
    {
        DelegateHost Read(User userInfo);
        void Create(DelegateHost DelegateHost);
        void Update(DelegateHost DelegateHost);
    }
}
