using HttpClientGuide.Server.Authorization;
using HttpClientGuide.Server.Storage;
using HttpClientGuide.Shared.Dto;
using HttpClientGuide.Shared.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HttpClientGuide.Server.Controllers
{
    [BearerAuthentication]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ApiResponse<User>> Get([FromQuery] string id)
        {
            if (id == null) return new ApiResponse<User>(false, "No id provided", new User());

            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if(user == null) return new ApiResponse<User>(false, "No user with matching id", new User());

            return new ApiResponse<User>(true, "Retrieved one user", user);
        }
        [HttpGet("getall")]
        public async Task<ApiResponse<List<User>>> GetAll()
        {
            return new ApiResponse<List<User>>(true, "Retrieved all users",
                await _context.Users.AsNoTracking().ToListAsync());
        }
        [HttpPost]
        public async Task<ApiResponse> Create([FromBody] User user)
        {
            if (_context.Users.Any(x => x.Id == user.Id))
            {
                return new ApiResponse(false, "User already exists");
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return new ApiResponse(true, "Created one user");
        }
        [HttpPut]
        public async Task<ApiResponse> Update([FromBody] User user)
        {
            if (!_context.Users.Any(x => x.Id == user.Id))
            {
                return new ApiResponse(false, "User did not exist");
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return new ApiResponse(true, "Updated one user");
        }
        [HttpDelete]
        public async Task<ApiResponse> Delete([FromQuery] string id)
        {
            if (!_context.Users.Any(x => x.Id == id))
            {
                return new ApiResponse(false, "User did not exist");
            }

            var user = await _context.Users.AsNoTracking().FirstAsync(x => x.Id == id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return new ApiResponse(true, "Deleted one user");
        }
    }
}
