using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly BloggerContext _context;

        public PictureRepository(BloggerContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Picture>> GetByPostIdAsync(int postId)
        {
            return await _context.Pictures.Include(x => x.Posts).Where(x => x.Posts.Select(x => x.Id).Contains(postId)).ToListAsync();
        }

        public async Task<Picture> GetByIdAsync(int id)
        {
            return await _context.Pictures.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Picture> AddAsync(Picture picture)
        {
            var createdPicture = await _context.Pictures.AddAsync(picture);
            await _context.SaveChangesAsync();
            return createdPicture.Entity;
        }

        public async Task SetMainPictureAsync(int postId, int id)
        {
            var currentMainPicture =
                await _context.Pictures.Include(x => x.Posts)
                .Where(x => x.Posts.Select(x => x.Id).Contains(postId))
                .SingleOrDefaultAsync(x => x.Main);
            currentMainPicture.Main = false;

            var newMainPicture =
                await _context.Pictures
                .SingleOrDefaultAsync(x => x.Id == id);
            newMainPicture.Main = true;

            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Picture picture)
        {
            _context.Pictures.Remove(picture);
            await _context.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
