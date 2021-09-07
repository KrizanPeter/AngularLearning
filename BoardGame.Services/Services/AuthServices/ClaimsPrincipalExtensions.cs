using BoardGame.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace BoardGame.Services.Services.AuthServices
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserName(this ClaimsPrincipal userClaim)
        {
            var userName = userClaim.FindFirstValue(ClaimTypes.NameIdentifier);
            return userName;
        }
    }
}
