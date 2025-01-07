using Microsoft.EntityFrameworkCore;
using TGU.Data;

public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<List<ApplicationUser>> GetAllCustomersAsync()
    {
        return await _context.Users
            .Where(u => u.Permission == "Customer")
            .ToListAsync();
    }

    public async Task<List<ApplicationUser>> GetAllMechanicsAsync()
    {
        return await _context.Users
            .Where(u => u.Permission == "Mechanic")
            .ToListAsync();
    }

    public async Task<ApplicationUser> GetUserByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(u => EF.Functions.Like(u.Email, email)) // Case-insensitive search using LIKE
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<String> GetUserPermissionByEmailAsync(string email)
    {
        var user = await _context.Users
            .Where(u => EF.Functions.Like(u.Email, email)) // Case-insensitive search using LIKE
            .FirstOrDefaultAsync();

        var permission = user.Permission;
        return permission;
    }

    public async Task<ApplicationUser> GetUserByPhoneAsync(string phone)
    {
        var user = await _context.Users
            .Where(u => EF.Functions.Like(u.PhoneNumber, phone)) // Case-insensitive search using LIKE
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task<Boolean> CheckUserPhoneAsync(string phone)
    {
        var phoneFound = false;
        var user = await _context.Users
            .Where(u => EF.Functions.Like(u.PhoneNumber, phone)) // Case-insensitive search using LIKE
            .FirstOrDefaultAsync();

        if (user != null)
        {
            phoneFound = true;
        }

        return phoneFound;
    }

    public async Task RegisterCarAsync(Car car)
    {
        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCarStatusAsync(Car car)
    {
        var existingCar = await _context.Cars.FirstOrDefaultAsync(c => c.Vin == car.Vin);
        if (existingCar != null)
        {
            existingCar.Status = car.Status;
            _context.Cars.Update(existingCar);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Car>> GetAllCarsAsync()
    {
        return await _context.Cars.ToListAsync();
    }

    public async Task<List<Car>> GetCarsByOwnerAsync(string email)
    {
        return await _context.Cars
            .Where(c => c.OwnerEmail == email)
            .ToListAsync();
    }

    public async Task<Car> GetCarByVinAsync(string vin)
    {
        var car = await _context.Cars
            .Where(c => EF.Functions.Like(c.Vin, vin)).
            FirstOrDefaultAsync();

        return car;
    }
}
