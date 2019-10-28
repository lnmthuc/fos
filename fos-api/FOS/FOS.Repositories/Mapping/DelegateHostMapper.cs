using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FOS.Model.Domain;

namespace FOS.Repositories.Mapping
{
    public interface IDelegateHostMapper
    {
        void MapToEfObject(DataModel.DelegateHost efObject, Model.Domain.DelegateHost domObject);
        Model.Domain.DelegateHost MapToDomain(DataModel.DelegateHost efObject);
    }
    public class DelegateHostMapper : IDelegateHostMapper
    {   
        public Model.Domain.DelegateHost MapToDomain(DataModel.DelegateHost efObject)
        {
            return new Model.Domain.DelegateHost
            {
                ID = new Guid(efObject.ID),
                Mail = efObject.Mail,
                DelegateUser = JsonConvert.DeserializeObject<List<User>>(efObject.DelegateUser)
            };
        }
        public void MapToEfObject(DataModel.DelegateHost efObject, Model.Domain.DelegateHost domObject)
        {
            efObject.ID = domObject.ID.ToString();
            efObject.Mail = domObject.Mail;
            efObject.DelegateUser = domObject.DelegateUser != null ? JsonConvert.SerializeObject(domObject.DelegateUser) : "";
        }
    }
}
