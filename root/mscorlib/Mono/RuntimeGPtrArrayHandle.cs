using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x0200002E RID: 46
	internal struct RuntimeGPtrArrayHandle
	{
		// Token: 0x060000DB RID: 219 RVA: 0x000043B8 File Offset: 0x000025B8
		internal unsafe RuntimeGPtrArrayHandle(RuntimeStructs.GPtrArray* value)
		{
			this.value = value;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x000043C1 File Offset: 0x000025C1
		internal unsafe RuntimeGPtrArrayHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GPtrArray*)(void*)ptr;
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000DD RID: 221 RVA: 0x000043CF File Offset: 0x000025CF
		internal unsafe int Length
		{
			get
			{
				return this.value->len;
			}
		}

		// Token: 0x1700000F RID: 15
		internal IntPtr this[int i]
		{
			get
			{
				return this.Lookup(i);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x000043E5 File Offset: 0x000025E5
		internal unsafe IntPtr Lookup(int i)
		{
			if (i >= 0 && i < this.Length)
			{
				return this.value->data[i];
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GPtrArrayFree(RuntimeStructs.GPtrArray* value);

		// Token: 0x060000E1 RID: 225 RVA: 0x00004410 File Offset: 0x00002610
		internal static void DestroyAndFree(ref RuntimeGPtrArrayHandle h)
		{
			RuntimeGPtrArrayHandle.GPtrArrayFree(h.value);
			h.value = null;
		}

		// Token: 0x04000CE3 RID: 3299
		private unsafe RuntimeStructs.GPtrArray* value;
	}
}
