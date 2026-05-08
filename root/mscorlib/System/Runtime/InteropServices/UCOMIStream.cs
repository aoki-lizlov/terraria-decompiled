using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200072F RID: 1839
	[Obsolete]
	[Guid("0000000c-0000-0000-c000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	public interface UCOMIStream
	{
		// Token: 0x06004220 RID: 16928
		void Read([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] [Out] byte[] pv, int cb, IntPtr pcbRead);

		// Token: 0x06004221 RID: 16929
		void Write([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] byte[] pv, int cb, IntPtr pcbWritten);

		// Token: 0x06004222 RID: 16930
		void Seek(long dlibMove, int dwOrigin, IntPtr plibNewPosition);

		// Token: 0x06004223 RID: 16931
		void SetSize(long libNewSize);

		// Token: 0x06004224 RID: 16932
		void CopyTo(UCOMIStream pstm, long cb, IntPtr pcbRead, IntPtr pcbWritten);

		// Token: 0x06004225 RID: 16933
		void Commit(int grfCommitFlags);

		// Token: 0x06004226 RID: 16934
		void Revert();

		// Token: 0x06004227 RID: 16935
		void LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06004228 RID: 16936
		void UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06004229 RID: 16937
		void Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x0600422A RID: 16938
		void Clone(out UCOMIStream ppstm);
	}
}
