using System;
using System.Reflection.Emit;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000734 RID: 1844
	[ComVisible(true)]
	[CLSCompliant(false)]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("BEBB2505-8B54-3443-AEAD-142A16DD9CC7")]
	[TypeLibImportClass(typeof(AssemblyBuilder))]
	public interface _AssemblyBuilder
	{
		// Token: 0x06004267 RID: 16999
		void GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);

		// Token: 0x06004268 RID: 17000
		void GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);

		// Token: 0x06004269 RID: 17001
		void GetTypeInfoCount(out uint pcTInfo);

		// Token: 0x0600426A RID: 17002
		void Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
	}
}
