namespace Hospital.Application.CustomExceptionMiddleware;

public interface IExceptionMiddlewareService
{
    Task<T> ExecuteAsync<T>(Func<Task<T>> action);
    Task ExecuteAsync(Func<Task> action);
}
