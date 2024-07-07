
using Entittes;

namespace Services.Services;

public interface IJwtService
{
     Task<string> GenerateAsync(User user);
}
