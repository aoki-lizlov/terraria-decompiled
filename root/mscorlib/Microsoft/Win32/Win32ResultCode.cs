using System;

namespace Microsoft.Win32
{
	// Token: 0x02000090 RID: 144
	internal class Win32ResultCode
	{
		// Token: 0x0600048F RID: 1167 RVA: 0x000025BE File Offset: 0x000007BE
		public Win32ResultCode()
		{
		}

		// Token: 0x04000E87 RID: 3719
		public const int Success = 0;

		// Token: 0x04000E88 RID: 3720
		public const int FileNotFound = 2;

		// Token: 0x04000E89 RID: 3721
		public const int AccessDenied = 5;

		// Token: 0x04000E8A RID: 3722
		public const int InvalidHandle = 6;

		// Token: 0x04000E8B RID: 3723
		public const int InvalidParameter = 87;

		// Token: 0x04000E8C RID: 3724
		public const int MoreData = 234;

		// Token: 0x04000E8D RID: 3725
		public const int NetworkPathNotFound = 53;

		// Token: 0x04000E8E RID: 3726
		public const int NoMoreEntries = 259;

		// Token: 0x04000E8F RID: 3727
		public const int MarkedForDeletion = 1018;

		// Token: 0x04000E90 RID: 3728
		public const int ChildMustBeVolatile = 1021;
	}
}
