using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LowSpagVM.Common {
    public class LSDbg {
        public static void WriteLine(string l) {
#if DEBUG
            //Debug.WriteLine(l);
#endif
        }
    }
}
