using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BAS.ConfigUtil
{
    class ILog
    {
        internal void WarnFormat(string p1, string p2, string temp)
        {
            throw new NotImplementedException();
        }

        internal void TraceFormat(string p, string configname, string temp)
        {
            throw new NotImplementedException();
        }

        internal void WarnFormat(string p1, Exception ex, string p2)
        {
            throw new NotImplementedException();
        }

        internal void WarnFormat(string p1, string p2, object p3)
        {
            throw new NotImplementedException();
        }

        internal void WarnFormat(string p, string temp)
        {
            throw new NotImplementedException();
        }
    }
}
