using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServicesShared.Services
{
    public class SharedIdentityService : ISharedIdentityService
    {
        readonly IHttpContextAccessor _httpContextAccessor;

        public SharedIdentityService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId { get
            {
                return _httpContextAccessor.HttpContext.User.Claims.Where(x => x.Type == "sub").FirstOrDefault().Value;
            } }
    }
}
