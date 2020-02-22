using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHW.TypeContainers
{
    /// <summary>
    /// 注册时类型没找到
    /// </summary>
    internal class RegisterTypeNotFoundException : Exception
    {
        public RegisterTypeNotFoundException(string message) : base(message)
        {

        }
    }
}
