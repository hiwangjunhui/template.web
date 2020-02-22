using System;
using System.Threading;

namespace JHW
{
    public static class FuncExtensions
    {
        /// <summary>
        /// 重试
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="action"></param>
        /// <param name="isSuccess">是否成功</param>
        /// <param name="retryCount">重试次数</param>
        /// <param name="interval">时间间隔，单位：毫秒</param>
        /// <returns></returns>
        public static TResult Retry<TResult>(this Func<TResult> action, Func<TResult, bool> isSuccess, int retryCount = 3, int interval = 0)
        {
            if (null == action)
            {
                return default(TResult);
            }

            TResult result;
            do
            {
                result = action();
                if (isSuccess?.Invoke(result) ?? false)
                {
                    return result;
                }

                if (interval > 0)
                    Thread.Sleep(millisecondsTimeout: interval);

                retryCount--;
            } while (retryCount > 0);

            return result;
        }

        public static TResult Retry<TIn, TResult>(this Func<TIn, TResult> action, TIn arg, Func<TResult, bool> isSuccess, int retryCount = 3, int interval = 0)
        {
            return Retry(() => action(arg), isSuccess, retryCount, interval);
        }

        public static TResult Retry<TIn1, TIn2, TResult>(this Func<TIn1, TIn2, TResult> action, TIn1 arg1, TIn2 arg2, Func<TResult, bool> isSuccess, int retryCount = 3, int interval = 0)
        {
            return Retry(() => action(arg1, arg2), isSuccess, retryCount, interval);
        }

        public static TResult Retry<TIn1, TIn2, TIn3, TResult>(this Func<TIn1, TIn2, TIn3, TResult> action, TIn1 arg1, TIn2 arg2, TIn3 arg3, Func<TResult, bool> isSuccess, int retryCount = 3, int interval = 0)
        {
            return Retry(() => action(arg1, arg2, arg3), isSuccess, retryCount, interval);
        }
    }
}
