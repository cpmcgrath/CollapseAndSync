// Guids.cs
// MUST match guids.h
using System;

namespace cpmcgrath.CollapseAndSync
{
    static class GuidList
    {
        public const string guidCollapseAndSyncPkgString = "8a270170-a54e-4815-86b6-9c0a93963640";
        public const string guidCollapseAndSyncCmdSetString = "b042a405-501f-49bf-9732-124b654eaa8e";

        public static readonly Guid guidCollapseAndSyncCmdSet = new Guid(guidCollapseAndSyncCmdSetString);
    };
}