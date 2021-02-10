using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace API.Data
{
    public class DirectMessageRepository : IDirectMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public DirectMessageRepository(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }

        public void AddDirectMessage(DirectMessage directMessage)
        {
            _context.DirectMessages.Add(directMessage);
        }

        public void DeleteDirectMessage(DirectMessage directMessage)
        {
            _context.DirectMessages.Remove(directMessage);
        }

        public async Task<DirectMessage> GetDirectMessage(int id)
        {
            return await _context.DirectMessages.FindAsync(id);
        }

        public async Task<PagedList<DirectMessageDto>> GetDirectMessagesForUser(DirectMessageParams directMessageParams)
        {
            var query = _context.DirectMessages.OrderByDescending(m => m.TimeSent).AsQueryable();

            query = directMessageParams.Container switch
            {
                "Inbox" => query.Where(u => u.Reciever.UserName == directMessageParams.Username),
                "Outbox" => query.Where(u => u.Sender.UserName == directMessageParams.Username),
                _ => query.Where(u => u.Reciever.UserName == directMessageParams.Username && u.TimeRead == null)
            };

            var directMessages = query.ProjectTo<DirectMessageDto>(_mapper.ConfigurationProvider);

            return await PagedList<DirectMessageDto>.CreateAsync(directMessages, directMessageParams.PageNumber, directMessageParams.PageSize);
        }

        public Task<IEnumerable<DirectMessageDto>> GetDirectMessageThread(int currentUserID, int recieverId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}