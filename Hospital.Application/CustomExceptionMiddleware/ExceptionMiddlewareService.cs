using Hospital.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace Hospital.Application.CustomExceptionMiddleware;

public class ExceptionMiddlewareService : IExceptionMiddlewareService
{
    private readonly ILogger<ExceptionMiddlewareService> _logger;

    public ExceptionMiddlewareService(ILogger<ExceptionMiddlewareService> logger)
    {
        _logger=logger;
    }

    public async Task<T> ExecuteAsync<T>(Func<Task<T>> action)
    {
        try
        {
            return await action();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred in  Action.");
            throw new ApplicationException("An error occurred. Please try again later.", ex);
        }
    }

    public async Task ExecuteAsync(Func<Task> action)
    {
        try
        {
            await action();
        }

        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while executing the asynchronous mathods.");
            throw new ApplicationException("An error occurred. Please try again later.", ex);
        }
    }

    public static  void ValidatePassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new DirectoryNotFoundException("Password Can not be null.");
        }

        if (password.Length < 8)
        {
            throw new ArgumentException("Password must be at least 8 characters long.");
        }

        if (!Regex.IsMatch(password, @"[A-Z]"))
        {
            throw new ArgumentException("Password must contain at least one uppercase letter.");
        }
    }
}
