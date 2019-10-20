using System.Collections.Generic;
using System.Threading.Tasks;
using dating_app.api.Helpers;
using dating_app.api.Models;

namespace dating_app.api.Data
{
    public interface IDatingRepository
    {
        void Add<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveAll();
        Task<PageList<User>> GetUsers(UserParams userParams);
        Task<User> GetUser(int id);
        Task<Photo> GetPhoto(int id);
        Task<Photo> GetMainPhotoForUser(int userId);
        Task<Like> GetLike(int userId, int recipientId);
        Task<Message> GetMessage(int id);
        Task<PageList<Message>> GetMessagesForUser();
        Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId); // conversation between 2 users
        
    }
}