using System;
using System.Diagnostics;
using System.Globalization;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell.Interop;
using System.Threading.Tasks;
using System.Threading;

namespace cpmcgrath.CollapseAndSync
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [InstalledProductRegistration("#110", "#112", "6.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidCollapseAndSyncPkgString)]
    public sealed class CollapseAndSyncPackage : AsyncPackage
    {
        protected async override Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await base.InitializeAsync(cancellationToken, progress);

            var mcs = await GetServiceAsync(typeof(IMenuCommandService)) as IMenuCommandService;
            mcs?.Register(GuidList.guidCollapseAndSyncCmdSet, PkgCmdIDList.cmdCollapseSync, CollapseAndSync);
        }

        void CollapseAndSync(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var dte = (EnvDTE80.DTE2)GetService(typeof(SDTE));
            dte?.Windows.Item(EnvDTE.Constants.vsWindowKindSolutionExplorer).Activate();

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
            ThreadHelper.ThrowIfNotOnUIThread();
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