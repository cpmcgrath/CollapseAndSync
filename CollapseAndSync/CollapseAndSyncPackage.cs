using System;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;

namespace cpmcgrath.CollapseAndSync
{
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "4.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidCollapseAndSyncPkgString)]
    public sealed class CollapseAndSyncPackage : Package
    {
        protected override void Initialize()
        {
            base.Initialize();

            var mcs = GetService(typeof(IMenuCommandService)) as IMenuCommandService;
            if (mcs == null)
                return;

            mcs.Register(GuidList.guidCollapseAndSyncCmdSet, PkgCmdIDList.cmdCollapseSync, CollapseAndSync);
        }

        void CollapseAndSync(object sender, EventArgs e)
        {
            var dte = (EnvDTE80.DTE2)GetService(typeof(SDTE));
            dte.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Activate();

            try
            {
                CollapseAll(dte);
                SyncWithActiveDocument(dte);
            }
            catch (COMException)
            {
            }
        }

        void CollapseAll(EnvDTE80.DTE2 dte)
        {
            int cmdidSolutionExplorerCollapseAll = 29;
            Guid guidCMDSETID_StandardCommandSet11 = new Guid("D63DB1F0-404E-4B21-9648-CA8D99245EC3");
            dte.Commands.Raise(guidCMDSETID_StandardCommandSet11.ToString("B"), cmdidSolutionExplorerCollapseAll, null, null);
        }

        void SyncWithActiveDocument(EnvDTE80.DTE2 dte)
        {
            dte.ExecuteCommand("SolutionExplorer.SyncWithActiveDocument");
        }
    }
}