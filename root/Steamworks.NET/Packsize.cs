using System;
using System.Runtime.InteropServices;

namespace Steamworks
{
	// Token: 0x0200018E RID: 398
	public static class Packsize
	{
		// Token: 0x0600090C RID: 2316 RVA: 0x0000D944 File Offset: 0x0000BB44
		public static bool Test()
		{
			int num = Marshal.SizeOf(typeof(Packsize.ValvePackingSentinel_t));
			int num2 = Marshal.SizeOf(typeof(RemoteStorageEnumerateUserSubscribedFilesResult_t));
			return num == 24 && num2 == 612;
		}

		// Token: 0x04000A70 RID: 2672
		public const int value = 4;

		// Token: 0x020001EE RID: 494
		[StructLayout(0, Pack = 4)]
		private struct ValvePackingSentinel_t
		{
			// Token: 0x04000B55 RID: 2901
			private uint m_u32;

			// Token: 0x04000B56 RID: 2902
			private ulong m_u64;

			// Token: 0x04000B57 RID: 2903
			private ushort m_u16;

			// Token: 0x04000B58 RID: 2904
			private double m_d;
		}
	}
}
