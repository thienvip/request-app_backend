using src.Core;
using src.Core.Domains;
using src.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repositories.Applications
{
    public class ApplicationRepository: IApplicationRepository
    {
        private readonly IDbContext _context;
        public ApplicationRepository(IDbContext context)
        {
            _context = context;
        }

        public List<Core.Domains.Applications> GetAllApplications()
        {
            return _context.Applications.ToList();
        }
 

    }
}
