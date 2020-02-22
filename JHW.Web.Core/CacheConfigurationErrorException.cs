using System;

namespace JHW.Web.Core
{
    internal class CacheConfigurationErrorException : Exception
    {
        public CacheConfigurationErrorException(string message) : base(message)
        {

        }
    }
}
