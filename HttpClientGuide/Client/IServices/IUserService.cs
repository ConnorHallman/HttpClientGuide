using HttpClientGuide.Shared.Dto;
using HttpClientGuide.Shared.Model;

namespace HttpClientGuide.Client.IServices
{
    public interface IUserService
    {
        public Task CreateAsync(User user);
        public Task<List<User>> GetAllAsync();
        public Task UpdateAsync(User user);
        public Task DeleteAsync(string userId);
    }
}
