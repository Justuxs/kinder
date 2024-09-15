﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kinder_app.Models
{
    public static class CurrentUserExtention
    {
        public static string GetUserID(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;

            return currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
        public static string GetUserName(this ClaimsPrincipal user)
        {
            if (!user.Identity.IsAuthenticated)
                return null;

            ClaimsPrincipal currentUser = user;

            return currentUser.FindFirst(ClaimTypes.Name).Value;
        }
    }
}
