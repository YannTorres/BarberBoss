using BarberBoss.Domain.Entities;
using BarberBoss.Domain.Security.Tokens;
using BarberBoss.Domain.Services.LoggedUser;
using BarberBoss.Infrastructure.DataAcess;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BarberBoss.Infrastructure.Services.LoggedUser;
internal class LoggedUser : ILoggedUser
{
    private readonly BarberBossDbContext _dbContext;
    private readonly ITokenProvider _tokenProvider;
    public LoggedUser(BarberBossDbContext dbContext, ITokenProvider tokenProvider)
    {
        _dbContext = dbContext;
        _tokenProvider = tokenProvider;
    }
    public async Task<User> Get()
    {
        var tokenOnRequest = _tokenProvider.TokenOnRequest();

        var tokenHandler = new JwtSecurityTokenHandler();

        var jwtSecurityToken = tokenHandler.ReadJwtToken(tokenOnRequest);

        var identifier = jwtSecurityToken.Claims.First(c => c.Type.Equals(ClaimTypes.Sid)).Value;

        return await _dbContext.Users
            .AsNoTracking()
            .FirstAsync(g => g.UserIdentifier.Equals(Guid.Parse(identifier)));
    }
}
