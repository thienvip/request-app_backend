using Microsoft.EntityFrameworkCore;
using src.Data;

namespace src.Repositories.Comments
{
    public class CommentsRepository : ICommentsRepository
    {
        private readonly IDbContext _context;

        public CommentsRepository(IDbContext context)
        {
            _context = context;
        }

        public async Task AddCommentAsync(Core.Domains.Comments model)
        {
          
                if (model == null)
                    throw new ArgumentNullException(nameof(model));

                _context.Comments.Add(model);

                await _context.SaveChangesAsync();
        }

        public async Task<List<Core.Domains.Comments>> GetCmtById(Guid Id)
        {
            var lstCmt = await _context.Comments.Where(x => x.Id == Id).ToListAsync();
            return lstCmt;
        }
    }
}
