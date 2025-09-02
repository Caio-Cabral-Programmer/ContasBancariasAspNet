using ContasBancariasAspNet.Api.Data;
using ContasBancariasAspNet.Api.Exceptions;
using ContasBancariasAspNet.Api.Models;
using ContasBancariasAspNet.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContasBancariasAspNet.Api.Services.Implementations;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<UserService> _logger;

    public UserService(ApplicationDbContext context, ILogger<UserService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<IEnumerable<User>> FindAllAsync()
    {
        return await _context.Users
            .Include(u => u.Account)
            .Include(u => u.Card)
            .ToListAsync();
    }

    public async Task<User> FindByIdAsync(long id)
    {
        var user = await _context.Users
            .Include(u => u.Account)
            .Include(u => u.Card)
            .FirstOrDefaultAsync(u => u.Id == id);

        return user ?? throw new NotFoundException($"User with ID {id} not found.");
    }

    public async Task<User> CreateAsync(User entity)
    {
        if (entity.Account?.Number != null)
        {
            var existingAccount = await _context.Accounts
                .FirstOrDefaultAsync(a => a.Number == entity.Account.Number);

            if (existingAccount != null)
                throw new BusinessException($"Account with number {entity.Account.Number} already exists.");
        }

        if (entity.Card?.Number != null)
        {
            var existingCard = await _context.Cards
                .FirstOrDefaultAsync(c => c.Number == entity.Card.Number);

            if (existingCard != null)
                throw new BusinessException($"Card with number {entity.Card.Number} already exists.");
        }

        _context.Users.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<User> UpdateAsync(long id, User entity)
    {
        var existingUser = await FindByIdAsync(id);

        existingUser.Name = entity.Name;

        if (entity.Account != null)
        {
            if (existingUser.Account != null)
            {
                existingUser.Account.Number = entity.Account.Number;
                existingUser.Account.Agency = entity.Account.Agency;
                existingUser.Account.Balance = entity.Account.Balance;
                existingUser.Account.Limit = entity.Account.Limit;
            }
            else
            {
                existingUser.Account = entity.Account;
            }
        }

        if (entity.Card != null)
        {
            if (existingUser.Card != null)
            {
                existingUser.Card.Number = entity.Card.Number;
                existingUser.Card.Limit = entity.Card.Limit;
            }
            else
            {
                existingUser.Card = entity.Card;
            }
        }

        await _context.SaveChangesAsync();
        return existingUser;
    }

    public async Task DeleteAsync(long id)
    {
        var user = await FindByIdAsync(id);
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}