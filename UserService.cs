using Microsoft.EntityFrameworkCore;
using TGU.Data;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(u => EF.Functions.Like(u.Email, email)) // Case-insensitive search using LIKE
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<ApplicationUser> GetUserByPhoneAsync(string phone)
    {
        var user = await _context.Users
            .Where(u => EF.Functions.Like(u.PhoneNumber, phone)) // Case-insensitive search using LIKE
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task RegisterCarAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task<List<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<List<ApplicationUser>> GetAllMechanicsAsync()
    {
        return await _context.Users
            .Where(u => u.Permission == "Mechanic")
            .ToListAsync();
    }
}
