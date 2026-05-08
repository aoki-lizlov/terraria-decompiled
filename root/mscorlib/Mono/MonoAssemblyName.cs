using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Mono
{
	// Token: 0x02000035 RID: 53
	internal struct MonoAssemblyName
	{
		// Token: 0x04000CF0 RID: 3312
		private const int MONO_PUBLIC_KEY_TOKEN_LENGTH = 17;

		// Token: 0x04000CF1 RID: 3313
		internal IntPtr name;

		// Token: 0x04000CF2 RID: 3314
		internal IntPtr culture;

		// Token: 0x04000CF3 RID: 3315
		internal IntPtr hash_value;

		// Token: 0x04000CF4 RID: 3316
		internal IntPtr public_key;

		// Token: 0x04000CF5 RID: 3317
		[FixedBuffer(typeof(byte), 17)]
		internal MonoAssemblyName.<public_key_token>e__FixedBuffer public_key_token;

		// Token: 0x04000CF6 RID: 3318
		internal uint hash_alg;

		// Token: 0x04000CF7 RID: 3319
		internal uint hash_len;

		// Token: 0x04000CF8 RID: 3320
		internal uint flags;

		// Token: 0x04000CF9 RID: 3321
		internal ushort major;

		// Token: 0x04000CFA RID: 3322
		internal ushort minor;

		// Token: 0x04000CFB RID: 3323
		internal ushort build;

		// Token: 0x04000CFC RID: 3324
		internal ushort revision;

		// Token: 0x04000CFD RID: 3325
		internal ushort arch;

		// Token: 0x02000036 RID: 54
		[CompilerGenerated]
		[UnsafeValueType]
		[StructLayout(LayoutKind.Sequential, Size = 17)]
		public struct <public_key_token>e__FixedBuffer
		{
			// Token: 0x04000CFE RID: 3326
			public byte FixedElementField;
		}
	}
}
