using clinic_system_be.DTOs.User;
using clinic_system_be.Models;
using Microsoft.EntityFrameworkCore;

namespace clinic_system_be.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ClinicSystemDbContext _context;

        public UserRepository(ClinicSystemDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task AddUser(AddUserDTO user)
        {
            var newUser = new User
            {
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                FullName = user.FullName,
                DateOfBirth = user.DateOfBirth,
                Gender = user.Gender,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                Status = user.Status
            };
            await _context.Users.AddAsync(newUser);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }
        public async Task<IEnumerable<User>> GetUsersByRole(string role)
        {
            return await _context.Users.Where(u => u.Role.ToLower() == role.ToLower()).ToListAsync();
        }
        public async Task<bool> PhoneNumberExists(string phoneNumber)
        {
            return await _context.Users.AnyAsync(u => u.PhoneNumber == phoneNumber);
        }
    }
}
