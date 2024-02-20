using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Core.Domains
{
    public partial class Applications
    {
        public  Applications()
        {
            ChangeRequests = new HashSet<ChangeRequest>();
        }
        public string Id;
        public string Name;
        public string Enable;

        //navigation property
        public virtual ICollection<ChangeRequest> ChangeRequests { get; set; }

    }
}
