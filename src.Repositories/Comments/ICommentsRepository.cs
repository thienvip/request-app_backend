using src.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src.Repositories.Comments
{
    public interface ICommentsRepository
    {
        Task AddCommentAsync(src.Core.Domains.Comments model);

        Task<List<Core.Domains.Comments>> GetCmtById(Guid Id);
    }
}
