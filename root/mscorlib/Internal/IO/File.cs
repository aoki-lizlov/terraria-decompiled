using System;

namespace Internal.IO
{
	// Token: 0x02000B66 RID: 2918
	internal static class File
	{
		// Token: 0x06006AD3 RID: 27347 RVA: 0x0016F158 File Offset: 0x0016D358
		internal static bool InternalExists(string fullPath)
		{
			Interop.Sys.FileStatus fileStatus;
			return (Interop.Sys.Stat(fullPath, out fileStatus) >= 0 || Interop.Sys.LStat(fullPath, out fileStatus) >= 0) && (fileStatus.Mode & 61440) != 16384;
		}
	}
}
