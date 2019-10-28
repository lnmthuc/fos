using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FOS.Model.Domain
{
    public class DelegateHost
    {
        public Guid ID { get; set; }
        public string Mail { get; set; }
        public virtual ICollection<User> DelegateUser { get; set; }
    }
}
