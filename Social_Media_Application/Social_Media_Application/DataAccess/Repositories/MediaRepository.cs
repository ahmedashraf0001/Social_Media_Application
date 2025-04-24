using Social_Media_Application.Common.Entities;
using Social_Media_Application.DataAccess.Data;
using System.Reflection.Metadata.Ecma335;

namespace Social_Media_Application.DataAccess.Repositories
{
    public class MediaRepository : Repository<Media>
    {
        private readonly SocialDBContext _context;
        public MediaRepository(SocialDBContext context) : base(context)
        {
            _context = context;
        }
        public async Task<MediaType> GetMediaType(int Id) 
        {
            var media = await _set.FindAsync(Id);
            return media.Type;
        }
    }
}
