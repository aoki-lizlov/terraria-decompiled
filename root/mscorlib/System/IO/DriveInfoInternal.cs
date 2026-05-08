using System;

namespace System.IO
{
	// Token: 0x0200098E RID: 2446
	internal static class DriveInfoInternal
	{
		// Token: 0x06005944 RID: 22852 RVA: 0x0005DDD7 File Offset: 0x0005BFD7
		internal static string[] GetLogicalDrives()
		{
			return Environment.GetLogicalDrivesInternal();
		}
	}
}
