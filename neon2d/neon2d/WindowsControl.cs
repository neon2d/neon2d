using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace neon2d.UI
{
    public class WindowsControl
    {

        public Control c;

        /// <param name="windowsControl">A System.Windows.Forms Control for this WindowsUI to use</param>
        public WindowsControl(Control windowsControl)
        {
            c = windowsControl;
        }

        /// <returns>Returns this WindowUI's System.Windows.Forms Control</returns>
        public Control getControl()
        {
            return c;
        }

        /// <param name="windowsControl">A System.Windows.Forms Control for this WindowsUI to use</param>
        public void setControl(Control windowsControl)
        {
            c = windowsControl;
        }

    }
}
