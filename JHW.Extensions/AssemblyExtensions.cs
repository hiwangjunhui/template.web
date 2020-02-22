using System;
using System.Reflection;

namespace JHW
{
    public static class AssemblyExtensions
    {
        //获取程序集的Hash值
        public static byte[] GetAssemblyHash(this Assembly assembly)
        {
            if (null == assembly)
            {
                return null;
            }

            var enumerator = assembly.Evidence.GetHostEnumerator();
            
            while (enumerator.MoveNext())
            {
                var hash = enumerator.Current as System.Security.Policy.Hash;
                if (null != hash)
                {
                    return hash.SHA256;
                }
            }

            return Guid.NewGuid().ToByteArray();
        }
    }
}
