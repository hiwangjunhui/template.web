using System.Web;
using System.Web.SessionState;

namespace JHW.Web.Core
{
    public class CustomSessionIDManager : SessionIDManager
    {
        public override string CreateSessionID(HttpContext context)
        {
            return $"wjh.{base.CreateSessionID(context)}";
        }

        public override bool Validate(string id)
        {
            var items = id?.Split('.');
            if ((items?.Length ?? 0) != 2) return false;
            return base.Validate(items[1]);
        }
    }
}
