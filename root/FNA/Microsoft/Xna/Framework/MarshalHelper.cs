using System;
using System.Runtime.InteropServices;

namespace Microsoft.Xna.Framework
{
	// Token: 0x02000038 RID: 56
	internal static class MarshalHelper
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x0001ACF7 File Offset: 0x00018EF7
		internal static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0001AD08 File Offset: 0x00018F08
		internal static string PtrToInternedStringAnsi(IntPtr ptr)
		{
			string text = Marshal.PtrToStringAnsi(ptr);
			if (text != null)
			{
				text = string.Intern(text);
			}
			return text;
		}
	}
}
