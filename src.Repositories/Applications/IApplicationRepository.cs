using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repositories.Applications
{
    public  interface IApplicationRepository
    {
        List<Core.Domains.Applications> GetAllApplications();
    }
}
