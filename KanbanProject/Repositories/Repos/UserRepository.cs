using KanbanProject.Data;
using KanbanProject.Data.DTOs.User;
using KanbanProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace KanbanProject.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserDTO?> GetUserByIdAsync(int id)
        {
            var user = await _context.Users
                .Where(u => u.Id == id && !u.IsActive)
                .FirstOrDefaultAsync();

            if (user == null) return null;

            return new UserDTO
            {
                UserId = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };
        }

        public async Task<IEnumerable<UserDTO>> GetAllInactiveUsersAsync()
        {
            return await _context.Users
                .Where(u => !u.IsActive)
                .Select(u => new UserDTO
                {
                    UserId = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password
                })
                .ToListAsync();
        }

        public async Task<User?> AddUserAsync(UserRegistrationDTO userDto, string userInclusion)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == userDto.Email);
            if (existingUser != null) return null;

            var user = new User
            {
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                Email = userDto.Email,
                Password = userDto.Password,
                UserInclusion = userInclusion,
                UserChange = userInclusion
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, UserDTO userDto, string userChange)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.DateTimeChange = DateTime.UtcNow;
            user.UserChange = userChange;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserPermissionAsync(int id, UserPermissionDTO userPermissionDto, string userChange)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsAdmin = userPermissionDto.IsAdmin;
            user.DateTimeChange = DateTime.UtcNow;
            user.UserChange = userChange;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DisableUserAsync(int id, string userChange)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.IsActive = true;
            user.DateTimeChange = DateTime.UtcNow;
            user.UserChange = userChange;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> LoginAsync(UserLoginDTO loginDto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginDto.Email);
            if (user == null || user.Password != loginDto.Password) return null;

            return user;
        }
    }
}
