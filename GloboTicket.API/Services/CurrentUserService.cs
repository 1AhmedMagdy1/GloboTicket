using GloboTicket.Application.Contracts.Services;
using System.Security.Claims;

namespace GloboTicket.API.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string? getCurrentUserId()
        {
            var userid = _httpContextAccessor?.HttpContext?.User.FindFirstValue("uid");
            return userid;
        }
    }
}
