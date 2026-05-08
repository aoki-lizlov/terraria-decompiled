using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000002 RID: 2
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000F")]
	[ComVisible(true)]
	public class ComHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool IsZipFile(string filename)
		{
			return ZipFile.IsZipFile(filename);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public bool IsZipFileWithExtract(string filename)
		{
			return ZipFile.IsZipFile(filename, true);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		public string GetZipLibraryVersion()
		{
			return ZipFile.LibraryVersion.ToString();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020ED File Offset: 0x000002ED
		public ComHelper()
		{
		}
	}
}
