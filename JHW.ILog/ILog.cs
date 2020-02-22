using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHW.ILog
{
    public interface ILog
    {
        void Config();

        void Write(string message);

        void Write(Exception exception);
    }
}
