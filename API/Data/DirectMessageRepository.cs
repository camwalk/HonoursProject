using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<DirectMessageDto>> GetDirectMessageThread(string currentUsername, string recieverUsername)
        {
            var directMessages = await _context.DirectMessages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Reciever).ThenInclude(p => p.Photos)
                .Where(m => m.Reciever.UserName == currentUsername
                && m.Sender.UserName == recieverUsername
                || m.Reciever.UserName == recieverUsername
                && m.Sender.UserName == currentUsername
            ).OrderBy(m => m.TimeSent).ToListAsync();

            var unreadDirectMessages = directMessages.Where(m => m.TimeRead == null && m.Reciever.UserName == currentUsername).ToList();

            if (unreadDirectMessages.Any())
            {
                foreach (var directMessage in unreadDirectMessages)
                {
                    directMessage.TimeRead = DateTime.Now;
                }

                await _context.SaveChangesAsync();
            }

            return _mapper.Map<IEnumerable<DirectMessageDto>>(directMessages);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}