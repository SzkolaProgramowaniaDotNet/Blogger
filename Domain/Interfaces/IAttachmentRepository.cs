using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAttachmentRepository
    {
        Task<IEnumerable<Attachment>> GetByPostIdAsync(int postId);
        Task<Attachment> GetByIdAsync(int id);
        Task<Attachment> AddAsync(Attachment attachment);
        Task DeleteAsync(Attachment picture);
    }
}
