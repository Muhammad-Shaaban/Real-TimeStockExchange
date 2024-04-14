using Microsoft.AspNetCore.Http;
using RealTimeStockExchange.BAL.IBusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RealTimeStockExchange.BAL.BusinessServices
{
    public class SessionService : ISessionService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public HttpContext HttpContext
        {
            get { return _httpContextAccessor.HttpContext; }
            set { _httpContextAccessor.HttpContext = value; }
        }

        public string? UserId
        {
            get
            {
                if (HttpContext.User is null)
                    return null;

                var claim = HttpContext.User.FindFirst(ClaimTypes.UserData);
                if (claim is null)
                    return null;

                return claim.Value;
            }
        }

        public string? UserName
        {
            get
            {
                if (HttpContext is null)
                    return null;

                var claim = HttpContext.User.FindFirst(ClaimTypes.Name);

                if (claim is null)
                    return null;

                return claim.Value;
            }
        }


    }
}
