﻿namespace _2DGraphicsLU2.WebApi.Services
{
    public interface IAuthenticationService
    {
        /// <summary>
        /// Returns the user name of the authenticated user
        /// </summary>
        /// <returns></returns>
        string? GetCurrentAuthenticatedUserId();
    }
}