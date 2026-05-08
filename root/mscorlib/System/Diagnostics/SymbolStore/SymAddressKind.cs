using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000A37 RID: 2615
	[ComVisible(true)]
	[Serializable]
	public enum SymAddressKind
	{
		// Token: 0x040039EC RID: 14828
		ILOffset = 1,
		// Token: 0x040039ED RID: 14829
		NativeRVA,
		// Token: 0x040039EE RID: 14830
		NativeRegister,
		// Token: 0x040039EF RID: 14831
		NativeRegisterRelative,
		// Token: 0x040039F0 RID: 14832
		NativeOffset,
		// Token: 0x040039F1 RID: 14833
		NativeRegisterRegister,
		// Token: 0x040039F2 RID: 14834
		NativeRegisterStack,
		// Token: 0x040039F3 RID: 14835
		NativeStackRegister,
		// Token: 0x040039F4 RID: 14836
		BitField,
		// Token: 0x040039F5 RID: 14837
		NativeSectionOffset
	}
}
