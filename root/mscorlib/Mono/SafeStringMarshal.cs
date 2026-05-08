using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x0200003F RID: 63
	internal struct SafeStringMarshal : IDisposable
	{
		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr StringToUtf8_icall(ref string str);

		// Token: 0x060000EE RID: 238 RVA: 0x0000459B File Offset: 0x0000279B
		public static IntPtr StringToUtf8(string str)
		{
			return SafeStringMarshal.StringToUtf8_icall(ref str);
		}

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void GFree(IntPtr ptr);

		// Token: 0x060000F0 RID: 240 RVA: 0x000045A4 File Offset: 0x000027A4
		public SafeStringMarshal(string str)
		{
			this.str = str;
			this.marshaled_string = IntPtr.Zero;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000045B8 File Offset: 0x000027B8
		public IntPtr Value
		{
			get
			{
				if (this.marshaled_string == IntPtr.Zero && this.str != null)
				{
					this.marshaled_string = SafeStringMarshal.StringToUtf8(this.str);
				}
				return this.marshaled_string;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000045EB File Offset: 0x000027EB
		public void Dispose()
		{
			if (this.marshaled_string != IntPtr.Zero)
			{
				SafeStringMarshal.GFree(this.marshaled_string);
				this.marshaled_string = IntPtr.Zero;
			}
		}

		// Token: 0x04000D0F RID: 3343
		private readonly string str;

		// Token: 0x04000D10 RID: 3344
		private IntPtr marshaled_string;
	}
}
