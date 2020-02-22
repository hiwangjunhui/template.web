using System;
using System.Threading;

namespace JHW
{
    public static class ActionExtensions
    {
        /// <summary>
        /// 重复某个行为
        /// </summary>
        /// <param name="action">要重要的动作</param>
        /// <param name="repeatCount">重复次数</param>
        /// <param name="interval">每次间隔时间，单位：毫秒</param>
        public static void Repeat(this Action action, int repeatCount = 3, int interval = 0)
        {
            do
            {
                action?.Invoke();

                if (interval > 0)
                    Thread.Sleep(millisecondsTimeout: interval);
                repeatCount--;
            } while (repeatCount > 0);
        }
    }
}
