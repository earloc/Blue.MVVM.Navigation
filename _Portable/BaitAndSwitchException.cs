using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.MVVM.Navigation {
    public class BaitAndSwitchException : Exception {

        public BaitAndSwitchException() : base ("This is a bate and switch PCL, not the Platform library. Please install the platform specific package into your Main- / UI-Project to gain access to the platform specific implementations. For mor info, see http://log.paulbetts.org/the-bait-and-switch-pcl-trick/") {

        }
    }
}
