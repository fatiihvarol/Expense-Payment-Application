using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Web.Business.Cqrs;
using Web.Data.DbContext;
using Web.Data.Entity;
using WebBase.Encryption;
using WebBase.Response;
using WebBase.Token;
using WebSchema;

namespace Web.Business.Command.TokenCommand;

public class TokenCommandHandler :
    IRequestHandler<CreateTokenCommand, ApiResponse<TokenResponse>>
{
    private readonly VbDbContext dbContext;
    private readonly JwtConfig jwtConfig;
    private readonly IMemoryCache memoryCache;


    public TokenCommandHandler(VbDbContext dbContext,IOptionsMonitor<JwtConfig> jwtConfig , IMemoryCache memoryCache)
    {
        this.dbContext = dbContext;
        this.jwtConfig = jwtConfig.CurrentValue;
        this.memoryCache = memoryCache;
    }
    
    public async Task<ApiResponse<TokenResponse>> Handle(CreateTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await dbContext.Set<ApplicationUser>().Where(x => x.UserName == request.Model.UserName)
            .FirstOrDefaultAsync(cancellationToken);
        if (user == null)
        {
            return new ApiResponse<TokenResponse>("Invalid user information");
        }

        string hash = Md5Extension.GetHash(request.Model.Password.Trim());
        if (hash != user.Password)
        {
            user.LastActivityDate = DateTime.UtcNow;
            user.PasswordRetryCount++;
            await dbContext.SaveChangesAsync(cancellationToken);
            return new ApiResponse<TokenResponse>("Invalid user information");
        }
        
        if (user.PasswordRetryCount > 3)
        {
            return new ApiResponse<TokenResponse>("Invalid user status");
        }
        
        user.LastActivityDate = DateTime.UtcNow;
        user.PasswordRetryCount = 0;
        await dbContext.SaveChangesAsync(cancellationToken);

        string token = Token(user);



        CacheEmployeeId(user,cancellationToken);
        return new ApiResponse<TokenResponse>( new TokenResponse()
        {
            Email = user.Email,
            Token = token,
            ExpireDate =  DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration)
        });
    }
    
    private string Token(ApplicationUser user)
    {
        Claim[] claims = GetClaims(user);
        var secret = Encoding.ASCII.GetBytes(jwtConfig.Secret);

        var jwtToken = new JwtSecurityToken(
            jwtConfig.Issuer,
            jwtConfig.Audience,
            claims,
            expires: DateTime.Now.AddMinutes(jwtConfig.AccessTokenExpiration),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(secret), SecurityAlgorithms.HmacSha256Signature)
        );

        string accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return accessToken;
    }
    
    private Claim[] GetClaims(ApplicationUser user)
    {
        var claims = new[]
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Email", user.Email),
            new Claim("UserName", user.UserName),
            new Claim(ClaimTypes.Role, user.Role)
        };

        return claims;
    }


    private async void CacheEmployeeId(ApplicationUser user, CancellationToken cancellationToken)
    {
        var employee = await dbContext.Set<Employee>()
            .FirstOrDefaultAsync(x => x.ApplicationUserId == user.Id, cancellationToken);

        if (employee != null)
        {
            // MemoryCache üzerine EmployeeId'yi önbelleğe alma işlemi
            var cacheKey = $"ApplicationUserId_{user.Id}";
            if (!memoryCache.TryGetValue(cacheKey, out int cachedEmployeeId))
            {
                // Önbellekte bulunamazsa, değeri ekleyin ve belirli bir süre boyunca önbellekte saklayın
                cachedEmployeeId = employee.Id;
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // Örnek: 30 dakika
                };
                memoryCache.Set(cacheKey, cachedEmployeeId, cacheEntryOptions);
            }

            // Şimdi cachedEmployeeId değerini kullanabilirsiniz
            Console.WriteLine($"Cached EmployeeId for UserId {user.Id}: {cachedEmployeeId}");
        }
    }

}