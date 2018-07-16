using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapeCity.Tools.ComponentOneControlPanel.Containers
{
  internal  class WebJsonArg:EventArgs
    {
        internal bool Success { get; set; }
        internal WebJsonFailureEnum Failuretype { get; set; }
    }
}
