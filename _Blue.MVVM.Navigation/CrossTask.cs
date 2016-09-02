using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public static class CrossTask {

        public static async Task Yield() {
#if BACKPORTED_TPL
            await TaskEx.Yield();
#else
            await Task.Yield();
#endif
        }

    }
}
