using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.Design;

namespace cpmcgrath.CollapseAndSync
{
    public static class VisualStudioHelperMethods
    {
        public static void Register(this IMenuCommandService instance, Guid guid, uint cmd, EventHandler method)
        {
            var menuCommandID = new CommandID(guid, (int)cmd);
            instance.AddCommand(new MenuCommand(method, menuCommandID));
        }

        public static IEnumerable<int> UpTo(this int start, int end)
        {
            for (int i = start; i <= end; i++)
                yield return i;
        }

        public const string SolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";
    }
}
