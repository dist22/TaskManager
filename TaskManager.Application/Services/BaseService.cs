namespace TaskManager.Application.Services;

public class BaseService
{
    protected async Task<T> GetIfNotNull<T>(Task<T> task)
    {
        var result = await task;
        if(result != null) return result;
            throw new NullReferenceException();
    }
    
    protected async Task EnsureSuccess(Task<bool> task)
    {
        var result = await task;
        if(result) return;
        throw new NullReferenceException();
    }
}