using System.Security.Claims;
using Epal.Application.Common.Models;
using Microsoft.AspNetCore.Http;

namespace Epal.Application.Interfaces;

public interface IUserService
{
    public AuthenticatedUser AuthenticatedUser { get; }
}