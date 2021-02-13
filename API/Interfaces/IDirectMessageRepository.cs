using System.Collections.Generic;
using System.Threading.Tasks;
using API.DTOs;
using API.Entities;
using API.Helpers;

namespace API.Interfaces
{
    public interface IDirectMessageRepository
    {
        void AddDirectMessage(DirectMessage directMessage);
        void DeleteDirectMessage(DirectMessage directMessage);
        Task<DirectMessage> GetDirectMessage(int id);
        Task<PagedList<DirectMessageDto>> GetDirectMessagesForUser(DirectMessageParams directMessageParams);
        Task<IEnumerable<DirectMessageDto>> GetDirectMessageThread(string currentUsername, string recieverUsername);
        Task<bool> SaveAllAsync();
    }
}