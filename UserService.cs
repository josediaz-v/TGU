using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using TGU.Data;

public class UserService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public UserService(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
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

    public async Task<string> GetUserPermissionByEmailAsync(string email)
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

    public async Task<bool> CheckUserPhoneAsync(string phone)
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
            .Where(c => EF.Functions.Like(c.Vin, vin))
            .FirstOrDefaultAsync();

        return car;
    }

    public async Task<string> UploadCarPictureAsync(IBrowserFile file, string vin)
    {
        var car = await _context.Cars.FirstOrDefaultAsync(c => c.Vin == vin);
        if (car == null)
        {
            throw new Exception("Car not found");
        }

        var uploadsFolder = Path.Combine(_environment.WebRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
        {
            Directory.CreateDirectory(uploadsFolder);
        }

        var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.Name;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.OpenReadStream().CopyToAsync(fileStream);
        }

        car.PictureUrls.Add("/uploads/" + uniqueFileName);
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();

        return "/uploads/" + uniqueFileName;
    }

    public async Task UpdateCarAsync(Car car)
    {
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
    }
}