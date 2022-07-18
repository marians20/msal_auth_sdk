using CSharpFunctionalExtensions;

namespace SDK.Extensions;

internal static class TryCatch
{
    public static Result<TResult> Execute<TResult>(Func<TResult> func)
    {
        try
        {
            return Result.Success(func());
        }
        catch(Exception ex)
        {
            return Result.Failure<TResult>(ex.ToString());
        }
    }

    public static Result<TResult> TryCatchFinally<TResult>(Func<TResult> func, Action onFinally)
    {
        try
        {
            return Result.Success(func());
        }
        catch (Exception ex)
        {
            return Result.Failure<TResult>(ex.ToString());
        }
        finally
        {
            onFinally();
        }
    }

    public static async Task<Result<TResult>> ExecuteAsync<TResult>(Func<Task<TResult>> func)
    {
        try
        {
            return Result.Success(await func());
        }
        catch (Exception ex)
        {
            return Result.Failure<TResult>(ex.ToString());
        }
    }

    public static async Task<Result<TResult>> TryCatchFinallyAsync<TResult>(Func<Task<TResult>> func, Action onFinally)
    {
        try
        {
            return Result.Success(await func());
        }
        catch (Exception ex)
        {
            return Result.Failure<TResult>(ex.ToString());
        }
        finally
        {
            onFinally();
        }
    }
}
