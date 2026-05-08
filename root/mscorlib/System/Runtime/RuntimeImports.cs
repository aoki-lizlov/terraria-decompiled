using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x02000525 RID: 1317
	internal static class RuntimeImports
	{
		// Token: 0x06003555 RID: 13653 RVA: 0x000C1A44 File Offset: 0x000BFC44
		internal unsafe static void RhZeroMemory(ref byte b, ulong byteLength)
		{
			fixed (byte* ptr = &b)
			{
				RuntimeImports.ZeroMemory((void*)ptr, (uint)byteLength);
			}
		}

		// Token: 0x06003556 RID: 13654 RVA: 0x000C1A5F File Offset: 0x000BFC5F
		internal unsafe static void RhZeroMemory(IntPtr p, UIntPtr byteLength)
		{
			RuntimeImports.ZeroMemory((void*)p, (uint)byteLength);
		}

		// Token: 0x06003557 RID: 13655
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ZeroMemory(void* p, uint byteLength);

		// Token: 0x06003558 RID: 13656
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Memmove(byte* dest, byte* src, uint len);

		// Token: 0x06003559 RID: 13657
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Memmove_wbarrier(byte* dest, byte* src, uint len, IntPtr type_handle);
	}
}
