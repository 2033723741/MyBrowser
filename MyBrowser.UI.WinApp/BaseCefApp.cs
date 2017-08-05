namespace MyBrowser.UI.WinApp
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Xilium.CefGlue;

    internal sealed class BaseCefApp : CefApp
    {
        public BaseCefApp()
        {

        }

        protected override void OnBeforeCommandLineProcessing(string processType, CefCommandLine commandLine)
        {
            if (string.IsNullOrEmpty(processType))
            {
                commandLine.AppendSwitch("disable-gpu");
                commandLine.AppendSwitch("disable-gpu-compositing");
                commandLine.AppendSwitch("enable-begin-frame-scheduling");
                commandLine.AppendSwitch("disable-smooth-scrolling");
                commandLine.AppendSwitch("enable-media-stream");
            }
        }
    }
}
