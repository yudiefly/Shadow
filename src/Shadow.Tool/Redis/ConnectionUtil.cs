using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shadow.Tool.Redis
{
    public static class ConnectionUtil
    {
        public static TResult Connect<TResult>(Func<TResult> action, int millisecondsTimeout, ILogger logger, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (CancellationTokenSource timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                using (CancellationTokenSource combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellationTokenSource.Token))
                {
                    CancellationToken combinedToken = combinedTokenSource.Token;

                    try
                    {
                        timeoutCancellationTokenSource.CancelAfter(millisecondsTimeout);

                        var actionTask = Task.Run(action, combinedToken);

                        try
                        {
                            actionTask.Wait(timeoutCancellationTokenSource.Token);
                        }
                        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1)
                        {
                            logger.LogError(ex, "Connect the redis server/sentinel error, inner exception.");
                            throw;
                        }

                        return actionTask.Result;
                    }
                    catch (Exception ex)
                    {
                        if (timeoutCancellationTokenSource.IsCancellationRequested)
                        {
                            logger.LogError(ex, "Connect the redis server/sentinel error timeout.");
                        }

                        throw;
                    }
                }
            }
        }

        public static async Task<TResult> ConnectAsync<TResult>(Func<TResult> action, int millisecondsTimeout, ILogger logger, CancellationToken cancellationToken = default(CancellationToken))
        {
            using (CancellationTokenSource timeoutCancellationTokenSource = new CancellationTokenSource())
            {
                using (CancellationTokenSource combinedTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutCancellationTokenSource.Token))
                {
                    CancellationToken combinedToken = combinedTokenSource.Token;

                    try
                    {
                        timeoutCancellationTokenSource.CancelAfter(millisecondsTimeout);

                        try
                        {
                            return await Task.Run(action, combinedToken);
                        }
                        catch (AggregateException ex) when (ex.InnerExceptions.Count == 1)
                        {
                            logger.LogError(ex, "Connect the redis server/sentinel error, inner exception.");
                            throw;
                        }
                    }
                    catch (Exception ex)
                    {
                        if (timeoutCancellationTokenSource.IsCancellationRequested)
                        {
                            logger.LogError(ex, "Connect the redis server/sentinel error timeout.");
                        }

                        throw;
                    }
                }
            }
        }
    }
}
